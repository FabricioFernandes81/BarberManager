using Application.Dispatcher;
using Application.Operacional.Compras.Commands;
using Application.Operacional.Compras.DTOs;
using Application.Operacional.Compras.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FornecedoresController : ControllerBase
    {

        private readonly IDispatcher _dispatcher;

        public FornecedoresController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        [ProducesResponseType(
            typeof(CriarFornecedorDto),
            StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CriarFornecedorDto>> Criar(
            [FromBody] CriarFornecedorCommand command,
            CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Command<
                CriarFornecedorCommand,
                CriarFornecedorDto>(
                    command,
                    cancellationToken);

            return Created(
                $"/api/Fornecedores/{resultado.Id}",
                resultado);
        }

        [HttpGet]
        [ProducesResponseType(
    typeof(ListarFornecedoresDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ListarFornecedoresDto>> Listar(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string? searchTerm = null,
    [FromQuery] bool? ativo = null,
    CancellationToken cancellationToken = default)
        {
            var query = new ListarFornecedoresQuery(
                pageNumber,
                pageSize,
                searchTerm,
                ativo);

            var resultado = await _dispatcher.Query<
                ListarFornecedoresQuery,
                ListarFornecedoresDto>(
                    query,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(FornecedorDetalheDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FornecedorDetalheDto>> ObterPorId([FromRoute] Guid id,
    CancellationToken cancellationToken)
        {
            var query = new ObterFornecedorPorIdQuery(id);

            var resultado = await _dispatcher.Query<
                ObterFornecedorPorIdQuery,
                FornecedorDetalheDto>(
                    query,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(FornecedorDetalheDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FornecedorDetalheDto>> Atualizar(
  [FromRoute] Guid id,
  [FromBody] AtualizarFornecedorCommand command,
  CancellationToken cancellationToken)
        {
            var comando = command with
            {
                FornecedorId = id
            };

            var resultado = await _dispatcher.Command<
                AtualizarFornecedorCommand,
                FornecedorDetalheDto>(
                    comando,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpPatch("{id:guid}/ativar")]
        [ProducesResponseType(
    typeof(FornecedorDetalheDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FornecedorDetalheDto>> Ativar(
    [FromRoute] Guid id,
    CancellationToken cancellationToken)
        {
            var command = new AlterarStatusFornecedorCommand(
                FornecedorId: id,
                Ativo: true);

            var resultado = await _dispatcher.Command<
                AlterarStatusFornecedorCommand,
                FornecedorDetalheDto>(
                    command,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpPatch("{id:guid}/desativar")]
        [ProducesResponseType(typeof(FornecedorDetalheDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FornecedorDetalheDto>> Desativar(
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var command = new AlterarStatusFornecedorCommand(
                FornecedorId: id,
                Ativo: false);

            var resultado = await _dispatcher.Command<
                AlterarStatusFornecedorCommand,
                FornecedorDetalheDto>(
                    command,
                    cancellationToken);

            return Ok(resultado);
        }
    }
}
