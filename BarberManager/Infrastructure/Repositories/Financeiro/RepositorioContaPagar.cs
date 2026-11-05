using Application.Interfaces.Financeiro;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Financeiro;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Financeiro
{
    public sealed class RepositorioContaPagar : IRepositorioContaPagar
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioContaPagar(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<(
            IReadOnlyList<ContaPagar> Itens,
            int TotalRegistros
        )> ListarAsync(
            Guid empresaId,
            int pageNumber,
            int pageSize,
            string? searchTerm,
            DateTime? dataInicio,
            DateTime? dataFim,
            Guid? fornecedorId,
            Guid? compraId,
            StatusContas? status,
            DateTime dataReferencia,
            CancellationToken cancellationToken)
        {
            IQueryable<ContaPagar> consulta = _context.ContasPagar
                .AsNoTracking()
                .Include(c => c.Fornecedor)
                .Where(c => c.EmpresaId == empresaId);

            if (dataInicio.HasValue)
            {
                var inicio = dataInicio.Value.Date;

                consulta = consulta.Where(
                    c => c.DataVencimento >= inicio);
            }

            if (dataFim.HasValue)
            {
                var fimExclusivo = dataFim.Value.Date.AddDays(1);

                consulta = consulta.Where(
                    c => c.DataVencimento < fimExclusivo);
            }

            if (fornecedorId.HasValue)
            {
                consulta = consulta.Where(
                    c => c.FornecedorId == fornecedorId.Value);
            }

            if (compraId.HasValue)
            {
                consulta = consulta.Where(
                    c => c.CompraId == compraId.Value);
            }

            if (status.HasValue)
            {
                switch (status.Value)
                {
                    case StatusContas.Vencida:
                        consulta = consulta.Where(c =>
                            c.Status == StatusContas.Vencida ||
                            (
                                (c.Status == StatusContas.Aberta ||
                                 c.Status == StatusContas.Parcial) &&
                                c.DataVencimento < dataReferencia
                            ));
                        break;

                    case StatusContas.Aberta:
                        consulta = consulta.Where(c =>
                            c.Status == StatusContas.Aberta &&
                            c.DataVencimento >= dataReferencia);
                        break;

                    case StatusContas.Parcial:
                        consulta = consulta.Where(c =>
                            c.Status == StatusContas.Parcial &&
                            c.DataVencimento >= dataReferencia);
                        break;

                    default:
                        consulta = consulta.Where(
                            c => c.Status == status.Value);
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var termo = searchTerm.Trim();

                consulta = consulta.Where(c =>
                    c.Descricao.Contains(termo) ||
                    c.Fornecedor.Nome.Contains(termo) ||
                    (c.Fornecedor.NomeFantasia != null &&
                     c.Fornecedor.NomeFantasia.Contains(termo)) ||
                    (c.Fornecedor.Documento != null &&
                     c.Fornecedor.Documento.Contains(termo)) ||
                    (c.Compra != null &&
                     c.Compra.NumeroDocumento != null &&
                     c.Compra.NumeroDocumento.Contains(termo)));
            }

            var totalRegistros = await consulta.CountAsync(
                cancellationToken);

            var contas = await consulta
                .OrderBy(c => c.DataVencimento)
                .ThenBy(c => c.NumeroParcela)
                .ThenBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (contas, totalRegistros);
        }

        public async Task<ContaPagar?> ObterDetalheAsync(
            Guid empresaId,
            Guid contaPagarId,
            CancellationToken cancellationToken)
        {
            return await _context.ContasPagar
                .AsNoTracking()
                .AsSplitQuery()
                .Include(c => c.Fornecedor)
                .Include(c => c.Pagamentos)
                    .ThenInclude(p => p.FormaPagamento)
                .SingleOrDefaultAsync(
                    c => c.EmpresaId == empresaId &&
                         c.Id == contaPagarId,
                    cancellationToken);
        }

        public async Task<ContaPagar?> ObterParaPagamentoAsync(
    Guid empresaId,
    Guid contaPagarId,
    CancellationToken cancellationToken)
        {
            return await _context.ContasPagar
                .FirstOrDefaultAsync(
                    x => x.EmpresaId == empresaId
                      && x.Id == contaPagarId,
                    cancellationToken);
        }

        public async Task<FormaPagamento?> ObterFormaPagamentoAtivaAsync(
            Guid empresaId,
            Guid formaPagamentoId,
            CancellationToken cancellationToken)
        {
            return await _context.FormaPagamentos
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.EmpresaId == empresaId
                      && x.Id == formaPagamentoId
                      && x.Ativo,
                    cancellationToken);
        }

        public async Task AdicionarPagamentoAsync(
            Pagamento pagamento,
            CancellationToken cancellationToken)
        {
            await _context.Pagamentos.AddAsync(
                pagamento,
                cancellationToken);
        }

        public async Task BloquearContaParaPagamentoAsync(
    Guid empresaId,
    Guid contaPagarId,
    CancellationToken cancellationToken)
        {
            if (empresaId == Guid.Empty || contaPagarId == Guid.Empty)
                throw new ArgumentException(
                    "Empresa e conta a pagar devem ser informadas.");

            var recurso =
                $"BarberManager:ContaPagar:{empresaId:N}:{contaPagarId:N}";

            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"""
        DECLARE @resultado INT;

        EXEC @resultado = sys.sp_getapplock
            @Resource = {recurso},
            @LockMode = 'Exclusive',
            @LockOwner = 'Transaction',
            @LockTimeout = 10000;

        IF @resultado < 0
        BEGIN
            THROW 51000, 'Não foi possível bloquear a conta a pagar.', 1;
        END;
        """,
                cancellationToken);
        }
    }
}