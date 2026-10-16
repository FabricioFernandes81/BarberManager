using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public class ObterStatusFaturaDto
    {
        public Guid FaturaId { get; set; }

        public decimal Valor { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime Vencimento { get; set; }

        public DateTime? DataPagamento { get; set; }

        public DateTime PeriodoInicio { get; set; }

        public DateTime PeriodoFim { get; set; }

        public string? ReferenciaGateway { get; set; }

        public Guid? TransacaoId { get; set; }

        public string? StatusTransacao { get; set; }

        public string? MetodoPagamento { get; set; }

        public DateTime? DataProcessamento { get; set; }
    }
}
