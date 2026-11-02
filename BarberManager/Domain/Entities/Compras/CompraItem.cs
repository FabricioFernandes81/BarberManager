using Domain.Entities.Common;
using Domain.Entities.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Compras
{
    public class CompraItem : EntityBase
    {
        public Guid CompraId { get; private set; }

        public Compra Compra { get; private set; } = null!;

        public Guid ProdutoId { get; private set; }

        public Produto Produto { get; private set; } = null!;

        public string DescricaoProduto { get; private set; }
            = string.Empty;

        public decimal Quantidade { get; private set; }

        public decimal QuantidadeRecebida { get; private set; }

        public decimal ValorUnitario { get; private set; }

        public decimal Desconto { get; private set; }

        public decimal ValorTotal { get; private set; }

        public decimal QuantidadePendente =>
            Quantidade - QuantidadeRecebida;

        protected CompraItem()
        {
        }

        public CompraItem(
            Guid compraId,
            Guid produtoId,
            string descricaoProduto,
            decimal quantidade,
            decimal valorUnitario,
            decimal desconto = 0)
        {
            if (compraId == Guid.Empty)
                throw new ArgumentException(
                    "A compra é obrigatória.",
                    nameof(compraId));

            if (produtoId == Guid.Empty)
                throw new ArgumentException(
                    "O produto é obrigatório.",
                    nameof(produtoId));

            if (string.IsNullOrWhiteSpace(descricaoProduto))
                throw new ArgumentException(
                    "A descrição do produto é obrigatória.",
                    nameof(descricaoProduto));

            if (quantidade <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(quantidade),
                    "A quantidade deve ser maior que zero.");

            if (valorUnitario <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(valorUnitario),
                    "O valor unitário deve ser maior que zero.");

            if (desconto < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(desconto),
                    "O desconto não pode ser negativo.");

            CompraId = compraId;
            ProdutoId = produtoId;

            DescricaoProduto = descricaoProduto.Trim();

            Quantidade = quantidade;
            QuantidadeRecebida = 0;

            ValorUnitario = Arredondar(valorUnitario);
            Desconto = Arredondar(desconto);

            var subtotal = Arredondar(
                Quantidade * ValorUnitario);

            if (Desconto > subtotal)
                throw new ArgumentOutOfRangeException(
                    nameof(desconto),
                    "O desconto não pode superar o subtotal do item.");

            ValorTotal = Arredondar(subtotal - Desconto);
        }

        private static decimal Arredondar(decimal valor)
        {
            return decimal.Round(
                valor,
                2,
                MidpointRounding.AwayFromZero);
        }
    }
}
