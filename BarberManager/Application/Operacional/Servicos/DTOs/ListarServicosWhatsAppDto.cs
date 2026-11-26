namespace Application.Operacional.Servicos.DTOs;

public sealed record ListarServicosWhatsAppDto(
    IReadOnlyCollection<ServicoAgendamentoWhatsAppDto> Servicos
);