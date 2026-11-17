namespace Application.Operacional.Notificacoes.DTOs;

public sealed record ProcessarRespostaWhatsAppResultadoDto(
    bool Processado,
    string Status,
    string Mensagem,
    Guid? AgendamentoId = null);