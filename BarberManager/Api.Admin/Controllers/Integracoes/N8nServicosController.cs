using Application.Dispatcher;
using Application.Operacional.Servicos.DTOs;
using Application.Operacional.Servicos.Queries;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers.Integracoes;

[ApiController]
[Route("api/integracoes/n8n/servicos")]
[Authorize(
    AuthenticationSchemes =
        N8nServiceDefaults.AuthenticationScheme)]
public sealed class N8nServicosController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public N8nServicosController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    [ProducesResponseType(
        typeof(ListarServicosWhatsAppDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ListarServicosWhatsAppDto>>
        Listar(
            [FromQuery] string instance,
            [FromQuery] string? searchTerm,
            CancellationToken cancellationToken)
    {
        var query = new ListarServicosWhatsAppQuery(
            instance,
            searchTerm);

        var resultado = await _dispatcher.Query<
            ListarServicosWhatsAppQuery,
            ListarServicosWhatsAppDto>(
                query,
                cancellationToken);

        return Ok(resultado);
    }
}