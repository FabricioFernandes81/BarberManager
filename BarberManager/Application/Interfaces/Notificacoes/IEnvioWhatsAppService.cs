using Application.Operacional.Notificacoes.DTOs;

namespace Application.Interfaces.Notificacoes;

public interface IEnvioWhatsAppService
{
    Task<EnviarMensagemWhatsAppResultadoDto> EnviarAsync(
        EnviarMensagemWhatsAppRequest request,
        CancellationToken cancellationToken);
}