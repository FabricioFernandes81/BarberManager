using Domain.Entities.Common;
using Domain.Entities.Profissionais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Agendamentos
{
    public class HorarioFuncionamento : EntityBase
    {
        public Guid EmpresaId { get; set; }

    
        public DayOfWeek DiaSemana { get; set; }

        // Horários locais da barbearia.
        public TimeOnly HoraInicio { get; set; }

        public TimeOnly HoraFim { get; set; }

        public bool Ativo { get; set; } = true;

        public void AtualizarDiaEHorario(
            DayOfWeek diaSemana,
            TimeOnly horaInicio,
            TimeOnly horaFim)
        {
            if (!Enum.IsDefined(typeof(DayOfWeek), diaSemana))
                throw new ArgumentOutOfRangeException(
                    nameof(diaSemana),
                    "O dia da semana informado é inválido.");

            if (horaFim <= horaInicio)
                throw new ArgumentException(
                    "O horário final deve ser posterior ao horário inicial.");

            DiaSemana = diaSemana;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }
    }
}
