using Domain.Entities.Financeiro;

namespace Application.Interfaces.Financeiro
{
    public interface IRepositorioEventoWebhookAsaas
    {
        Task<EventoWebhookAsaas?> ObterPorEventoAsync(
            Guid empresaId,
            string eventoId,
            CancellationToken cancellationToken);

        Task AdicionarAsync(
            EventoWebhookAsaas evento,
            CancellationToken cancellationToken);
    }
}
