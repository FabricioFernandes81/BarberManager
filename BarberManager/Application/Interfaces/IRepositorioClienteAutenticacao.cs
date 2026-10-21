using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepositorioClienteAutenticacao
    {
        Task<bool> PossuiClienteAtivoPorUsuarioIdAsync(
            Guid usuarioId,
            CancellationToken cancellationToken = default);
    }
}
