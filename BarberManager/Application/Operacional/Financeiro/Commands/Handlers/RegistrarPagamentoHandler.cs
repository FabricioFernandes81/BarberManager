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
        // 1. Contexto da empresa.
        if (!_tenantContext.IsAvailable ||
            _tenantContext.EmpresaId == Guid.Empty)
        {
            throw Erro(
                "Não foi possível identificar a empresa do usuário.");
        }

        var empresaId = _tenantContext.EmpresaId;

        // 2. Autenticação.
        if (!_usuarioLogado.IsAuthenticated ||
            _usuarioLogado.UsuarioId == Guid.Empty)
        {
            throw Erro(
                "É necessário estar autenticado para registrar um pagamento.");
        }

        // 3. Identificadores.
        if (command.ContaPagarId == Guid.Empty)
            throw Erro("O identificador da conta a pagar é obrigatório.");

        if (command.FormaPagamentoId == Guid.Empty)
            throw Erro("A forma de pagamento deve ser informada.");

        // 4. Valor.
        if (command.Valor <= 0)
            throw Erro("O valor do pagamento deve ser maior que zero.");

        if (command.Valor != decimal.Round(command.Valor, 2))
            throw Erro(
                "O valor do pagamento deve possuir no máximo duas casas decimais.");

        // 5. Observação.
        var observacao = command.Observacao?.Trim();

        if (observacao is { Length: > 500 })
            throw Erro("A observação não pode ultrapassar 500 caracteres.");

        // 6. Bloqueia a conta para evitar pagamentos concorrentes.
        await _repositorioContaPagar.BloquearContaParaPagamentoAsync(
            empresaId,
            command.ContaPagarId,
            cancellationToken);

        // 7. Bloqueia operações simultâneas no caixa do usuário.
        await _repositorioCaixa.BloquearOperacaoCaixaAsync(
            empresaId,
            _usuarioLogado.UsuarioId,
            cancellationToken);

        // 8. Obtém o caixa aberto rastreado.
        var caixa = await _repositorioCaixa.ObterCaixaAbertoParaAtualizacaoAsync(
            empresaId,
            _usuarioLogado.UsuarioId,
            cancellationToken);

        if (caixa is null)
        {
            throw Erro(
                "Não existe um caixa aberto para este usuário nesta empresa. " +
                "Abra o caixa antes de registrar o pagamento.");
        }

        // 9. Obtém a conta a pagar dentro do tenant.
        var conta = await _repositorioContaPagar.ObterParaPagamentoAsync(
            empresaId,
            command.ContaPagarId,
            cancellationToken);

        if (conta is null)
            throw Erro("Conta a pagar não encontrada para a empresa.");

        // 10. Valida a forma de pagamento.
        var formaPagamento =
            await _repositorioContaPagar.ObterFormaPagamentoAtivaAsync(
                empresaId,
                command.FormaPagamentoId,
                cancellationToken);

        if (formaPagamento is null)
        {
            throw Erro(
                "A forma de pagamento não existe, está inativa ou não pertence à empresa.");
        }

        // 11. Data definida pelo servidor.
        var dataPagamento = DateTime.UtcNow;

        // 12. Aplica as regras do domínio.
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

        // 13. Registra o pagamento individual.
        var pagamento = new Pagamento
        {
            ContaPagarId = conta.Id,
            FormaPagamentoId = formaPagamento.Id,
            DataPagamento = dataPagamento,
            Valor = command.Valor,
            Observacao = observacao
        };

        await _repositorioContaPagar.AdicionarPagamentoAsync(
            pagamento,
            cancellationToken);

        // 14. Registra a saída no caixa.
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

        // 15. O TransactionMiddleware persiste tudo atomicamente.
        return new RegistrarPagamentoDto(
            pagamento.Id,
            conta.Id,
            pagamento.Valor,
            conta.ValorPago,
            conta.ObterSaldoRestante(),
            conta.Status,
            pagamento.DataPagamento);
    }

    private static CustomValidationException Erro(string mensagem)
    {
        return new CustomValidationException(new[] { mensagem });
    }
}