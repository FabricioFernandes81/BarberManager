using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Produtos.Queries
{
    public sealed record ListarProdutosQuery(
     int PageNumber = 1,
     int PageSize = 10,
     string? SearchTerm = null,
     Guid? CategoriaId = null,
     Guid? MarcaId = null,
     bool? Ativo = null
 );
}
