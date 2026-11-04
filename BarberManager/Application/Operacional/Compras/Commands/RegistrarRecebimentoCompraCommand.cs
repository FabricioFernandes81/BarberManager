using Application.Common.Transactions;
using Application.Operacional.Compras.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operacional.Compras.Commands
{
    public class RegistrarRecebimentoCompraCommand : ITransactionalCommand
    {
        public Guid CompraId { get; set; }

        public string ChaveIdempotencia { get; set; } = string.Empty;

        public string? Observacao { get; set; }

        public List<RegistrarRecebimentoCompraItemDto> Itens { get; set; }
            = new();

        [NotMapped]
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}