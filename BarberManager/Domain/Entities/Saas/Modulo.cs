using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Saas
{
    public class Modulo : EntityBase
    {
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public StatusGeral Ativo { get; set; }

        public ICollection<EmpresaModulo> EmpresaModulos { get; set; }

    }
}
