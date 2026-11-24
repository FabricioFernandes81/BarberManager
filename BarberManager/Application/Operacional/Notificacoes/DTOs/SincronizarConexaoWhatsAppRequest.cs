namespace Application.Operacional.Notificacoes.DTOs;

public sealed record SincronizarConexaoWhatsAppRequest(
    string Instance,
    string State,
    string? StatusReason,
    string? Wuid);