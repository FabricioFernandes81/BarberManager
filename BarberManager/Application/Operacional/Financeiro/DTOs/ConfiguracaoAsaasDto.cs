using Domain.Entities.Financeiro;

namespace Application.Operacional.Financeiro.DTOs
{
    public sealed record ConfiguracaoAsaasDto(
        bool Configurado,
        bool Ativo,
        AmbienteAsaas? Ambiente,
        DateTime? DataUltimaValidacao);
}