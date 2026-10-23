using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Profissionais;
using Application.Operacional.Profissionais.DTOs;

namespace Application.Operacional.Profissionais.Queries.Handlers
{
    public class ListarProfissionaisHandler : IQueryHandler<ListarProfissionaisQuery, ListarProfissionaisDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioProfissionais _repositorio;

        public ListarProfissionaisHandler(ITenantContext tenantContext,IRepositorioProfissionais repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<ListarProfissionaisDto> Handle(ListarProfissionaisQuery query, CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable ||
         _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            if (query.PageNumber < 1)
            {
                throw new CustomValidationException(
                    new[] { "A página deve ser maior que zero." });
            }

            if (query.PageSize < 1 || query.PageSize > 100)
            {
                throw new CustomValidationException(
                    new[] { "O tamanho da página deve estar entre 1 e 100." });
            }

            var empresaId = _tenantContext.EmpresaId;

            var (profissionais, totalRegistros) =
                await _repositorio.ListarAsync(
                    empresaId,
                    query.PageNumber,
                    query.PageSize,
                    query.SearchTerm,
                    query.Status,
                    cancellationToken);

            var itens = profissionais
                .Select(p => new ProfissionalListaDto(
                    p.Id,
                    p.Nome,
                    p.Sobrenome,
                    p.Celular,
                    p.Email,
                    p.Cargo,
                    p.Especialidade,
                    p.Foto,
                    p.Status.ToString(),
                    p.DataAdmissao,
                    p.AgendaOnline,
                    p.ExibirAgendamentoOnline,
                    p.OrdemAgenda))
                .ToArray();

            var totalPaginas = (int)Math.Ceiling(
                totalRegistros / (double)query.PageSize);

            return new ListarProfissionaisDto(
                itens,
                query.PageNumber,
                query.PageSize,
                totalRegistros,
                totalPaginas);
        }
    }
}

