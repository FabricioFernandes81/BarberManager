using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.DTOs
{
    public class HorarioDisponivelDto
    {
        public DateTimeOffset DataHoraInicio { get; set; }
        public  DateTimeOffset DataHoraFim { get; set; }
    }
}
