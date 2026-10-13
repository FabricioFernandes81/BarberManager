using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Servicos
{
    public class Servico : EntityBase
    {
        public Guid EmpresaId { get; set; }
        public Guid CategoriaServicoId { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public decimal Valor { get; set; }

        public decimal Custo { get; set; }

        public int DuracaoMinutos { get; set; }

        public int IntervaloMinutos { get; set; }

        public TipoComissaoServico TipoComissao { get; set; }

        public decimal PercentualComissao { get; set; }

        public bool PermiteAgendamentoOnline { get; set; }

        public bool ExibirAplicativo { get; set; }

        public StatusGeral Ativo { get; set; } = StatusGeral.Ativo;

        public string? Observacao { get; set; }

        public CategoriaServico CategoriaServico { get; set; } = null!;

        public ICollection<ProfissionalServicos> Profissionais { get; set; }

    }
}
