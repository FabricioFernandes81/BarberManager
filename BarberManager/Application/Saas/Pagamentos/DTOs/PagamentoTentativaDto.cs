using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public class PagamentoTentativaDto
    {
        public Guid Id { get; set; }

        public Guid FaturaId { get; set; }

        public string Provedor { get; set; } = string.Empty;

        public string? ReferenciaGateway { get; set; }

        public decimal Valor { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; }

        public DateTime? DataProcessamento { get; set; }

        public string? CodigoErro { get; set; }

        public string? MensagemErro { get; set; }

    }
}
