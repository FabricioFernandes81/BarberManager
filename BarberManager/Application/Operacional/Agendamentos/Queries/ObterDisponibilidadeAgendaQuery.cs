using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.Queries
{
    public sealed record ObterDisponibilidadeAgendaQuery(
    Guid ProfissionalId,
    Guid ServicoId,
    DateOnly Data);
}
