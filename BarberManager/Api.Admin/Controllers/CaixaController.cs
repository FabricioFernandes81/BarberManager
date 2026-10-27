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
    public class CaixaController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public CaixaController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("abrir")]
        public async Task<ActionResult<ResumoCaixaDto>> Abrir(
       [FromBody] AbrirCaixaCommand command,
       CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Command<
                AbrirCaixaCommand,
                ResumoCaixaDto>(
                    command,
                    cancellationToken);

            return StatusCode(StatusCodes.Status201Created, resultado);
        }

        [HttpGet("saldo")]
        public async Task<ActionResult<ResumoCaixaDto>> ObterSaldo(
            CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Query<
                ObterResumoCaixaQuery,
                ResumoCaixaDto>(
                    new ObterResumoCaixaQuery(),
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpGet("movimentos")]
        public async Task<ActionResult<ListarMovimentosCaixaDto>> ListarMovimentos(
            [FromQuery] ListarMovimentosCaixaQuery query,
            CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Query<
                ListarMovimentosCaixaQuery,
                ListarMovimentosCaixaDto>(
                    query,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpPost("fechar")]
        public async Task<ActionResult<ResumoCaixaDto>> Fechar(
            [FromBody] FecharCaixaCommand command,
            CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Command<
                FecharCaixaCommand,
                ResumoCaixaDto>(
                    command,
                    cancellationToken);

            return Ok(resultado);
        }
    }
}
