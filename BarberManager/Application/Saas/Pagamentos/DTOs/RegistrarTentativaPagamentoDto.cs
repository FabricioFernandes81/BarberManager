using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public class RegistrarTentativaPagamentoDto
    {
        public Guid TransacaoId { get; set; }

        public Guid FaturaId { get; set; }

        public decimal Valor { get; set; }

        public string Provedor { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; }

        public bool JaExistia { get; set; }
    }
}
