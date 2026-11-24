
using Domain.Entities.Common;

namespace Domain.Entities.Notificacoes;

public class OpcaoInteracaoWhatsApp : EntityBase
{
    public Guid InteracaoWhatsAppPendenteId { get; set; }

    public Guid AgendamentoId { get; set; }

    public int Ordem { get; set; }

    public DateTimeOffset DataHoraInicioSnapshot { get; set; }

    public string ServicosSnapshot { get; set; } = string.Empty;

    public string ProfissionalSnapshot { get; set; } = string.Empty;

    public InteracaoWhatsAppPendente InteracaoWhatsAppPendente
    { get; set; } = null!;
}
