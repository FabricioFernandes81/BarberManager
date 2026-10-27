using Application.Dispatcher;
using Application.Operacional.Financeiro.Commands;
using Application.Operacional.Financeiro.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContasReceberController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public ContasReceberController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        /// <summary>
        /// Registra um recebimento parcial ou total de uma conta.
        /// </summary>
        [HttpPost("{contaReceberId:guid}/recebimentos")]
        [ProducesResponseType(
            typeof(RegistrarRecebimentoDto),
            StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RegistrarRecebimentoDto>>
            RegistrarRecebimento(
                [FromRoute] Guid contaReceberId,
                [FromBody] RegistrarRecebimentoRequest request,
                CancellationToken cancellationToken)
        {
            var command = new RegistrarRecebimentoCommand(
                ContaReceberId: contaReceberId,
                FormaPagamentoId: request.FormaPagamentoId,
                Valor: request.Valor,
                Observacao: request.Observacao);

            var resultado = await _dispatcher.Command<
                RegistrarRecebimentoCommand,
                RegistrarRecebimentoDto>(
                    command,
                    cancellationToken);

            return StatusCode(
                StatusCodes.Status201Created,
                resultado);
        }
    }
}

