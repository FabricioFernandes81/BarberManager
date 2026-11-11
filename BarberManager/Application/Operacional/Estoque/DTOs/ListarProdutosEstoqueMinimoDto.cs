using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Estoque.DTOs
{
    public sealed record ListarProdutosEstoqueMinimoDto(
         IReadOnlyList<ProdutoEstoqueMinimoDto> Itens,
         int Pagina,
         int TamanhoPagina,
         int TotalRegistros,
         int TotalPaginas
     );
}
