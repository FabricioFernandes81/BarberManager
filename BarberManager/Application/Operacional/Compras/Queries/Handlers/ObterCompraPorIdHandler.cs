using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Compras;
using Application.Operacional.Compras.DTOs;

namespace Application.Operacional.Compras.Queries.Handlers
{
    public sealed class ObterCompraPorIdHandler
        : IQueryHandler<ObterCompraPorIdQuery, CompraDetalheDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioCompra _repositorio;

        public ObterCompraPorIdHandler(
            ITenantContext tenantContext,
            IRepositorioCompra repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<CompraDetalheDto> Handle(
            ObterCompraPorIdQuery query,
            CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            if (query.CompraId == Guid.Empty)
            {
                throw new CustomValidationException(
                    new[] { "O identificador da compra é inválido." });
            }

            var (compra, contasPagar) =
                await _repositorio.ObterDetalheAsync(
                    _tenantContext.EmpresaId,
                    query.CompraId,
                    cancellationToken);

            if (compra is null)
            {
                throw new CustomValidationException(
                    new[] { "Compra não encontrada." });
            }

            var itens = compra.Itens
                .OrderBy(i => i.DescricaoProduto)
                .Select(i => new CompraItemDetalheDto(
                    i.Id,
                    i.ProdutoId,
                    i.DescricaoProduto,
                    i.Quantidade,
                    i.QuantidadeRecebida,
                    i.QuantidadePendente,
                    i.ValorUnitario,
                    i.Desconto,
                    i.ValorTotal))
                .ToArray();

            var recebimentos = compra.Recebimentos
                .OrderBy(r => r.DataRecebimento)
                .Select(r => new RecebimentoCompraDetalheDto(
                    r.Id,
                    r.UsuarioResponsavelId,
                    r.DataRecebimento,
                    r.ChaveIdempotencia,
                    r.Observacao,
                    r.Itens
                        .Select(i => new RecebimentoCompraItemDetalheDto(
                            i.Id,
                            i.CompraItemId,
                            i.ProdutoId,
                            i.CompraItem.DescricaoProduto,
                            i.QuantidadeRecebida))
                        .ToArray()))
                .ToArray();

            var contas = contasPagar
                .Select(c => new ContaPagarCompraDto(
                    c.Id,
                    c.Descricao,
                    c.DataEmissao,
                    c.DataVencimento,
                    c.Valor,
                    c.ValorPago,
                    Math.Max(0, c.Valor - c.ValorPago),
                    c.Status,
                    c.NumeroParcela,
                    c.TotalParcelas,
                    c.Pagamentos
                        .OrderBy(p => p.DataPagamento)
                        .Select(p => new PagamentoCompraDto(
                            p.Id,
                            p.FormaPagamentoId,
                            p.FormaPagamento.Nome,
                            p.DataPagamento,
                            p.Valor,
                            p.Observacao))
                        .ToArray()))
                .ToArray();

            return new CompraDetalheDto(
                compra.Id,
                compra.FornecedorId,
                compra.Fornecedor.Nome,
                compra.Fornecedor.Documento,
                compra.NumeroDocumento,
                compra.SerieDocumento,
                compra.DataEmissao,
                compra.DataCompra,
                compra.Subtotal,
                compra.Desconto,
                compra.Frete,
                compra.OutrasDespesas,
                compra.ValorTotal,
                compra.Observacao,
                compra.Status,
                compra.DataCadastro,
                compra.DataAtualizacao,
                itens,
                recebimentos,
                contas);
        }
    }
}