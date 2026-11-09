namespace Application.Operacional.Financeiro.DTOs.Asaas;

public sealed record ReservaCobrancaPixDto(
    Guid CobrancaId,
    Guid ContaReceberId,
    string ExternalReference,
    decimal Valor,
    string Status,
    string? GatewayPagamentoId,
    bool JaExistente
);