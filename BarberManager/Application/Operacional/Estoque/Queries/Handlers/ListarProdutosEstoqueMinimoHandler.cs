using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Estoque;
using Application.Operacional.Estoque.DTOs;
using Application.Operacional.Estoque.Queries;

namespace Application.Operacional.Estoque.Queries.Handlers
{
    public sealed class ListarProdutosEstoqueMinimoHandler
        : IQueryHandler<
            ListarProdutosEstoqueMinimoQuery,
            ListarProdutosEstoqueMinimoDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioEstoque _repositorio;

        public ListarProdutosEstoqueMinimoHandler(
            ITenantContext tenantContext,
            IRepositorioEstoque repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<ListarProdutosEstoqueMinimoDto> Handle(
            ListarProdutosEstoqueMinimoQuery query,
            CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            var erros = new List<string>();

            if (query.PageNumber < 1)
            {
                erros.Add("A página deve ser maior que zero.");
            }

            if (query.PageSize < 1 || query.PageSize > 100)
            {
                erros.Add(
                    "O tamanho da página deve estar entre 1 e 100.");
            }

            if (erros.Count > 0)
            {
                throw new CustomValidationException(erros);
            }

            var (produtos, totalRegistros) =
                await _repositorio.ListarProdutosEstoqueMinimoAsync(
                    _tenantContext.EmpresaId,
                    query.PageNumber,
                    query.PageSize,
                    cancellationToken);

            var itens = produtos
                .Where(p => p.Estoque != null)
                .Select(p => new ProdutoEstoqueMinimoDto(
                    p.Id,
                    p.Nome,
                    p.CodigoInterno,
                    p.Estoque!.QuantidadeAtual,
                    p.Estoque.EstoqueMinimo,
                    Math.Max(
                        0,
                        p.Estoque.EstoqueMinimo -
                        p.Estoque.QuantidadeAtual)))
                .ToArray();

            var totalPaginas = (int)Math.Ceiling(
                totalRegistros / (double)query.PageSize);

            return new ListarProdutosEstoqueMinimoDto(
                itens,
                query.PageNumber,
                query.PageSize,
                totalRegistros,
                totalPaginas);
        }
    }
}