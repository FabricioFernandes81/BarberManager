namespace Application.Operacional.Servicos.DTOs;

public sealed record ServicoAgendamentoWhatsAppDto(
    Guid Id,
    string Nome,
    string? Descricao,
    decimal Valor,
    int DuracaoMinutos,
    int IntervaloMinutos,
    string? Categoria
);