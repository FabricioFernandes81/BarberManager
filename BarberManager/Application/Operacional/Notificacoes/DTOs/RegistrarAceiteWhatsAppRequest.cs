namespace Application.Operacional.Notificacoes.DTOs;

public sealed record RegistrarAceiteWhatsAppRequest(
    Guid OutboxEventId,
    string? ProviderMessageId);