using Application.Common.Transactions;

namespace Application.Operacional.Financeiro.Commands
{
    public sealed record RegistrarClienteAsaasOperacionalCommand(
        Guid EmpresaId,
        Guid ClienteId,
        string ClienteAsaasId
    ) : ITransactionalCommand
    {
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}