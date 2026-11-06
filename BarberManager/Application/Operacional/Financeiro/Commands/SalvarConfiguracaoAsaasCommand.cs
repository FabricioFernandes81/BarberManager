using Application.Common.Transactions;
using Domain.Entities.Financeiro;

namespace Application.Operacional.Financeiro.Commands
{
    public sealed record SalvarConfiguracaoAsaasCommand(
        string ApiTokenProtegido,
        string WebhooksTokenProtegido,
        AmbienteAsaas Ambiente,
        DateTime DataUltimaValidacao
    ) : ITransactionalCommand
    {
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}