using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Notificacoes;
using Application.Operacional.Notificacoes.DTOs;

namespace Application.Operacional.Notificacoes.Commands.Handlers;

public sealed class AtualizarConfiguracaoWhatsAppHandler
    : ICommandHandler<AtualizarConfiguracaoWhatsAppCommand, ConfiguracaoWhatsAppDto>
{
    private readonly ITenantContext _tenantContext;
    private readonly IRepositorioConfiguracaoWhatsApp _repositorio;

    public AtualizarConfiguracaoWhatsAppHandler(
        ITenantContext tenantContext,
        IRepositorioConfiguracaoWhatsApp repositorio)
    {
        _tenantContext = tenantContext;
        _repositorio = repositorio;
    }

    public async Task<ConfiguracaoWhatsAppDto> Handle(
        AtualizarConfiguracaoWhatsAppCommand command,
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

        string? numeroWhatsApp = null;

        if (!string.IsNullOrWhiteSpace(command.NumeroWhatsApp))
        {
            numeroWhatsApp = new string(
                command.NumeroWhatsApp
                    .Where(char.IsDigit)
                    .ToArray());

            if (numeroWhatsApp.Length is < 10 or > 15)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "O número WhatsApp deve possuir entre 10 e 15 dígitos, incluindo o código do país."
                    });
            }
        }

        configuracao.NumeroWhatsApp = numeroWhatsApp;
        configuracao.Ativo = command.Ativo;

        await _repositorio.AtualizarAsync(
            configuracao,
            cancellationToken);

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