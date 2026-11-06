using Domain.Entities.Financeiro;

namespace Application.Operacional.Financeiro.DTOs
{
    public sealed record ConfigurarCredenciaisAsaasRequest(
        string ApiToken,
        string WebhooksToken,
        AmbienteAsaas Ambiente);
}