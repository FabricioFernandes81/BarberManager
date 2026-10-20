using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Clientes
{
    public class ClienteEmpresa : EntityBase
    {
        public Guid ClienteId { get; set; }

        // Empresa de destino. A validação de existência e acesso
        // será feita pela aplicação, pois Empresa pertence ao contexto SaaS.
        public Guid EmpresaId { get; set; }

        public DateTime DataVinculo { get; set; } = DateTime.UtcNow;

        public DateTime? DataPrimeiroAtendimento { get; set; }

        public DateTime? DataUltimoAtendimento { get; set; }

        public string? ObservacoesInternas { get; set; }

        public bool Ativo { get; set; } = true;

        public Cliente Cliente { get; set; } = null!;
    }
}
