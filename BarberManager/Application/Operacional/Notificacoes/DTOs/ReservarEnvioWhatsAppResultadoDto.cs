namespace Application.Operacional.Notificacoes.DTOs;

public sealed record ReservarEnvioWhatsAppResultadoDto(
    bool Reservado,
    Guid? EnvioId,
    int? Tentativa);