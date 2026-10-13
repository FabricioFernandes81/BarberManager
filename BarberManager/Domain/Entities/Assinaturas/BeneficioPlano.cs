using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Assinaturas
{
    public class BeneficioPlano : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public Guid PlanoAssinaturaId { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public TipoBeneficioPlano Tipo { get; set; }

        // Percentual de desconto ou valor monetário,
        // conforme o tipo do benefício.
        public decimal? Valor { get; set; }

        public bool Ativo { get; set; } = true;

        public PlanoAssinatura PlanoAssinatura { get; set; }
            = null!;
    }
}
