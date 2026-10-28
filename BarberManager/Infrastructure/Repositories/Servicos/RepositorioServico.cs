using Application.Interfaces.Servicos;
using Domain.Entities.Servicos;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Servicos
{
    public sealed class RepositorioServico : IRepositorioServico
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioServico(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<Servico?> ObterPorIdAsync(
            Guid empresaId,
            Guid servicoId,
            CancellationToken cancellationToken)
        {
            return await _context.Servicos
                    .AsNoTracking()
                    .SingleOrDefaultAsync(
                        servico =>
                            servico.Id == servicoId &&
                            servico.EmpresaId == empresaId,
                        cancellationToken);
        }
    }
}
