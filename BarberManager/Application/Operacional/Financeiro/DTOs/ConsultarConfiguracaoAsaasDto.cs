namespace Application.Operacional.Financeiro.DTOs
{
    public sealed record ConsultarConfiguracaoAsaasDto(
        bool Configurado,
        bool Ativo,
        string? Ambiente,
        DateTime? DataUltimaValidacao);
}