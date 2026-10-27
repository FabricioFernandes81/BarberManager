using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Financeiro
{
    public class Pagamento : EntityBase
    {
        public Guid ContaPagarId { get; set; }

        public ContaPagar ContaPagar { get; set; } = null!;

        public Guid FormaPagamentoId { get; set; }

        public FormaPagamento FormaPagamento { get; set; } = null!;

        public DateTime DataPagamento { get; set; }

        public decimal Valor { get; set; }

        public string? Observacao { get; set; }
    }

}
