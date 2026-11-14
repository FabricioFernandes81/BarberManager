namespace Application.Operacional.Notificacoes.DTOs;

public sealed record RegistrarResultadoIndeterminadoWhatsAppRequest(
    Guid OutboxEventId,
    string Motivo);