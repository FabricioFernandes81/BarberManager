using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.DTOs
{
    public sealed record AgendamentoListaDto(
        Guid Id,
        Guid ClienteId,
        string NomeCliente,
        Guid ProfissionalId,
        string NomeProfissional,
        DateTimeOffset DataHoraInicio,
        DateTimeOffset DataHoraFim,
        StatusAgendamento Status,
        decimal ValorTotal,
        string? Observacoes
    );
}
