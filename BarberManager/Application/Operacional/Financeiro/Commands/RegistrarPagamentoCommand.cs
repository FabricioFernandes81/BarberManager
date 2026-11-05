using Application.Common.Transactions;

namespace Application.Operacional.Financeiro.Commands;

public sealed record RegistrarPagamentoCommand(
    Guid ContaPagarId,
    Guid FormaPagamentoId,
    decimal Valor,
    string? Observacao,
    string ChaveIdempotencia
) : ITransactionalCommand
{
    public TransactionContext TransactionContext =>
        TransactionContext.Operacional;
}