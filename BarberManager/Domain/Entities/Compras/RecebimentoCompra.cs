
using Domain.Entities.Common;

namespace Domain.Entities.Compras
{
    public class RecebimentoCompra : EntityBase
    {
        public Guid EmpresaId { get; private set; }

        public Guid CompraId { get; private set; }

        public Compra Compra { get; private set; } = null!;

        public Guid UsuarioResponsavelId { get; private set; }

        public DateTimeOffset DataRecebimento { get; private set; }

        public string ChaveIdempotencia { get; private set; }
            = string.Empty;

        public string? Observacao { get; private set; }

        public ICollection<RecebimentoCompraItem> Itens
        {
            get;
            private set;
        } = new List<RecebimentoCompraItem>();

        protected RecebimentoCompra()
        {
        }

        public RecebimentoCompra(
            Guid empresaId,
            Guid compraId,
            Guid usuarioResponsavelId,
            string chaveIdempotencia,
            string? observacao = null,
            DateTimeOffset? dataRecebimento = null)
        {
            if (empresaId == Guid.Empty)
                throw new ArgumentException(
                    "A empresa é obrigatória.",
                    nameof(empresaId));

            if (compraId == Guid.Empty)
                throw new ArgumentException(
                    "A compra é obrigatória.",
                    nameof(compraId));

            if (usuarioResponsavelId == Guid.Empty)
                throw new ArgumentException(
                    "O usuário responsável é obrigatório.",
                    nameof(usuarioResponsavelId));

            if (string.IsNullOrWhiteSpace(chaveIdempotencia))
                throw new ArgumentException(
                    "A chave de idempotência é obrigatória.",
                    nameof(chaveIdempotencia));

            chaveIdempotencia = chaveIdempotencia.Trim();

            if (chaveIdempotencia.Length > 100)
                throw new ArgumentException(
                    "A chave de idempotência deve possuir no máximo 100 caracteres.",
                    nameof(chaveIdempotencia));

            if (observacao?.Length > 500)
                throw new ArgumentException(
                    "A observação deve possuir no máximo 500 caracteres.",
                    nameof(observacao));

            EmpresaId = empresaId;
            CompraId = compraId;
            UsuarioResponsavelId = usuarioResponsavelId;
            ChaveIdempotencia = chaveIdempotencia;

            Observacao = string.IsNullOrWhiteSpace(observacao)
                ? null
                : observacao.Trim();

            DataRecebimento =
                dataRecebimento ?? DateTimeOffset.UtcNow;
        }

        public void AdicionarItem(RecebimentoCompraItem item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (item.RecebimentoCompraId != Id)
                throw new InvalidOperationException(
                    "O item não pertence a este recebimento.");

            if (Itens.Any(i => i.CompraItemId == item.CompraItemId))
                throw new InvalidOperationException(
                    "O item da compra já foi informado neste recebimento.");

            Itens.Add(item);
        }

        public void ValidarItens()
        {
            if (Itens.Count == 0)
                throw new InvalidOperationException(
                    "O recebimento deve possuir pelo menos um item.");
        }
    }
}
