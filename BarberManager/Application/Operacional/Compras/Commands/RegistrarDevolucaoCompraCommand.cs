
using Application.Common.Transactions;
using Application.Operacional.Compras.DTOs;
using Domain.Entities.Common.Enuns;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operacional.Compras.Commands
{
    public sealed class RegistrarDevolucaoCompraCommand
        : ITransactionalCommand
    {
        public Guid CompraId { get; set; }

        public string ChaveIdempotencia { get; set; } = string.Empty;

        public string Motivo { get; set; } = string.Empty;

        public string? Observacao { get; set; }

        public PoliticaFinanceiraDevolucao? PoliticaFinanceiraDevolucao
        {
            get;
            set;
        }

        public List<RegistrarDevolucaoCompraItemDto> Itens { get; set; }
            = new();

        [NotMapped]
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
