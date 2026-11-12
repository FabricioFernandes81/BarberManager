
using Domain.Entities.Common;
using Domain.Entities.Produtos;

namespace Domain.Entities.Compras
{
    public class DevolucaoCompraItem : EntityBase
    {
        public Guid DevolucaoCompraId { get; private set; }

        public DevolucaoCompra DevolucaoCompra { get; private set; }
            = null!;

        public Guid RecebimentoCompraItemId { get; private set; }

        public RecebimentoCompraItem RecebimentoCompraItem { get; private set; }
            = null!;

        public Guid ProdutoId { get; private set; }

        public Produto Produto { get; private set; } = null!;

        public decimal QuantidadeDevolvida { get; private set; }

        protected DevolucaoCompraItem()
        {
        }

        public DevolucaoCompraItem(
            Guid devolucaoCompraId,
            Guid recebimentoCompraItemId,
            Guid produtoId,
            decimal quantidadeDevolvida)
        {
            if (devolucaoCompraId == Guid.Empty)
                throw new ArgumentException(
                    "A devolução é obrigatória.",
                    nameof(devolucaoCompraId));

            if (recebimentoCompraItemId == Guid.Empty)
                throw new ArgumentException(
                    "O item do recebimento é obrigatório.",
                    nameof(recebimentoCompraItemId));

            if (produtoId == Guid.Empty)
                throw new ArgumentException(
                    "O produto é obrigatório.",
                    nameof(produtoId));

            if (quantidadeDevolvida <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(quantidadeDevolvida),
                    "A quantidade devolvida deve ser maior que zero.");

            if (quantidadeDevolvida !=
                decimal.Round(quantidadeDevolvida, 3))
            {
                throw new ArgumentException(
                    "A quantidade devolvida deve possuir no máximo três casas decimais.",
                    nameof(quantidadeDevolvida));
            }

            DevolucaoCompraId = devolucaoCompraId;
            RecebimentoCompraItemId = recebimentoCompraItemId;
            ProdutoId = produtoId;
            QuantidadeDevolvida = quantidadeDevolvida;
        }
    }
}
