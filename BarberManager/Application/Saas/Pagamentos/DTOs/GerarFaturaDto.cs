using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public class GerarFaturaDto
    {
        public Guid? FaturaId { get; set; }

        public Guid AssinaturaId { get; set; }

        public decimal? Valor { get; set; }

        public DateTime? Vencimento { get; set; }

        public DateTime? PeriodoInicio { get; set; }

        public DateTime? PeriodoFim { get; set; }

        public string? Status { get; set; }

        public bool JaExistia { get; set; }

        public bool Ignorada { get; set; }

        public string? Motivo { get; set; }
    }
}
