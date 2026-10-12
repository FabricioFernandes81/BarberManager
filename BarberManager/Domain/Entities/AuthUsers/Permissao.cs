using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AuthUsers
{
    public class Permissao : EntityBase
    {

        public string Codigo { get; set; } = null!;

        public string Nome { get; set; } = null!;

        public string Descricao { get; set; } = null!;

        public ICollection<GrupoPermissaoPermissao> GrupoPermissao { get; set; }


    }
}

