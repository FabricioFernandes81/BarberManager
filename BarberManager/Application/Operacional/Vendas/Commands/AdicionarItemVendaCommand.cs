using Application.Common.Transactions;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Vendas.Commands
{
    public sealed record AdicionarItemVendaCommand(
        Guid VendaId,
        TipoItemVenda TipoItem,
        Guid ItemId,
        decimal Quantidade,
        decimal Desconto
    ) : ITransactionalCommand
    {
        
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
