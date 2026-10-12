using Application.Autenticacao.Commands;
using Application.Dispatcher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public readonly IDispatcher _dispatcher;

        public AuthController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<ActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var result = await _dispatcher.Command<LoginCommand, LoginResult>(
                command, cancellationToken);

            if (result.RequerSelecaoEmpresa)
            {
                return Ok(result);
            }
            if (!result.Sucesso)
            {
                return Unauthorized(new
                {
                    mensagem = result.Mensagem
                });
            }
            return Ok(result);
        }

    }
}
