using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.Queries
{
    public sealed record CriarAgendamentoDto(
    Guid Id,
    Guid ClienteId,
    Guid ProfissionalId,
    Guid ServicoId,
    DateTimeOffset DataHoraInicio,
    DateTimeOffset DataHoraFim,
    string? Observacoes,
    decimal ValorTotal,
    string Status);
}
