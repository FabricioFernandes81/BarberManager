using Application.Common.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.Commands
{
    public sealed record GerarFaturaCommand(
     Guid AssinaturaId,
     DateTime PeriodoInicio,
     DateTime PeriodoFim,
     DateTime Vencimento
 ) : ITransactionalCommand
    {
        public TransactionContext TransactionContext =>
            TransactionContext.SaaS;
    }
}
