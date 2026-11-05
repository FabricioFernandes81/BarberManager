
using Domain.Entities.Common;

namespace Domain.Entities.Financeiro
{
    public class ConfiguracaoAssasClient : EntityBase
    {
        public Guid EmpresaId { get; set; }

        // Credenciais armazenadas criptografadas.
        public string ApiTokenProtegido { get; set; } = string.Empty;

        public string WebhooksTokenProtegido { get; set; } = string.Empty;

        // Ambiente utilizado pela empresa.
        public AmbienteAsaas Ambiente { get; set; }
            = AmbienteAsaas.Sandbox;

        // Indica se a integração está habilitada.
        public bool Ativo { get; set; }

        // Última validação bem-sucedida das credenciais.
        public DateTime? DataUltimaValidacao { get; set; }
    }

    public enum AmbienteAsaas
    {
        Sandbox = 1,
        Producao = 2
    }
}
