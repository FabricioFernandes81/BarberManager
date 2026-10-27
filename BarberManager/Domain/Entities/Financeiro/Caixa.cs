using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Financeiro
{
    public class Caixa : EntityBase
    {

        public Guid EmpresaId { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime DataAbertura { get; set; }

        public DateTime? DataFechamento { get; set; }

        public decimal SaldoInicial { get; set; }

        public decimal SaldoFinal { get; set; }

        public StatusCaixa Status { get; set; }

        public string? Observacao { get; set; }

        public decimal? SaldoEsperadoFechamento { get; set; }

        public decimal? DiferencaFechamento { get; set; }

        public string? ObservacaoFechamento { get; set; }
        public ICollection<MovimentoCaixa> Movimentos { get; set; }
            = new List<MovimentoCaixa>();


    }
}
