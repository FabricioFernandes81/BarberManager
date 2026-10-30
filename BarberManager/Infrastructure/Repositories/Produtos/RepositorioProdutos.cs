using Application.Interfaces.Produtos;
using Domain.Entities.Estoque;
using Domain.Entities.Produtos;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Produtos
{
    public sealed class RepositorioProdutos : IRepositorioProdutos
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioProdutos(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<(
            IReadOnlyList<Produto> Itens,
            int TotalRegistros
        )> ListarAsync(
            Guid empresaId,
            int pageNumber,
            int pageSize,
            string? searchTerm,
            Guid? categoriaId,
            Guid? marcaId,
            bool? ativo,
            CancellationToken cancellationToken)
        {
            IQueryable<Produto> consulta = _context.Produtos
                .AsNoTracking()
                .Where(p => p.EmpresaId == empresaId);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var termo = searchTerm.Trim();

                consulta = consulta.Where(p =>
                    p.Nome.Contains(termo) ||
                    (p.CodigoInterno != null &&
                     p.CodigoInterno.Contains(termo)) ||
                    (p.CodigoBarras != null &&
                     p.CodigoBarras.Contains(termo)));
            }

            if (categoriaId.HasValue)
            {
                consulta = consulta.Where(
                    p => p.CategoriaProdutoId == categoriaId.Value);
            }

            if (marcaId.HasValue)
            {
                consulta = consulta.Where(
                    p => p.MarcaId == marcaId.Value);
            }

            if (ativo.HasValue)
            {
                consulta = consulta.Where(
                    p => p.Ativo == ativo.Value);
            }

            var totalRegistros = await consulta.CountAsync(
                cancellationToken);

            var produtos = await consulta
                .Include(p => p.CategoriaProduto)
                .Include(p => p.Marca)
                .Include(p => p.Estoque)
                .OrderBy(p => p.Nome)
                .ThenBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (produtos, totalRegistros);
        }

        public async Task<Produto?> ObterPorIdAsync(Guid empresaId, Guid produtoId, CancellationToken cancellationToken)
        {
            return await _context.Produtos
         .AsNoTracking()
         .Include(p => p.CategoriaProduto)
         .Include(p => p.Marca)
         .Include(p => p.Estoque)
         .SingleOrDefaultAsync(
             p => p.Id == produtoId &&
                  p.EmpresaId == empresaId,
             cancellationToken);
        }

        public async Task<bool> CategoriaPertenceEmpresaAsync(
        Guid empresaId,
        Guid categoriaId,
        CancellationToken cancellationToken)
        {
            return await _context.CategoriasProdutos
                .AsNoTracking()
                .AnyAsync(
                    c => c.Id == categoriaId &&
                         c.EmpresaId == empresaId &&
                         c.Ativo,
                    cancellationToken);
        }

        public async Task<bool> MarcaPertenceEmpresaAsync(
            Guid empresaId,
            Guid marcaId,
            CancellationToken cancellationToken)
        {
            return await _context.Marcas
                .AsNoTracking()
                .AnyAsync(
                    m => m.Id == marcaId &&
                         m.EmpresaId == empresaId &&
                         m.Ativo,
                    cancellationToken);
        }

        public async Task<bool> CodigoInternoExisteAsync(
            Guid empresaId,
            string codigoInterno,
            CancellationToken cancellationToken)
        {
            return await _context.Produtos
                .AsNoTracking()
                .AnyAsync(
                    p => p.EmpresaId == empresaId &&
                         p.CodigoInterno == codigoInterno,
                    cancellationToken);
        }

        public async Task<bool> CodigoBarrasExisteAsync(
            Guid empresaId,
            string codigoBarras,
            CancellationToken cancellationToken)
        {
            return await _context.Produtos
                .AsNoTracking()
                .AnyAsync(
                    p => p.EmpresaId == empresaId &&
                         p.CodigoBarras == codigoBarras,
                    cancellationToken);
        }

        public async Task Adicionar(Produto produto)
        {
        
           await _context.Produtos.AddAsync(produto);
        }

        public async Task AdicionarComEstoqueInicialAsync(
    Produto produto,
    EstoqueProduto? estoque,
    MovimentacaoEstoque? movimentacao,
    CancellationToken cancellationToken)
        {
            await _context.Produtos.AddAsync(
                produto,
                cancellationToken);

            if (estoque is not null)
            {
                await _context.EstoquesProdutos.AddAsync(
                    estoque,
                    cancellationToken);
            }

            if (movimentacao is not null)
            {
                await _context.MovimentacoesEstoque.AddAsync(
                    movimentacao,
                    cancellationToken);
            }
        }
    }
}
