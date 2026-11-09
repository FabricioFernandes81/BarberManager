using Application.Common.Transactions;

namespace Application.Operacional.Financeiro.Commands;

public sealed record IniciarProcessamentoCobrancaPixCommand(
    Guid CobrancaId
) : ITransactionalCommand
{
    public TransactionContext TransactionContext =>
        TransactionContext.Operacional;
}