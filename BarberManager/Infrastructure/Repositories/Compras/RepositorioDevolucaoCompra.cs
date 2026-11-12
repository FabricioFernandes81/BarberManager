
using Application.Interfaces.Compras;
using Domain.Entities.Compras;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Compras
{
    public sealed class RepositorioDevolucaoCompra
        : IRepositorioDevolucaoCompra
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioDevolucaoCompra(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<DevolucaoCompra?>
            ObterPorChaveIdempotenciaAsync(
                Guid empresaId,
                string chaveIdempotencia,
                CancellationToken cancellationToken)
        {
            return await _context.DevolucoesCompras
                .FromSqlInterpolated($@"
                    SELECT *
                    FROM dbo.DevolucoesCompras WITH (
                        UPDLOCK,
                        HOLDLOCK,
                        INDEX(UX_DevolucoesCompras_Empresa_ChaveIdempotencia)
                    )
                    WHERE EmpresaId = {empresaId}
                      AND ChaveIdempotencia = {chaveIdempotencia}")
                .AsNoTracking()
                .Include(d => d.Itens)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<Compra?> ObterParaDevolucaoAsync(
            Guid empresaId,
            Guid compraId,
            CancellationToken cancellationToken)
        {
            return await _context.Compras
                .AsSplitQuery()
                .Include(c => c.Itens)
                .Include(c => c.Recebimentos)
                    .ThenInclude(r => r.Itens)
                .SingleOrDefaultAsync(
                    c => c.Id == compraId &&
                         c.EmpresaId == empresaId,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            DevolucaoCompra devolucao,
            CancellationToken cancellationToken)
        {
            await _context.DevolucoesCompras.AddAsync(
                devolucao,
                cancellationToken);

            // Não executar SaveChangesAsync.
            // O TransactionMiddleware controla o commit.
        }


        public async Task<DevolucaoCompra?> ObterDetalheAsync(
            Guid empresaId,
            Guid devolucaoId,
            CancellationToken cancellationToken)
        {
            return await _context.DevolucoesCompras
                .AsNoTracking()
                .AsSplitQuery()
                .Include(d => d.Compra)
                    .ThenInclude(c => c.Fornecedor)
                .Include(d => d.Itens)
                    .ThenInclude(i => i.RecebimentoCompraItem)
                        .ThenInclude(r => r.CompraItem)
                .SingleOrDefaultAsync(
                    d => d.Id == devolucaoId &&
                         d.EmpresaId == empresaId,
                    cancellationToken);
        }

    }
}
