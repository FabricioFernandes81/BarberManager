using Application.Empresas.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Empresas
{
    public interface IRepositorioEmpresa
    {
        Task<(List<GetListEmpresaDto> Empresas, int TotalRegistros)> ListarAsync(
                     int pageNumber,int pageSize, string? searchTerm, string? sortBy,
                                            CancellationToken cancellationToken = default);
        }
}
