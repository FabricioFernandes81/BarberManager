namespace Application.Operacional.Financeiro.DTOs.Asaas;

public sealed record CobrancaOperacionalResultadoDto(
    string Id,
    string Status,
    string BillingType,
    decimal Valor,
    string? InvoiceUrl,
    string? ExternalReference,
    string? Customer);