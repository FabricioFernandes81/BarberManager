using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Assinaturas.DTOs
{
    public class GetAssinaturaDto
    {

        public Guid Id { get; set; }

        public string Empresa { get; set; } = null!;

        public string Plano { get; set; } = null!;

        public decimal Valor { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime? DataUltimoPagamento { get; set; }

        public int DiaVencimento { get; set; }

        public bool Trial { get; set; }

        public bool RenovacaoAutomatica { get; set; }

    

        public int DiasParaVencer { get; set; }

        public string Status { get; set; }


    }
}
