using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Produtos.DTOs
{
    public sealed record ProdutoListaDto(
    Guid Id,
    string Nome,
    string? CodigoInterno,
    string? CodigoBarras,
    string? Categoria,
    string? Marca,
    decimal ValorVenda,
    decimal Custo,
    bool ControlaEstoque,
    decimal? QuantidadeEstoque,
    decimal? EstoqueMinimo,
    bool Ativo,
    DateTime DataCadastro
);
}
