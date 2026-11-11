using Domain.Entities.Common.Enuns;

namespace Domain.Entities.Common;

public class OutboxOperacionalMessage : EntityBase
{
    public Guid? EmpresaId { get; set; }

    public Guid? AggregateId { get; set; }

    public string EventType { get; set; } = string.Empty;

    public string TargetWorkflow { get; set; } = string.Empty;

    public string Payload { get; set; } = string.Empty;

    public OutboxMessageStatus Status { get; set; }
        = OutboxMessageStatus.Pendente;

    public int Attempts { get; set; }

    public DateTime OccurredAtUtc { get; set; } = DateTime.UtcNow;

    public DateTime NextAttemptAtUtc { get; set; } = DateTime.UtcNow;

    public DateTime? LockedUntilUtc { get; set; }

    public DateTime? ProcessedAtUtc { get; set; }

    public string? LastError { get; set; }
}