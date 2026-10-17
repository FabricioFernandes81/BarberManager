using Application.Saas.Empresas.DTOs;
using Application.Saas.Empresas.Enums;
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
                     int pageNumber,int pageSize, string? searchTerm, string? sortBy, string? Status,
                                            CancellationToken cancellationToken = default);

                    Task<bool> ExisteAsync(
            Guid empresaId,
            CancellationToken cancellationToken = default);


        Task<ResultadoAlteracaoAcessoEmpresa> AlterarAcessoAsync(
    Guid empresaId,
    bool habilitarAcesso,
    string? motivo,
    CancellationToken cancellationToken = default);
    }
}
