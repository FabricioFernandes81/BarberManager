
using Domain.Entities.Common;

namespace Domain.Entities.Notificacoes;

public class RespostaWhatsAppProcessada : EntityBase
{
    public Guid EmpresaId { get; set; }

    public string InstanceName { get; set; } = string.Empty;

    public string ProviderMessageId { get; set; } = string.Empty;

    public string Telefone { get; set; } = string.Empty;

    public string Comando { get; set; } = string.Empty;

    public Guid? AgendamentoId { get; set; }

    public string Resultado { get; set; } = string.Empty;

    public DateTime DataProcessamentoUtc { get; set; }
}
