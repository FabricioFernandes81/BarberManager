using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Agendamentos;
using Application.Operacional.Agendamentos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.Queries.Handlers
{
    public sealed class ObterAgendaPeriodoHandler
      : IQueryHandler<
          ObterAgendaPeriodoQuery,
          ObterAgendaPeriodoDto>
    {
        private readonly IRepositorioGerenciarAgendamento _repositorio;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public ObterAgendaPeriodoHandler(
            IRepositorioGerenciarAgendamento repositorio,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorio = repositorio;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<ObterAgendaPeriodoDto> Handle(
            ObterAgendaPeriodoQuery query,
            CancellationToken cancellationToken)
        {
            if (!_usuarioLogado.IsAuthenticated)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "O usuário precisa estar autenticado."
                    });
            }

            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Não foi possível identificar a empresa do usuário."
                    });
            }

            if (query.Inicio == default ||
                query.Fim == default)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "As datas inicial e final são obrigatórias."
                    });
            }

            if (query.Inicio > query.Fim)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "A data inicial não pode ser posterior à data final."
                    });
            }

            if (query.PageNumber < 1)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "O número da página deve ser maior que zero."
                    });
            }

            if (query.PageSize < 1 || query.PageSize > 100)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "O tamanho da página deve estar entre 1 e 100."
                    });
            }

            return await _repositorio.ObterAgendaPeriodoAsync(
                _tenantContext.EmpresaId,
                query,
                cancellationToken);
        }
    }
}
