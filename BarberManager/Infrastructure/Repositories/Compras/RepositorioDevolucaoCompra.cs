
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
                .Include(c => c.Fornecedor)
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
        public async Task<(
    IReadOnlyList<DevolucaoCompra> Itens,
    int TotalRegistros
)> ListarAsync(
    Guid empresaId,
    int pageNumber,
    int pageSize,
    Guid? compraId,
    Guid? fornecedorId,
    DateTime? inicio,
    DateTime? fim,
    string? searchTerm,
    CancellationToken cancellationToken)
        {
            IQueryable<DevolucaoCompra> consulta =
                _context.DevolucoesCompras
                    .AsNoTracking()
                    .Include(d => d.Compra)
                        .ThenInclude(c => c.Fornecedor)
                    .Include(d => d.Itens)
                    .Where(d => d.EmpresaId == empresaId);

            if (compraId.HasValue)
            {
                consulta = consulta.Where(
                    d => d.CompraId == compraId.Value);
            }

            if (fornecedorId.HasValue)
            {
                consulta = consulta.Where(
                    d => d.Compra.FornecedorId == fornecedorId.Value);
            }

            if (inicio.HasValue)
            {
                var dataInicio = new DateTimeOffset(
                    DateTime.SpecifyKind(
                        inicio.Value.Date,
                        DateTimeKind.Utc));

                consulta = consulta.Where(
                    d => d.DataDevolucao >= dataInicio);
            }

            if (fim.HasValue)
            {
                var dataFimExclusiva = new DateTimeOffset(
                    DateTime.SpecifyKind(
                        fim.Value.Date.AddDays(1),
                        DateTimeKind.Utc));

                consulta = consulta.Where(
                    d => d.DataDevolucao < dataFimExclusiva);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var termo = searchTerm.Trim();

                consulta = consulta.Where(d =>
                    d.Motivo.Contains(termo) ||
                    (d.Observacao != null &&
                     d.Observacao.Contains(termo)) ||
                    (d.Compra.NumeroDocumento != null &&
                     d.Compra.NumeroDocumento.Contains(termo)) ||
                    d.Compra.Fornecedor.Nome.Contains(termo) ||
                    (d.Compra.Fornecedor.NomeFantasia != null &&
                     d.Compra.Fornecedor.NomeFantasia.Contains(termo)));
            }

            var totalRegistros = await consulta.CountAsync(
                cancellationToken);

            var devolucoes = await consulta
                .OrderByDescending(d => d.DataDevolucao)
                .ThenByDescending(d => d.DataCadastro)
                .ThenBy(d => d.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (devolucoes, totalRegistros);
        }
    }
}
