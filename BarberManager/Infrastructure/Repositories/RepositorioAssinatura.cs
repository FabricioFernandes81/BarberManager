using Application.Interfaces.Empresas;
using Application.Saas.Assinaturas.DTOs;
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
    public class RepositorioAssinatura : IRepositorioAssinatura
    {
        public readonly BarbeariaSaasDbContext _dbContext;

        public RepositorioAssinatura(BarbeariaSaasDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<(List<GetAssinaturaDto> asssinaturas, int totalrecord)> ListAsync(int pageNumber, int pageSize, string? searchTerm, string? sortBy, string? Status, CancellationToken cancellationToken = default)
        {
            var queryAssinatura = _dbContext.Assinaturas
                .Include(e=>e.Empresa)
                .AsQueryable()
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = $"%{searchTerm}%";
                queryAssinatura = queryAssinatura.Where(c => c.Empresa.NomeFantasia.Contains(term));
            }

            if (!string.IsNullOrWhiteSpace(Status) && Status != "Todos")
            {
                if (Enum.TryParse<StatusAssinaturaCliente>(Status, out var statusEnum))
                {

                    queryAssinatura = queryAssinatura.Where(s => s.Ativa == statusEnum);

                }
            }

            var total = await queryAssinatura.CountAsync(cancellationToken);

             queryAssinatura = sortBy switch
             {

                _=> queryAssinatura.OrderBy(x=>x.Id)
             };

            var items = await queryAssinatura.Select(a => new GetAssinaturaDto
            {
                Id = a.Id,
                Empresa = a.Empresa.NomeFantasia,
                Plano = a.Plano.Nome,
                Valor = 20m,
                DataInicio = a.DataInicio,
                DataVencimento = a.DataVencimento,
                DataUltimoPagamento = a.DataUltimoPagamento,
                DiaVencimento = a.DiaVencimento,
                DiasParaVencer = 0,
                Status = a.Ativa.ToString(),
                RenovacaoAutomatica = a.RenovacaoAutomatica,
                Trial = a.Trial,

            }).Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

            return (items, total);

        }
    }
}
