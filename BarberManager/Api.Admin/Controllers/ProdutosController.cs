using Application.Dispatcher;
using Application.Operacional.Produtos.DTOs;
using Application.Operacional.Produtos.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProdutosController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public ProdutosController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        [HttpGet]
        [ProducesResponseType(
       typeof(ListarProdutosDto),
       StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ListarProdutosDto>> Listar(
       [FromQuery] int pageNumber = 1,
       [FromQuery] int pageSize = 10,
       [FromQuery] string? searchTerm = null,
       [FromQuery] Guid? categoriaId = null,
       [FromQuery] Guid? marcaId = null,
       [FromQuery] bool? ativo = null,
       CancellationToken cancellationToken = default)
        {
            var query = new ListarProdutosQuery(
                pageNumber,
                pageSize,
                searchTerm,
                categoriaId,
                marcaId,
                ativo);

            var resultado = await _dispatcher.Query<
                ListarProdutosQuery,
                ListarProdutosDto>(
                    query,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(
    typeof(ProdutoDetalheDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProdutoDetalheDto>> ObterPorId(
    [FromRoute] Guid id,
    CancellationToken cancellationToken)
        {
            var query = new ObterProdutoPorIdQuery(id);

            var resultado = await _dispatcher.Query<
                ObterProdutoPorIdQuery,
                ProdutoDetalheDto>(
                    query,
                    cancellationToken);

            return Ok(resultado);
        }
    }
}
