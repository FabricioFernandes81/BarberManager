using Domain.Entities.Common;

public class ConfiguracaoWhatsApp : EntityBase
{
    public Guid EmpresaId { get; set; }

    public string InstanceName { get; set; } = string.Empty;

    public string BaseUrl { get; set; } = string.Empty;

    public string ApiKeyProtegida { get; set; } = string.Empty;

    public string? NumeroWhatsApp { get; set; }

    public bool Ativo { get; set; } = true;

    public DateTime? DataConexao { get; set; }
}