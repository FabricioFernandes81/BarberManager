using Application.Interfaces.Notificacoes;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Infrastructure.Notifications;

public sealed class EvolutionInstanceClient : IEvolutionInstanceClient
{
    private readonly HttpClient _httpClient;
    private readonly EvolutionOptions _options;

    public EvolutionInstanceClient(
        HttpClient httpClient,
        IOptions<EvolutionOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<CriarInstanciaEvolutionResultadoDto>
        CriarInstanciaAsync(
            string instanceName,
            string instanceToken,
            CancellationToken cancellationToken)
    {
        ValidarConfiguracao();

        if (string.IsNullOrWhiteSpace(instanceName))
            throw new ArgumentException("Nome da instância não informado.");

        if (string.IsNullOrWhiteSpace(instanceToken))
            throw new ArgumentException("Token da instância não informado.");

        var url = new Uri(
            $"{_options.BaseUrl.TrimEnd('/')}/instance/create");

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            url);

        request.Headers.TryAddWithoutValidation(
            "apikey",
            _options.GlobalApiKey);

        request.Content = JsonContent.Create(new
        {
            instanceName,
            token = instanceToken,
            qrcode = true
        });

        using var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        var conteudo = await response.Content.ReadAsStringAsync(
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Falha ao criar instância Evolution. " +
                $"HTTP {(int)response.StatusCode}.",
            null,
            response.StatusCode);
        }

        using var documento = JsonDocument.Parse(conteudo);

        var root = documento.RootElement;

        var nomeRetornado = instanceName;

        if (root.TryGetProperty("instance", out var instanceElement))
        {
            if (instanceElement.TryGetProperty(
                    "instanceName",
                    out var instanceNameElement))
            {
                nomeRetornado =
                    instanceNameElement.GetString() ?? instanceName;
            }
        }

        string? qrCode = null;
        string? qrCodeBase64 = null;

        if (root.TryGetProperty("qrcode", out var qrElement))
        {
            if (qrElement.TryGetProperty(
                    "code",
                    out var codeElement))
            {
                qrCode = codeElement.GetString();
            }

            if (qrElement.TryGetProperty(
                    "base64",
                    out var base64Element))
            {
                qrCodeBase64 = base64Element.GetString();
            }
        }

        return new CriarInstanciaEvolutionResultadoDto(
            nomeRetornado,
            "Criada",
            qrCode,
            qrCodeBase64);
    }

    private void ValidarConfiguracao()
    {
        if (string.IsNullOrWhiteSpace(_options.BaseUrl))
            throw new InvalidOperationException(
                "Evolution:BaseUrl não configurada.");

        if (string.IsNullOrWhiteSpace(_options.GlobalApiKey))
            throw new InvalidOperationException(
                "Evolution:GlobalApiKey não configurada.");

        if (!Uri.TryCreate(
                _options.BaseUrl,
                UriKind.Absolute,
                out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttps &&
             uri.Scheme != Uri.UriSchemeHttp))
        {
            throw new InvalidOperationException(
                "Evolution:BaseUrl inválida.");
        }
    }
}