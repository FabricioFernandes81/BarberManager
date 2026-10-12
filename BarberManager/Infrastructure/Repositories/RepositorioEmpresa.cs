using Application.Empresas.DTOs;
using Application.Interfaces.Empresas;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositorioEmpresa : IRepositorioEmpresa
    {

        private readonly BarbeariaSaasDbContext _dbContext;

        public RepositorioEmpresa(BarbeariaSaasDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<(List<GetListEmpresaDto> Empresas, int TotalRegistros)> ListarAsync(int pageNumber, int pageSize, string? searchTerm, string? sortBy, CancellationToken cancellationToken = default)
        {
            var query = await _dbContext.Empresas
                .Include(x=>x.Assinaturas)
                .ThenInclude(p=>p.Plano)
                .ToListAsync();

            return (null,1);
        }
    }
}
