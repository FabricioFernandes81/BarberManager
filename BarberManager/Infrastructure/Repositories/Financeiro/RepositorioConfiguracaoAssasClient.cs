using Application.Interfaces.Financeiro;
using Domain.Entities.Financeiro;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Financeiro
{
    public class RepositorioConfiguracaoAssasClient
        : IRepositorioConfiguracaoAssasClient
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioConfiguracaoAssasClient(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<ConfiguracaoAssasClient?>
            ObterPorEmpresaAsync(
                Guid empresaId,
                CancellationToken cancellationToken)
        {
            return await _context.ConfiguracoesAssasClient
                .FirstOrDefaultAsync(
                    x => x.EmpresaId == empresaId,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            ConfiguracaoAssasClient configuracao,
            CancellationToken cancellationToken)
        {
            await _context.ConfiguracoesAssasClient.AddAsync(
                configuracao,
                cancellationToken);
        }
    }
}