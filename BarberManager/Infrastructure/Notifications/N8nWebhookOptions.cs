using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Notifications
{
    public class N8nWebhookOptions
    {
        public const string SectionName = "N8n";

        public string ProprietarioWebhookUrl { get; set; } = string.Empty;

        public string AtendimentoWebhookUrl { get; set; } = string.Empty;

        public string WebhookSecret { get; set; } = string.Empty;

        public int PollIntervalSeconds { get; set; } = 10;

        public int BatchSize { get; set; } = 20;

        public int MaxAttempts { get; set; } = 8;

        public int LeaseDurationSeconds { get; set; } = 120;

        public int HttpTimeoutSeconds { get; set; } = 15;
    }
}
