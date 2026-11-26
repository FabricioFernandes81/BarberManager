
using Application.Dispatcher;
using Application.Operacional.Agendamentos.Commands;
using Application.Operacional.Agendamentos.Queries;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers.Integracoes;

[ApiController]
[Route("api/integracoes/n8n/agendamentos")]
[Authorize(
    AuthenticationSchemes =
        N8nServiceDefaults.AuthenticationScheme)]
public sealed class N8nAgendamentoController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public N8nAgendamentoController(
        IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    /// <summary>
    /// Cria um agendamento a partir do agente WhatsApp.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(
        typeof(CriarAgendamentoDto),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CriarAgendamentoDto>> Criar(
        [FromBody] CriarAgendamentoWhatsAppCommand command,
        CancellationToken cancellationToken)
    {
        var resultado = await _dispatcher.Command<
            CriarAgendamentoWhatsAppCommand,
            CriarAgendamentoDto>(
                command,
                cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            resultado);
    }
}
