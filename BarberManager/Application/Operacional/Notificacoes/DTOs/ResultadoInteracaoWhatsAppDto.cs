namespace Application.Operacional.Notificacoes.DTOs;

public sealed record ResultadoInteracaoWhatsAppDto(
    bool Processado,
    string Status,
    string Mensagem,
    Guid? InteracaoId = null,
    Guid? AgendamentoId = null,
    int? OrdemSelecionada = null,
    DateTimeOffset? DataHoraInicio = null,
    string? Servicos = null,
    string? Profissional = null);