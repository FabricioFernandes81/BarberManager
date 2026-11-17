using Domain.Entities.Agendamentos;
using Domain.Entities.Notificacoes;

namespace Application.Interfaces.Notificacoes;

public interface IRepositorioRespostaWhatsApp
{
    Task<Guid?> ObterEmpresaPorInstanciaAsync(
        string instance,
        CancellationToken cancellationToken);

    Task<List<Guid>> ObterClientesPorTelefoneAsync(
        Guid empresaId,
        string telefoneNormalizado,
        CancellationToken cancellationToken);

    Task<List<Agendamento>> ObterAgendamentosElegiveisAsync(
        Guid empresaId,
        IReadOnlyCollection<Guid> clientesIds,
        CancellationToken cancellationToken);

    Task<Agendamento?> ObterAgendamentoParaAtualizacaoAsync(
        Guid empresaId,
        Guid agendamentoId,
        CancellationToken cancellationToken);

    Task InvalidarLembretesPendentesAsync(
        Guid empresaId,
        Guid agendamentoId,
        string motivo,
        CancellationToken cancellationToken);

    Task<bool> MensagemJaProcessadaAsync(
    Guid empresaId,
    string providerMessageId,
    CancellationToken cancellationToken);

    Task AdicionarRespostaProcessadaAsync(
        RespostaWhatsAppProcessada resposta,
        CancellationToken cancellationToken);
}