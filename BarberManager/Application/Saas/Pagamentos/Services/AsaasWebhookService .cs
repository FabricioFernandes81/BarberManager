using Application.Interfaces.Empresas;
using Application.Interfaces.Empresas.Pagamentos;
using Application.Saas.Pagamentos.DTOs;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.Services
{
    public sealed class AsaasWebhookService : IAsaasWebhookService
    {
        private const string ProvedorAsaas = "Asaas";

        private readonly IRepositorioPagamento _repositorio;


        public AsaasWebhookService(
          IRepositorioPagamento repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task ProcessarAsync(
            AsaasWebhookDto evento,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(evento);

            if (evento.Pagamento is null ||
                string.IsNullOrWhiteSpace(evento.Pagamento.Id))
            {
                throw new ArgumentException(
                    "O evento não contém uma cobrança válida.");
            }

            // Por enquanto, processamos somente eventos de confirmação.
            if (evento.Evento is not (
                "PAYMENT_CONFIRMED" or "PAYMENT_RECEIVED"))
            {
                return;
            }

            var transacao =
                await _repositorio.ObterPorReferenciaGatewayAsync(
                    ProvedorAsaas,
                    evento.Pagamento.Id,
                    cancellationToken);

            if (transacao is null)
            {
                throw new InvalidOperationException(
                    $"Não foi encontrada uma transação para a cobrança " +
                    $"Asaas '{evento.Pagamento.Id}'.");
            }

            // Impede que uma notificação repetida reaplique a confirmação.
            if (transacao.Status == StatusTransacaoPagamento.Aprovada &&
                transacao.Fatura.Status == StatusFatura.Paga)
            {
                return;
            }

            // Não aceitar confirmação para transações já encerradas
            // em estados incompatíveis com a aprovação.
            if (transacao.Status is
                StatusTransacaoPagamento.Recusada or
                StatusTransacaoPagamento.Cancelada or
                StatusTransacaoPagamento.Estornada or
                StatusTransacaoPagamento.Expirada)
            {
                throw new InvalidOperationException(
                    "A transação está encerrada e não pode ser aprovada " +
                    "automaticamente pelo webhook.");
            }

            if (transacao.Valor != evento.Pagamento.Valor ||
                transacao.Fatura.Valor != evento.Pagamento.Valor)
            {
                throw new InvalidOperationException(
                    "O valor informado pelo Asaas não corresponde " +
                    "ao valor da transação e da fatura.");
            }

            if (transacao.Fatura.Status == StatusFatura.Cancelada)
            {
                throw new InvalidOperationException(
                    "Não é possível confirmar automaticamente uma fatura cancelada.");
            }

            transacao.Status = StatusTransacaoPagamento.Aprovada;
            transacao.DataProcessamento = DateTime.UtcNow;

            transacao.CodigoErro = null;
            transacao.MensagemErro = null;

            transacao.Fatura.Status = StatusFatura.Paga;
            transacao.Fatura.DataPagamento = DateTime.UtcNow;

            await _repositorio.SalvarAlteracoesAsync(
                cancellationToken);
        }
    }
}
