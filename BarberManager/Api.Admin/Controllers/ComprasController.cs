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
    public class ComprasController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public ComprasController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        [ProducesResponseType(
            typeof(CriarCompraDto),
            StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CriarCompraDto>> Criar(
            [FromBody] CriarCompraCommand command,
            CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Command<
                CriarCompraCommand,
                CriarCompraDto>(
                    command,
                    cancellationToken);

            return Created(
                $"/api/Compras/{resultado.Id}",
                resultado);
        }


        [HttpPost("{compraId:guid}/recebimentos")]
        [ProducesResponseType(
    typeof(RegistrarRecebimentoCompraDto),
    StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<RegistrarRecebimentoCompraDto>> RegistrarRecebimento(
    [FromRoute] Guid compraId,
    [FromHeader(Name = "Idempotency-Key")] string chaveIdempotencia,
    [FromBody] RegistrarRecebimentoCompraRequest request,
    CancellationToken cancellationToken)
        {
            var command = new RegistrarRecebimentoCompraCommand
            {
                CompraId = compraId,
                ChaveIdempotencia = chaveIdempotencia,
                Observacao = request.Observacao,
                Itens = request.Itens?.ToList() ?? new List<RegistrarRecebimentoCompraItemDto>()
            };

            var resultado = await _dispatcher.Command<
                RegistrarRecebimentoCompraCommand,
                RegistrarRecebimentoCompraDto>(
                    command,
                    cancellationToken);

            return Created(
                $"/api/Compras/{compraId}/recebimentos/{resultado.Id}",
                resultado);
        }

        [HttpPatch("{compraId:guid}/cancelar")]
        [ProducesResponseType(
    typeof(CompraCanceladaDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompraCanceladaDto>> Cancelar(
    [FromRoute] Guid compraId,
    CancellationToken cancellationToken)
        {
            var command = new CancelarCompraCommand(compraId);

            var resultado = await _dispatcher.Command<
                CancelarCompraCommand,
                CompraCanceladaDto>(
                    command,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpGet]
        [ProducesResponseType(
    typeof(ListarComprasDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ListarComprasDto>> Listar(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string? searchTerm = null,
    [FromQuery] DateTime? inicio = null,
    [FromQuery] DateTime? fim = null,
    [FromQuery] Guid? fornecedorId = null,
    [FromQuery] Domain.Entities.Common.Enuns.StatusCompra? status = null,
    CancellationToken cancellationToken = default)
        {
            var query = new ListarComprasQuery(
                pageNumber,
                pageSize,
                searchTerm,
                inicio,
                fim,
                fornecedorId,
                status);

            var resultado = await _dispatcher.Query<
                ListarComprasQuery,
                ListarComprasDto>(
                    query,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpGet("{compraId:guid}")]
        [ProducesResponseType(
    typeof(CompraDetalheDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompraDetalheDto>> ObterPorId(
    [FromRoute] Guid compraId,
    CancellationToken cancellationToken)
        {
            var query = new ObterCompraPorIdQuery(compraId);

            var resultado = await _dispatcher.Query<
                ObterCompraPorIdQuery,
                CompraDetalheDto>(
                    query,
                    cancellationToken);

            return Ok(resultado);
        }


        [HttpPost("{compraId:guid}/devolucoes")]
        [ProducesResponseType(
            typeof(RegistrarDevolucaoCompraDto),
            StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<RegistrarDevolucaoCompraDto>> RegistrarDevolucao(
            [FromRoute] Guid compraId,
            [FromHeader(Name = "Idempotency-Key")] string chaveIdempotencia,
            [FromBody] RegistrarDevolucaoCompraRequest request,
            CancellationToken cancellationToken)
        {
            var command = new RegistrarDevolucaoCompraCommand
            {
                CompraId = compraId,
                ChaveIdempotencia = chaveIdempotencia,
                Motivo = request.Motivo,
                Observacao = request.Observacao,
                Itens = request.Itens?.ToList()
                    ?? new List<RegistrarDevolucaoCompraItemDto>()
            };

            var resultado = await _dispatcher.Command<
                RegistrarDevolucaoCompraCommand,
                RegistrarDevolucaoCompraDto>(
                    command,
                    cancellationToken);

            return Created(
                $"/api/Compras/{compraId}/devolucoes/{resultado.Id}",
                resultado);
        }

        [HttpGet("{compraId:guid}/devolucoes/{devolucaoId:guid}")]
        [ProducesResponseType(
            typeof(DevolucaoCompraDetalheDto),
            StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DevolucaoCompraDetalheDto>>
            ObterDevolucaoPorId(
                [FromRoute] Guid compraId,
                [FromRoute] Guid devolucaoId,
                CancellationToken cancellationToken)
        {
            if (compraId == Guid.Empty || devolucaoId == Guid.Empty)
            {
                return BadRequest("Os identificadores informados são inválidos.");
            }

            var query = new ObterDevolucaoCompraPorIdQuery(devolucaoId);

            var resultado = await _dispatcher.Query<
                ObterDevolucaoCompraPorIdQuery,
                DevolucaoCompraDetalheDto>(
                    query,
                    cancellationToken);

            if (resultado.CompraId != compraId)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

    }
}
