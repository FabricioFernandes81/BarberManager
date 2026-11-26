namespace Application.Operacional.Agendamentos.Queries;

public sealed record ListarDisponibilidadeWhatsAppQuery(
    string Instance,
    Guid ProfissionalId,
    Guid ServicoId,
    DateOnly Data
);