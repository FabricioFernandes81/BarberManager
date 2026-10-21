using Domain.Entities.Clientes;
using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Profissionais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Agendamentos
{
    public class Agendamento : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public Guid ClienteId { get; set; }

        public Guid ProfissionalId { get; set; }

        public DateTimeOffset DataHoraInicio { get; set; }

        public DateTimeOffset DataHoraFim { get; set; }

        public StatusAgendamento Status { get; set; }
            = StatusAgendamento.Pendente;

        public decimal ValorTotal { get; set; }

        public string? Observacoes { get; set; }

        public DateTimeOffset? DataCancelamento { get; set; }

        public string? MotivoCancelamento { get; set; }

        public Guid? CanceladoPorUsuarioId { get; set; }

        public Cliente Cliente { get; set; } = null!;

        public Profissional Profissional { get; set; } = null!;

        public ICollection<AgendamentoServico> Servicos { get; set; }
            = new List<AgendamentoServico>();
    }
}
