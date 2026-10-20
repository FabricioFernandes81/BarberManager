using Domain.Entities.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Clientes
{
    public interface IRepositorioCliente
    {
        Task<Cliente?> ObterPorUsuarioIdAsync(
       Guid usuarioId,
       CancellationToken cancellationToken = default);

        Task<bool> CriarAsync(
            Cliente cliente,
            CancellationToken cancellationToken = default);

    }
}
