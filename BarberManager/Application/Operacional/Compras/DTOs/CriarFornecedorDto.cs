using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.DTOs
{
    public record CriarFornecedorDto(
       Guid Id,
       string Nome,
       string? NomeFantasia,
       string? Documento,
       string? Email,
       string? Telefone,
       string? Observacao,
       bool Ativo,
       DateTime DataCadastro, PoliticaFinanceiraDevolucao PoliticaFinanceiraDevolucao);
}
