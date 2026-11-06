using Application.Common.Transactions;

namespace Application.Operacional.Financeiro.Webhooks.Commands
{
    public sealed record RegistrarEventoWebhookAsaasCommand(
        Guid EmpresaId,
        string EventoId,
        string PagamentoGatewayId,
        string TipoEvento,
        string Payload
    ) : ITransactionalCommand
    {
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}