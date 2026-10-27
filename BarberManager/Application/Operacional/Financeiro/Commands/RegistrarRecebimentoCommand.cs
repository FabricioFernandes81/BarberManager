using Application.Common.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Financeiro.Commands
{
    public sealed record RegistrarRecebimentoCommand(
     Guid ContaReceberId,
     Guid FormaPagamentoId,
     decimal Valor,
     string? Observacao
 ) : ITransactionalCommand
    {
       
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
