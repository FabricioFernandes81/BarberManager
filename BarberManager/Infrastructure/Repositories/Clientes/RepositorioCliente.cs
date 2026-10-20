using Application.Interfaces.Clientes;
using Domain.Entities.Clientes;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Clientes
{
    public class RepositorioCliente : IRepositorioCliente
    {

        private readonly BarbeariaOperacionalDbContext _dbContext;


        public RepositorioCliente(BarbeariaOperacionalDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<bool> CriarAsync(Cliente cliente, CancellationToken cancellationToken = default)
        {
            await _dbContext.Clientes.AddAsync(
          cliente,
          cancellationToken);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (DbUpdateException)
            {
                // A restrição única em UsuarioId protege contra
                // duas requisições simultâneas para a mesma conta.
                var perfilExistente = await _dbContext.Clientes
                    .AsNoTracking()
                    .AnyAsync(
                        x => x.UsuarioId == cliente.UsuarioId,
                        cancellationToken);

                if (perfilExistente)
                {
                    _dbContext.Entry(cliente).State =
                        EntityState.Detached;

                    return false;
                }

                // Não mascarar erros de banco que não sejam
                // a duplicidade do perfil.
                throw;
            }

        }

        public async Task<Cliente?> ObterPorUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Clientes
                .AsNoTracking()
                .SingleOrDefaultAsync(
                x => x.UsuarioId == usuarioId, cancellationToken);
        }
    }
}
