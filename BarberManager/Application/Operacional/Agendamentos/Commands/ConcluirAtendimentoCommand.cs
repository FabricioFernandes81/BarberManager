using Application.Common.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.Commands
{
    public sealed record ConcluirAtendimentoCommand(
         Guid AgendamentoId
     ) : ITransactionalCommand
    {
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
