using Application.Common.Transactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operacional.Compras.Commands
{
    public sealed record CancelarCompraCommand(
        Guid CompraId
    ) : ITransactionalCommand
    {
        [NotMapped]
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
