using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public class OutboxMessage : EntityBase
    {
        public string EventType { get; set; } = string.Empty;
        public string TargetWorkflow { get; set; } = string.Empty;

        public string Payload { get; set; } = string.Empty;

        public OutboxMessageStatus Status { get; set; }
        = OutboxMessageStatus.Pendente;

        public int Attempts { get; set; }

        public DateTime OccurredAtUtc { get; set; }
            = DateTime.UtcNow;

        public DateTime NextAttemptAtUtc { get; set; }
            = DateTime.UtcNow;

        public DateTime? LockedUntilUtc { get; set; }

        public DateTime? ProcessedAtUtc { get; set; }

        public string? LastError { get; set; }
    }


    public enum OutboxMessageStatus
    {
        Pendente = 0,
        Processando = 1,
        Entregue = 2,
        FalhaPermanente = 3
    }
}
