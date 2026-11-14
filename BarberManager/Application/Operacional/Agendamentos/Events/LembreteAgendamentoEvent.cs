namespace Application.Operacional.Agendamentos.Events;

public sealed record LembreteAgendamentoEvent(
    Guid EventId,
    Guid EmpresaId,
    Guid AgendamentoId,
    Guid ClienteId,
    string ClienteNome,
    string? TelefoneCliente,
    string ProfissionalNome,
    string ServicoNome,
    DateTimeOffset DataHoraInicio,
    DateTimeOffset DataHoraFim,
    int AntecedenciaMinutos,
    bool Notificavel,
    string? MotivoNaoEnvio
);