using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Usuarios.DTOs
{
    public class GetUsuarioDetalheDto
    {
        public Guid Id { get; init; }

        public string? NomeUsuario { get; init; }

        public string? Email { get; init; }

        public string? Telefone { get; init; }

        public bool EmailConfirmado { get; init; }

        public bool Ativo { get; init; }

        public List<UsuarioEmpresaDto> Empresas { get; init; } = [];
    }

    public class UsuarioEmpresaDto
    {
        public Guid EmpresaId { get; init; }

        public bool Ativo { get; init; }

        public DateTime DataVinculo { get; init; }

        public DateTime? DataDesvinculo { get; init; }
    }
}
