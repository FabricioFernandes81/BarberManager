using Application.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositorioClienteAutenticacao : IRepositorioClienteAutenticacao
    {
        private readonly BarbeariaOperacionalDbContext _dbContext;

        public RepositorioClienteAutenticacao(
            BarbeariaOperacionalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> PossuiClienteAtivoPorUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Clientes
              .AsNoTracking()
              .AnyAsync(
                  cliente =>
                      cliente.UsuarioId == usuarioId &&
                      cliente.Ativo,
                  cancellationToken);
        }
    }
}
