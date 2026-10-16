using Application.Interfaces.Empresas.Pagamentos;
using Application.Saas.Pagamentos.DTOs;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Api.Saas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AsaasWebhookController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAsaasWebhookService _webhookService;
        private readonly ILogger<AsaasWebhookController> _logger;
        public AsaasWebhookController(
            IConfiguration configuration,
           IAsaasWebhookService webhookService, ILogger<AsaasWebhookController> logger)
        {
            _configuration = configuration;
           _webhookService = webhookService;
            _logger = logger;
        }

        [HttpGet("teste")]
        [AllowAnonymous]
        public IActionResult Teste()
        {
            return Ok(new
            {
                mensagem = "AsaasWebhookController acessível",
                data = DateTime.UtcNow
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Receber(
    [FromBody] AsaasWebhookDto evento,
    CancellationToken cancellationToken)
        {
            var tokenEsperado = _configuration["Asaas:WebhookToken"];

            if (string.IsNullOrWhiteSpace(tokenEsperado))
            {
                _logger.LogError(
                    "Webhook Asaas: token esperado não configurado.");

                return Unauthorized();
            }

            if (!Request.Headers.TryGetValue(
                "asaas-access-token", out var tokenRecebido))
            {
                _logger.LogWarning(
                    "Webhook Asaas: cabeçalho de autenticação ausente.");

                return Unauthorized();
            }
          /*  //debuger
            _logger.LogInformation(
                     "Token do asaas:{Request}", Request.Headers["asaas-access-token"]);

            _logger.LogInformation(
                        "Token Aplicação: {Recebido}", _configuration["Asaas:WebhookToken"]);
*/
            var esperadoBytes = Encoding.UTF8.GetBytes(tokenEsperado?.Trim());
            var recebidoBytes = Encoding.UTF8.GetBytes(tokenRecebido.ToString().Trim());

           
            
            if (!CryptographicOperations.FixedTimeEquals(
                esperadoBytes, recebidoBytes))
            {
                _logger.LogWarning(
                    "Webhook Asaas: token recebido inválido.");

                return Unauthorized();
            }

            _logger.LogInformation(
                "Webhook Asaas: autenticação validada. Evento: {Evento}",
                evento.Evento);

            await _webhookService.ProcessarAsync(
                evento, cancellationToken);

            return Ok();
        }
    }
}
