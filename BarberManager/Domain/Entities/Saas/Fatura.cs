using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Saas
{
    public class Fatura : EntityBase
    {
        public Guid AssinaturaId { get; set; }

        public Assinatura Assinatura { get; set; } = null!;

        public decimal Valor { get; set; }

        public DateTime Vencimento { get; set; }

        public DateTime? DataPagamento { get; set; }

        public StatusFatura Status { get; set; } = StatusFatura.Pendente;

        // Período de serviço cobrado pela fatura.
        public DateTime PeriodoInicio { get; set; }

        public DateTime PeriodoFim { get; set; }

        // Identificador da cobrança no gateway, quando existir.
        public string? ReferenciaGateway { get; set; }

    }
}
