using Application.Dispatcher;
using Application.Saas.Usuarios.Commands;
using Application.Saas.Usuarios.DTOs;
using Application.Saas.Usuarios.Enums;
using Application.Saas.Usuarios.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Saas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;
        public UsuariosController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("{usuarioId:guid}")]
        public async Task<ActionResult> ObterPorId(Guid usuarioId, CancellationToken cancellationToken)
        {
            if(usuarioId == Guid.Empty)
                return BadRequest("o Identificador do usuário é inválido");


            var result = await _dispatcher.Query<GetUsuarioDetalheQuery, GetUsuarioDetalheDto?>(
                new GetUsuarioDetalheQuery
                {
                    UsuarioId = usuarioId
                },cancellationToken);

            if (result is null)
                return NotFound();

            return Ok(result);
        }
        [HttpPost("empresa")]
        public async Task<IActionResult> VincularUsuarioEmpresa([FromBody]
        VincularUsuarioEmpresaCommand command, CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Command<VincularUsuarioEmpresaCommand,
                ResultadoVinculoUsuarioEmpresa>(
                command, cancellationToken
                );
            return resultado switch
            {
                ResultadoVinculoUsuarioEmpresa.Criado =>
                StatusCode(StatusCodes.Status201Created,
                    new { mensagem = "Usuário vinculado à empresa." }),
                ResultadoVinculoUsuarioEmpresa.Reativado =>
                Ok(new { mensagem = "Vínculo reativado com sucesso." }),
                ResultadoVinculoUsuarioEmpresa.JaAtivo =>
                Conflict(
                    new { mensagem = "Usuário já está vinculado à empresa." }),
                ResultadoVinculoUsuarioEmpresa.UsuarioNaoEncontrado =>
                NotFound(
                    new { mensagem = "Usuário não encontrado." }),

                ResultadoVinculoUsuarioEmpresa.EmpresaNaoEncontrada =>
                NotFound(
                    new { mensagem = "Empresa nao encontrada." }),

                    _=> BadRequest(new { mensagem = "Dados do vínculo são inválidos." })
            };
        }
    }
}
