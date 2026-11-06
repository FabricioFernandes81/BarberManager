using Application.Interfaces.Financeiro;
using Domain.Entities.Financeiro;
using System.Net;
using System.Net.Http.Headers;

namespace Infrastructure.Payments.Asaas
{
    public sealed class AsaasOperacionalClient
        : IAsaasOperacionalClient
    {
        private readonly HttpClient _httpClient;

        private const string UserAgent =
            "BarberManager/1.0";

        public AsaasOperacionalClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ValidarCredencialAsync(
            string apiToken,
            AmbienteAsaas ambiente,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(apiToken))
            {
                throw new ArgumentException(
                    "O token da API deve ser informado.",
                    nameof(apiToken));
            }

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

            using var request = new HttpRequestMessage(
                HttpMethod.Get,
                new Uri(new Uri(baseUrl), "myAccount/status/"));

            // Credencial exclusiva desta requisição.
            request.Headers.TryAddWithoutValidation(
                "access_token",
                apiToken.Trim());

            request.Headers.UserAgent.ParseAdd(UserAgent);

            request.Headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue(
                    "application/json"));

            using var response = await _httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }

            response.EnsureSuccessStatusCode();

            return true;
        }
    }
}