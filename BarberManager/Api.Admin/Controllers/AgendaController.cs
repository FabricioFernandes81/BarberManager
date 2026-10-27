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


        [HttpPost("cancelar")]
        [ProducesResponseType(
    typeof(CancelarAgendamentoDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CancelarAgendamentoDto>> Cancelar(
    [FromBody] CancelarAgendamentoCommand command,
    CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Command<
                CancelarAgendamentoCommand,
                CancelarAgendamentoDto>(
                command,
                cancellationToken);

            return Ok(resultado);
        }

        [HttpPost("confirmar")]
        [ProducesResponseType(
    typeof(ConfirmarAgendamentoDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ConfirmarAgendamentoDto>> Confirmar(
    [FromBody] ConfirmarAgendamentoCommand command,
    CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Command<
                ConfirmarAgendamentoCommand,
                ConfirmarAgendamentoDto>(
                    command,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpPost("iniciar-atendimento")]
        [ProducesResponseType(
    typeof(IniciarAtendimentoDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IniciarAtendimentoDto>> IniciarAtendimento(
    [FromBody] IniciarAtendimentoCommand command,
    CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Command<
                IniciarAtendimentoCommand,
                IniciarAtendimentoDto>(
                    command,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpPost("concluir-atendimento")]
        [ProducesResponseType(
    typeof(ConcluirAtendimentoDto),
    StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ConcluirAtendimentoDto>>
    ConcluirAtendimento(
        [FromBody] ConcluirAtendimentoCommand command,
        CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Command<
                ConcluirAtendimentoCommand,
                ConcluirAtendimentoDto>(
                    command,
                    cancellationToken);

            return Ok(resultado);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ObterAgendaPeriodoDto>> Listar(
      [FromQuery] ObterAgendaPeriodoQuery query,
      CancellationToken cancellationToken)
        {
            var resultado = await _dispatcher.Query<
                ObterAgendaPeriodoQuery,
                ObterAgendaPeriodoDto>(
                    query,
                    cancellationToken);

            return Ok(resultado);
        }
    }
}
