using Application.Common.Transactions;

namespace Application.Operacional.Financeiro.Commands;

public sealed record RegistrarPagamentoGatewayCommand(
    Guid CobrancaId,
    string GatewayPagamentoId
) : ITransactionalCommand
{
    public TransactionContext TransactionContext =>
        TransactionContext.Operacional;
}