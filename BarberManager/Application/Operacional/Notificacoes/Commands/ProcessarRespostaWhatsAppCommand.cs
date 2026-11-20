
using Application.Common.Transactions;

namespace Application.Operacional.Notificacoes.Commands;

public sealed record ProcessarRespostaWhatsAppCommand(
    string Instance,
    string Telefone,
    string Mensagem,
    string MessageId
) : ITransactionalCommand
{
    public TransactionContext TransactionContext =>
        TransactionContext.Operacional;
}
