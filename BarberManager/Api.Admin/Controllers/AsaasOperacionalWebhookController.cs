
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Application.Dispatcher;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.Webhooks.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers.Webhooks
{
    [ApiController]
    [Route("api/webhooks/asaas")]
    [AllowAnonymous]
    public sealed class AsaasOperacionalWebhookController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        private readonly IRepositorioConfiguracaoAssasClient _repositorioConfiguracao;
        private readonly IProtecaoCredenciaisAsaas _protecaoCredenciais;

        public AsaasOperacionalWebhookController(
            IDispatcher dispatcher,
            IRepositorioConfiguracaoAssasClient repositorioConfiguracao,
            IProtecaoCredenciaisAsaas protecaoCredenciais)
        {
            _dispatcher = dispatcher;
            _repositorioConfiguracao = repositorioConfiguracao;
            _protecaoCredenciais = protecaoCredenciais;
        }

        [HttpPost("{empresaId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Receber(
            Guid empresaId,
            [FromBody] JsonElement payload,
            CancellationToken cancellationToken)
        {
            if (empresaId == Guid.Empty)
                return Unauthorized();

            // 1. Localiza a configuração da empresa.
            var configuracao =
                await _repositorioConfiguracao.ObterPorEmpresaAsync(
                    empresaId,
                    cancellationToken);

            if (configuracao is null || !configuracao.Ativo)
                return Unauthorized();

            // 2. Obtém o token enviado pelo Asaas.
            if (!Request.Headers.TryGetValue(
                    "asaas-access-token",
                    out var tokenRecebido))
            {
                return Unauthorized();
            }

            var token = tokenRecebido.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized();

            // 3. Desprotege e compara o token em tempo constante.
            string tokenConfigurado;

            try
            {
                tokenConfigurado =
                    _protecaoCredenciais.Desproteger(
                        configuracao.WebhooksTokenProtegido);
            }
            catch (CryptographicException)
            {
                return Unauthorized();
            }

            var tokenRecebidoBytes = Encoding.UTF8.GetBytes(token);
            var tokenConfiguradoBytes =
                Encoding.UTF8.GetBytes(tokenConfigurado);

            if (!CryptographicOperations.FixedTimeEquals(
                    tokenRecebidoBytes,
                    tokenConfiguradoBytes))
            {
                return Unauthorized();
            }

            // 4. Valida a estrutura mínima do evento.
            if (payload.ValueKind != JsonValueKind.Object)
                return BadRequest("Payload inválido.");

            if (!payload.TryGetProperty("id", out var eventoIdElement) ||
                !payload.TryGetProperty("event", out var tipoEventoElement) ||
                !payload.TryGetProperty("payment", out var pagamentoElement))
            {
                return BadRequest("Estrutura do webhook inválida.");
            }

            var eventoId = ObterString(eventoIdElement);
            var tipoEvento = ObterString(tipoEventoElement);

            if (pagamentoElement.ValueKind != JsonValueKind.Object ||
                !pagamentoElement.TryGetProperty(
                    "id",
                    out var pagamentoIdElement))
            {
                return BadRequest("Identificador do pagamento inválido.");
            }

            var pagamentoGatewayId = ObterString(pagamentoIdElement);

            if (string.IsNullOrWhiteSpace(eventoId) ||
                string.IsNullOrWhiteSpace(tipoEvento) ||
                string.IsNullOrWhiteSpace(pagamentoGatewayId))
            {
                return BadRequest("Dados obrigatórios do webhook não informados.");
            }

            // 5. Preserva o JSON recebido para processamento posterior.
            var payloadOriginal = payload.GetRawText();

            // 6. Registra o evento na Inbox.
            // O Dispatcher aguarda a conclusão da transação.
            var registrado = await _dispatcher.Command<
                RegistrarEventoWebhookAsaasCommand,
                bool>(
                    new RegistrarEventoWebhookAsaasCommand(
                        empresaId,
                        eventoId,
                        pagamentoGatewayId,
                        tipoEvento,
                        payloadOriginal),
                    cancellationToken);

            // Evento duplicado também recebe confirmação.
            return Ok(new
            {
                recebido = true,
                duplicado = !registrado
            });
        }

        private static string? ObterString(JsonElement element)
        {
            return element.ValueKind == JsonValueKind.String
                ? element.GetString()
                : null;
        }
    }
}
