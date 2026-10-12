using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AuthUsers
{
    public class UsuarioEmpresa : EntityBase
    {
        public Guid UsuarioId { get; set; }

        public Guid EmpresaId { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataVinculo { get; set; }

        public DateTime? DataDesvinculo { get; set; }

        public Usuario Usuario { get; set; }
    }
}
