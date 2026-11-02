using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.DTOs
{
    public record ListarFornecedoresDto(
        IReadOnlyList<FornecedorListaDto> Itens,
        int PageNumber,
        int PageSize,
        int TotalRegistros,
        int TotalPaginas);
}
