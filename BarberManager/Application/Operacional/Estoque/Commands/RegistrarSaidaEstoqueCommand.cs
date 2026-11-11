using Application.Common.Transactions;

public sealed record RegistrarSaidaEstoqueCommand(
    Guid ProdutoId,
    decimal Quantidade,
    string? Observacao
) : ITransactionalCommand
{
    public TransactionContext TransactionContext =>
        TransactionContext.Operacional;
}