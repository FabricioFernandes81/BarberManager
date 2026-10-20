using Application.Interfaces.Empresas;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Saas;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositorioFatura : IRepositorioFatura
    {
        public readonly BarbeariaSaasDbContext _dbContext;

        public RepositorioFatura(BarbeariaSaasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AdicionarAsync(Fatura fatura, CancellationToken cancellationToken = default)
        {
            await _dbContext.Faturas.AddAsync(fatura, cancellationToken);
        }

        public async Task<Assinatura?> ObterAssinaturaAsync(Guid assinaturaId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Assinaturas
                .FirstOrDefaultAsync(x => x.Id == assinaturaId, cancellationToken);

        }

        public async Task<IReadOnlyList<Assinatura>> ObterAssinaturasParaFaturamentoAsync(DateTime limiteVencimento, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Assinaturas
       .AsNoTracking()
       .Where(a =>
           !a.Trial &&
           (a.Ativa == StatusAssinaturaCliente.Ativa ||
            a.Ativa == StatusAssinaturaCliente.Pendente) &&
           a.DataVencimento < limiteVencimento)
       .OrderBy(a => a.DataVencimento)
       .ToListAsync(cancellationToken);
        }

        public async Task<Fatura?> ObterPorPeriodoAsync(Guid assinaturaId, DateTime periodoInicio, DateTime periodoFim, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Faturas
                    .FirstOrDefaultAsync(x => x.AssinaturaId == assinaturaId &&
                    x.PeriodoInicio == periodoInicio &&
                    x.PeriodoFim == periodoFim, cancellationToken);
        }
    }
}
