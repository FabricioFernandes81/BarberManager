using Application.Clientes.Commands;
using Application.Clientes.DTOs;
using Application.Clientes.Queries;
using Application.Dispatcher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Cliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public ClientesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("perfil")]
        [ProducesResponseType(
        typeof(CriarPerfilClienteResultadoDto),
        StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CriarPerfil(
        [FromBody] CriarPerfilClienteCommand command,
        CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Command<
                CriarPerfilClienteCommand,
                CriarPerfilClienteResultadoDto>(
                    command,
                    cancellationToken);

            if (resultado.Sucesso)
            {
                return StatusCode(
                    StatusCodes.Status201Created,
                    resultado);
            }

            return resultado.Codigo switch
            {
                "NaoAutenticado" or
                "ContaIdentityNaoEncontrada" or
                "ContaBloqueada" =>
                    Unauthorized(resultado),

                "PerfilJaExiste" =>
                    Conflict(resultado),

                _ =>
                    BadRequest(resultado)
            };
        }

        [HttpGet("me")]
        [ProducesResponseType(
            typeof(PerfilClienteDto),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterMeuPerfil(
            CancellationToken cancellationToken)
        {
            var perfil = await _dispatcher.Query<
                ObterMeuPerfilClienteQuery,
                PerfilClienteDto?>(
                    new ObterMeuPerfilClienteQuery(),
                    cancellationToken);

            if (perfil is null)
            {
                return NotFound(new
                {
                    Codigo = "PerfilNaoEncontrado",
                    Mensagem = "A conta ainda não possui um perfil de cliente."
                });
            }

            return Ok(perfil);
        }
    }
}
