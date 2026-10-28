using Application.Common.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Vendas.Commands
{
    public sealed class RemoverItemVendaCommand : ITransactionalCommand
    {
        public Guid VendaId { get; init; }

        public Guid ItemVendaId { get; init; }

        [NotMapped]
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
