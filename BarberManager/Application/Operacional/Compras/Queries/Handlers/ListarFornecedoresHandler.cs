using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Compras;
using Application.Operacional.Compras.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.Queries.Handlers
{
    public sealed class ListarFornecedoresHandler
       : IQueryHandler<ListarFornecedoresQuery, ListarFornecedoresDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioFornecedor _repositorio;

        public ListarFornecedoresHandler(
            ITenantContext tenantContext,
            IRepositorioFornecedor repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<ListarFornecedoresDto> Handle(
            ListarFornecedoresQuery query,
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

            var empresaId = _tenantContext.EmpresaId;

            var searchTerm = string.IsNullOrWhiteSpace(query.SearchTerm)
                ? null
                : query.SearchTerm.Trim();

            var (fornecedores, totalRegistros) =
                await _repositorio.ListarAsync(
                    empresaId,
                    query.PageNumber,
                    query.PageSize,
                    searchTerm,
                    query.Ativo,
                    cancellationToken);

            var itens = fornecedores
                .Select(f => new FornecedorListaDto(
                    f.Id,
                    f.Nome,
                    f.NomeFantasia,
                    f.Documento,
                    f.Email,
                    f.Telefone,
                    f.Ativo,
                    f.DataCadastro))
                .ToArray();

            var totalPaginas = (int)Math.Ceiling(
                totalRegistros / (double)query.PageSize);

            return new ListarFornecedoresDto(
                itens,
                query.PageNumber,
                query.PageSize,
                totalRegistros,
                totalPaginas);
        }
    }
}
