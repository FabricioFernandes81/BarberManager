using Application.Common.Transactions;
using Domain.Entities.Common.Enuns;
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
       string ChaveIdempotencia,
       MetodoPagamento MetodoPagamento)
       : ITransactionalCommand
    {
        public TransactionContext TransactionContext =>
            TransactionContext.SaaS;
    }
}
