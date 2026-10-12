using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AuthUsers
{
    public class Usuario : IdentityUser<Guid>
    {
       
        public ICollection<UsuarioPermissao> PermissoesGrupo { get; set; } 
            = new List<UsuarioPermissao>();
    }
}
