using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Financeiro;
using Application.Interfaces.Vendas;
using Application.Operacional.Vendas.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Vendas.Commands.Handlers
{
    public sealed class FinalizarVendaHandler
         : ICommandHandler<FinalizarVendaCommand, FinalizarVendaDto>
    {
        private readonly IRepositorioVenda _repositorioVenda;
        private readonly IRepositorioRecebimento _repositorioRecebimento;
        private readonly IRepositorioCaixa _repositorioCaixa;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public FinalizarVendaHandler(
            IRepositorioVenda repositorioVenda,
            IRepositorioRecebimento repositorioRecebimento,
            IRepositorioCaixa repositorioCaixa,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorioVenda = repositorioVenda;
            _repositorioRecebimento = repositorioRecebimento;
            _repositorioCaixa = repositorioCaixa;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<FinalizarVendaDto> Handle(
            FinalizarVendaCommand command,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // 1. Validar o contexto da empresa.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw Erro(
                    "Não foi possível identificar a empresa do usuário.");
            }

            var empresaId = _tenantContext.EmpresaId;

            // 2. Validar o usuário autenticado.
            if (!_usuarioLogado.IsAuthenticated)
            {
                throw Erro(
                    "É necessário estar autenticado para finalizar a venda.");
            }

            var usuarioId = _usuarioLogado.UsuarioId;

            if (usuarioId == Guid.Empty)
            {
                throw Erro(
                    "O identificador do usuário autenticado é inválido.");
            }

            // 3. Validar os identificadores recebidos.
            if (command.VendaId == Guid.Empty)
            {
                throw Erro(
                    "O identificador da venda é obrigatório.");
            }

            if (command.FormaPagamentoId == Guid.Empty)
            {
                throw Erro(
                    "A forma de pagamento deve ser informada.");
            }

            // 4. Carregar a venda rastreada pelo EF Core.
            var venda = await _repositorioVenda.ObterParaAtualizacaoAsync(
                empresaId,
                command.VendaId,
                cancellationToken);

            if (venda is null)
            {
                throw Erro(
                    "Venda não encontrada para a empresa informada.");
            }

            // 5. Impedir a finalização repetida.
            if (venda.Status != StatusVenda.Aberta)
            {
                throw Erro(
                    "Só é possível finalizar uma venda que esteja aberta.");
            }

            if (venda.Itens.Count == 0)
            {
                throw Erro(
                    "Não é possível finalizar uma venda sem itens.");
            }

            if (venda.ValorTotal <= 0)
            {
                throw Erro(
                    "Não é possível finalizar uma venda com valor total zero ou negativo.");
            }

            // 6. Validar a observação.
            var observacao = command.Observacao?.Trim();

            if (observacao is { Length: > 500 })
            {
                throw Erro(
                    "A observação não pode ultrapassar 500 caracteres.");
            }

            // 7. Bloquear operações concorrentes no caixa.
            // O TransactionMiddleware deve ter iniciado a transação
            // antes de executar este handler.
            await _repositorioCaixa.BloquearOperacaoCaixaAsync(
                empresaId,
                usuarioId,
                cancellationToken);

            // 8. Localizar o caixa aberto do usuário.
            var caixa = await _repositorioRecebimento.ObterCaixaAbertoAsync(
                empresaId,
                usuarioId,
                cancellationToken);

            if (caixa is null)
            {
                throw Erro(
                    "Não existe um caixa aberto para este usuário nesta empresa. " +
                    "Abra o caixa antes de finalizar a venda.");
            }

            // 9. Validar a forma de pagamento.
            var formaPagamento =
                await _repositorioRecebimento.ObterFormaPagamentoAtivaAsync(
                    empresaId,
                    command.FormaPagamentoId,
                    cancellationToken);

            if (formaPagamento is null)
            {
                throw Erro(
                    "A forma de pagamento não existe, está inativa " +
                    "ou não pertence à empresa.");
            }

            // 10. Impedir duplicidade de conta financeira.
            var contaExistente =
                await _repositorioRecebimento.ObterPorVendaAsync(
                    empresaId,
                    venda.Id,
                    cancellationToken);

            if (contaExistente is not null)
            {
                throw Erro(
                    "Já existe uma conta a receber vinculada a esta venda. " +
                    "Verifique o financeiro antes de tentar finalizá-la novamente.");
            }

            // 11. Definir os valores exclusivamente no servidor.
            var dataFinalizacao = DateTimeOffset.UtcNow;
            var dataOperacao = dataFinalizacao.UtcDateTime;

            var valorVenda = decimal.Round(
                venda.ValorTotal,
                2,
                MidpointRounding.AwayFromZero);

            // 12. Criar a conta a receber para a venda.
            var contaReceber = new ContaReceber(
                empresaId: empresaId,
                clienteId: venda.ClienteId,
                dataEmissao: dataOperacao,
                dataVencimento: dataOperacao,
                valor: valorVenda,
                descricao: $"Venda {venda.Id}");

            contaReceber.VincularVenda(venda.Id);

            // Registrar o recebimento integral da venda.
            try
            {
                contaReceber.RegistrarRecebimento(
                    valorVenda,
                    dataOperacao);
            }
            catch (InvalidOperationException ex)
            {
                throw Erro(ex.Message);
            }

            await _repositorioRecebimento.AdicionarContaAsync(
                contaReceber,
                cancellationToken);

            // 13. Criar o registro individual do recebimento.
            var recebimento = new Recebimento
            {
                ContaReceberId = contaReceber.Id,
               // ContaReceber = contaReceber,
                FormaPagamentoId = formaPagamento.Id,
                FormaPagamento = formaPagamento,
                DataRecebimento = dataOperacao,
                Valor = valorVenda,
                Observacao = observacao
            };

            await _repositorioRecebimento.AdicionarAsync(
                recebimento,
                cancellationToken);

            // 14. Registrar a entrada no caixa.
            var movimento = new MovimentoCaixa
            {
                CaixaId = caixa.Id,
                Caixa = caixa,
                TipoMovimento = TipoMovimentoCaixa.Entrada,
                FormaPagamentoId = formaPagamento.Id,
                FormaPagamento = formaPagamento,
                DataMovimento = dataOperacao,
                Valor = valorVenda,
                Historico = $"Recebimento da venda {venda.Id}",
                Documento = recebimento.Id.ToString()
            };

            await _repositorioRecebimento.AdicionarMovimentoCaixaAsync(
                movimento,
                cancellationToken);

            // 15. Finalizar a venda.
            venda.Finalizar(dataFinalizacao);

            // Não chamar SaveChangesAsync.
            // O TransactionMiddleware confirma ou reverte
            // todas as alterações da operação.
            return new FinalizarVendaDto(
                VendaId: venda.Id,
                Status: venda.Status,
                DataAbertura: venda.DataAbertura,
                DataFinalizacao: venda.DataFinalizacao!.Value,
                Subtotal: venda.Subtotal,
                Desconto: venda.Desconto,
                ValorTotal: venda.ValorTotal);
        }

        private static CustomValidationException Erro(
            string mensagem)
        {
            return new CustomValidationException(
                new[] { mensagem });
        }
    }
}
