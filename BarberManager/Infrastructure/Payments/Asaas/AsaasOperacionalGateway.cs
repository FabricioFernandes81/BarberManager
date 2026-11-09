using Application.Operacional.Financeiro.DTOs.Asaas;
using Application.Operacional.Financeiro.Interfaces;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Financeiro;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Infrastructure.Payments.Asaas;

public sealed class AsaasOperacionalGateway : IAsaasOperacionalGateway
{
    private readonly HttpClient _httpClient;

    private const string UserAgent = "BarberManager/1.0";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public AsaasOperacionalGateway(HttpClient httpClient)
    {
        _httpClient = httpClient;

        if (!_httpClient.DefaultRequestHeaders.UserAgent.Any())
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
        }

        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<ClienteAsaasOperacionalResultadoDto> CriarClienteAsync(
        string apiToken,
        AmbienteAsaas ambiente,
        CriarClienteAsaasOperacionalDto cliente,
        CancellationToken cancellationToken)
    {
        ValidarToken(apiToken);

        ArgumentNullException.ThrowIfNull(cliente);

        var requestBody = new
        {
            name = cliente.Nome,
            cpfCnpj = cliente.CpfCnpj,
            email = cliente.Email,
            phone = cliente.Telefone,
            externalReference = cliente.ExternalReference
        };

        using var request = CriarRequest(
            HttpMethod.Post,
            ambiente,
            "customers",
            apiToken);

        request.Content = JsonContent.Create(requestBody);

        using var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        await GarantirSucessoAsync(response, cancellationToken);

        var resultado = await response.Content
            .ReadFromJsonAsync<ClienteResponse>(
                JsonOptions,
                cancellationToken)
            ?? throw new InvalidOperationException(
                "O Asaas não retornou os dados do cliente.");

        if (string.IsNullOrWhiteSpace(resultado.Id))
        {
            throw new InvalidOperationException(
                "O Asaas retornou um cliente sem identificador.");
        }

        return new ClienteAsaasOperacionalResultadoDto(
            resultado.Id,
            resultado.Name,
            resultado.CpfCnpj,
            resultado.Email);
    }

    public async Task<CobrancaOperacionalResultadoDto> CriarCobrancaAsync(
        string apiToken,
        AmbienteAsaas ambiente,
        CriarCobrancaOperacionalDto cobranca,
        CancellationToken cancellationToken)
    {
        ValidarToken(apiToken);

        ArgumentNullException.ThrowIfNull(cobranca);

        var billingType = cobranca.MetodoPagamento switch
        {
            MetodoPagamento.Pix => "PIX",
            MetodoPagamento.CartaoCredito => "CREDIT_CARD",
            _ => throw new ArgumentOutOfRangeException(
                nameof(cobranca.MetodoPagamento),
                "Método de pagamento não suportado.")
        };

        object requestBody;

        if (cobranca.MetodoPagamento == MetodoPagamento.Pix)
        {
            requestBody = new
            {
                customer = cobranca.ClienteAsaasId,
                billingType,
                value = cobranca.Valor,
                dueDate = cobranca.Vencimento.ToString(
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture),
                description = cobranca.Descricao,
                externalReference = cobranca.ExternalReference
            };
        }
        else
        {
            var cartao = cobranca.CartaoCredito
                ?? throw new ArgumentException(
                    "Os dados do cartão são obrigatórios.");

            if (string.IsNullOrWhiteSpace(cobranca.RemoteIp))
            {
                throw new ArgumentException(
                    "O IP remoto é obrigatório para pagamento com cartão.");
            }

            requestBody = new
            {
                customer = cobranca.ClienteAsaasId,
                billingType,
                value = cobranca.Valor,
                dueDate = cobranca.Vencimento.ToString(
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture),
                description = cobranca.Descricao,
                externalReference = cobranca.ExternalReference,

                creditCard = new
                {
                    holderName = cartao.NomeTitular,
                    number = cartao.Numero,
                    expiryMonth = cartao.MesValidade,
                    expiryYear = cartao.AnoValidade,
                    ccv = cartao.Cvv
                },

                creditCardHolderInfo = new
                {
                    name = cartao.NomeTitular,
                    email = cartao.EmailTitular,
                    cpfCnpj = cartao.CpfCnpjTitular,
                    postalCode = cartao.CepTitular,
                    addressNumber = cartao.NumeroEnderecoTitular,
                    phone = cartao.TelefoneTitular
                },

                remoteIp = cobranca.RemoteIp
            };
        }

        using var request = CriarRequest(
            HttpMethod.Post,
            ambiente,
            "payments",
            apiToken);

        request.Content = JsonContent.Create(requestBody);

        using var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        await GarantirSucessoAsync(response, cancellationToken);

        var resultado = await response.Content
            .ReadFromJsonAsync<PagamentoResponse>(
                JsonOptions,
                cancellationToken)
            ?? throw new InvalidOperationException(
                "O Asaas não retornou os dados da cobrança.");

        if (string.IsNullOrWhiteSpace(resultado.Id))
        {
            throw new InvalidOperationException(
                "O Asaas retornou uma cobrança sem identificador.");
        }

        return new CobrancaOperacionalResultadoDto(
       resultado.Id,
       resultado.Status,
       resultado.BillingType,
       resultado.Value,
       resultado.InvoiceUrl,
       resultado.ExternalReference,
       resultado.Customer);
    }

