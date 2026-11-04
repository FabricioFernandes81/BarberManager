using Application.Interfaces.Compras;
using Domain.Entities.Compras;
using Domain.Entities.Financeiro;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Compras
{
    public class RepositorioCompra : IRepositorioCompra
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioCompra(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(
            Compra compra,
            IReadOnlyCollection<ContaPagar> contasPagar,
            CancellationToken cancellationToken)
        {
            await _context.Compras.AddAsync(
                compra,
                cancellationToken);

            if (contasPagar.Count > 0)
            {
                await _context.ContasPagar.AddRangeAsync(
                    contasPagar,
                    cancellationToken);
            }

            // Não chamar SaveChangesAsync aqui.
            // O TransactionMiddleware confirma a transação.
        }

        public async Task<Compra?> ObterParaRecebimentoAsync(
            Guid empresaId,
            Guid compraId,
            CancellationToken cancellationToken)
        {
            return await _context.Compras
                .Include(c => c.Itens)
                .SingleOrDefaultAsync(
                    c => c.Id == compraId &&
                         c.EmpresaId == empresaId,
                    cancellationToken);
        }

        public async Task<RecebimentoCompra?>
            ObterPorChaveIdempotenciaAsync(
                Guid empresaId,
                string chaveIdempotencia,
                CancellationToken cancellationToken)
        {
            return await _context.RecebimentosCompras
                .AsNoTracking()
                .Include(r => r.Itens)
                .SingleOrDefaultAsync(
                    r => r.EmpresaId == empresaId &&
                         r.ChaveIdempotencia == chaveIdempotencia,
                    cancellationToken);
        }

        public async Task AdicionarRecebimentoAsync(
            RecebimentoCompra recebimento,
            CancellationToken cancellationToken)
        {
            await _context.RecebimentosCompras.AddAsync(
                recebimento,
                cancellationToken);

            // Não executar SaveChangesAsync.
            // O TransactionMiddleware controla o commit.
        }
    }
}
