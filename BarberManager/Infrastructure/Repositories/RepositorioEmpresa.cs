using Application.Interfaces.Empresas;
using Application.Saas.Empresas.DTOs;
using Domain.Entities.Common.Enuns;
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


        public async Task<(List<GetListEmpresaDto> Empresas, int TotalRegistros)> ListarAsync(int pageNumber, int pageSize, string? searchTerm, string? sortBy, string? Status, CancellationToken cancellationToken = default)
        {
            var queryEmpresa = _dbContext.Empresas
                .Include(x => x.Assinaturas)
                .ThenInclude(p => p.Plano)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = $"%{searchTerm}%";
                queryEmpresa = queryEmpresa.Where(c => c.NomeFantasia.Contains(term));
            }
            if(!string.IsNullOrWhiteSpace(Status) && Status != "Todos")
            {
                if(Enum.TryParse<StatusGeral>(Status, out var statusEnum)){

                    queryEmpresa = queryEmpresa.Where(s => s.Ativa == statusEnum);

                }
            }

            var recordTotal = await queryEmpresa.CountAsync();

            var sortQuery = sortBy switch
            {


                _=> queryEmpresa.OrderBy(c=>c.Id)
            };

            var itemsQuery = await sortQuery
                .Select(e=> new GetListEmpresaDto
                {
                    Id = e.Id.ToString(),
                    CNPJ = e.Cnpj,
                    NameFantasia = e.NomeFantasia,
                    Status = e.Ativa.ToString(),
                    Plano = e.Assinaturas.OrderByDescending(a=>a.DataInicio)
                    .Select(a=>a.Plano.Nome).FirstOrDefault()?? "Sem assinatura",
                  //  e.Assinaturas.Plano.Nome,
                    StatusAssinatura = e.Assinaturas.OrderByDescending(a=>a.DataInicio)
                    .Select(a=> a.Ativa.ToString()).FirstOrDefault() ?? "Sem assinatura",
                    //e.Assinaturas.Ativa.ToString()
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
                
            return (itemsQuery, recordTotal);
        }
    }
}
