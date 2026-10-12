using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AuthUsers
{
    public class GrupoPermissaoPermissao
    {
        public Guid PermissaoGrupoId { get; set; }

        public Guid PermissaoId { get; set; }

        public Permissao Permissao { get; set; }

        public PermissaoGrupo PermissaoGrupo { get; set; }

    }
}
