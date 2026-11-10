using Application.Common.Transactions;
using Application.Operacional.Financeiro.DTOs;

namespace Application.Operacional.Financeiro.Commands;

public sealed record RegistrarPagamentoGatewayCommand(
       Guid CobrancaId,
    string GatewayPagamentoId,
    string? InvoiceUrl,
    CobrancaPixCriadaEvent Evento
) : ITransactionalCommand
{
    public TransactionContext TransactionContext =>
        TransactionContext.Operacional;
}