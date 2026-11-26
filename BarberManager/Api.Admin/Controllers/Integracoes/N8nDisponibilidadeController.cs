using Application.Dispatcher;
using Application.Operacional.Agendamentos.DTOs;
using Application.Operacional.Agendamentos.Queries;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers.Integracoes;

[ApiController]
[Route("api/integracoes/n8n/disponibilidade")]
[Authorize(
    AuthenticationSchemes =
        N8nServiceDefaults.AuthenticationScheme)]
public sealed class N8nDisponibilidadeController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public N8nDisponibilidadeController(
        IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(DisponibilidadeAgendaDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<DisponibilidadeAgendaDto>> Listar(
        [FromQuery] string instance,
        [FromQuery] Guid profissionalId,
        [FromQuery] Guid servicoId,
        [FromQuery] DateOnly data,
        CancellationToken cancellationToken)
    {
        if (data == default)
            return BadRequest("A data da consulta é obrigatória.");

        var query = new ListarDisponibilidadeWhatsAppQuery(
            instance,
            profissionalId,
            servicoId,
            data);

        var resultado =
            await _dispatcher.Query<
                ListarDisponibilidadeWhatsAppQuery,
                DisponibilidadeAgendaDto>(
                    query,
                    cancellationToken);

        return Ok(resultado);
    }
}