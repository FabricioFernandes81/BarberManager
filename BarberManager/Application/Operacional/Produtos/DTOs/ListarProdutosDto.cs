using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Produtos.DTOs
{
    public sealed record ListarProdutosDto(
     IReadOnlyCollection<ProdutoListaDto> Itens,
     int Pagina,
     int TamanhoPagina,
     int TotalRegistros,
     int TotalPaginas
 );
}
