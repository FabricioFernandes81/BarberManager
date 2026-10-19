using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Saas
{
    public class PlanoModulo
    {
        public Guid PlanoId { get; set; }
        public Plano Plano { get; set; }

        public Guid ModuloId { get; set; }
        public Modulo Modulo { get; set; } 
    }
}
