using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.Queries
{
    public sealed record ObterAgendaPeriodoQuery(
        DateTime Inicio,
        DateTime Fim,
        Guid? ProfissionalId = null,
        Guid? ClienteId = null,
        int PageNumber = 1,
        int PageSize = 10,
        string? SearchTerm = null,
        string? SortBy = null,
        string? Status = null
    );
}
