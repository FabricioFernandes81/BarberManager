using Application.Dispatcher;
using Application.Operacional.Notificacoes.Commands;
using Application.Operacional.Notificacoes.DTOs;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers.Integracoes;

[ApiController]
[Route("api/integracoes/n8n/conexao-whatsapp")]
[Authorize(
    AuthenticationSchemes =
        N8nServiceDefaults.AuthenticationScheme)]
public sealed class N8nConexaoWhatsAppController : ControllerBase
{
    private readonly IDispatcher _dispatcher;

    public N8nConexaoWhatsAppController(
        IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    /// <summary>
    /// Sincroniza o estado da conexão da instância WhatsApp
    /// recebido da Evolution API através do n8n.
    /// </summary>
    [HttpPost("processar")]
    [ProducesResponseType(
        typeof(SincronizarConexaoWhatsAppResultadoDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<
        SincronizarConexaoWhatsAppResultadoDto>> Processar(
        [FromBody] SincronizarConexaoWhatsAppRequest request,
        CancellationToken cancellationToken)
    {
        var command = new SincronizarConexaoWhatsAppCommand(
            Instance: request.Instance,
            State: request.State,
            StatusReason: request.StatusReason,
            Wuid: request.Wuid);

        var resultado = await _dispatcher.Command<
            SincronizarConexaoWhatsAppCommand,
            SincronizarConexaoWhatsAppResultadoDto>(
                command,
                cancellationToken);

        return Ok(resultado);
    }
}