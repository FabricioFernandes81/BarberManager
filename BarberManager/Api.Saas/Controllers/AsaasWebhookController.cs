using Application.Interfaces.Empresas.Pagamentos;
using Application.Saas.Pagamentos.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Api.Saas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsaasWebhookController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAsaasWebhookService _webhookService;

        public AsaasWebhookController(
            IConfiguration configuration,
            IAsaasWebhookService webhookService)
        {
            _configuration = configuration;
            _webhookService = webhookService;
        }

        [HttpPost]
        public async Task<IActionResult> Receber(
            [FromBody] AsaasWebhookDto evento,
            CancellationToken cancellationToken)
        {
            var tokenEsperado =
                _configuration["Asaas:WebhookToken"];

            if (string.IsNullOrWhiteSpace(tokenEsperado) ||
                !Request.Headers.TryGetValue(
                    "asaas-access-token", out var tokenRecebido))
            {
                return Unauthorized();
            }

            var esperadoBytes = Encoding.UTF8.GetBytes(tokenEsperado);
            var recebidoBytes = Encoding.UTF8.GetBytes(
                tokenRecebido.ToString());

            if (!CryptographicOperations.FixedTimeEquals(
                esperadoBytes, recebidoBytes))
            {
                return Unauthorized();
            }

            await _webhookService.ProcessarAsync(
                evento, cancellationToken);

            return Ok();
        }
    }
}
