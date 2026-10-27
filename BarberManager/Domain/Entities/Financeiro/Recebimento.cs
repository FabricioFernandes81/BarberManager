using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Financeiro
{
    public class Recebimento : EntityBase
    {
        public Guid ContaReceberId { get; set; }

        public ContaReceber ContaReceber { get; set; } = null!;

        public Guid FormaPagamentoId { get; set; }

        public FormaPagamento FormaPagamento { get; set; } = null!;

        public DateTime DataRecebimento { get; set; }

        public decimal Valor { get; set; }

        public string? Observacao { get; set; }
    }

}
