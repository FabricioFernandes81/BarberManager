using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            RecalcularTotais();
        }

        public void RecalcularTotais()
        {
            Subtotal = Arredondar(
                Itens.Sum(i => i.Quantidade * i.ValorUnitario));

            var descontosItens = Arredondar(
                Itens.Sum(i => i.Desconto));

            if (Desconto > Subtotal - descontosItens)
                throw new InvalidOperationException(
                    "O desconto da compra não pode superar o valor dos itens.");

            ValorTotal = Arredondar(
                Subtotal
                - descontosItens
                - Desconto
                + Frete
                + OutrasDespesas);

            if (ValorTotal <= 0)
                throw new InvalidOperationException(
                    "O valor total da compra deve ser maior que zero.");
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
    }
}
