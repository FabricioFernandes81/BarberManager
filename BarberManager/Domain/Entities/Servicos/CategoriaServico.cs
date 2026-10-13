using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Servicos
{
    public class CategoriaServico : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public string? Cor { get; set; }

        public string? Icone { get; set; }

        public int Ordem { get; set; }

        public StatusGeral Ativo { get; set; } = StatusGeral.Ativo;

        public ICollection<Servico> Servicos { get; set; }
            = new List<Servico>();
    }
}
