using Application.Dispatcher;
using Application.Operacional.Vendas.Commands;
using Application.Operacional.Vendas.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VendasController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public VendasController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("adicionar-item")]
        [ProducesResponseType(
           typeof(AdicionarItemVendaDto),
           StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdicionarItemVendaDto>> AdicionarItem(
           [FromBody] AdicionarItemVendaCommand command,
           CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Command<
                AdicionarItemVendaCommand,
                AdicionarItemVendaDto>(
                    command,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpDelete("{vendaId:guid}/itens/{itemVendaId:guid}")]
        [ProducesResponseType(
        typeof(RemoverItemVendaDto),
        StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RemoverItemVendaDto>> RemoverItem(
        Guid vendaId,
        Guid itemVendaId,
        CancellationToken cancellationToken)
        {
            var command = new RemoverItemVendaCommand
            {
                VendaId = vendaId,
                ItemVendaId = itemVendaId
            };

            var resultado = await _dispatcher.Command<
                RemoverItemVendaCommand,
                RemoverItemVendaDto>(
                    command,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpPut("{vendaId:guid}/finalizar")]
        [ProducesResponseType(
        typeof(FinalizarVendaDto),
        StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinalizarVendaDto>> FinalizarVenda(
        [FromRoute] Guid vendaId,
        [FromBody] FinalizarVendaRequest request,
        CancellationToken cancellationToken)
        {
            if (vendaId == Guid.Empty)
            {
                return BadRequest(new
                {
                    mensagem = "O identificador da venda é obrigatório."
                });
            }

            if (request.Pagamentos is null ||
                request.Pagamentos.Count == 0)
            {
                return BadRequest(new
                {
                    mensagem = "Informe pelo menos uma forma de pagamento."
                });
            }

            var command = new FinalizarVendaCommand(
                VendaId: vendaId,

                Pagamentos: request.Pagamentos
                    .Select(p => new PagamentoVendaCommand(
                        p.FormaPagamentoId,
                        p.Valor))
                    .ToArray(),

                Observacao: request.Observacao
            );

            var resultado = await _dispatcher.Command<
                FinalizarVendaCommand,
                FinalizarVendaDto>(
                    command,
                    cancellationToken);

            return Ok(resultado);
        }
    }
}
