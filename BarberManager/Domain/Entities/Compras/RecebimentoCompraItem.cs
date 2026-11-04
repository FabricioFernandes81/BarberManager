
using Domain.Entities.Common;

namespace Domain.Entities.Compras
{
    public class RecebimentoCompraItem : EntityBase
    {
        public Guid RecebimentoCompraId { get; private set; }

        public RecebimentoCompra RecebimentoCompra { get; private set; }
            = null!;

        public Guid CompraItemId { get; private set; }

        public CompraItem CompraItem { get; private set; }
            = null!;

        public Guid ProdutoId { get; private set; }

        public decimal QuantidadeRecebida { get; private set; }

        protected RecebimentoCompraItem()
        {
        }

        public RecebimentoCompraItem(
            Guid recebimentoCompraId,
            Guid compraItemId,
            Guid produtoId,
            decimal quantidadeRecebida)
        {
            if (recebimentoCompraId == Guid.Empty)
                throw new ArgumentException(
                    "O recebimento é obrigatório.",
                    nameof(recebimentoCompraId));

            if (compraItemId == Guid.Empty)
                throw new ArgumentException(
                    "O item da compra é obrigatório.",
                    nameof(compraItemId));

            if (produtoId == Guid.Empty)
                throw new ArgumentException(
                    "O produto é obrigatório.",
                    nameof(produtoId));

            if (quantidadeRecebida <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(quantidadeRecebida),
                    "A quantidade recebida deve ser maior que zero.");

            if (quantidadeRecebida !=
                decimal.Round(quantidadeRecebida, 3))
            {
                throw new ArgumentException(
                    "A quantidade deve possuir no máximo três casas decimais.",
                    nameof(quantidadeRecebida));
            }

            RecebimentoCompraId = recebimentoCompraId;
            CompraItemId = compraItemId;
            ProdutoId = produtoId;
            QuantidadeRecebida = quantidadeRecebida;
        }


    }
}
