using Domain.Entities.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Servicos
{
    public interface IRepositorioServico
    {
        Task<Servico?> ObterPorIdAsync(
           Guid empresaId,
           Guid servicoId,
           CancellationToken cancellationToken);


        Task<IReadOnlyCollection<Servico>> ListarDisponiveisWhatsAppAsync(
    Guid empresaId,
    string? searchTerm,
    CancellationToken cancellationToken);
    }
}
