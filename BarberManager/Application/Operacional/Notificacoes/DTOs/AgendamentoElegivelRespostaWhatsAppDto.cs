public sealed record AgendamentoElegivelRespostaWhatsAppDto(
    Guid AgendamentoId,
    DateTimeOffset DataHoraInicio,
    string Servicos,
    string Profissional);