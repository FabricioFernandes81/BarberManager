namespace Application.Operacional.Notificacoes.DTOs;

public sealed record ReservarEnvioWhatsAppRequest(
    Guid OutboxEventId,
    string Telefone,
    string Conteudo);