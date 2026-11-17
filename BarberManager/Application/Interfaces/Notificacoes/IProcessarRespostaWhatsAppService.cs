using Application.Operacional.Notificacoes.DTOs;

namespace Application.Interfaces.Notificacoes;

public interface IProcessarRespostaWhatsAppService
{
    Task<ProcessarRespostaWhatsAppResultadoDto> ProcessarAsync(
        ProcessarRespostaWhatsAppRequest request,
        CancellationToken cancellationToken);
}