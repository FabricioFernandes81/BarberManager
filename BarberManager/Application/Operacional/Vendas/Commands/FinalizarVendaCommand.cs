using Application.Common.Transactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operacional.Vendas.Commands
{
    public sealed record FinalizarVendaCommand(
        Guid VendaId,
        IReadOnlyCollection<PagamentoVendaCommand>? Pagamentos,
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