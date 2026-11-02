using Application.Common.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.Commands
{
    public sealed record AlterarStatusFornecedorCommand(
        Guid FornecedorId,
        bool Ativo
    ) : ITransactionalCommand
    {
        [NotMapped]
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
