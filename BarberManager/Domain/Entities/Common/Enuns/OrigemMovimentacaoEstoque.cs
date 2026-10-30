using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common.Enuns
{
    public enum OrigemMovimentacaoEstoque
    {
        CadastroInicial = 1,
        Venda = 2,
        Compra = 3,
        AjusteManual = 4,
        DevolucaoVenda = 5,
        DevolucaoCompra = 6,
        CancelamentoVenda = 7
    }
}
