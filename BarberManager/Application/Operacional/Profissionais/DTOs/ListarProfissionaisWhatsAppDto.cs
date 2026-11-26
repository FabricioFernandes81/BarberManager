namespace Application.Operacional.Profissionais.DTOs;

public sealed record ListarProfissionaisWhatsAppDto(
    IReadOnlyCollection<ProfissionalAgendamentoWhatsAppDto>
        Profissionais
);