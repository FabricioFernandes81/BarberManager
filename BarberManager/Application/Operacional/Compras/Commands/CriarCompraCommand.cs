using Application.Common.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.Commands
{
    public class CriarCompraCommand : ITransactionalCommand
    {
        public Guid FornecedorId { get; set; }

        public string? NumeroDocumento { get; set; }

        public string? SerieDocumento { get; set; }

        public DateTime DataEmissao { get; set; }

        public DateTime DataCompra { get; set; }

        public decimal Desconto { get; set; }

        public decimal Frete { get; set; }

        public decimal OutrasDespesas { get; set; }

        public string? Observacao { get; set; }

        public List<CriarCompraItemCommand> Itens { get; set; }
            = new();

        public List<CriarCompraParcelaCommand> Parcelas { get; set; }
            = new();

        [NotMapped]
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }

    public class CriarCompraItemCommand
    {
        public Guid ProdutoId { get; set; }

        public decimal Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }

        public decimal Desconto { get; set; }
    }

    public class CriarCompraParcelaCommand
    {
        public DateTime DataVencimento { get; set; }

        public decimal Valor { get; set; }
    }
}
