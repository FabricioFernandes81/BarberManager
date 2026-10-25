using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Agendamentos
{
    public class ConfiguracaoAgendaEmpresa : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public int ToleranciaEncerramentoMinutos { get; set; } = 0;
    }
}
