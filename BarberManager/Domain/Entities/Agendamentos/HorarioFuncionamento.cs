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

        public Guid ProfissionalId { get; set; }

        public DayOfWeek DiaSemana { get; set; }

        // Horários locais da barbearia.
        public TimeOnly HoraInicio { get; set; }

        public TimeOnly HoraFim { get; set; }

        public bool Ativo { get; set; } = true;

        public Profissional Profissional { get; set; } = null!;
    }
}
