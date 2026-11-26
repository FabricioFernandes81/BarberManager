using Application.Dispatcher;
using Application.Operacional.Profissionais.DTOs;
using Application.Operacional.Profissionais.Queries;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers.Integracoes;

[ApiController]
[Route("api/integracoes/n8n/profissionais")]
[Authorize(
    AuthenticationSchemes =
        N8nServiceDefaults.AuthenticationScheme)]
public sealed class N8nProfissionaisController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public N8nProfissionaisController(
        IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    public async Task<ActionResult<ListarProfissionaisWhatsAppDto>>
        Listar(
            [FromQuery] string instance,
            [FromQuery] Guid servicoId,
            CancellationToken cancellationToken)
    {
        var query = new ListarProfissionaisWhatsAppQuery(
            instance,
            servicoId);

        var resultado =
            await _dispatcher.Query<
                ListarProfissionaisWhatsAppQuery,
                ListarProfissionaisWhatsAppDto>(
                    query,
                    cancellationToken);

        return Ok(resultado);
    }
}