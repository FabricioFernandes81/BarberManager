using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Assinaturas
{
    public class BeneficiarioAssinatura : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public Guid AssinaturaClienteId { get; set; }

        public Guid ClienteId { get; set; }

        public bool Titular { get; set; }

        public bool Ativo { get; set; } = true;

        public DateTime DataVinculo { get; set; } = DateTime.UtcNow;

        public DateTime? DataDesvinculo { get; set; }

        public AssinaturaCliente AssinaturaCliente { get; set; }
            = null!;
    }
}
