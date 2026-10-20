using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clientes
{
    public class Cliente : EntityBase
    {
        // Referência ao usuário global do ASP.NET Core Identity.
        // A integridade dessa referência será validada pela aplicação.
        public Guid UsuarioId { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Sobrenome { get; set; }

        public string? Cpf { get; set; }

        public DateTime? DataNascimento { get; set; }

        public bool Ativo { get; set; } = true;

        public ICollection<ClienteEmpresa> Empresas { get; set; }
            = new List<ClienteEmpresa>();
    }
}
