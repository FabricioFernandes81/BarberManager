
using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Compras;
using Application.Operacional.Compras.DTOs;

namespace Application.Operacional.Compras.Queries.Handlers
{
    public sealed class ObterDevolucaoCompraPorIdHandler
        : IQueryHandler<
            ObterDevolucaoCompraPorIdQuery,
            DevolucaoCompraDetalheDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioDevolucaoCompra _repositorio;

        public ObterDevolucaoCompraPorIdHandler(
            ITenantContext tenantContext,
            IRepositorioDevolucaoCompra repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<DevolucaoCompraDetalheDto> Handle(
            ObterDevolucaoCompraPorIdQuery query,
            CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            if (query.DevolucaoId == Guid.Empty)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "O identificador da devolução é inválido."
                    });
            }

            var devolucao = await _repositorio.ObterDetalheAsync(
                _tenantContext.EmpresaId,
                query.DevolucaoId,
                cancellationToken);

            if (devolucao is null)
            {
                throw new CustomValidationException(
                    new[] { "Devolução não encontrada." });
            }

            var itens = devolucao.Itens
                .OrderBy(i => i.RecebimentoCompraItem.CompraItem.DescricaoProduto)
                .Select(i => new DevolucaoCompraItemDetalheDto(
                    i.Id,
                    i.RecebimentoCompraItemId,
                    i.RecebimentoCompraItem.CompraItemId,
                    i.ProdutoId,
                    i.RecebimentoCompraItem.CompraItem.DescricaoProduto,
                    i.RecebimentoCompraItem.QuantidadeRecebida,
                    i.QuantidadeDevolvida))
                .ToArray();

            return new DevolucaoCompraDetalheDto(
                devolucao.Id,
                devolucao.CompraId,
                devolucao.Compra.FornecedorId,
                devolucao.Compra.Fornecedor.Nome,
                devolucao.Compra.NumeroDocumento,
                devolucao.UsuarioResponsavelId,
                devolucao.DataDevolucao,
                devolucao.Motivo,
                devolucao.Observacao,
                itens);
        }
    }
}
