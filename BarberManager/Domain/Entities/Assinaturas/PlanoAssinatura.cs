using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Assinaturas
{
    public class PlanoAssinatura : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public decimal Valor { get; set; }

        public PeriodicidadeAssinatura Periodicidade { get; set; }

        public int QuantidadeCiclos { get; set; } = 1;

        public StatusGeral Ativo { get; set; } = StatusGeral.Inativo;

        public bool DisponivelOnline { get; set; }

        public DateTime? DataInicioVigencia { get; set; }

        public DateTime? DataFimVigencia { get; set; }

        public ICollection<PlanoAssinaturaServico> Servicos
        { get; set; } = new List<PlanoAssinaturaServico>();

        public ICollection<BeneficioPlano> Beneficios
        { get; set; } = new List<BeneficioPlano>();

        public ICollection<AssinaturaCliente> Assinaturas
        { get; set; } = new List<AssinaturaCliente>();
    }
}
