namespace Application.Operacional.Financeiro.DTOs.Asaas;

public sealed record CriarCobrancaPixResponse(
    Guid CobrancaId,
    Guid ContaReceberId,
    string GatewayPagamentoId,
    decimal Valor,
    string Status,
    string PayloadPix,
    string ImagemQrCode,
    DateTime? ExpiracaoPix
);