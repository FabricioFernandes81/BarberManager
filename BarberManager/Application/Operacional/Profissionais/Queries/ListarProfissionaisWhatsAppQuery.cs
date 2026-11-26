namespace Application.Operacional.Profissionais.Queries;

public sealed record ListarProfissionaisWhatsAppQuery(
    string Instance,
    Guid ServicoId
);