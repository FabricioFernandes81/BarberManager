using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Produtos.DTOs
{
    public sealed record ProdutoDetalheDto(
    Guid Id,
    Guid? CategoriaProdutoId,
    string? Categoria,
    Guid? MarcaId,
    string? Marca,
    string Nome,
    string? Descricao,
    string? CodigoInterno,
    string? CodigoBarras,
    decimal ValorVenda,
    decimal Custo,
    bool ControlaEstoque,
    decimal? QuantidadeEstoque,
    decimal? EstoqueMinimo,
    bool Ativo,
    DateTime DataCadastro,
    DateTime? DataAtualizacao
);
}
