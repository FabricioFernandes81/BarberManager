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

namespace Application.Operacional.Produtos.Commands.Handlers
{
    public class AlterarStatusProdutoHandler : ICommandHandler<AlterarStatusProdutoCommand, ProdutoDetalheDto>
    {
        private readonly IRepositorioProdutos _repositorio;
        private readonly ITenantContext _tenantContext;

        public AlterarStatusProdutoHandler(IRepositorioProdutos repositorio,ITenantContext tenantContext)
        {
            _repositorio = repositorio;
            _tenantContext = tenantContext;
        }
        public async Task<ProdutoDetalheDto> Handle(AlterarStatusProdutoCommand command, CancellationToken cancellationToken)
        {
            // 1. Validar o contexto da empresa.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            // 2. Validar o identificador.
            if (command.ProdutoId == Guid.Empty)
            {
                throw new CustomValidationException(
                    new[] { "O ID do produto é inválido." });
            }

            cancellationToken.ThrowIfCancellationRequested();

            // 3. Buscar o produto no tenant autenticado.
            var produto = await _repositorio.ObterPorIdParaAtualizacaoAsync(
                _tenantContext.EmpresaId,command.ProdutoId,
                cancellationToken);

            if (produto is null)
            {
                throw new CustomValidationException(
                    new[] { "Produto não encontrado." });
            }

            // 4. Alterar somente quando houver mudança de estado.
            if (command.Ativo && !produto.Ativo)
            {
                produto.Ativar();
            }
            else if (!command.Ativo && produto.Ativo)
            {
                produto.Desativar();
            }

            // 5. Retornar os dados atualizados.
            // O DbContext rastreia a entidade e a unidade de trabalho
            // é responsável por persistir a alteração.
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
