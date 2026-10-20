using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Clientes.Commands
{
    public class CriarPerfilClienteCommand
    {
        public string Nome { get; init; } = string.Empty;

        public string? Sobrenome { get; init; }

        public string? Cpf { get; init; }

        public DateTime? DataNascimento { get; init; }
    }
}
