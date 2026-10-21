using Domain.Entities.Common;
using Domain.Entities.Profissionais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Agendamentos
{
    public class BloqueioAgenda : EntityBase
    {
        public Guid EmpresaId { get; set; }

        // Nulo significa bloqueio de toda a agenda da empresa.
        public Guid? ProfissionalId { get; set; }

        public DateTimeOffset DataHoraInicio { get; set; }

        public DateTimeOffset DataHoraFim { get; set; }

        public string Motivo { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;

        public Profissional? Profissional { get; set; }
    }
}
