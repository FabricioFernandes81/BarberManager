using Application.Common.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Vendas.Commands
{
    public sealed record FinalizarVendaCommand(
        Guid VendaId,
        IReadOnlyCollection<PagamentoVendaCommand> Pagamentos,
        string? Observacao
    ) : ITransactionalCommand
    {
        [NotMapped]
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }

    public sealed record PagamentoVendaCommand(
        Guid FormaPagamentoId,
        decimal Valor
    );
}
