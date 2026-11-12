
using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;

namespace Domain.Entities.Compras
{
    public class DevolucaoCompra : EntityBase
    {
        public Guid EmpresaId { get; private set; }

        public Guid CompraId { get; private set; }

        public Compra Compra { get; private set; } = null!;

        public Guid UsuarioResponsavelId { get; private set; }

        public string ChaveIdempotencia { get; private set; }
            = string.Empty;

        public DateTimeOffset DataDevolucao { get; private set; }

        public string Motivo { get; private set; }
            = string.Empty;

        public string? Observacao { get; private set; }

        public PoliticaFinanceiraDevolucao PoliticaFinanceiraDevolucao
        {
            get;
            private set;
        }
        public ICollection<DevolucaoCompraItem> Itens
        {
            get;
            private set;
        } = new List<DevolucaoCompraItem>();

        protected DevolucaoCompra()
        {
        }

        public DevolucaoCompra(
            Guid empresaId,
            Guid compraId,
            Guid usuarioResponsavelId,
            string chaveIdempotencia,
            string motivo,
            string? observacao = null,
            DateTimeOffset? dataDevolucao = null,
            PoliticaFinanceiraDevolucao politicaFinanceiraDevolucao = PoliticaFinanceiraDevolucao.CreditoFornecedor)
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

            if (string.IsNullOrWhiteSpace(motivo))
                throw new ArgumentException(
                    "O motivo da devolução é obrigatório.",
                    nameof(motivo));

            motivo = motivo.Trim();

            if (motivo.Length > 200)
                throw new ArgumentException(
                    "O motivo deve possuir no máximo 200 caracteres.",
                    nameof(motivo));

            if (observacao?.Length > 500)
                throw new ArgumentException(
                    "A observação deve possuir no máximo 500 caracteres.",
                    nameof(observacao));

            if (!Enum.IsDefined(politicaFinanceiraDevolucao))
                throw new ArgumentOutOfRangeException(
                    nameof(politicaFinanceiraDevolucao),
                    "A política financeira da devolução é inválida.");


            EmpresaId = empresaId;
            CompraId = compraId;
            UsuarioResponsavelId = usuarioResponsavelId;

            ChaveIdempotencia = chaveIdempotencia;
            Motivo = motivo;

            Observacao = string.IsNullOrWhiteSpace(observacao)
                ? null
                : observacao.Trim();

            DataDevolucao = dataDevolucao ?? DateTimeOffset.UtcNow;
            PoliticaFinanceiraDevolucao = politicaFinanceiraDevolucao;
        }

        public void AdicionarItem(DevolucaoCompraItem item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (item.DevolucaoCompraId != Id)
                throw new InvalidOperationException(
                    "O item não pertence a esta devolução.");

            if (Itens.Any(i =>
                i.RecebimentoCompraItemId == item.RecebimentoCompraItemId))
            {
                throw new InvalidOperationException(
                    "O recebimento já foi informado nesta devolução.");
            }

            Itens.Add(item);
        }

        public void ValidarItens()
        {
            if (Itens.Count == 0)
                throw new InvalidOperationException(
                    "A devolução deve possuir pelo menos um item.");
        }
    }
}
