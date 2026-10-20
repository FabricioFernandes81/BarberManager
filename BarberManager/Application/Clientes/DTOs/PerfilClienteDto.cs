using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Clientes.DTOs
{
    public sealed record PerfilClienteDto(
     Guid Id,
     Guid UsuarioId,
     string Nome,
     string? Sobrenome,
     string? Cpf,
     DateTime? DataNascimento,
     string? Email,
     string? Celular,
     bool Ativo,
     DateTime DataCadastro);
}
