using Application.Dispatcher;
using Application.Saas.Pagamentos.Commands;
using Application.Saas.Pagamentos.DTOs;
using Application.Saas.Pagamentos.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Saas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly IDispatcher dispatcher;

        public PagamentoController(IDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        [HttpGet("faturas/{faturaId:guid}/tentativas")]
        public async Task<ActionResult> ObterTentativas(Guid faturaId, CancellationToken cancellationToken)
        {
            var resultado = await this.dispatcher.Query<ObterTentativasPagamentoQuery, List<PagamentoTentativaDto>>(
                new ObterTentativasPagamentoQuery
                {
                    FaturaId = faturaId
                }, cancellationToken);

            return Ok(resultado);
        }

        [HttpPost("tentativas")]
        [ProducesResponseType(
       typeof(RegistrarTentativaPagamentoDto),
       StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<RegistrarTentativaPagamentoDto>>
       RegistrarTentativaPagamento(
           [FromBody] RegistrarTentativaPagamentoRequest request,
           [FromHeader(Name = "Idempotency-Key")]
            string chaveIdempotencia,
           CancellationToken cancellationToken)
        {
            var command = new RegistrarTentativaPagamentoCommand
            (
                request.FaturaId,
                request.Provedor,
                chaveIdempotencia
            );

            var resultado = await this.dispatcher
                .Command<
                    RegistrarTentativaPagamentoCommand,
                    RegistrarTentativaPagamentoDto>(
                        command,
                        cancellationToken
                    );

            // Requisição repetida: devolve o resultado já existente.
            if (resultado.JaExistia)
            {
                return Ok(resultado);
            }

            return StatusCode(
                StatusCodes.Status201Created,
                resultado
            );
        }
    }
}


