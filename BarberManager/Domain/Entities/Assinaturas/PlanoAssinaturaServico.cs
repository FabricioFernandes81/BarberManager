using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Assinaturas
{
    public class PlanoAssinaturaServico : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public Guid PlanoAssinaturaId { get; set; }

        public Guid ServicoId { get; set; }

        // Null representa quantidade ilimitada.
        public int? QuantidadePorCiclo { get; set; }

        public int? LimitePorDia { get; set; }

        public bool Ativo { get; set; } = true;

        public PlanoAssinatura PlanoAssinatura { get; set; }
            = null!;
    }
}
