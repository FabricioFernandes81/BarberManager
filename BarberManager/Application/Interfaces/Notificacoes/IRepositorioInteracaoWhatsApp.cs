using Domain.Entities.Notificacoes;

namespace Application.Interfaces.Notificacoes;

public interface IRepositorioInteracaoWhatsApp
{
    Task AdicionarAsync(
        InteracaoWhatsAppPendente interacao,
        CancellationToken cancellationToken);

    Task<InteracaoWhatsAppPendente?> ObterInteracaoAtivaAsync(
        Guid empresaId,
        string instanceName,
        string telefone,
        CancellationToken cancellationToken);

    Task<InteracaoWhatsAppPendente?> ObterInteracaoParaAtualizacaoAsync(
        Guid empresaId,
        string instanceName,
        string telefone,
        Guid interacaoId,
        CancellationToken cancellationToken);

    Task<bool> MensagemJaProcessadaAsync(
    Guid empresaId,
    string providerMessageId,
    CancellationToken cancellationToken);

    Task AdicionarRespostaProcessadaAsync(
        RespostaWhatsAppProcessada resposta,
        CancellationToken cancellationToken);
}