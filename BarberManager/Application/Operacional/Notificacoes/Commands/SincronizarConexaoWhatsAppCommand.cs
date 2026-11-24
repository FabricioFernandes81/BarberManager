using Application.Common.Transactions;

namespace Application.Operacional.Notificacoes.Commands;

public sealed record SincronizarConexaoWhatsAppCommand(
    string Instance,
    string State,
    string? StatusReason,
    string? Wuid
) : ITransactionalCommand
{
    public TransactionContext TransactionContext =>
        TransactionContext.Operacional;
}