using Application.Dispatcher;
using Application.Operacional.Compras.Commands;
using Application.Operacional.Compras.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComprasController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public ComprasController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        [ProducesResponseType(
            typeof(CriarCompraDto),
            StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CriarCompraDto>> Criar(
            [FromBody] CriarCompraCommand command,
            CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Command<
                CriarCompraCommand,
                CriarCompraDto>(
                    command,
                    cancellationToken);

            return Created(
                $"/api/Compras/{resultado.Id}",
                resultado);
        }


        [HttpPost("{compraId:guid}/recebimentos")]
        [ProducesResponseType(
    typeof(RegistrarRecebimentoCompraDto),
    StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<RegistrarRecebimentoCompraDto>> RegistrarRecebimento(
    [FromRoute] Guid compraId,
    [FromHeader(Name = "Idempotency-Key")] string chaveIdempotencia,
    [FromBody] RegistrarRecebimentoCompraRequest request,
    CancellationToken cancellationToken)
        {
            var command = new RegistrarRecebimentoCompraCommand
            {
                CompraId = compraId,
                ChaveIdempotencia = chaveIdempotencia,
                Observacao = request.Observacao,
                Itens = request.Itens?.ToList() ?? new List<RegistrarRecebimentoCompraItemDto>()
            };

            var resultado = await _dispatcher.Command<
                RegistrarRecebimentoCompraCommand,
                RegistrarRecebimentoCompraDto>(
                    command,
                    cancellationToken);

            return Created(
                $"/api/Compras/{compraId}/recebimentos/{resultado.Id}",
                resultado);
        }
    }
}
