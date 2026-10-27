using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.DTOs
{
    public class CancelarAgendamentoDto
    {

        public Guid AgendamentoId { get; set; }
        public StatusAgendamento Status { get; set; }

       public DateTimeOffset DataCancelamento { get; set; }

        public string? MotivoCancelamento { get; set; }
        public Guid? CanceladoPorUsuarioId { get; set; }


    }
}
