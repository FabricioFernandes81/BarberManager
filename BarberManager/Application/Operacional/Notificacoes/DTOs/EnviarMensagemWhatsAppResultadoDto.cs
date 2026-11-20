namespace Application.Operacional.Notificacoes.DTOs;

public sealed record EnviarMensagemWhatsAppResultadoDto(
    string? ProviderMessageId,
    string? Status);