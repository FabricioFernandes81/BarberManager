using Application.Saas.Planos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Empresas
{
    public interface IRepositórioPlano
    {
        Task<(List<GetPlanoDto> planos, int totalRecords)> ListarAsync(
            int pageNumber, int pageSize, string? searchTerm, string? sortBy, string? Status,
                                            CancellationToken cancellationToken = default);


    }
}
