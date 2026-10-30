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
    public class ObterProdutoPorIdHandler : IQueryHandler<ObterProdutoPorIdQuery, ProdutoDetalheDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioProdutos _repositorio;


        public ObterProdutoPorIdHandler(
        ITenantContext tenantContext,
        IRepositorioProdutos repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }
        public async Task<ProdutoDetalheDto> Handle(ObterProdutoPorIdQuery query, CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable ||
        _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            if (query.ProdutoId == Guid.Empty)
            {
                throw new CustomValidationException(
                    new[] { "O ID do produto é inválido." });
            }

            var produto = await _repositorio.ObterPorIdAsync(
                _tenantContext.EmpresaId,
                query.ProdutoId,
                cancellationToken);

            if (produto is null)
            {
                throw new CustomValidationException(
                    new[] { "Produto não encontrado." });
            }

            return new ProdutoDetalheDto(
                produto.Id,
                produto.CategoriaProdutoId,
                produto.CategoriaProduto?.Nome,
                produto.MarcaId,
                produto.Marca?.Nome,
                produto.Nome,
                produto.Descricao,
                produto.CodigoInterno,
                produto.CodigoBarras,
                produto.ValorVenda,
                produto.Custo,
                produto.ControlaEstoque,
                produto.Estoque?.QuantidadeAtual,
                produto.Estoque?.EstoqueMinimo,
                produto.Ativo,
                produto.DataCadastro,
                produto.DataAtualizacao);
        }
    }
}
