using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Saas
{
    public class Plano : EntityBase
    {
        public string Nome { get; set; } = string.Empty;

        public decimal ValorMensal { get; set; }

        public decimal ValorAnual { get; set; }

        public int LimiteProfissionais { get; set; }

        public int LimiteEmpresas { get; set; }

        public StatusGeral Ativo { get; set; }

        public ICollection<Assinatura> Assinaturas { get; set; }
        = new List<Assinatura>();

       public ICollection<PlanoModulo> Modulos { get; set; } = [];
    }
}
