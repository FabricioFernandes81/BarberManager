using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AuthUsers
{
    public class Roles : IdentityRole<Guid>
    {
        public string? Descricao { get; set; }

        public bool Ativo { get; set; } = true;

        public ICollection<RolePermissao> Permissoes { get; set; }
    }
}
