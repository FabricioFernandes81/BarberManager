using Application.Common.Transactions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operacional.Agendamentos.Commands
{
    public sealed record CriarAgendamentoCommand(
     Guid ClienteId,
     Guid ProfissionalId,
     Guid ServicoId,
     DateTimeOffset DataHoraInicio,
     string? Observacoes) : ITransactionalCommand
    {
        [NotMapped]
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
