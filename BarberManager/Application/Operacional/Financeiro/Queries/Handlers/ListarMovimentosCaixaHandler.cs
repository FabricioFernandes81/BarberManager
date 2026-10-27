using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Financeiro.Queries.Handlers
{
    public class ListarMovimentosCaixaHandler
     : IQueryHandler<ListarMovimentosCaixaQuery, ListarMovimentosCaixaDto>
    {
        private readonly IRepositorioCaixa _repositorio;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public ListarMovimentosCaixaHandler(
            IRepositorioCaixa repositorio,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorio = repositorio;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<ListarMovimentosCaixaDto> Handle(
            ListarMovimentosCaixaQuery query,
            CancellationToken cancellationToken)
        {
            if (!_usuarioLogado.IsAuthenticated ||
                _usuarioLogado.UsuarioId == Guid.Empty)
            {
                throw Validacao("O usuário precisa estar autenticado.");
            }

            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw Validacao("Não foi possível identificar a empresa do usuário.");
            }

            if (query.PageNumber < 1)
                throw Validacao("O número da página deve ser maior que zero.");

            if (query.PageSize is < 1 or > 100)
                throw Validacao("O tamanho da página deve estar entre 1 e 100.");

            if (query.DataInicio.HasValue &&
                query.DataFim.HasValue &&
                query.DataInicio.Value.Date > query.DataFim.Value.Date)
            {
                throw Validacao("A data inicial não pode ser posterior à data final.");
            }

            return await _repositorio.ListarMovimentosAsync(
                _tenantContext.EmpresaId,
                _usuarioLogado.UsuarioId,
                query.DataInicio,
                query.DataFim,
                query.TipoMovimento,
                query.PageNumber,
                query.PageSize,
                cancellationToken);
        }

        private static CustomValidationException Validacao(string mensagem) =>
            new(new[] { mensagem });
    }
}
