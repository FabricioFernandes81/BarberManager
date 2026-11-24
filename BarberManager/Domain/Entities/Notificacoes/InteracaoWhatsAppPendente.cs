
using Domain.Common.Enums;
using Domain.Entities.Common;

namespace Domain.Entities.Notificacoes;

public class InteracaoWhatsAppPendente : EntityBase
{
    public Guid EmpresaId { get; set; }

    public Guid ClienteId { get; set; }

    public string InstanceName { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public TipoInteracaoWhatsApp TipoInteracao { get; set; }

    public StatusInteracaoWhatsApp Status { get; set; }
        = StatusInteracaoWhatsApp.AguardandoSelecao;

    public Guid? AgendamentoSelecionadoId { get; set; }

    public string MessageIdOrigem { get; set; } = string.Empty;

    public DateTimeOffset CriadaEmUtc { get; set; }

    public DateTimeOffset ExpiraEmUtc { get; set; }

    public DateTimeOffset? ConcluidaEmUtc { get; set; }

    public ICollection<OpcaoInteracaoWhatsApp> Opcoes { get; set; }
        = new List<OpcaoInteracaoWhatsApp>();
}
