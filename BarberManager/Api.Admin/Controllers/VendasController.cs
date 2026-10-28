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
    }
}
