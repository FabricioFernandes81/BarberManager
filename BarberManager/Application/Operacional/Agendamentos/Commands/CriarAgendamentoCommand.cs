using Application.Common.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.Commands
{
    public sealed record CriarAgendamentoCommand(
     Guid ClienteId,
     Guid ProfissionalId,
     Guid ServicoId,
     DateTimeOffset DataHoraInicio,
     string? Observacoes) : ITransactionalCommand
    {
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
