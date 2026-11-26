using Application.Common.Middlewares.Handler;
using Application.Interfaces.Agendamentos;
using Application.Interfaces.Notificacoes;
using Application.Operacional.Agendamentos.DTOs;
using Application.Operacional.Agendamentos.Queries;

namespace Application.Operacional.Agendamentos.Queries.Handlers;

public sealed class ListarDisponibilidadeWhatsAppHandler
    : IQueryHandler<
        ListarDisponibilidadeWhatsAppQuery,
        DisponibilidadeAgendaDto>
{
    private readonly IRepositorioConfiguracaoWhatsApp _repositorioConfiguracao;
    private readonly IDisponibilidadeAgendaService _disponibilidadeService;

    public ListarDisponibilidadeWhatsAppHandler(
        IRepositorioConfiguracaoWhatsApp repositorioConfiguracao,
        IDisponibilidadeAgendaService disponibilidadeService)
    {
        _repositorioConfiguracao = repositorioConfiguracao;
        _disponibilidadeService = disponibilidadeService;
    }

    public async Task<DisponibilidadeAgendaDto> Handle(
        ListarDisponibilidadeWhatsAppQuery query,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(query.Instance))
            throw new ArgumentException(
                "A instância WhatsApp é obrigatória.");

        if (query.ProfissionalId == Guid.Empty ||
            query.ServicoId == Guid.Empty)
            throw new ArgumentException(
                "Profissional e serviço devem ser informados.");

        var configuracao =
            await _repositorioConfiguracao.ObterPorInstanceNameAsync(
                query.Instance.Trim(),
                cancellationToken);

        if (configuracao is null || !configuracao.Ativo)
            throw new UnauthorizedAccessException(
                "Instância WhatsApp não encontrada ou inativa.");

        return await _disponibilidadeService.ObterAsync(
            configuracao.EmpresaId,
            query.ProfissionalId,
            query.ServicoId,
            query.Data,
            cancellationToken);
    }
}