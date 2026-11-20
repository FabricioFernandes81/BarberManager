namespace Application.Operacional.Notificacoes.DTOs;

public sealed record EnviarMensagemWhatsAppRequest(
    Guid OutboxEventId,
    string Telefone,
    string Conteudo);