using Domain.Entities.Notificacoes;

namespace Application.Interfaces.Notificacoes;

public interface IRepositorioNotificacaoWhatsAppEnvio
{
    /// <summary>
    /// Obtém o registro de envio associado ao evento da Outbox,
    /// respeitando o isolamento por empresa.
    /// </summary>
    Task<NotificacaoWhatsAppEnvio?> ObterPorEventoAsync(
        Guid empresaId,
        Guid outboxEventId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Tenta reservar atomicamente o envio de uma notificação.
    /// Deve criar o registro quando ainda não existir e impedir
    /// que dois processos reservem o mesmo evento simultaneamente.
    /// </summary>
    /// <returns>
    /// O registro reservado ou null quando outro processo já
    /// reservou o envio ou ele não está elegível.
    /// </returns>
    Task<NotificacaoWhatsAppEnvio?> TentarReservarEnvioAsync(
        Guid empresaId,
        Guid outboxEventId,
        string hashConteudo,
        DateTime agoraUtc,
        DateTime leaseExpiraEmUtc,
        CancellationToken cancellationToken);

    /// <summary>
    /// Registra que o provedor aceitou a mensagem.
    /// </summary>
    Task<bool> RegistrarAceiteAsync(
        Guid empresaId,
        Guid outboxEventId,
        string? providerMessageId,
        DateTime dataAceiteUtc,
        CancellationToken cancellationToken);

    /// <summary>
    /// Registra uma falha de envio confirmada.
    /// </summary>
    Task<bool> RegistrarFalhaAsync(
        Guid empresaId,
        Guid outboxEventId,
        string erro,
        DateTime dataTentativaUtc,
        CancellationToken cancellationToken);

    /// <summary>
    /// Registra resultado indeterminado quando não é possível
    /// confirmar se o provedor recebeu a mensagem.
    /// </summary>
    Task<bool> RegistrarResultadoIndeterminadoAsync(
        Guid empresaId,
        Guid outboxEventId,
        string motivo,
        DateTime dataTentativaUtc,
        CancellationToken cancellationToken);
}