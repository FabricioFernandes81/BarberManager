using Application.Interfaces.Notificacoes;
using System.Net.Http.Json;
using System.Text.Json;

namespace Infrastructure.Notifications;

public sealed class EvolutionApiClient : IEvolutionApiClient
{
    private readonly HttpClient _httpClient;

    public EvolutionApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResultadoEnvioEvolutionDto> EnviarTextoAsync(
        string baseUrl,
        string instanceName,
        string apiKey,
        string numero,
        string texto,
        CancellationToken cancellationToken)
    {
        ValidarParametros(
            baseUrl,
            instanceName,
            apiKey,
            numero,
            texto);

        var endereco = new Uri(
            $"{baseUrl.TrimEnd('/')}/message/sendText/" +
            Uri.EscapeDataString(instanceName));

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            endereco);

        request.Headers.TryAddWithoutValidation(
            "apikey",
            apiKey);

        request.Content = JsonContent.Create(new
        {
            number = numero,
            text = texto
        });

        using var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Evolution API respondeu com HTTP " +
                $"{(int)response.StatusCode}.",
                null,
                response.StatusCode);
        }

        await using var stream = await response.Content
            .ReadAsStreamAsync(cancellationToken);

        using var documento = await JsonDocument.ParseAsync(
            stream,
            cancellationToken: cancellationToken);

        var raiz = documento.RootElement;

        string? messageId = null;
        string? status = null;

        if (raiz.TryGetProperty("key", out var key) &&
            key.TryGetProperty("id", out var id))
        {
            messageId = id.GetString();
        }

        if (raiz.TryGetProperty("status", out var statusElement))
        {
            status = statusElement.GetString();
        }

        return new ResultadoEnvioEvolutionDto(
            messageId,
            status);
    }

    private static void ValidarParametros(
        string baseUrl,
        string instanceName,
        string apiKey,
        string numero,
        string texto)
    {
        if (!Uri.TryCreate(
                baseUrl,
                UriKind.Absolute,
                out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttps &&
             uri.Scheme != Uri.UriSchemeHttp))
        {
            throw new ArgumentException(
                "A URL da Evolution API é inválida.");
        }

        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Instância não informada.");

        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException("API Key não informada.");

        if (string.IsNullOrWhiteSpace(numero))
            throw new ArgumentException("Número do destinatário não informado.");

        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentException("Texto da mensagem não informado.");
    }
}