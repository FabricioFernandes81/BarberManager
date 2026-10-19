using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Saas
{
    public class Responsavel : EntityBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public string? Cpf { get; set; }
        public Guid UsuarioId { get; set; }

        public ICollection<Empresa> Empresas { get; set; }


    }
}
