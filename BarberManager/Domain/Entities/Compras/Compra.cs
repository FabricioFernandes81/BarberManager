
using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;

namespace Domain.Entities.Compras
{
    public class Compra : EntityBase
    {
        public Guid EmpresaId { get; private set; }

        public Guid FornecedorId { get; private set; }

        public Fornecedor Fornecedor { get; private set; } = null!;

        public string? NumeroDocumento { get; private set; }

        public string? SerieDocumento { get; private set; }

        public DateTime DataEmissao { get; private set; }

        public DateTime DataCompra { get; private set; }

        public decimal Subtotal { get; private set; }

        public decimal Desconto { get; private set; }

        public decimal Frete { get; private set; }

        public decimal OutrasDespesas { get; private set; }

        public decimal ValorTotal { get; private set; }

        public string? Observacao { get; private set; }

        public StatusCompra Status { get; private set; }

        public ICollection<CompraItem> Itens { get; private set; }
            = new List<CompraItem>();

        public ICollection<RecebimentoCompra> Recebimentos { get; private set; }
            = new List<RecebimentoCompra>();
        protected Compra()
        {
        }

        public Compra(
            Guid empresaId,
            Guid fornecedorId,
            DateTime dataEmissao,
            DateTime dataCompra,
            decimal desconto,
            decimal frete,
            decimal outrasDespesas,
            string? numeroDocumento = null,
            string? serieDocumento = null,
            string? observacao = null)
        {
            if (empresaId == Guid.Empty)
                throw new ArgumentException(
                    "A empresa é obrigatória.",
                    nameof(empresaId));

            if (fornecedorId == Guid.Empty)
                throw new ArgumentException(
                    "O fornecedor é obrigatório.",
                    nameof(fornecedorId));

            if (dataEmissao == default)
                throw new ArgumentException(
                    "A data de emissão é obrigatória.",
                    nameof(dataEmissao));

            if (dataCompra == default)
                throw new ArgumentException(
                    "A data da compra é obrigatória.",
                    nameof(dataCompra));

            ValidarValorNaoNegativo(desconto, nameof(desconto));
            ValidarValorNaoNegativo(frete, nameof(frete));
            ValidarValorNaoNegativo(outrasDespesas, nameof(outrasDespesas));

            EmpresaId = empresaId;
            FornecedorId = fornecedorId;

            DataEmissao = dataEmissao.Date;
            DataCompra = dataCompra.Date;

            Desconto = Arredondar(desconto);
            Frete = Arredondar(frete);
            OutrasDespesas = Arredondar(outrasDespesas);

            NumeroDocumento = Normalizar(numeroDocumento);
            SerieDocumento = Normalizar(serieDocumento);
            Observacao = Normalizar(observacao);

            Status = StatusCompra.Pendente;
        }

        public void AdicionarItem(CompraItem item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (Status != StatusCompra.Pendente)
                throw new InvalidOperationException(
                    "Não é possível adicionar itens a esta compra.");

            if (item.CompraId != Id)
                throw new InvalidOperationException(
                    "O item informado não pertence a esta compra.");

            if (Itens.Any(i => i.ProdutoId == item.ProdutoId))
                throw new InvalidOperationException(
                    "O produto já foi adicionado à compra.");

            Itens.Add(item);
        }

        public void RecalcularTotais()
        {
            if (Itens.Count == 0)
                throw new InvalidOperationException(
                    "A compra deve possuir pelo menos um item.");

            var subtotal = Arredondar(
                Itens.Sum(i => i.Quantidade * i.ValorUnitario));

            var descontosItens = Arredondar(
                Itens.Sum(i => i.Desconto));

            var valorDisponivelParaDesconto =
                subtotal - descontosItens;

            if (Desconto > valorDisponivelParaDesconto)
                throw new InvalidOperationException(
                    "O desconto da compra não pode superar o valor dos itens.");

            var valorTotal = Arredondar(
                subtotal
                - descontosItens
                - Desconto
                + Frete
                + OutrasDespesas);

            if (valorTotal <= 0)
                throw new InvalidOperationException(
                    "O valor total da compra deve ser maior que zero.");

            // Atualiza somente depois que todas as validações passam.
            Subtotal = subtotal;
            ValorTotal = valorTotal;
        }

        private static void ValidarValorNaoNegativo(
            decimal valor,
            string nomeParametro)
        {
            if (valor < 0)
                throw new ArgumentOutOfRangeException(
                    nomeParametro,
                    "O valor não pode ser negativo.");
        }

        private static decimal Arredondar(decimal valor)
        {
            return decimal.Round(
                valor,
                2,
                MidpointRounding.AwayFromZero);
        }

        private static string? Normalizar(string? valor)
        {
            return string.IsNullOrWhiteSpace(valor)
                ? null
                : valor.Trim();
        }


        public void AtualizarStatusRecebimento()
        {
            if (Status == StatusCompra.Cancelada)
                throw new InvalidOperationException(
                    "Uma compra cancelada não pode ter seu recebimento atualizado.");

            if (Itens.Count == 0)
                throw new InvalidOperationException(
                    "A compra não possui itens.");

            var possuiRecebimento = Itens.Any(
                item => item.QuantidadeRecebida > 0);

            var possuiPendencia = Itens.Any(
                item => item.QuantidadePendente > 0);

            if (!possuiRecebimento)
                Status = StatusCompra.Pendente;
            else if (possuiPendencia)
                Status = StatusCompra.ParcialmenteRecebida;
            else
                Status = StatusCompra.Recebida;

            DataAtualizacao = DateTime.UtcNow;
        }

    }
}
