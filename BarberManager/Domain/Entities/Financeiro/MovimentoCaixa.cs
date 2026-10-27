using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Financeiro
{
    public class MovimentoCaixa : EntityBase
    {
        public Guid CaixaId { get; set; }

        public Caixa Caixa { get; set; } = null!;

        public TipoMovimentoCaixa TipoMovimento { get; set; }

        public Guid FormaPagamentoId { get; set; }

        public FormaPagamento FormaPagamento { get; set; } = null!;

        public DateTime DataMovimento { get; set; }

        public decimal Valor { get; set; }

        public string Historico { get; set; } = string.Empty;

        public string? Documento { get; set; }
    }

}
