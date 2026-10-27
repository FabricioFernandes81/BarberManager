using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.DTOs
{
    public sealed record ConfirmarAgendamentoDto(
         Guid AgendamentoId,
         StatusAgendamento Status,
         DateTimeOffset DataHoraInicio,
         DateTimeOffset DataHoraFim);
}
