using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.DTOs
{
    public sealed record ObterAgendaPeriodoDto(
        IReadOnlyCollection<AgendamentoListaDto> Itens,
        int PageNumber,
        int PageSize,
        int TotalRegistros,
        int TotalPaginas
    );
}
