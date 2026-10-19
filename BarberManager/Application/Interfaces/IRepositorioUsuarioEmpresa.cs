using Application.Saas.Empresas.DTOs;
using Application.Saas.Usuarios.DTOs;
using Application.Saas.Usuarios.Enums;
using Domain.Entities.Saas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
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

        Task<GetUsuarioDetalheDto?> ObterDetalheAsync(
      Guid usuarioId,
      CancellationToken cancellationToken = default);


        Task<ResultadoVinculoUsuarioEmpresa> VincularOuReativarAsync(
    Guid usuarioId,
    Guid empresaId,
    CancellationToken cancellationToken = default);

        Task RemoverVinculoCriadoAsync(
    Guid usuarioId,
    Guid empresaId,
    CancellationToken cancellationToken = default);


    }
}
