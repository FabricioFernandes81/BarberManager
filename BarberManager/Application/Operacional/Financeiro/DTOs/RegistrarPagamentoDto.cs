using Domain.Entities.Common.Enuns;

namespace Application.Operacional.Financeiro.DTOs;

public sealed record RegistrarPagamentoDto(
    Guid PagamentoId,
    Guid ContaPagarId,
    decimal ValorPagamento,
    decimal TotalPago,
    decimal SaldoRestante,
    StatusContas Status,
    DateTime DataPagamento
);