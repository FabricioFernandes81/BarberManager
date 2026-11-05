using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Financeiro;

namespace Application.Operacional.Financeiro.Commands.Handlers;

public sealed class RegistrarPagamentoHandler
    : ICommandHandler<RegistrarPagamentoCommand, RegistrarPagamentoDto>
{
    private readonly IRepositorioContaPagar _repositorioContaPagar;
    private readonly IRepositorioCaixa _repositorioCaixa;
    private readonly ITenantContext _tenantContext;
    private readonly IUsuarioLogado _usuarioLogado;

    public RegistrarPagamentoHandler(
        IRepositorioContaPagar repositorioContaPagar,
        IRepositorioCaixa repositorioCaixa,
        ITenantContext tenantContext,
        IUsuarioLogado usuarioLogado)
    {
        _repositorioContaPagar = repositorioContaPagar;
        _repositorioCaixa = repositorioCaixa;
        _tenantContext = tenantContext;
        _usuarioLogado = usuarioLogado;
    }

    public async Task<RegistrarPagamentoDto> Handle(
    RegistrarPagamentoCommand command,
    CancellationToken cancellationToken)
    {
        // 1. Valida o contexto da empresa.
        if (!_tenantContext.IsAvailable ||
            _tenantContext.EmpresaId == Guid.Empty)
        {
            throw Erro(
                "Não foi possível identificar a empresa do usuário.");
        }

        var empresaId = _tenantContext.EmpresaId;

        // 2. Valida a autenticação.
        if (!_usuarioLogado.IsAuthenticated ||
            _usuarioLogado.UsuarioId == Guid.Empty)
        {
            throw Erro(
                "É necessário estar autenticado para registrar um pagamento.");
        }

        // 3. Valida os identificadores.
        if (command.ContaPagarId == Guid.Empty)
            throw Erro("O identificador da conta a pagar é obrigatório.");

        if (command.FormaPagamentoId == Guid.Empty)
            throw Erro("A forma de pagamento deve ser informada.");

        // 4. Valida a chave idempotente.
        var chaveIdempotencia = command.ChaveIdempotencia?.Trim();

        if (string.IsNullOrWhiteSpace(chaveIdempotencia))
            throw Erro("A chave de idempotência é obrigatória.");

        if (chaveIdempotencia.Length > 100)
            throw Erro(
                "A chave de idempotência não pode ultrapassar 100 caracteres.");

        // 5. Valida o valor.
        if (command.Valor <= 0)
            throw Erro("O valor do pagamento deve ser maior que zero.");

        if (command.Valor != decimal.Round(command.Valor, 2))
        {
            throw Erro(
                "O valor do pagamento deve possuir no máximo duas casas decimais.");
        }

        // 6. Normaliza a observação.
        var observacao = command.Observacao?.Trim();

        if (observacao is { Length: > 500 })
            throw Erro("A observação não pode ultrapassar 500 caracteres.");

        // 7. Bloqueia a chave e a conta na transação atual.
        await _repositorioContaPagar.BloquearContaParaPagamentoAsync(
            empresaId,
            command.ContaPagarId,
            chaveIdempotencia,
            cancellationToken);

        // 8. Verifica se a chave já foi utilizada.
        var pagamentoExistente =
            await _repositorioContaPagar.ObterPagamentoPorChaveAsync(
                empresaId,
                chaveIdempotencia,
                cancellationToken);

        if (pagamentoExistente is not null)
        {
            // A mesma chave não pode representar outra operação.
            var mesmaOperacao =
                pagamentoExistente.ContaPagarId == command.ContaPagarId &&
                pagamentoExistente.FormaPagamentoId == command.FormaPagamentoId &&
                pagamentoExistente.Valor == command.Valor &&
                pagamentoExistente.Observacao == observacao;

            if (!mesmaOperacao)
            {
                throw Erro(
                    "A chave de idempotência já foi utilizada em outra operação " +
                    "ou com dados diferentes.");
            }

            // Retorna o resultado original sem movimentar o caixa novamente.
            return new RegistrarPagamentoDto(
                pagamentoExistente.Id,
                pagamentoExistente.ContaPagarId,
                pagamentoExistente.Valor,
                pagamentoExistente.ValorPagoAposPagamento,
                pagamentoExistente.SaldoRestanteAposPagamento,
                pagamentoExistente.StatusContaAposPagamento,
                pagamentoExistente.DataPagamento);
        }

        // 9. Bloqueia operações simultâneas no caixa do usuário.
        await _repositorioCaixa.BloquearOperacaoCaixaAsync(
            empresaId,
            _usuarioLogado.UsuarioId,
            cancellationToken);

        // 10. Obtém o caixa aberto.
        var caixa =
            await _repositorioCaixa.ObterCaixaAbertoParaAtualizacaoAsync(
                empresaId,
                _usuarioLogado.UsuarioId,
                cancellationToken);

        if (caixa is null)
        {
            throw Erro(
                "Não existe um caixa aberto para este usuário nesta empresa. " +
                "Abra o caixa antes de registrar o pagamento.");
        }

        // 11. Obtém a conta a pagar dentro do tenant.
        var conta =
            await _repositorioContaPagar.ObterParaPagamentoAsync(
                empresaId,
                command.ContaPagarId,
                cancellationToken);

        if (conta is null)
            throw Erro("Conta a pagar não encontrada para a empresa.");

        // 12. Valida a forma de pagamento.
        var formaPagamento =
            await _repositorioContaPagar.ObterFormaPagamentoAtivaAsync(
                empresaId,
                command.FormaPagamentoId,
                cancellationToken);

        if (formaPagamento is null)
        {
            throw Erro(
                "A forma de pagamento não existe, está inativa " +
                "ou não pertence à empresa.");
        }

        // 13. Define a data pelo servidor.
        var dataPagamento = DateTime.UtcNow;

        // 14. Executa a regra de domínio.
        try
        {
            conta.RegistrarPagamento(
                command.Valor,
                dataPagamento);
        }
        catch (InvalidOperationException ex)
        {
            throw Erro(ex.Message);
        }

        // 15. Cria o registro do pagamento.
        var pagamento = new Pagamento
        {
            EmpresaId = empresaId,
            ChaveIdempotencia = chaveIdempotencia,

            ContaPagarId = conta.Id,
            FormaPagamentoId = formaPagamento.Id,

            DataPagamento = dataPagamento,
            Valor = command.Valor,
            Observacao = observacao,

            // Snapshot financeiro após o pagamento.
            ValorPagoAposPagamento = conta.ValorPago,
            SaldoRestanteAposPagamento = conta.ObterSaldoRestante(),
            StatusContaAposPagamento = conta.Status
        };

        await _repositorioContaPagar.AdicionarPagamentoAsync(
            pagamento,
            cancellationToken);

        // 16. Registra a saída no caixa.
        var movimento = new MovimentoCaixa
        {
            CaixaId = caixa.Id,
            TipoMovimento = TipoMovimentoCaixa.Saida,
            FormaPagamentoId = formaPagamento.Id,
            DataMovimento = dataPagamento,
            Valor = command.Valor,
            Historico = $"Pagamento da conta a pagar {conta.Id}",
            Documento = pagamento.Id.ToString()
        };

        await _repositorioCaixa.AdicionarMovimentoAsync(
            movimento,
            cancellationToken);

        // 17. Retorna o resultado.
        // O TransactionMiddleware realiza a persistência e o commit.
        return new RegistrarPagamentoDto(
            pagamento.Id,
            conta.Id,
            pagamento.Valor,
            pagamento.ValorPagoAposPagamento,
            pagamento.SaldoRestanteAposPagamento,
            pagamento.StatusContaAposPagamento,
            pagamento.DataPagamento);
    }

    private static CustomValidationException Erro(string mensagem)
    {
        return new CustomValidationException(new[] { mensagem });
    }
}