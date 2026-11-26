using Application.Common.Middlewares.Handler;
using Application.Interfaces.Notificacoes;
using Application.Interfaces.Servicos;
using Application.Operacional.Servicos.DTOs;

namespace Application.Operacional.Servicos.Queries.Handlers;

public sealed class ListarServicosWhatsAppHandler
    : IQueryHandler<
        ListarServicosWhatsAppQuery,
        ListarServicosWhatsAppDto>
{
    private readonly IRepositorioConfiguracaoWhatsApp
        _repositorioConfiguracao;

    private readonly IRepositorioServico _repositorioServico;

    public ListarServicosWhatsAppHandler(
        IRepositorioConfiguracaoWhatsApp repositorioConfiguracao,
        IRepositorioServico repositorioServico)
    {
        _repositorioConfiguracao = repositorioConfiguracao;
        _repositorioServico = repositorioServico;
    }

    public async Task<ListarServicosWhatsAppDto> Handle(
        ListarServicosWhatsAppQuery query,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(query.Instance))
            throw new ArgumentException(
                "A instância WhatsApp é obrigatória.");

        var configuracao =
            await _repositorioConfiguracao.ObterPorInstanceNameAsync(
                query.Instance.Trim(),
                cancellationToken);

        if (configuracao is null || !configuracao.Ativo)
            throw new UnauthorizedAccessException(
                "Instância WhatsApp não encontrada ou inativa.");

        var servicos =
            await _repositorioServico.ListarDisponiveisWhatsAppAsync(
                configuracao.EmpresaId,
                query.SearchTerm,
                cancellationToken);

        var itens = servicos.Select(servico =>
            new ServicoAgendamentoWhatsAppDto(
                servico.Id,
                servico.Nome,
                servico.Descricao,
                servico.Valor,
                servico.DuracaoMinutos,
                servico.IntervaloMinutos,
                servico.CategoriaServico?.Nome
            )).ToArray();

        return new ListarServicosWhatsAppDto(itens);
    }
}