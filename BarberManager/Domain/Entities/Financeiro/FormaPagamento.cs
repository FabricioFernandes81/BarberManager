using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Financeiro
{
    public class FormaPagamento : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public string Nome { get; set; } = string.Empty;

        public bool PermiteParcelamento { get; set; }

        public bool GeraTroco { get; set; }

        public bool Ativo { get; set; }
    }

}
