using Domain.Entities.Clientes;
using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Financeiro
{
    public class ContaReceber : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public Guid? ClienteId { get; set; }

        public Cliente? Cliente { get; set; }

        public DateTime DataEmissao { get; set; }

        public DateTime DataVencimento { get; set; }

        public decimal Valor { get; set; }

        public decimal ValorRecebido { get; set; }

        public StatusContas Status { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public ICollection<Recebimento> Recebimentos { get; set; }
            = new List<Recebimento>();
    }

}
