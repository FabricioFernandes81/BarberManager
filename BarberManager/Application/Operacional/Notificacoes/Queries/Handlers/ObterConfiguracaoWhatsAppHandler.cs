using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Notificacoes;
using Application.Operacional.Notificacoes.DTOs;

namespace Application.Operacional.Notificacoes.Queries.Handlers;

public sealed class ObterConfiguracaoWhatsAppHandler
    : IQueryHandler<
        ObterConfiguracaoWhatsAppQuery,
        ConfiguracaoWhatsAppDto>
{
    private readonly ITenantContext _tenantContext;
    private readonly IRepositorioConfiguracaoWhatsApp _repositorio;

    public ObterConfiguracaoWhatsAppHandler(
        ITenantContext tenantContext,
        IRepositorioConfiguracaoWhatsApp repositorio)
    {
        _tenantContext = tenantContext;
        _repositorio = repositorio;
    }

    public async Task<ConfiguracaoWhatsAppDto> Handle(
        ObterConfiguracaoWhatsAppQuery query,
        CancellationToken cancellationToken)
    {
        if (!_tenantContext.IsAvailable ||
            _tenantContext.EmpresaId == Guid.Empty)
        {
            throw new UnauthorizedAccessException(
                "Não foi possível identificar a empresa do usuário.");
        }

        var configuracao = await _repositorio.ObterPorEmpresaAsync(
            _tenantContext.EmpresaId,
            cancellationToken);

        if (configuracao is null)
        {
            throw new CustomValidationException(
                new[] { "Configuração WhatsApp não encontrada." });
        }

        return new ConfiguracaoWhatsAppDto(
            configuracao.Id,
            configuracao.InstanceName,
            configuracao.NumeroWhatsApp,
            configuracao.StatusConexao,
            configuracao.Ativo,
            configuracao.DataConexao
        );
    }
}