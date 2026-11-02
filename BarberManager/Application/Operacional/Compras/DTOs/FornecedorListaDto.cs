using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.DTOs
{
    public record FornecedorListaDto(
       Guid Id,
       string Nome,
       string? NomeFantasia,
       string? Documento,
       string? Email,
       string? Telefone,
       bool Ativo,
       DateTime DataCadastro);
}
