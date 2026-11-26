namespace Application.Operacional.Servicos.Queries;

public sealed record ListarServicosWhatsAppQuery(
    string Instance,
    string? SearchTerm = null
);