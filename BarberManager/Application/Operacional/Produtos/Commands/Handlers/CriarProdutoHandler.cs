using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Produtos;
using Domain.Entities.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Produtos.Commands.Handlers
{
    public class CriarProdutoHandler : ICommandHandler<CriarProdutoCommand, Guid>
    {
        private readonly IRepositorioProdutos _repositorioProdutos;
        private readonly ITenantContext _tenantContext;

        public CriarProdutoHandler( IRepositorioProdutos repositorioProdutos,
       ITenantContext tenantContext)
        {
            _repositorioProdutos = repositorioProdutos;
            _tenantContext = tenantContext;
        }
        public async Task<Guid> Handle(CriarProdutoCommand command, CancellationToken cancellationToken)
        {
          

            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new CustomValidationException(
                    new[]
                    {
                    "Não foi possível identificar a empresa do usuário autenticado."
                    });
            }

            var empresaId = _tenantContext.EmpresaId;

            // =====================================================
            // 2. VALIDAR DADOS OBRIGATÓRIOS
            // =====================================================

            if (string.IsNullOrWhiteSpace(command.Produto.Nome))
            {
                throw new CustomValidationException(
                    new[]
                    {
                    "O nome do produto é obrigatório."
                    });
            }

            if (command.Produto.ValorVenda < 0)
            {
                throw new CustomValidationException(
                    new[]
                    {
                    "O valor de venda não pode ser negativo."
                    });
            }

            if (command.Produto.Custo < 0)
            {
                throw new CustomValidationException(
                    new[]
                    {
                    "O custo do produto não pode ser negativo."
                    });
            }

            // =====================================================
            // 3. VALIDAR CATEGORIA
            // =====================================================

            if (command.CategoriaProdutoId.HasValue)
            {
                var categoriaExiste = await _context
                    .Set<CategoriaProduto>()
                    .AsNoTracking()
                    .AnyAsync(
                        categoria =>
                            categoria.Id == command.CategoriaProdutoId.Value &&
                            categoria.EmpresaId == empresaId &&
                            categoria.Ativo,
                        cancellationToken);

                if (!categoriaExiste)
                {
                    throw new CustomValidationException(
                        new[]
                        {
                        "A categoria informada não existe, está inativa ou não pertence à empresa."
                        });
                }
            }

            // =====================================================
            // 4. VALIDAR MARCA
            // =====================================================

            if (command.MarcaId.HasValue)
            {
                var marcaExiste = await _context
                    .Set<Marca>()
                    .AsNoTracking()
                    .AnyAsync(
                        marca =>
                            marca.Id == command.MarcaId.Value &&
                            marca.EmpresaId == empresaId &&
                            marca.Ativo,
                        cancellationToken);

                if (!marcaExiste)
                {
                    throw new CustomValidationException(
                        new[]
                        {
                        "A marca informada não existe, está inativa ou não pertence à empresa."
                        });
                }
            }

            // =====================================================
            // 5. VALIDAR CÓDIGO INTERNO
            // =====================================================

            if (!string.IsNullOrWhiteSpace(command.CodigoInterno))
            {
                var codigoInterno = command.CodigoInterno.Trim();

                var codigoExiste = await _context
                    .Set<Produto>()
                    .AsNoTracking()
                    .AnyAsync(
                        produto =>
                            produto.EmpresaId == empresaId &&
                            produto.CodigoInterno == codigoInterno,
                        cancellationToken);

                if (codigoExiste)
                {
                    throw new CustomValidationException(
                        new[]
                        {
                        "Já existe um produto cadastrado com esse código interno."
                        });
                }
            }

            // =====================================================
            // 6. VALIDAR CÓDIGO DE BARRAS
            // =====================================================

            if (!string.IsNullOrWhiteSpace(command.CodigoBarras))
            {
                var codigoBarras = command.CodigoBarras.Trim();

                var codigoExiste = await _context
                    .Set<Produto>()
                    .AsNoTracking()
                    .AnyAsync(
                        produto =>
                            produto.EmpresaId == empresaId &&
                            produto.CodigoBarras == codigoBarras,
                        cancellationToken);

                if (codigoExiste)
                {
                    throw new CustomValidationException(
                        new[]
                        {
                        "Já existe um produto cadastrado com esse código de barras."
                        });
                }
            }

            // =====================================================
            // 7. CRIAR ENTIDADE DE DOMÍNIO
            // =====================================================

            var produto = new Produto(
                empresaId: empresaId,
                categoriaProdutoId: command.CategoriaProdutoId,
                marcaId: command.MarcaId,
                nome: command.Nome.Trim(),
                descricao: command.Descricao,
                codigoInterno: command.CodigoInterno?.Trim(),
                codigoBarras: command.CodigoBarras?.Trim(),
                valorVenda: command.ValorVenda,
                custo: command.Custo,
                controlaEstoque: command.ControlaEstoque
                );

          
                await _repositorioProdutos.Adicionar(
                    produto,
                    cancellationToken);

          
                // =================================================
                // 9. RETORNAR IDENTIFICADOR DO PRODUTO
                // =================================================

                return produto.Id;
           
        }
    }
}
