using Application.Interfaces.Compras;
using Domain.Entities.Common.Enuns;
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
         .FromSqlInterpolated($@"
            SELECT *
            FROM dbo.RecebimentosCompras WITH (
                UPDLOCK,
                HOLDLOCK,
                INDEX(UX_RecebimentosCompras_Empresa_ChaveIdempotencia)
            )
            WHERE EmpresaId = {empresaId}
              AND ChaveIdempotencia = {chaveIdempotencia}")
         .AsNoTracking()
         .Include(r => r.Itens)
         .SingleOrDefaultAsync(cancellationToken);
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

        public async Task<Compra?> ObterParaCancelamentoAsync(
    Guid empresaId,
    Guid compraId,
    CancellationToken cancellationToken)
        {
            return await _context.Compras
                .Include(c => c.Itens)
                .Include(c => c.Recebimentos)
                .SingleOrDefaultAsync(
                    c => c.Id == compraId &&
                         c.EmpresaId == empresaId,
                    cancellationToken);
        }

        public async Task<IReadOnlyCollection<ContaPagar>>
            ObterContasPagarParaCancelamentoAsync(
                Guid empresaId,
                Guid compraId,
                CancellationToken cancellationToken)
        {
            return await _context.ContasPagar
                .Include(c => c.Pagamentos)
                .Where(c =>
                    c.EmpresaId == empresaId &&
                    c.CompraId == compraId)
                .ToListAsync(cancellationToken);
        }


        public async Task<(
    IReadOnlyList<Compra> Itens,
    int TotalRegistros
)> ListarAsync(
    Guid empresaId,
    int pageNumber,
    int pageSize,
    string? searchTerm,
    DateTime? inicio,
    DateTime? fim,
    Guid? fornecedorId,
    StatusCompra? status,
    CancellationToken cancellationToken)
        {
            IQueryable<Compra> consulta = _context.Compras
                .AsNoTracking()
                .Include(c => c.Fornecedor)
                .Where(c => c.EmpresaId == empresaId);

            if (inicio.HasValue)
            {
                var dataInicio = inicio.Value.Date;

                consulta = consulta.Where(
                    c => c.DataCompra >= dataInicio);
            }

            if (fim.HasValue)
            {
                var dataFimExclusiva = fim.Value.Date.AddDays(1);

                consulta = consulta.Where(
                    c => c.DataCompra < dataFimExclusiva);
            }

            if (fornecedorId.HasValue)
            {
                consulta = consulta.Where(
                    c => c.FornecedorId == fornecedorId.Value);
            }

            if (status.HasValue)
            {
                consulta = consulta.Where(
                    c => c.Status == status.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var termo = searchTerm.Trim();

                consulta = consulta.Where(c =>
                    (c.NumeroDocumento != null &&
                     c.NumeroDocumento.Contains(termo)) ||
                    (c.SerieDocumento != null &&
                     c.SerieDocumento.Contains(termo)) ||
                    c.Fornecedor.Nome.Contains(termo) ||
                    (c.Fornecedor.NomeFantasia != null &&
                     c.Fornecedor.NomeFantasia.Contains(termo)));
            }

            var totalRegistros = await consulta.CountAsync(
                cancellationToken);

            var compras = await consulta
                .OrderByDescending(c => c.DataCompra)
                .ThenByDescending(c => c.DataCadastro)
                .ThenBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (compras, totalRegistros);
        }


        public async Task<(
    Compra? Compra,
    IReadOnlyList<ContaPagar> ContasPagar
)> ObterDetalheAsync(
    Guid empresaId,
    Guid compraId,
    CancellationToken cancellationToken)
        {
            var compra = await _context.Compras
                .AsNoTracking()
                .AsSplitQuery()
                .Include(c => c.Fornecedor)
                .Include(c => c.Itens)
                .Include(c => c.Recebimentos)
                    .ThenInclude(r => r.Itens)
                        .ThenInclude(i => i.CompraItem)
                .SingleOrDefaultAsync(
                    c => c.Id == compraId &&
                         c.EmpresaId == empresaId,
                    cancellationToken);

            if (compra is null)
            {
                return (
                    null,
                    Array.Empty<ContaPagar>());
            }

            var contasPagar = await _context.ContasPagar
                .AsNoTracking()
                .AsSplitQuery()
                .Where(c =>
                    c.EmpresaId == empresaId &&
                    c.CompraId == compraId)
                .Include(c => c.Pagamentos)
                    .ThenInclude(p => p.FormaPagamento)
                .OrderBy(c => c.NumeroParcela)
                .ThenBy(c => c.DataVencimento)
                .ToListAsync(cancellationToken);

            return (compra, contasPagar);
        }
    }
}
