using Application.Common.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Produtos.Commands
{
    public sealed record AtualizarProdutoCommand(
        Guid ProdutoId,
        string Nome,
        string? Descricao,
        string? CodigoInterno,
        string? CodigoBarras,
        Guid? CategoriaProdutoId,
        Guid? MarcaId,
        decimal ValorVenda,
        decimal Custo
    ) : ITransactionalCommand
    {
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
