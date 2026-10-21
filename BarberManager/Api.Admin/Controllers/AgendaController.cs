using Application.Dispatcher;
using Application.Operacional.Agendamentos.Commands;
using Application.Operacional.Agendamentos.DTOs;
using Application.Operacional.Agendamentos.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AgendaController : ControllerBase
    {
        private readonly IDispatcher _dispatcher;

        public AgendaController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("disponibilidade")]
        [ProducesResponseType(
       typeof(DisponibilidadeAgendaDto),
       StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<DisponibilidadeAgendaDto>>
       ObterDisponibilidade(
           [FromQuery] Guid profissionalId,
           [FromQuery] Guid servicoId,
           [FromQuery] DateOnly data,
           CancellationToken cancellationToken)
        {
            var query = new ObterDisponibilidadeAgendaQuery(
                profissionalId,
                servicoId,
                data);

            var resultado =
                await _dispatcher.Query<
                    ObterDisponibilidadeAgendaQuery,
                    DisponibilidadeAgendaDto>(
                    query,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<CriarAgendamentoDto>> Criar(
                                [FromBody] CriarAgendamentoCommand command,
                                CancellationToken cancellationToken)
        {
            var resultado =
                await _dispatcher.Command<
                    CriarAgendamentoCommand,
                    CriarAgendamentoDto>(
                    command,
                    cancellationToken);

            return StatusCode(
                StatusCodes.Status201Created,
                resultado);
        }
    }
}
