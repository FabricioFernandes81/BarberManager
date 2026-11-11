
using Application.Dispatcher;
using Application.Operacional.Estoque.Commands;
using Application.Operacional.Estoque.DTOs;
using Application.Operacional.Estoque.Queries;
using Domain.Entities.Common.Enuns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EstoqueController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public EstoqueController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("produtos/{produtoId:guid}/movimentacoes")]
        [ProducesResponseType(
            typeof(ListarMovimentacoesEstoqueDto),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ListarMovimentacoesEstoqueDto>>
            ListarMovimentacoes(
                [FromRoute] Guid produtoId,
                [FromQuery] DateTimeOffset? dataInicio = null,
                [FromQuery] DateTimeOffset? dataFim = null,
                [FromQuery] TipoMovimentacaoEstoque? tipo = null,
                [FromQuery] OrigemMovimentacaoEstoque? origem = null,
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10,
                CancellationToken cancellationToken = default)
        {
            var query = new ListarMovimentacoesEstoqueQuery(
                produtoId,
                dataInicio,
                dataFim,
                tipo,
                origem,
                pageNumber,
                pageSize);

            var resultado = await _dispatcher.Query<
                ListarMovimentacoesEstoqueQuery,
                ListarMovimentacoesEstoqueDto>(
                    query,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpPost("produtos/{produtoId:guid}/entradas")]
        [ProducesResponseType(
    typeof(MovimentacaoEstoqueDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MovimentacaoEstoqueDto>>
    RegistrarEntrada(
        [FromRoute] Guid produtoId,
        [FromBody] RegistrarEntradaEstoqueCommand command,
        CancellationToken cancellationToken)
        {
            var comando = command with
            {
                ProdutoId = produtoId
            };

            var resultado = await _dispatcher.Command<
                RegistrarEntradaEstoqueCommand,
                MovimentacaoEstoqueDto>(
                    comando,
                    cancellationToken);

            return Ok(resultado);
        }


        [HttpPost("produtos/{produtoId:guid}/saidas")]
        [ProducesResponseType(
            typeof(MovimentacaoEstoqueDto),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MovimentacaoEstoqueDto>>
            RegistrarSaida(
                [FromRoute] Guid produtoId,
                [FromBody] RegistrarSaidaEstoqueCommand command,
                CancellationToken cancellationToken)
        {
            var comando = command with
            {
                ProdutoId = produtoId
            };

            var resultado = await _dispatcher.Command<
                RegistrarSaidaEstoqueCommand,
                MovimentacaoEstoqueDto>(
                    comando,
                    cancellationToken);

            return Ok(resultado);
        }


        [HttpPost("produtos/{produtoId:guid}/ajustes")]
        [ProducesResponseType(
            typeof(MovimentacaoEstoqueDto),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MovimentacaoEstoqueDto>>
            RegistrarAjuste(
                [FromRoute] Guid produtoId,
                [FromBody] RegistrarAjusteEstoqueCommand command,
                CancellationToken cancellationToken)
        {
            var comando = command with
            {
                ProdutoId = produtoId
            };

            var resultado = await _dispatcher.Command<
                RegistrarAjusteEstoqueCommand,
                MovimentacaoEstoqueDto>(
                    comando,
                    cancellationToken);

            return Ok(resultado);
        }

    }
}
