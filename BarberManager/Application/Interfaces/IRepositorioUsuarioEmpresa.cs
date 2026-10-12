using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IRepositorioUsuarioEmpresa
    {
        Task<bool> PossuiVinculoAtivoAsync(Guid usuarioId,
            Guid empresaId,CancellationToken cancellationToken = default);

      Task<IReadOnlyList<Guid>> ObterEmpresasAtivasAsync(
            Guid usuarioId,CancellationToken cancellationToken = default);

        Task<IReadOnlyList<string>> ObterCodigosAsync(
       Guid usuarioId,
       CancellationToken cancellationToken = default);
    }
}
