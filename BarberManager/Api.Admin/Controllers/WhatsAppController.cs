using Application.Dispatcher;
using Application.Operacional.Notificacoes.Commands;
using Application.Operacional.Notificacoes.DTOs;
using Application.Operacional.Notificacoes.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class WhatsAppController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public WhatsAppController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost("instancia")]
    [ProducesResponseType(
        typeof(CriarInstanciaWhatsAppDto),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CriarInstanciaWhatsAppDto>>
        CriarInstancia(CancellationToken cancellationToken)
    {
        var command = new CriarInstanciaWhatsAppCommand();

        var resultado = await _dispatcher.Command<
            CriarInstanciaWhatsAppCommand,
            CriarInstanciaWhatsAppDto>(
                command,
                cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            resultado);
    }

    [HttpGet("instancia")]
    [ProducesResponseType(
    typeof(ConfiguracaoWhatsAppDto),
    StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ConfiguracaoWhatsAppDto>>
    ObterConfiguracao(CancellationToken cancellationToken)
    {
        var query = new ObterConfiguracaoWhatsAppQuery();

        var resultado = await _dispatcher.Query<
            ObterConfiguracaoWhatsAppQuery,
            ConfiguracaoWhatsAppDto>(
                query,
                cancellationToken);

        return Ok(resultado);
    }

    [HttpPut("instancia")]
    [ProducesResponseType(
    typeof(ConfiguracaoWhatsAppDto),
    StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ConfiguracaoWhatsAppDto>>
    AtualizarConfiguracao(
        [FromBody] AtualizarConfiguracaoWhatsAppCommand command,
        CancellationToken cancellationToken)
    {
        var resultado = await _dispatcher.Command<
            AtualizarConfiguracaoWhatsAppCommand,
            ConfiguracaoWhatsAppDto>(
                command,
                cancellationToken);

        return Ok(resultado);
    }
}