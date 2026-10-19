using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Saas
{
    public class EmpresaModulo
    {
        public Guid EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public Guid ModuloId { get; set; }
        public Modulo Modulo { get; set; }

    }
}
