using Application.Common.Middlewares.Handler;
using Application.Interfaces.Notificacoes;
using Application.Interfaces.Profissionais;
using Application.Operacional.Profissionais.DTOs;
using Application.Operacional.Profissionais.Queries;

namespace Application.Operacional.Profissionais.Queries.Handlers;

public sealed class ListarProfissionaisWhatsAppHandler
    : IQueryHandler<
        ListarProfissionaisWhatsAppQuery,
        ListarProfissionaisWhatsAppDto>
{
    private readonly IRepositorioConfiguracaoWhatsApp
        _repositorioConfiguracao;

    private readonly IRepositorioProfissionais _repositorio;

    public ListarProfissionaisWhatsAppHandler(
        IRepositorioConfiguracaoWhatsApp repositorioConfiguracao,
        IRepositorioProfissionais repositorio)
    {
        _repositorioConfiguracao = repositorioConfiguracao;
        _repositorio = repositorio;
    }

    public async Task<ListarProfissionaisWhatsAppDto> Handle(
        ListarProfissionaisWhatsAppQuery query,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(query.Instance))
            throw new ArgumentException(
                "A instância WhatsApp é obrigatória.");

        if (query.ServicoId == Guid.Empty)
            throw new ArgumentException(
                "O serviço informado é inválido.");

        var configuracao =
            await _repositorioConfiguracao.ObterPorInstanceNameAsync(
                query.Instance.Trim(),
                cancellationToken);

        if (configuracao is null || !configuracao.Ativo)
            throw new UnauthorizedAccessException(
                "Instância WhatsApp não encontrada ou inativa.");

        var profissionais =
            await _repositorio.ListarParaAgendamentoWhatsAppAsync(
                configuracao.EmpresaId,
                query.ServicoId,
                cancellationToken);

        var resultado = profissionais.Select(p =>
        {
            var vinculo = p.Servicos.First();

            return new ProfissionalAgendamentoWhatsAppDto(
                p.Id,
                $"{p.Nome} {p.Sobrenome}".Trim(),
                p.Especialidade,
                p.Foto,
                vinculo.ServicoId,
                vinculo.ValorPersonalizado
                    ?? vinculo.Servico.Valor,
                vinculo.DuracaoPersonalizada
                    ?? vinculo.Servico.DuracaoMinutos
            );
        }).ToArray();

        return new ListarProfissionaisWhatsAppDto(resultado);
    }
}