namespace Application.Operacional.Profissionais.DTOs;

public sealed record ProfissionalAgendamentoWhatsAppDto(
    Guid Id,
    string Nome,
    string? Especialidade,
    string? Foto,
    Guid ServicoId,
    decimal ValorServico,
    int DuracaoMinutos
);