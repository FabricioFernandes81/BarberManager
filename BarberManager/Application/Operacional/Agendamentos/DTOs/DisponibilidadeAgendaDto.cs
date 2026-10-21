using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.DTOs
{
    public sealed record DisponibilidadeAgendaDto(
     Guid ProfissionalId,
     Guid ServicoId,
     DateOnly Data,
     IReadOnlyCollection<HorarioDisponivelDto> Horarios);
}
