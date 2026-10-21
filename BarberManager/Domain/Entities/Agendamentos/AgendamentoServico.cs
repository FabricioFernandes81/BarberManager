using Domain.Entities.Common;
using Domain.Entities.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Agendamentos
{
    public class AgendamentoServico : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public Guid AgendamentoId { get; set; }

        public Guid ServicoId { get; set; }

        // Snapshot dos dados no momento da reserva.
        public string NomeServico { get; set; } = string.Empty;

        public decimal Valor { get; set; }

        public int DuracaoMinutos { get; set; }

        public int Ordem { get; set; }

        public Agendamento Agendamento { get; set; } = null!;

        public Servico Servico { get; set; } = null!;
    }
}
