namespace Application.Interfaces.Notificacoes;

public interface IEvolutionInstanceClient
{
    string BaseUrl { get; }

    Task<CriarInstanciaEvolutionResultadoDto> CriarInstanciaAsync(
        string instanceName,
        string instanceToken,
        CancellationToken cancellationToken);
}

public sealed record CriarInstanciaEvolutionResultadoDto(
    string InstanceName,
    string Status,
    string? QrCode,
    string? QrCodeBase64);