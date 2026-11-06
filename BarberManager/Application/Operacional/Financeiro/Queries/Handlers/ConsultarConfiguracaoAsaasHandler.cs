using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.DTOs;

namespace Application.Operacional.Financeiro.Queries.Handlers
{
    public class ConsultarConfiguracaoAsaasHandler
        : IQueryHandler<
            ConsultarConfiguracaoAsaasQuery,
            ConsultarConfiguracaoAsaasDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioConfiguracaoAssasClient _repositorio;

        public ConsultarConfiguracaoAsaasHandler(
            ITenantContext tenantContext,
            IRepositorioConfiguracaoAssasClient repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<ConsultarConfiguracaoAsaasDto> Handle(
            ConsultarConfiguracaoAsaasQuery query,
            CancellationToken cancellationToken)
        {
            // 1. Validar o contexto da empresa.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            var empresaId = _tenantContext.EmpresaId;

            // 2. Consultar a configuração da empresa autenticada.
            var configuracao =
                await _repositorio.ObterPorEmpresaAsync(
                    empresaId,
                    cancellationToken);

            // 3. Empresa ainda não configurou o Asaas.
            if (configuracao is null)
            {
                return new ConsultarConfiguracaoAsaasDto(
                    Configurado: false,
                    Ativo: false,
                    Ambiente: null,
                    DataUltimaValidacao: null);
            }

            // 4. Retornar somente informações não sensíveis.
            return new ConsultarConfiguracaoAsaasDto(
                Configurado: true,
                Ativo: configuracao.Ativo,
                Ambiente: configuracao.Ambiente.ToString(),
                DataUltimaValidacao: configuracao.DataUltimaValidacao);
        }
    }
}