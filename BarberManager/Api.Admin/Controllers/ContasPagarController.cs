using Application.Dispatcher;
using Application.Operacional.Financeiro.Commands;
using Application.Operacional.Financeiro.DTOs;
using Application.Operacional.Financeiro.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContasPagarController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public ContasPagarController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        [ProducesResponseType(
            typeof(ListarContasPagarDto),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ListarContasPagarDto>> Listar(
            [FromQuery] ListarContasPagarQuery query,
            CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Query<
                ListarContasPagarQuery,
                ListarContasPagarDto>(
                    query,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpGet("{contaPagarId:guid}")]
        [ProducesResponseType(
            typeof(ContaPagarDetalheDto),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ContaPagarDetalheDto>> ObterPorId(
            [FromRoute] Guid contaPagarId,
            CancellationToken cancellationToken)
        {
            var query = new ObterContaPagarPorIdQuery(contaPagarId);

            var resultado = await _dispatcher.Query<
                ObterContaPagarPorIdQuery,
                ContaPagarDetalheDto>(
                    query,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpPost("{contaPagarId:guid}/pagamentos")]
        [ProducesResponseType(
    typeof(RegistrarPagamentoDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RegistrarPagamentoDto>> RegistrarPagamento(
    [FromRoute] Guid contaPagarId,
    [FromBody] RegistrarPagamentoCommand command,
    CancellationToken cancellationToken)
        {
            if (contaPagarId == Guid.Empty)
            {
                return BadRequest(new
                {
                    mensagem = "O identificador da conta a pagar é obrigatório."
                });
            }

            var comando = command with
            {
                ContaPagarId = contaPagarId
            };

            var resultado = await _dispatcher.Command<
                RegistrarPagamentoCommand,
                RegistrarPagamentoDto>(
                    comando,
                    cancellationToken);

            return Ok(resultado);
        }
    }
}