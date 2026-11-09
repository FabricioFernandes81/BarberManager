
using Application.Interfaces.Financeiro;
using Domain.Entities.Clientes;
using Domain.Entities.Financeiro;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Financeiro
{
    public class RepositorioClienteAsaasOperacional
        : IRepositorioClienteAsaasOperacional
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioClienteAsaasOperacional(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<ClienteAsaasOperacional?> ObterPorClienteAsync(
            Guid empresaId,
            Guid clienteId,
            CancellationToken cancellationToken)
        {
            return await _context.ClientesAsaasOperacionais
                .SingleOrDefaultAsync(
                    x => x.EmpresaId == empresaId
                      && x.ClienteId == clienteId,
                    cancellationToken);
        }

        public async Task<ClienteAsaasOperacional?>
            ObterPorClienteAsaasIdAsync(
                Guid empresaId,
                string clienteAsaasId,
                CancellationToken cancellationToken)
        {
            return await _context.ClientesAsaasOperacionais
                .SingleOrDefaultAsync(
                    x => x.EmpresaId == empresaId
                      && x.ClienteAsaasId == clienteAsaasId,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            ClienteAsaasOperacional clienteAsaas,
            CancellationToken cancellationToken)
        {
            await _context.ClientesAsaasOperacionais.AddAsync(
                clienteAsaas,
                cancellationToken);

            // Não chamar SaveChangesAsync.
            // A UnitOfWork controla a persistência.
        }

        public async Task<Cliente?> ObterPorUsuarioEEmpresaAsync(
    Guid usuarioId,
    Guid empresaId,
    CancellationToken cancellationToken = default)
        {
            return await _context.Clientes
                .AsNoTracking()
                .SingleOrDefaultAsync(
                    x => x.UsuarioId == usuarioId
                        && x.Ativo
                        && x.Empresas.Any(e =>
                            e.EmpresaId == empresaId &&
                            e.Ativo),
                    cancellationToken);
        }
    }
}
