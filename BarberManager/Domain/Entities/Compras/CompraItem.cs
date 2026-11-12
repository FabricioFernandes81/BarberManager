
using Domain.Entities.Common;
using Domain.Entities.Produtos;

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

     
        public byte[]? RowVersion { get; private set; }


        protected CompraItem()
        {
        }

        public CompraItem(
            Guid compraId,
            Guid produtoId,
            string descricaoProduto,
            decimal quantidade,
            decimal valorUnitario,
            decimal desconto = 0, decimal quantidadeDevolvida = 0)
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

            descricaoProduto = descricaoProduto.Trim();

            if (descricaoProduto.Length > 200)
                throw new ArgumentException(
                    "A descrição do produto deve possuir no máximo 200 caracteres.",
                    nameof(descricaoProduto));

            if (quantidade <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(quantidade),
                    "A quantidade deve ser maior que zero.");

            if (quantidade != decimal.Round(quantidade, 3))
                throw new ArgumentException(
                    "A quantidade deve possuir no máximo três casas decimais.",
                    nameof(quantidade));

            if (valorUnitario <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(valorUnitario),
                    "O valor unitário deve ser maior que zero.");

            if (desconto < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(desconto),
                    "O desconto não pode ser negativo.");

            var valorUnitarioArredondado = Arredondar(valorUnitario);
            var descontoArredondado = Arredondar(desconto);

            if (valorUnitarioArredondado <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(valorUnitario),
                    "O valor unitário deve ser maior que zero após o arredondamento.");

            var subtotal = Arredondar(
                quantidade * valorUnitarioArredondado);

            if (descontoArredondado > subtotal)
                throw new ArgumentOutOfRangeException(
                    nameof(desconto),
                    "O desconto não pode superar o subtotal do item.");

            CompraId = compraId;
            ProdutoId = produtoId;
            DescricaoProduto = descricaoProduto;

            Quantidade = quantidade;
            QuantidadeRecebida = 0;

            ValorUnitario = valorUnitarioArredondado;
            Desconto = descontoArredondado;

            ValorTotal = Arredondar(
                subtotal - descontoArredondado);

           
        }

        private static decimal Arredondar(decimal valor)
        {
            return decimal.Round(
                valor,
                2,
                MidpointRounding.AwayFromZero);
        }


        public void RegistrarRecebimento(decimal quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(quantidade),
                    "A quantidade recebida deve ser maior que zero.");

            if (quantidade != decimal.Round(quantidade, 3))
                throw new ArgumentException(
                    "A quantidade deve possuir no máximo três casas decimais.",
                    nameof(quantidade));

            if (quantidade > QuantidadePendente)
                throw new InvalidOperationException(
                    "A quantidade recebida não pode superar a quantidade pendente.");

            QuantidadeRecebida += quantidade;

            DataAtualizacao = DateTime.UtcNow;
        }

       
    }
}
