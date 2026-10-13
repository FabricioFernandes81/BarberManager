using Application.Saas.Assinaturas.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Empresas
{
    public interface IRepositorioAssinatura
    {
        Task<(List<GetAssinaturaDto> asssinaturas, int totalrecord)> ListAsync(int pageNumber, int pageSize, string? searchTerm, string? sortBy, string? Status,
                                            CancellationToken cancellationToken = default);

    }
}
