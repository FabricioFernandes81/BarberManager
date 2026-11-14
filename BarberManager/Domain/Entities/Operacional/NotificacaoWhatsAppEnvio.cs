using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;

namespace Domain.Entities.Notificacoes;

public class NotificacaoWhatsAppEnvio : EntityBase
{
    public Guid EmpresaId { get; set; }

    public Guid OutboxEventId { get; set; }

    // SHA-256 do número normalizado + conteúdo da mensagem.
    public string HashConteudo { get; set; } = string.Empty;

    public StatusEnvioWhatsApp Status { get; set; }
        = StatusEnvioWhatsApp.Pendente;

    public int Tentativas { get; set; }

    public DateTime? DataInicioProcessamentoUtc { get; set; }

    public DateTime? LeaseExpiraEmUtc { get; set; }

    public DateTime? DataUltimaTentativaUtc { get; set; }

    public DateTime? DataAceiteUtc { get; set; }

    public string? ProviderMessageId { get; set; }

    public string? UltimoErro { get; set; }
}