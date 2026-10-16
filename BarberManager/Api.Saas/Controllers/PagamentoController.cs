using Application.Dispatcher;
using Application.Interfaces.Empresas.Pagamentos;
using Application.Saas.Pagamentos.Commands;
using Application.Saas.Pagamentos.DTOs;
using Application.Saas.Pagamentos.Queries;
using Domain.Entities.Common.Enuns;
using Microsoft.AspNetCore.Mvc;

namespace Api.Saas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        private readonly IDispatcher dispatcher;
        private readonly IPagamentoService _pagamentoService;

        public PagamentoController(IDispatcher dispatcher, IPagamentoService pagamentoService)
        {
            this.dispatcher = dispatcher;
            _pagamentoService = pagamentoService;
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
                chaveIdempotencia,
                 request.MetodoPagamento
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


        [HttpPost("faturas")]
        [ProducesResponseType(
                   typeof(GerarFaturaDto),
  StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GerarFaturaDto>> GerarFatura(
                   [FromBody] GerarFaturaRequest request,
                    CancellationToken cancellationToken)
        {
            var command = new GerarFaturaCommand(
                request.Assinatura,
                request.PeriodoInicio,
                request.PeriodoFim,
                request.Vencimento
            );

            var resultado = await this.dispatcher
                .Command<GerarFaturaCommand, GerarFaturaDto>(
                    command,
                    cancellationToken
                );

            // Não criou uma nova fatura: Trial ou fatura existente.
            if (resultado.Ignorada || resultado.JaExistia)
            {
                return Ok(resultado);
            }

            return StatusCode(
                StatusCodes.Status201Created,
                resultado
            );
        }

        [HttpPost("faturas/{faturaId:guid}/pagamentos")]
        public async Task<ActionResult<IniciarPagamentoResultadoDto>>
     IniciarPagamento(
         Guid faturaId,
         [FromBody] IniciarPagamentoRequest request,
         [FromHeader(Name = "Idempotency-Key")]
        string chaveIdempotencia,
         CancellationToken cancellationToken)
        {
            if (request.FaturaId != faturaId)
                return BadRequest(
                    "A fatura informada na rota difere da fatura do corpo.");

            if (!Enum.IsDefined(request.MetodoPagamento))
                return BadRequest("Método de pagamento inválido.");

            if (request.MetodoPagamento == MetodoPagamento.Pix
                && request.CartaoCredito is not null)
            {
                return BadRequest(
                    "Não envie dados de cartão para pagamento Pix.");
            }

            if (request.MetodoPagamento == MetodoPagamento.CartaoCredito
                && request.CartaoCredito is null)
            {
                return BadRequest(
                    "Os dados do cartão de crédito são obrigatórios.");
            }

            var remoteIp =
                HttpContext.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrWhiteSpace(remoteIp))
                return BadRequest("Não foi possível identificar o IP remoto.");

            var resultado = await _pagamentoService.IniciarAsync(
                request,
                chaveIdempotencia,
                remoteIp,
                cancellationToken);

            return StatusCode(StatusCodes.Status201Created, resultado);
        }

        [HttpGet("faturas/{faturaId:guid}/status")]
        [ProducesResponseType(
    typeof(ObterStatusFaturaDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ObterStatusFaturaDto>>
    ObterStatusFatura(
        Guid faturaId,
        CancellationToken cancellationToken)
        {
            if (faturaId == Guid.Empty)
            {
                return BadRequest(
                    "O identificador da fatura é obrigatório.");
            }

            var resultado = await dispatcher.Query<
                ObterStatusFaturaQuery,
                ObterStatusFaturaDto?>(
                    new ObterStatusFaturaQuery
                    {
                        FaturaId = faturaId
                    },
                    cancellationToken);

            if (resultado is null)
            {
                return NotFound(new
                {
                    mensagem = "Fatura não encontrada."
                });
            }

            return Ok(resultado);
        }

    }
}


