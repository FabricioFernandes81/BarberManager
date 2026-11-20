using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Notificacoes;
using Application.Operacional.Notificacoes.DTOs;
using Domain.Entities.Operacional.Enums;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace Application.Operacional.Notificacoes.Commands.Handlers;

public sealed class CriarInstanciaWhatsAppHandler
    : ICommandHandler<CriarInstanciaWhatsAppCommand, CriarInstanciaWhatsAppDto>
{
    private readonly ITenantContext _tenantContext;
    private readonly IRepositorioConfiguracaoWhatsApp _repositorio;
    private readonly IEvolutionInstanceClient _evolutionClient;
    private readonly IProtecaoCredenciaisWhatsApp _protecao;
   
    private readonly ILogger<CriarInstanciaWhatsAppHandler> _logger;

    public CriarInstanciaWhatsAppHandler(
    ITenantContext tenantContext,
    IRepositorioConfiguracaoWhatsApp repositorio,
    IEvolutionInstanceClient evolutionClient,
    IProtecaoCredenciaisWhatsApp protecao,
    ILogger<CriarInstanciaWhatsAppHandler> logger)
    {
        _tenantContext = tenantContext;
        _repositorio = repositorio;
        _evolutionClient = evolutionClient;
        _protecao = protecao;
        _logger = logger;
    }

    public async Task<CriarInstanciaWhatsAppDto> Handle(
        CriarInstanciaWhatsAppCommand command,
        CancellationToken cancellationToken)
    {
        if (!_tenantContext.IsAvailable ||
            _tenantContext.EmpresaId == Guid.Empty)
        {
            throw new UnauthorizedAccessException(
                "Não foi possível identificar a empresa autenticada.");
        }

        var empresaId = _tenantContext.EmpresaId;

        var configuracao = await _repositorio.ObterPorEmpresaAsync(
            empresaId,
            cancellationToken);

        if (configuracao is not null)
        {
            if (configuracao.StatusConexao is
                StatusConexaoWhatsApp.Provisionando or
                StatusConexaoWhatsApp.AguardandoConexao or
                StatusConexaoWhatsApp.Conectado)
            {
                throw new InvalidOperationException(
                    "Já existe uma instância WhatsApp em processo de provisionamento ou ativa para esta empresa.");
            }

            if (configuracao.StatusConexao ==
                StatusConexaoWhatsApp.Desconectado)
            {
                throw new InvalidOperationException(
                    "A instância existente está desconectada. A rotina de reconexão será implementada separadamente.");
            }
        }

        var instanceName = $"barber_{empresaId:N}";

        var instanceToken = WebEncoders.Base64UrlEncode(
            RandomNumberGenerator.GetBytes(48));

        if (configuracao is null)
        {
            configuracao = new ConfiguracaoWhatsApp
            {
                EmpresaId = empresaId,
                InstanceName = instanceName,
                BaseUrl = _evolutionClient.BaseUrl,
                Ativo = true,
                StatusConexao = StatusConexaoWhatsApp.Provisionando
            };

            await _repositorio.AdicionarAsync(
                configuracao,
                cancellationToken);
        }
        else
        {
            configuracao.InstanceName = instanceName;
            configuracao.BaseUrl = _evolutionClient.BaseUrl;
            configuracao.Ativo = true;
            configuracao.StatusConexao =
                StatusConexaoWhatsApp.Provisionando;

            await _repositorio.AtualizarAsync(
                configuracao,
                cancellationToken);
        }

        try
        {
            var resultado = await _evolutionClient.CriarInstanciaAsync(
                instanceName,
                instanceToken,
                cancellationToken);

            configuracao.ApiKeyProtegida =
                _protecao.Proteger(instanceToken);

            configuracao.StatusConexao =
                StatusConexaoWhatsApp.AguardandoConexao;

            await _repositorio.AtualizarAsync(
                configuracao,
                cancellationToken);

            return new CriarInstanciaWhatsAppDto(
                configuracao.Id,
                resultado.InstanceName,
                resultado.Status,
                resultado.QrCode,
                resultado.QrCodeBase64
            );
        }
        catch (Exception ex)
        {
            configuracao.StatusConexao =
                StatusConexaoWhatsApp.FalhaProvisionamento;

            try
            {
                await _repositorio.AtualizarAsync(
                    configuracao,
                    cancellationToken);
            }
            catch (Exception persistenciaEx)
            {
                _logger.LogError(
                    persistenciaEx,
                    "Falha ao persistir o estado de erro do provisionamento WhatsApp. EmpresaId: {EmpresaId}",
                    empresaId);
            }

            _logger.LogError(
                ex,
                "Falha ao provisionar instância WhatsApp. EmpresaId: {EmpresaId}",
                empresaId);

            throw;
        }
    }
}