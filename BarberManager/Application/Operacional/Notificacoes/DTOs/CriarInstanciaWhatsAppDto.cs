namespace Application.Operacional.Notificacoes.DTOs;

public sealed record CriarInstanciaWhatsAppDto(
    Guid ConfiguracaoId,
    string InstanceName,
    string Status,
    string? QrCode,
    string? QrCodeBase64
);