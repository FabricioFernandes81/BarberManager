
using Application.Interfaces.Notificacoes;
using Application.Operacional.Notificacoes.DTOs;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers.Integracoes;

[ApiController]
[Route("api/integracoes/n8n/respostas-whatsapp")]
[Authorize(
    AuthenticationSchemes =
        N8nServiceDefaults.AuthenticationScheme)]
public sealed class N8nRespostaWhatsAppController : ControllerBase
{
    private readonly IProcessarRespostaWhatsAppService _service;

    public N8nRespostaWhatsAppController(
        IProcessarRespostaWhatsAppService service)
    {
        _service = service;
    }

    /// <summary>
    /// Processa uma resposta recebida pelo WhatsApp.
    /// </summary>
    [HttpPost("processar")]
    [ProducesResponseType(
        typeof(ProcessarRespostaWhatsAppResultadoDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ProcessarRespostaWhatsAppResultadoDto>>
        Processar(
            [FromBody] ProcessarRespostaWhatsAppRequest request,
            CancellationToken cancellationToken)
    {
        var resultado = await _service.ProcessarAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }
}
