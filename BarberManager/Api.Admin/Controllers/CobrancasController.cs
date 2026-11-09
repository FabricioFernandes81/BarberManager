
using Application.Dispatcher;
using Application.Operacional.Financeiro.Commands;
using Application.Operacional.Financeiro.DTOs.Asaas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CobrancasController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public CobrancasController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Reserva uma cobrança Pix para uma conta a receber.
        /// </summary>
        [HttpPost("contas-receber/{contaReceberId:guid}/pix")]
        [ProducesResponseType(
            typeof(ReservaCobrancaPixDto),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReservaCobrancaPixDto>> RegistrarCobrancaPix(
            [FromRoute] Guid contaReceberId,
            [FromHeader(Name = "Idempotency-Key")] string chaveIdempotencia,
            CancellationToken cancellationToken)
        {
            if (contaReceberId == Guid.Empty)
            {
                return BadRequest(new
                {
                    mensagem = "O identificador da conta a receber é obrigatório."
                });
            }

            if (string.IsNullOrWhiteSpace(chaveIdempotencia))
            {
                return BadRequest(new
                {
                    mensagem = "O cabeçalho Idempotency-Key é obrigatório."
                });
            }

            var command = new RegistrarCobrancaPixCommand(
                ContaReceberId: contaReceberId,
                ChaveIdempotencia: chaveIdempotencia.Trim()
            );

            var resultado = await _dispatcher.Command<
                RegistrarCobrancaPixCommand,
                ReservaCobrancaPixDto>(
                    command,
                    cancellationToken);

            return Ok(resultado);
        }
    }
}
