using Domain.Entities.Common;

namespace Domain.Entities.Financeiro
{
    public class EventoWebhookAsaas : EntityBase
    {
        public Guid EmpresaId { get; set; }

        // Identificador do evento enviado pelo Asaas.
        public string EventoId { get; set; } = string.Empty;

        // Identificador da cobrança no Asaas.
        public string PagamentoGatewayId { get; set; } = string.Empty;

        public string TipoEvento { get; set; } = string.Empty;

        // Payload original recebido.
        public string Payload { get; set; } = string.Empty;

        public StatusEventoWebhookAsaas Status { get; set; }
            = StatusEventoWebhookAsaas.Pendente;

        public int Tentativas { get; set; }

        public DateTime RecebidoEmUtc { get; set; }
            = DateTime.UtcNow;

        public DateTime ProximaTentativaEmUtc { get; set; }
            = DateTime.UtcNow;

        public DateTime? BloqueadoAteUtc { get; set; }

        public DateTime? ProcessadoEmUtc { get; set; }

        public string? UltimoErro { get; set; }
    }

    public enum StatusEventoWebhookAsaas
    {
        Pendente = 1,
        Processando = 2,
        Processado = 3,
        FalhaPermanente = 4
    }
}