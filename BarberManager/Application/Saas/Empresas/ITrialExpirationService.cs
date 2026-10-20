using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Empresas
{
    public interface ITrialExpirationService
    {
       
        Task<int> ExpirarTrialsVencidosAsync(
            DateTime agoraUtc,
            CancellationToken cancellationToken = default);
    }
}
