namespace Application.Operacional.Notificacoes.DTOs;

public sealed record RegistrarFalhaWhatsAppRequest(
    Guid OutboxEventId,
    string Erro);