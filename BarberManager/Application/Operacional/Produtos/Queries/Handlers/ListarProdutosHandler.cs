using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Produtos;
using Application.Operacional.Produtos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Produtos.Queries.Handlers
{
    public sealed class ListarProdutosHandler
     : IQueryHandler<ListarProdutosQuery, ListarProdutosDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioProdutos _repositorio;

        public ListarProdutosHandler(
            ITenantContext tenantContext,
            IRepositorioProdutos repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<ListarProdutosDto> Handle(
            ListarProdutosQuery query,
            CancellationToken cancellationToken)
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
                    new[]
                    {
                    "O tamanho da página deve estar entre 1 e 100."
                    });
            }

            if (query.CategoriaId == Guid.Empty)
            {
                throw new CustomValidationException(
                    new[] { "A categoria informada é inválida." });
            }

            if (query.MarcaId == Guid.Empty)
            {
                throw new CustomValidationException(
                    new[] { "A marca informada é inválida." });
            }

            var empresaId = _tenantContext.EmpresaId;

            var searchTerm = string.IsNullOrWhiteSpace(query.SearchTerm)
                ? null
                : query.SearchTerm.Trim();

            var (produtos, totalRegistros) =
                await _repositorio.ListarAsync(
                    empresaId,
                    query.PageNumber,
                    query.PageSize,
                    searchTerm,
                    query.CategoriaId,
                    query.MarcaId,
                    query.Ativo,
                    cancellationToken);

            var itens = produtos
                .Select(p => new ProdutoListaDto(
                    p.Id,
                    p.Nome,
                    p.CodigoInterno,
                    p.CodigoBarras,
                    p.CategoriaProduto?.Nome,
                    p.Marca?.Nome,
                    p.ValorVenda,
                    p.Custo,
                    p.ControlaEstoque,
                    p.Estoque?.QuantidadeAtual,
                    p.Estoque?.EstoqueMinimo,
                    p.Ativo,
                    p.DataCadastro))
                .ToArray();

            var totalPaginas = (int)Math.Ceiling(
                totalRegistros / (double)query.PageSize);

            return new ListarProdutosDto(
                itens,
                query.PageNumber,
                query.PageSize,
                totalRegistros,
                totalPaginas);
        }
    }
}
