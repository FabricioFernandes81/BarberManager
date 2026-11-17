namespace Application.Operacional.Notificacoes.DTOs;

public sealed record ProcessarRespostaWhatsAppRequest(
    string Instance,
    string Telefone,
    string Mensagem,
    string MessageId);