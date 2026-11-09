using Application.Common.Transactions;

namespace Application.Operacional.Financeiro.Commands;

public sealed record RegistrarCobrancaPixCommand(
    Guid ContaReceberId,
    string ChaveIdempotencia
) : ITransactionalCommand
{
    public TransactionContext TransactionContext =>
        TransactionContext.Operacional;
}