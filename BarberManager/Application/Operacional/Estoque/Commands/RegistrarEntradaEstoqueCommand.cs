using Application.Common.Transactions;

namespace Application.Operacional.Estoque.Commands
{
    public sealed record RegistrarEntradaEstoqueCommand(
        Guid ProdutoId,
        decimal Quantidade,
        string? Observacao
    ) : ITransactionalCommand
    {
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}