using Application.Saas.Empresas;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Saas;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TrialExpirationService : ITrialExpirationService
    {
        private readonly BarbeariaSaasDbContext _dbContext;

        public TrialExpirationService(BarbeariaSaasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> ExpirarTrialsVencidosAsync(DateTime agoraUtc, CancellationToken cancellationToken = default)
        {
           if (agoraUtc.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("O horário informatdo deve estar em UTC.", nameof(agoraUtc));
          
            }

            return await _dbContext.Assinaturas.Where(a =>
             a.Trial && a.Ativa == StatusAssinaturaCliente.Ativa &&
             a.DataFimTrial.HasValue && a.DataFimTrial.Value <= agoraUtc)
             .ExecuteUpdateAsync(atualizacao => atualizacao
             .SetProperty(assinatura => assinatura.Ativa, StatusAssinaturaCliente.Expirada)
             .SetProperty(assinatura => assinatura.DataAtualizacao, (DateTime?)agoraUtc), cancellationToken);
            
            
        }
    }
}
