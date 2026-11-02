using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Financeiro
{
    public class ContaPagar : EntityBase
    {
        public Guid EmpresaId { get; set; }

        public Guid FornecedorId { get; set; }

        public Fornecedor Fornecedor { get; set; } = null!;

        public DateTime DataEmissao { get; set; }

        public DateTime DataVencimento { get; set; }

        public decimal Valor { get; set; }

        public decimal ValorPago { get; set; }

        public StatusContas Status { get; set; }

        public string Descricao { get; set; } = string.Empty;

        public Guid? CompraId { get; set; }
        public Compra? Compra { get; set; }

        public int NumeroParcela { get; set; }

        public int TotalParcelas { get; set; }


        public ICollection<Pagamento> Pagamentos { get; set; }
            = new List<Pagamento>();
    }

}
