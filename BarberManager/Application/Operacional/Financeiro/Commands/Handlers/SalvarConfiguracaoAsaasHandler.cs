using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.DTOs;
using Domain.Entities.Financeiro;

namespace Application.Operacional.Financeiro.Commands.Handlers
{
    public sealed class SalvarConfiguracaoAsaasHandler
        : ICommandHandler<
            SalvarConfiguracaoAsaasCommand,
            ConfiguracaoAsaasDto>
    {
        private readonly IRepositorioConfiguracaoAssasClient _repositorio;
        private readonly ITenantContext _tenantContext;

        public SalvarConfiguracaoAsaasHandler(
            IRepositorioConfiguracaoAssasClient repositorio,
            ITenantContext tenantContext)
        {
            _repositorio = repositorio;
            _tenantContext = tenantContext;
        }

        public async Task<ConfiguracaoAsaasDto> Handle(
            SalvarConfiguracaoAsaasCommand command,
            CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            if (string.IsNullOrWhiteSpace(command.ApiTokenProtegido) ||
                string.IsNullOrWhiteSpace(command.WebhooksTokenProtegido))
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "As credenciais protegidas são obrigatórias."
                    });
            }

            if (!Enum.IsDefined(typeof(AmbienteAsaas), command.Ambiente))
            {
                throw new CustomValidationException(
                    new[] { "O ambiente Asaas informado é inválido." });
            }

            cancellationToken.ThrowIfCancellationRequested();

            var empresaId = _tenantContext.EmpresaId;

            var configuracao =
                await _repositorio.ObterPorEmpresaAsync(
                    empresaId,
                    cancellationToken);

            if (configuracao is null)
            {
                configuracao = new ConfiguracaoAssasClient
                {
                    EmpresaId = empresaId,
                    ApiTokenProtegido = command.ApiTokenProtegido,
                    WebhooksTokenProtegido = command.WebhooksTokenProtegido,
                    Ambiente = command.Ambiente,
                    Ativo = true,
                    DataUltimaValidacao = command.DataUltimaValidacao
                };

                await _repositorio.AdicionarAsync(
                    configuracao,
                    cancellationToken);
            }
            else
            {
                configuracao.ApiTokenProtegido =
                    command.ApiTokenProtegido;

                configuracao.WebhooksTokenProtegido =
                    command.WebhooksTokenProtegido;

                configuracao.Ambiente = command.Ambiente;
                configuracao.Ativo = true;
                configuracao.DataUltimaValidacao =
                    command.DataUltimaValidacao;
            }

            return new ConfiguracaoAsaasDto(
                Configurado: true,
                Ativo: configuracao.Ativo,
                Ambiente: configuracao.Ambiente,
                DataUltimaValidacao:
                    configuracao.DataUltimaValidacao);
        }
    }
}