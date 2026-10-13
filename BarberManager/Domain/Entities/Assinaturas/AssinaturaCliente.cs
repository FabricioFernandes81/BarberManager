using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Assinaturas
{
    public class AssinaturaCliente : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public Guid ClienteId { get; set; }

        public Guid PlanoAssinaturaId { get; set; }

        // Valor acordado no momento da contratação.
        public decimal ValorContratado { get; set; }

        public int Periodicidade { get; set; }

        public StatusAssinaturaCliente Status { get; set; }
            = StatusAssinaturaCliente.Pendente;

        public DateTime DataInicio { get; set; }

        public DateTime DataFimCicloAtual { get; set; }

        public DateTime? DataProximaCobranca { get; set; }

        public DateTime? DataCancelamento { get; set; }

        public bool RenovacaoAutomatica { get; set; } = true;

        // Identificador externo do contrato no gateway,
        // quando houver integração de pagamentos.
        public string? ReferenciaGateway { get; set; }

        public PlanoAssinatura PlanoAssinatura { get; set; }
            = null!;

        public ICollection<BeneficiarioAssinatura> Beneficiarios
        { get; set; } = new List<BeneficiarioAssinatura>();

        public ICollection<UtilizacaoAssinatura> Utilizacoes
        { get; set; } = new List<UtilizacaoAssinatura>();
    }
}
