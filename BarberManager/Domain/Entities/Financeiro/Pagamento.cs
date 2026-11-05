using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;

namespace Domain.Entities.Financeiro
{
    public class Pagamento : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public string ChaveIdempotencia { get; set; }
            = string.Empty;

        public Guid ContaPagarId { get; set; }

        public ContaPagar ContaPagar { get; set; } = null!;

        public Guid FormaPagamentoId { get; set; }

        public FormaPagamento FormaPagamento { get; set; } = null!;

        public DateTime DataPagamento { get; set; }

        public decimal Valor { get; set; }

        public string? Observacao { get; set; }

        public decimal ValorPagoAposPagamento { get; set; }

        public decimal SaldoRestanteAposPagamento { get; set; }

        public StatusContas StatusContaAposPagamento { get; set; }
    }
}