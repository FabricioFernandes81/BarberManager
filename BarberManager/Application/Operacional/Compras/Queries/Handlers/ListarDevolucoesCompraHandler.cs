
using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Compras;
using Application.Operacional.Compras.DTOs;

namespace Application.Operacional.Compras.Queries.Handlers
{
    public sealed class ListarDevolucoesCompraHandler
        : IQueryHandler<ListarDevolucoesCompraQuery, ListarDevolucoesCompraDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioDevolucaoCompra _repositorio;

        public ListarDevolucoesCompraHandler(
            ITenantContext tenantContext,
            IRepositorioDevolucaoCompra repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<ListarDevolucoesCompraDto> Handle(
            ListarDevolucoesCompraQuery query,
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
                erros.Add("A página deve ser maior que zero.");

            if (query.PageSize < 1 || query.PageSize > 100)
            {
                erros.Add(
                    "O tamanho da página deve estar entre 1 e 100.");
            }

            if (query.CompraId.HasValue &&
                query.CompraId.Value == Guid.Empty)
            {
                erros.Add("O identificador da compra é inválido.");
            }

            if (query.FornecedorId.HasValue &&
                query.FornecedorId.Value == Guid.Empty)
            {
                erros.Add("O identificador do fornecedor é inválido.");
            }

            if (query.Inicio.HasValue &&
                query.Inicio.Value == default)
            {
                erros.Add("A data inicial informada é inválida.");
            }

            if (query.Fim.HasValue &&
                query.Fim.Value == default)
            {
                erros.Add("A data final informada é inválida.");
            }

            if (query.Inicio.HasValue &&
                query.Fim.HasValue &&
                query.Inicio.Value.Date > query.Fim.Value.Date)
            {
                erros.Add(
                    "A data inicial não pode ser maior que a data final.");
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            var searchTerm = string.IsNullOrWhiteSpace(query.SearchTerm)
                ? null
                : query.SearchTerm.Trim();

            var (devolucoes, totalRegistros) =
                await _repositorio.ListarAsync(
                    _tenantContext.EmpresaId,
                    query.PageNumber,
                    query.PageSize,
                    query.CompraId,
                    query.FornecedorId,
                    query.Inicio,
                    query.Fim,
                    searchTerm,
                    cancellationToken);

            var itens = devolucoes
                .Select(d => new DevolucaoCompraListaDto(
                    d.Id,
                    d.CompraId,
                    d.Compra.FornecedorId,
                    d.Compra.Fornecedor.Nome,
                    d.Compra.NumeroDocumento,
                    d.DataDevolucao,
                    d.Motivo,
                    d.Itens.Count))
                .ToArray();

            var totalPaginas = (int)Math.Ceiling(
                totalRegistros / (double)query.PageSize);

            return new ListarDevolucoesCompraDto(
                itens,
                query.PageNumber,
                query.PageSize,
                totalRegistros,
                totalPaginas);
        }
    }
}
