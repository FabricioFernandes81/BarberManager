using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Profissionais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Servicos
{
    public class ProfissionalServicos : EntityBase
    {
        public Guid EmpresaId { get; set; }
        public Guid ProfissionalId { get; set; }
        public Guid ServicoId { get; set; }

        public decimal? ValorPersonalizado { get; set; }
        public int? DuracaoPersonalizada { get; set; }
        public DateTime DataVinculo { get; set; } = DateTime.UtcNow;

        public StatusGeral Ativo { get; set; } = StatusGeral.Ativo;

        public Profissional Profissional { get; set; } = null!;
        public Servico Servico { get; set; } = null!;
    }
}
