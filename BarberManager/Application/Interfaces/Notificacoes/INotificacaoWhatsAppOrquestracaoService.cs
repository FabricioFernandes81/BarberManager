using Application.Operacional.Notificacoes.DTOs;

namespace Application.Interfaces.Notificacoes;

public interface INotificacaoWhatsAppOrquestracaoService
{
    Task<ReservarEnvioWhatsAppResultadoDto> ReservarAsync(
        ReservarEnvioWhatsAppRequest request,
        CancellationToken cancellationToken);

    Task<bool> RegistrarAceiteAsync(
        Guid outboxEventId,
        string? providerMessageId,
        CancellationToken cancellationToken);

    Task<bool> RegistrarFalhaAsync(
        Guid outboxEventId,
        string erro,
        CancellationToken cancellationToken);

    Task<bool> RegistrarResultadoIndeterminadoAsync(
        Guid outboxEventId,
        string motivo,
        CancellationToken cancellationToken);
}