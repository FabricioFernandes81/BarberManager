using Application.Interfaces.Financeiro;
using Domain.Entities.Financeiro;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Financeiro
{
    public class RepositorioCobrancaOperacional
        : IRepositorioCobrancaOperacional
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioCobrancaOperacional(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<CobrancaOperacional?> ObterPorIdempotenciaAsync(
            Guid empresaId,
            string chaveIdempotencia,
            CancellationToken cancellationToken)
        {
            return await _context.CobrancasOperacionais
                .FirstOrDefaultAsync(
                    x => x.EmpresaId == empresaId
                      && x.ChaveIdempotencia == chaveIdempotencia,
                    cancellationToken);
        }

        public async Task<CobrancaOperacional?> ObterPorPagamentoGatewayAsync(
            Guid empresaId,
            string gateway,
            string pagamentoGatewayId,
            CancellationToken cancellationToken)
        {
            return await _context.CobrancasOperacionais
                .FirstOrDefaultAsync(
                    x => x.EmpresaId == empresaId
                      && x.Gateway == gateway
                      && x.GatewayPagamentoId == pagamentoGatewayId,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            CobrancaOperacional cobranca,
            CancellationToken cancellationToken)
        {
            await _context.CobrancasOperacionais.AddAsync(
                cobranca,
                cancellationToken);

            // Não chamar SaveChangesAsync.
            // A UnitOfWork controla a persistência.
        }

        public async Task<CobrancaOperacional?> ObterPorIdAsync(
            Guid empresaId,
            Guid cobrancaId,
            CancellationToken cancellationToken)
        {
            return await _context.Set<CobrancaOperacional>()
                .FirstOrDefaultAsync(
                    x => x.Id == cobrancaId
                      && x.EmpresaId == empresaId,
                    cancellationToken);
        }


        public async Task<bool> TentarIniciarProcessamentoAsync(
            Guid empresaId,
            Guid cobrancaId,
            CancellationToken cancellationToken)
        {
            var linhasAfetadas = await _context.CobrancasOperacionais
                .Where(x =>
                    x.Id == cobrancaId &&
                    x.EmpresaId == empresaId &&
                    x.Status == StatusCobrancaOperacional.Reservada &&
                    x.GatewayPagamentoId == null)
                .ExecuteUpdateAsync(
                    atualizacao => atualizacao.SetProperty(
                        x => x.Status,
                        StatusCobrancaOperacional.EmProcessamento),
                    cancellationToken);

            if (linhasAfetadas != 1)
                return false;

            // Sincroniza a entidade que eventualmente já esteja
            // rastreada pelo DbContext.
            var entrada = _context.ChangeTracker
                .Entries<CobrancaOperacional>()
                .FirstOrDefault(x =>
                    x.Entity.Id == cobrancaId &&
                    x.Entity.EmpresaId == empresaId);

            if (entrada is not null)
            {
                await entrada.ReloadAsync(cancellationToken);
            }

            return true;
        }

    }
}