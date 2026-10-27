using Application.Common.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Financeiro.Commands
{
    public sealed record FecharCaixaCommand(
    decimal SaldoContado,
    string? ObservacaoFechamento
) : ITransactionalCommand
    {
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
