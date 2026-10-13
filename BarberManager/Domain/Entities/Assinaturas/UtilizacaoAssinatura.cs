using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Assinaturas
{
    public class UtilizacaoAssinatura : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public Guid AssinaturaClienteId { get; set; }

        public Guid? BeneficiarioAssinaturaId { get; set; }

        public Guid ServicoId { get; set; }

        // Referência ao agendamento que originou o consumo.
        public Guid? AgendamentoId { get; set; }

        public DateTime DataUtilizacao { get; set; }

        public int Quantidade { get; set; } = 1;

        public string? Observacao { get; set; }

        // Navegações
        public AssinaturaCliente AssinaturaCliente { get; set; }
            = null!;

        public BeneficiarioAssinatura? BeneficiarioAssinatura
        { get; set; }
    }
}
