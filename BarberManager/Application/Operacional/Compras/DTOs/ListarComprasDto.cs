using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.DTOs
{
    public sealed record ListarComprasDto(
        IReadOnlyList<CompraListaDto> Itens,
        int PageNumber,
        int PageSize,
        int TotalRegistros,
        int TotalPaginas
    );
}
