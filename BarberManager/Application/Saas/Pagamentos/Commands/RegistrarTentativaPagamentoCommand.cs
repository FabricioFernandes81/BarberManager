using Application.Common.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.Commands
{
    public sealed record RegistrarTentativaPagamentoCommand(
    Guid FaturaId,
    string Provedor,
    string ChaveIdempotencia)
    : ITransactionalCommand
    {
        public TransactionContext TransactionContext =>
            TransactionContext.SaaS;
    }
}
