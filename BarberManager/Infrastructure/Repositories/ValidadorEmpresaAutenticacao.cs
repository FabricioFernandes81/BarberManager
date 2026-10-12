using Domain.Entities.Common.Enuns;
using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public sealed class ValidadorEmpresaAutenticacao
    : IValidadorEmpresaAutenticacao
    {
        private readonly BarbeariaSaasDbContext _context;

        public ValidadorEmpresaAutenticacao(
            BarbeariaSaasDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EmpresaEstaAtivaAsync(
            Guid empresaId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Empresas
         .AsNoTracking()
         .AnyAsync(
             empresa =>
                 empresa.Id == empresaId &&
                 empresa.Ativa == StatusGeral.Ativo,
             cancellationToken);
        }
    }
}
