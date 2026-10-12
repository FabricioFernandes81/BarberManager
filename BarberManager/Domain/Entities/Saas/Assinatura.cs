using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Saas
{
    public class Assinatura : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public Empresa Empresa { get; set; }

        public Guid PlanoId { get; set; }

        public Plano Plano { get; set; } = null!;

        public DateTime DataInicio { get; set; }

        public DateTime DataVencimento { get; set; }

        public bool RenovacaoAutomatica { get; set; }

        public bool Trial { get; set; }

        public StatusGeral Ativa { get; set; }
    }
}
