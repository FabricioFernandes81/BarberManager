using Application.Interfaces.Estoque;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Estoque;
using Domain.Entities.Produtos;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositorioEstoque : IRepositorioEstoque
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioEstoque(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ProdutoExisteAsync(
            Guid empresaId,
            Guid produtoId,
            CancellationToken cancellationToken)
        {
            return await _context.Produtos
                .AsNoTracking()
                .AnyAsync(
                    p => p.Id == produtoId &&
                         p.EmpresaId == empresaId,
                    cancellationToken);
        }

        public async Task<(
            IReadOnlyList<MovimentacaoEstoque> Itens,
            int TotalRegistros
        )> ListarMovimentacoesAsync(
            Guid empresaId,
            Guid produtoId,
            DateTimeOffset? dataInicio,
            DateTimeOffset? dataFim,
            TipoMovimentacaoEstoque? tipo,
            OrigemMovimentacaoEstoque? origem,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            IQueryable<MovimentacaoEstoque> consulta =
                _context.MovimentacoesEstoque
                    .AsNoTracking()
                    .Where(m =>
                        m.EmpresaId == empresaId &&
                        m.ProdutoId == produtoId);

            if (dataInicio.HasValue)
            {
                consulta = consulta.Where(
                    m => m.DataMovimento >= dataInicio.Value);
            }

            if (dataFim.HasValue)
            {
                consulta = consulta.Where(
                    m => m.DataMovimento <= dataFim.Value);
            }

            if (tipo.HasValue)
            {
                consulta = consulta.Where(
                    m => m.Tipo == tipo.Value);
            }

            if (origem.HasValue)
            {
                consulta = consulta.Where(
                    m => m.Origem == origem.Value);
            }

            var totalRegistros = await consulta.CountAsync(
                cancellationToken);

            var movimentacoes = await consulta
                .OrderByDescending(m => m.DataMovimento)
                .ThenByDescending(m => m.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (movimentacoes, totalRegistros);
        }
        public async Task<Produto?> ObterProdutoParaMovimentacaoAsync(
    Guid empresaId,
    Guid produtoId,
    CancellationToken cancellationToken)
        {
            return await _context.Produtos
                .Include(p => p.Estoque)
                .FirstOrDefaultAsync(
                    p => p.Id == produtoId &&
                         p.EmpresaId == empresaId,
                    cancellationToken);
        }

        public async Task AdicionarMovimentacaoAsync(
    MovimentacaoEstoque movimentacao,
    CancellationToken cancellationToken)
        {
            await _context.MovimentacoesEstoque.AddAsync(
                movimentacao,
                cancellationToken);
        }

    }
}
