namespace Application.Operacional.Agendamentos.Events;

public sealed record LembreteAgendamentoEvent(
    Guid EmpresaId,
    Guid AgendamentoId,
    Guid ClienteId,
    string ClienteNome,
    string? TelefoneCliente,
    string ProfissionalNome,
    string ServicoNome,
    DateTimeOffset DataHoraInicio,
    DateTimeOffset DataHoraFim,
    int AntecedenciaMinutos
);