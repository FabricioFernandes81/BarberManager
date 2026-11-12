
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

        public decimal QuantidadeAcumuladaAposRecebimento
        {
            get;
            private set;
        }

        public decimal QuantidadePendenteAposRecebimento
        {
            get;
            private set;
        }

        public decimal QuantidadeDevolvida { get; private set; }

        public decimal QuantidadeDisponivelParaDevolucao =>
            QuantidadeRecebida - QuantidadeDevolvida;

        protected RecebimentoCompraItem()
        {
        }

        public RecebimentoCompraItem(
            Guid recebimentoCompraId,
            Guid compraItemId,
            Guid produtoId,
            decimal quantidadeRecebida,
            decimal quantidadeAcumuladaAposRecebimento,
            decimal quantidadePendenteAposRecebimento)
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

            if (quantidadeAcumuladaAposRecebimento < quantidadeRecebida)
            {
                throw new ArgumentException(
                    "A quantidade acumulada não pode ser menor que a quantidade recebida.",
                    nameof(quantidadeAcumuladaAposRecebimento));
            }

            if (quantidadePendenteAposRecebimento < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(quantidadePendenteAposRecebimento),
                    "A quantidade pendente não pode ser negativa.");
            }

            RecebimentoCompraId = recebimentoCompraId;
            CompraItemId = compraItemId;
            ProdutoId = produtoId;

            QuantidadeRecebida = quantidadeRecebida;

            QuantidadeAcumuladaAposRecebimento =
                quantidadeAcumuladaAposRecebimento;

            QuantidadePendenteAposRecebimento =
                quantidadePendenteAposRecebimento;

            QuantidadeDevolvida = 0;
        }

        public void RegistrarDevolucao(decimal quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(quantidade),
                    "A quantidade devolvida deve ser maior que zero.");

            if (quantidade != decimal.Round(quantidade, 3))
                throw new ArgumentException(
                    "A quantidade devolvida deve possuir no máximo três casas decimais.",
                    nameof(quantidade));

            if (quantidade > QuantidadeDisponivelParaDevolucao)
                throw new InvalidOperationException(
                    "A quantidade devolvida não pode superar a quantidade disponível para devolução.");

            QuantidadeDevolvida += quantidade;

            DataAtualizacao = DateTime.UtcNow;
        }
    }
}
