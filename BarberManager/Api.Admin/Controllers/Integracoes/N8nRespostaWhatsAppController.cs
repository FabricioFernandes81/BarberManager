
using Application.Dispatcher;
using Application.Operacional.Notificacoes.Commands;
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
    private readonly IDispatcher _dispatcher;

    public N8nRespostaWhatsAppController(
        IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
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
        var command = new ProcessarRespostaWhatsAppCommand(
            Instance: request.Instance,
            Telefone: request.Telefone,
            Mensagem: request.Mensagem,
            MessageId: request.MessageId);

        var resultado = await _dispatcher.Command<
            ProcessarRespostaWhatsAppCommand,
            ProcessarRespostaWhatsAppResultadoDto>(
                command,
                cancellationToken);

        return Ok(resultado);
    }
}
