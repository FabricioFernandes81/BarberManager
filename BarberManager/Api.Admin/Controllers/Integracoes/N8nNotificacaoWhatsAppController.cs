using Application.Interfaces.Notificacoes;
using Application.Operacional.Notificacoes.DTOs;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Admin.Controllers.Integracoes;

[ApiController]
[Route("api/integracoes/n8n/notificacoes-whatsapp")]
[Authorize(
    AuthenticationSchemes =
        N8nServiceDefaults.AuthenticationScheme)]
public sealed class N8nNotificacaoWhatsAppController : ControllerBase
{
    private readonly INotificacaoWhatsAppOrquestracaoService _service;

    public N8nNotificacaoWhatsAppController(
        INotificacaoWhatsAppOrquestracaoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Reserva o processamento de uma notificação WhatsApp.
    /// </summary>
    [HttpPost("reservar")]
    [ProducesResponseType(
        typeof(ReservarEnvioWhatsAppResultadoDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ReservarEnvioWhatsAppResultadoDto>>
        Reservar(
            [FromBody] ReservarEnvioWhatsAppRequest request,
            CancellationToken cancellationToken)
    {
        var resultado = await _service.ReservarAsync(
            request,
            cancellationToken);

        return Ok(resultado);
    }

    /// <summary>
    /// Registra o aceite da mensagem pelo provedor.
    /// </summary>
    [HttpPost("registrar-aceite")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegistrarAceite(
        [FromBody] RegistrarAceiteWhatsAppRequest request,
        CancellationToken cancellationToken)
    {
        var registrado = await _service.RegistrarAceiteAsync(
            request.OutboxEventId,
            request.ProviderMessageId,
            cancellationToken);

        if (!registrado)
            return Conflict(new
            {
                mensagem = "Não foi possível registrar o aceite. " +
                           "Verifique o estado atual do envio."
            });

        return Ok(new { registrado = true });
    }

    /// <summary>
    /// Registra uma falha de envio confirmada.
    /// </summary>
    [HttpPost("registrar-falha")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegistrarFalha(
        [FromBody] RegistrarFalhaWhatsAppRequest request,
        CancellationToken cancellationToken)
    {
        var registrado = await _service.RegistrarFalhaAsync(
            request.OutboxEventId,
            request.Erro,
            cancellationToken);

        if (!registrado)
            return Conflict(new
            {
                mensagem = "Não foi possível registrar a falha. " +
                           "Verifique o estado atual do envio."
            });

        return Ok(new { registrado = true });
    }

    /// <summary>
    /// Registra um resultado indeterminado.
    /// Não deve provocar reenvio automático.
    /// </summary>
    [HttpPost("registrar-resultado-indeterminado")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegistrarResultadoIndeterminado(
        [FromBody] RegistrarResultadoIndeterminadoWhatsAppRequest request,
        CancellationToken cancellationToken)
    {
        var registrado =
            await _service.RegistrarResultadoIndeterminadoAsync(
                request.OutboxEventId,
                request.Motivo,
                cancellationToken);

        if (!registrado)
            return Conflict(new
            {
                mensagem = "Não foi possível registrar o resultado " +
                           "indeterminado. Verifique o estado atual do envio."
            });

        return Ok(new { registrado = true });
    }
}