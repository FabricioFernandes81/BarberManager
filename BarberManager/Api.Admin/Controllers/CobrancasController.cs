
using Application.Dispatcher;
using Application.Operacional.Financeiro.Commands;
using Application.Operacional.Financeiro.DTOs.Asaas;
using Application.Operacional.Financeiro.Interfaces;
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
        private readonly ICobrancaPixOperacionalService _cobrancaPixService;

        public CobrancasController(
            IDispatcher dispatcher,
            ICobrancaPixOperacionalService cobrancaPixService)
        {
            _dispatcher = dispatcher;
            _cobrancaPixService = cobrancaPixService;
        }

        /// <summary>
        /// Registra uma reserva e inicia ou recupera uma cobrança Pix.
        /// </summary>
        [HttpPost("contas-receber/{contaReceberId:guid}/pix")]
        [ProducesResponseType(
            typeof(CriarCobrancaPixResponse),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CriarCobrancaPixResponse>>
            RegistrarCobrancaPix(
                [FromRoute] Guid contaReceberId,
                [FromHeader(Name = "Idempotency-Key")]
                string chaveIdempotencia,
                CancellationToken cancellationToken)
        {
            if (contaReceberId == Guid.Empty)
            {
                return BadRequest(new
                {
                    mensagem =
                        "O identificador da conta a receber é obrigatório."
                });
            }

            if (string.IsNullOrWhiteSpace(chaveIdempotencia))
            {
                return BadRequest(new
                {
                    mensagem =
                        "O cabeçalho Idempotency-Key é obrigatório."
                });
            }

            // 1. Reservar a cobrança localmente.
            var command = new RegistrarCobrancaPixCommand(
                ContaReceberId: contaReceberId,
                ChaveIdempotencia: chaveIdempotencia.Trim()
            );

            var reserva = await _dispatcher.Command<
                RegistrarCobrancaPixCommand,
                ReservaCobrancaPixDto>(
                    command,
                    cancellationToken);

            // 2. Iniciar ou recuperar a cobrança no Asaas.
            var resultado = await _cobrancaPixService.IniciarAsync(
                reserva.CobrancaId,
                cancellationToken);

            return Ok(resultado);
        }
    }
}
