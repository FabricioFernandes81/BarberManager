namespace Application.Interfaces.Notificacoes;

public interface IEvolutionApiClient
{
    Task<ResultadoEnvioEvolutionDto> EnviarTextoAsync(
        string baseUrl,
        string instanceName,
        string apiKey,
        string numero,
        string texto,
        CancellationToken cancellationToken);
}

public sealed record ResultadoEnvioEvolutionDto(
    string? MessageId,
    string? Status);