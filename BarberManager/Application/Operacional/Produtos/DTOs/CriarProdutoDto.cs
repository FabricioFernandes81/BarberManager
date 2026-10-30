using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Produtos.DTOs
{
    public sealed record CriarProdutoDto(
         Guid Id,
         string Nome,
         string? CodigoInterno,
         string? CodigoBarras,
         Guid? CategoriaProdutoId,
         Guid? MarcaId,
         decimal ValorVenda,
         decimal Custo,
         bool ControlaEstoque,
         decimal QuantidadeEstoqueInicial,
         decimal EstoqueMinimo,
         bool Ativo);
}
