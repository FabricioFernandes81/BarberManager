using Application.Interfaces.Empresas;
using Application.Saas.Planos.DTOs;
using Domain.Entities.Common.Enuns;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositórioPlano : IRepositórioPlano
    {

        private readonly BarbeariaSaasDbContext _dbContext;

        public RepositórioPlano(BarbeariaSaasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<GetPlanoDto> planos, int totalRecords)> ListarAsync(int pageNumber, int pageSize, string? searchTerm, string? sortBy, string? Status, CancellationToken cancellationToken = default)
        {
            var queryPlanos = _dbContext.Planos
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = $"%{searchTerm}%";
                queryPlanos = queryPlanos.Where(c => c.Nome.Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(Status) && Status != "Todos")
            {
                if (Enum.TryParse<StatusGeral>(Status, out var statusEnum))
                {

                    queryPlanos = queryPlanos.Where(s => s.Ativo == statusEnum);

                }
            }

            var total = await queryPlanos.CountAsync(cancellationToken);

            var queryPlano = sortBy switch
            {
                _ => queryPlanos.OrderBy(c => c.Id)
            };

            var items = await queryPlano
                .Select(p => new GetPlanoDto
                {
                    Id = p.Id,
                    ValorMensal = p.ValorMensal,
                    ValorAnual = p.ValorAnual,
                    LimiteProfissionais = p.LimiteProfissionais,
                    LimiteEmpresa = p.LimiteEmpresas,
                    Ativo = p.Ativo.ToString(),
                    Nome = p.Nome
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, total);
        }
    }
}
