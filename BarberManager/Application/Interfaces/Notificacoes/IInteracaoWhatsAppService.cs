
using Application.Operacional.Notificacoes.DTOs;
using Domain.Common.Enums;

namespace Application.Interfaces.Notificacoes;

public interface IInteracaoWhatsAppService
{
    Task<Guid> CriarInteracaoAsync(
        Guid empresaId,
        Guid clienteId,
        string instanceName,
        string telefone,
        TipoInteracaoWhatsApp tipoInteracao,
        string messageIdOrigem,
        IReadOnlyCollection<AgendamentoElegivelRespostaWhatsAppDto> agendamentos,
        CancellationToken cancellationToken);

    Task<ResultadoInteracaoWhatsAppDto> SelecionarOpcaoAsync(
        Guid empresaId,
        string instanceName,
        string telefone,
        int ordem,
        string providerMessageId,
        CancellationToken cancellationToken);

    Task<ResultadoInteracaoWhatsAppDto> CancelarInteracaoAsync(
        Guid empresaId,
        string instanceName,
        string telefone,
        string providerMessageId,
        CancellationToken cancellationToken);
    Task<ResultadoInteracaoWhatsAppDto> ConcluirInteracaoAsync(
    Guid empresaId,
    string instanceName,
    string telefone,
    string providerMessageId,
    CancellationToken cancellationToken);
}
