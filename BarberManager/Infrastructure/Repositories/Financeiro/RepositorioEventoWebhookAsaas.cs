using Application.Interfaces.Financeiro;
using Domain.Entities.Financeiro;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Financeiro
{
    public class RepositorioEventoWebhookAsaas
        : IRepositorioEventoWebhookAsaas
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioEventoWebhookAsaas(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<EventoWebhookAsaas?> ObterPorEventoAsync(
            Guid empresaId,
            string eventoId,
            CancellationToken cancellationToken)
        {
            return await _context.EventosWebhookAsaas
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.EmpresaId == empresaId
                      && x.EventoId == eventoId,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            EventoWebhookAsaas evento,
            CancellationToken cancellationToken)
        {
            await _context.EventosWebhookAsaas.AddAsync(
                evento,
                cancellationToken);

            // Não chamar SaveChangesAsync.
            // A persistência será coordenada pela UnitOfWork.
        }
    }
}