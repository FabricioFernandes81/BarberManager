using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IValidadorEmpresaAutenticacao
    {
        Task<bool> EmpresaEstaAtivaAsync(
            Guid empresaId,
            CancellationToken cancellationToken = default);
    }
}