    public async Task<PixOperacionalResultadoDto> ObterQrCodePixAsync(
        string apiToken,
        AmbienteAsaas ambiente,
        string pagamentoGatewayId,
        CancellationToken cancellationToken)
    {
        ValidarToken(apiToken);

        if (string.IsNullOrWhiteSpace(pagamentoGatewayId))
        {
            throw new ArgumentException(
                "O identificador da cobrança é obrigatório.",
                nameof(pagamentoGatewayId));
        }

        var id = Uri.EscapeDataString(pagamentoGatewayId);

        using var request = CriarRequest(
            HttpMethod.Get,
            ambiente,
            $"payments/{id}/pixQrCode",
            apiToken);

        using var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        await GarantirSucessoAsync(response, cancellationToken);

        var resultado = await response.Content
            .ReadFromJsonAsync<PixResponse>(
                JsonOptions,
                cancellationToken)
            ?? throw new InvalidOperationException(
                "O Asaas não retornou o QR Code Pix.");

        DateTime? expiracao = null;

        if (!string.IsNullOrWhiteSpace(resultado.ExpirationDate))
        {
            if (!DateTime.TryParse(
                    resultado.ExpirationDate,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind,
                    out var data))
            {
                throw new InvalidOperationException(
                    "O Asaas retornou uma data de expiração Pix inválida.");
            }

            expiracao = data;
        }

        return new PixOperacionalResultadoDto(
            resultado.EncodedImage,
            resultado.Payload,
            expiracao);
    }

    private static HttpRequestMessage CriarRequest(
        HttpMethod metodo,
        AmbienteAsaas ambiente,
        string endpoint,
        string apiToken)
    {
        var baseUrl = ambiente switch
        {
            AmbienteAsaas.Sandbox =>
                "https://api-sandbox.asaas.com/v3/",

            AmbienteAsaas.Producao =>
                "https://api.asaas.com/v3/",

            _ => throw new ArgumentOutOfRangeException(
                nameof(ambiente),
                "Ambiente Asaas inválido.")
        };

        var request = new HttpRequestMessage(
            metodo,
            new Uri(new Uri(baseUrl), endpoint));

        request.Headers.TryAddWithoutValidation(
            "access_token",
            apiToken.Trim());

        request.Headers.UserAgent.ParseAdd(UserAgent);

        request.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        return request;
    }

    private static void ValidarToken(string apiToken)
    {
        if (string.IsNullOrWhiteSpace(apiToken))
        {
            throw new ArgumentException(
                "O token da API Asaas é obrigatório.",
                nameof(apiToken));
        }
    }

    private static async Task GarantirSucessoAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
            return;

        var statusCode = response.StatusCode;

        // Não incluir o corpo da resposta em exceções genéricas:
        // respostas de cartão podem conter dados sensíveis.
        throw new HttpRequestException(
            $"Falha na comunicação com o Asaas. HTTP {(int)statusCode}.",
            null,
            statusCode);
    }
    public async Task<IReadOnlyList<CobrancaOperacionalResultadoDto>>
    ListarCobrancasPorReferenciaAsync(
        string apiToken,
        AmbienteAsaas ambiente,
        string externalReference,
        CancellationToken cancellationToken)
    {
        ValidarToken(apiToken);

        if (string.IsNullOrWhiteSpace(externalReference))
        {
            throw new ArgumentException(
                "A referência externa é obrigatória.",
                nameof(externalReference));
        }

        const int limite = 100;

        var offset = 0;

        var cobrancas = new List<CobrancaOperacionalResultadoDto>();

        var referencia = Uri.EscapeDataString(
            externalReference.Trim());

        bool hasMore;

        do
        {
            cancellationToken.ThrowIfCancellationRequested();

            var endpoint =
                $"payments?externalReference={referencia}" +
                $"&offset={offset}&limit={limite}";

            using var request = CriarRequest(
                HttpMethod.Get,
                ambiente,
                endpoint,
                apiToken);

            using var response = await _httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            await GarantirSucessoAsync(
                response,
                cancellationToken);

            var resultado = await response.Content
                .ReadFromJsonAsync<ListarCobrancasResponse>(
                    JsonOptions,
                    cancellationToken)
                ?? throw new InvalidOperationException(
                    "O Asaas não retornou os dados da consulta de cobranças.");

            if (resultado.Data is null)
            {
                throw new InvalidOperationException(
                    "O Asaas retornou uma lista de cobranças inválida.");
            }

            if (resultado.Data.Count == 0 && resultado.HasMore)
            {
                throw new InvalidOperationException(
                    "O Asaas informou que existem outras páginas, " +
                    "mas não retornou registros.");
            }

            foreach (var pagamento in resultado.Data)
            {
                if (string.IsNullOrWhiteSpace(pagamento.Id))
                {
                    throw new InvalidOperationException(
                        "O Asaas retornou uma cobrança sem identificador.");
                }

                cobrancas.Add(
                    new CobrancaOperacionalResultadoDto(
                        pagamento.Id,
                        pagamento.Status,
                        pagamento.BillingType,
                        pagamento.Value,
                        pagamento.InvoiceUrl,
                        pagamento.ExternalReference,
                        pagamento.Customer));
            }

            offset += resultado.Data.Count;

            hasMore = resultado.HasMore;

        } while (hasMore);

        return cobrancas;
    }


    private sealed class ClienteResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? CpfCnpj { get; set; }
        public string? Email { get; set; }
    }

    private sealed class PagamentoResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string BillingType { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string? InvoiceUrl { get; set; }
        public string? ExternalReference { get; set; }

        public string? Customer { get; set; }
    }

    private sealed class PixResponse
    {
        public string? EncodedImage { get; set; }
        public string? Payload { get; set; }
        public string? ExpirationDate { get; set; }
    }

    private sealed class ListarCobrancasResponse
    {
        public List<PagamentoResponse> Data { get; set; } = [];

        public int TotalCount { get; set; }

        public bool HasMore { get; set; }
    }
}