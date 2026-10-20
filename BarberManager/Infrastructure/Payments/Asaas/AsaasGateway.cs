using Application.Interfaces.Empresas.Pagamentos;
using Application.Saas.Pagamentos.DTOs;
using Domain.Entities.Common.Enuns;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;

namespace Infrastructure.Payments.Asaas;

public sealed class AsaasGateway : IAsaasGateway
{
    private readonly HttpClient _httpClient;

    public AsaasGateway(
        HttpClient httpClient,
        IOptions<AsaasOptions> options)
    {
        _httpClient = httpClient;

        var config = options.Value;

        if (string.IsNullOrWhiteSpace(config.BaseUrl))
            throw new InvalidOperationException(
                "Asaas:BaseUrl não foi configurada.");

        if (string.IsNullOrWhiteSpace(config.ApiKey))
            throw new InvalidOperationException(
                "Asaas:ApiKey não foi configurada.");

        _httpClient.BaseAddress = new Uri(config.BaseUrl);
        _httpClient.DefaultRequestHeaders.Remove("access_token");
        _httpClient.DefaultRequestHeaders.Add(
            "access_token",
            config.ApiKey);

        // Identifica a aplicação que está consumindo a API.
        if (!_httpClient.DefaultRequestHeaders.UserAgent.Any())
        {
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
                "BarberManager/1.0");
        }
    }

    public async Task<AsaasClienteResultadoDto> CriarClienteAsync(
        AsaasCriarClienteDto cliente,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            name = cliente.Nome,
            cpfCnpj = cliente.CpfCnpj,
            email = cliente.Email,
            phone = cliente.Telefone,
            externalReference = cliente.ExternalReference
        };

        using var response = await _httpClient.PostAsJsonAsync(
            "customers",
            request,
            cancellationToken);

        await GarantirSucessoAsync(response, cancellationToken);

        var resultado =
            await response.Content.ReadFromJsonAsync<ClienteResponse>(
                cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException(
                "O Asaas não retornou os dados do cliente.");

        return new AsaasClienteResultadoDto(
            resultado.Id,
            resultado.Name,
            resultado.Email);
    }

    public async Task<AsaasCobrancaResultadoDto> CriarCobrancaAsync(
        AsaasCriarCobrancaDto cobranca,
        CancellationToken cancellationToken = default)
    {
        var billingType = cobranca.MetodoPagamento switch
        {
            MetodoPagamento.Pix => "PIX",
            MetodoPagamento.CartaoCredito => "CREDIT_CARD",
            _ => throw new ArgumentOutOfRangeException(
                nameof(cobranca.MetodoPagamento),
                "Método de pagamento não suportado.")
        };

        object request;

        if (cobranca.MetodoPagamento == MetodoPagamento.Pix)
        {
            request = new
            {
                customer = cobranca.ClienteAsaasId,
                billingType,
                value = cobranca.Valor,
                dueDate = cobranca.Vencimento.ToString("yyyy-MM-dd"),
                description = cobranca.Descricao,
                externalReference = cobranca.TransacaoId.ToString()
            };
        }
        else
        {
            var cartao = cobranca.CartaoCredito
                ?? throw new ArgumentException(
                    "Os dados do cartão de crédito são obrigatórios.");

            if (string.IsNullOrWhiteSpace(cobranca.RemoteIp))
                throw new ArgumentException(
                    "O IP remoto é obrigatório para pagamento com cartão.");

            request = new
            {
                customer = cobranca.ClienteAsaasId,
                billingType,
                value = cobranca.Valor,
                dueDate = cobranca.Vencimento.ToString("yyyy-MM-dd"),
                description = cobranca.Descricao,
                externalReference = cobranca.TransacaoId.ToString(),

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

        using var response = await _httpClient.PostAsJsonAsync(
            "payments",
            request,
            cancellationToken);

        await GarantirSucessoAsync(response, cancellationToken);

        var resultado =
            await response.Content.ReadFromJsonAsync<PagamentoResponse>(
                cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException(
                "O Asaas não retornou os dados da cobrança.");

        return new AsaasCobrancaResultadoDto(
            resultado.Id,
            resultado.Status,
            resultado.BillingType,
            resultado.Value,
            resultado.InvoiceUrl,
            resultado.ExternalReference,
            resultado.CreditCard?.CreditCardBrand,
            resultado.CreditCard?.CreditCardNumber);
    }

    public async Task<AsaasPixResultadoDto> ObterQrCodePixAsync(
    string referenciaGateway,
    CancellationToken cancellationToken = default)
    {
        var referencia = Uri.EscapeDataString(referenciaGateway);

        using var response = await _httpClient.GetAsync(
            $"payments/{referencia}/pixQrCode",
            cancellationToken);

        await GarantirSucessoAsync(response, cancellationToken);

        var json = await response.Content.ReadAsStringAsync(
            cancellationToken);

        var resultado = JsonSerializer.Deserialize<AsaasPixQrCodeResponse>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (resultado is null)
        {
            throw new InvalidOperationException(
                "O Asaas retornou uma resposta vazia para o QR Code Pix.");
        }

        DateTime? expiracao = null;

        if (!string.IsNullOrWhiteSpace(resultado.ExpirationDate))
        {
            var formatos = new[]
            {
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd'T'HH:mm:ss",
            "yyyy-MM-dd'T'HH:mm:ss.FFFFFFFK"
        };

            var convertido = DateTime.TryParseExact(
                resultado.ExpirationDate,
                formatos,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var data);

            if (!convertido)
            {
                convertido = DateTime.TryParse(
                    resultado.ExpirationDate,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out data);
            }

            if (!convertido)
            {
                throw new InvalidOperationException(
                    "O Asaas retornou uma data de expiração Pix inválida.");
            }

            expiracao = data;
        }

        return new AsaasPixResultadoDto(
          resultado.EncodedImage,
          resultado.Payload,
          expiracao);
    }

    private static async Task GarantirSucessoAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
            return;

        var corpo = await response.Content.ReadAsStringAsync(
            cancellationToken);

        // Evite registrar dados sensíveis do request.
        throw new HttpRequestException(
            $"Falha na API Asaas. HTTP {(int)response.StatusCode}. " +
            $"Resposta: {corpo}",
            inner: null,
            response.StatusCode);
    }

    public async Task<AsaasCobrancaDto?> ObterCobrancaPorReferenciaExternaAsync(string referenciaExterna, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(referenciaExterna))
        {
            throw new ArgumentException(
                "A referência externa é obrigatória.",
                nameof(referenciaExterna));
        }

        var referencia = referenciaExterna.Trim();
        const int limite = 100;
        var offset = 0;

        AsaasCobrancaDto? cobrancaEncontrada = null;

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var referenciaEscapada = Uri.EscapeDataString(referencia);

            var endpoint =
                $"payments?offset={offset}" +
                $"&limit={limite}" +
                $"&externalReference={referenciaEscapada}";

            using var response = await _httpClient.GetAsync(
                endpoint,
                cancellationToken);

            await GarantirSucessoAsync(response, cancellationToken);

            var resultado =
                await response.Content.ReadFromJsonAsync<PagamentosListaResponse>(
                    cancellationToken: cancellationToken)
                ?? throw new InvalidOperationException(
                    "O Asaas retornou uma resposta vazia ao consultar cobranças.");

            foreach (var pagamento in resultado.Data)
            {
                // Confirma a referência exata antes de aceitar o resultado.
                if (!string.Equals(
                        pagamento.ExternalReference,
                        referencia,
                        StringComparison.Ordinal))
                {
                    continue;
                }

                if (cobrancaEncontrada is not null)
                {
                    throw new InvalidOperationException(
                        "Foram encontradas múltiplas cobranças para a mesma " +
                        "referência externa. É necessária uma reconciliação manual.");
                }

                cobrancaEncontrada = new AsaasCobrancaDto(
                    pagamento.Id,
                    pagamento.Status,
                    pagamento.BillingType,
                    pagamento.Value,
                    pagamento.InvoiceUrl,
                    pagamento.ExternalReference,
                    pagamento.CreditCard?.CreditCardBrand,
                    pagamento.CreditCard?.CreditCardNumber);
            }

            if (!resultado.HasMore)
                break;

            offset += limite;
        }

        return cobrancaEncontrada;
    }

    private sealed class ClienteResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
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
        public CartaoResponse? CreditCard { get; set; }
    }

    private sealed class CartaoResponse
    {
        public string? CreditCardBrand { get; set; }
        public string? CreditCardNumber { get; set; }
    }


    private sealed class AsaasPixQrCodeResponse
    {
        public string? EncodedImage { get; set; }

        public string? Payload { get; set; }

        public string? ExpirationDate { get; set; }
    }
    /*private sealed class PixResponse
    {
        public string EncodedImage { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTime? ExpirationDate { get; set; }
    }*/

    private sealed class PagamentosListaResponse
    {
        public List<PagamentoResponse> Data { get; set; } = new();

        public bool HasMore { get; set; }
    }
}