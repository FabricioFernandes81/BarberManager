using Domain.Entities.Agendamentos;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Profissionais;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Profissionais
{
    public interface IRepositorioProfissionais
    {
        Task<(IReadOnlyList<Profissional> Itens,int TotalRegistros )> ListarAsync(
       Guid empresaId,int pageNumber,
       int pageSize, string? searchTerm,
       StatusProfissional? status, CancellationToken cancellationToken);

        Task<bool> ExisteCpfAsync(
      Guid empresaId,
      string cpf,
      CancellationToken cancellationToken,
      Guid? profissionalIdIgnorado = null);

        Task<bool> ExisteMatriculaAsync(
            Guid empresaId,
            string matricula,
            CancellationToken cancellationToken,
            Guid? profissionalIdIgnorado = null);

        Task<Profissional?> ObterPorIdAsync(
            Guid empresaId,
            Guid profissionalId,
            CancellationToken cancellationToken,
            bool rastrearAlteracoes = false);

        Task<IReadOnlyList<HorarioProfissional>>
            ObterHorariosPorProfissionalAsync(
                Guid empresaId,
                Guid profissionalId,
                CancellationToken cancellationToken,
                bool rastrearAlteracoes = false);

        Task AdicionarHorarioAsync(
            HorarioProfissional horario,
            CancellationToken cancellationToken);

        Task CriarAsync(
            Profissional profissional,
            IReadOnlyCollection<HorarioProfissional> horarios,
            CancellationToken cancellationToken);


    /*    Task<Profissional?> ObterPorIdAsync(
    Guid empresaId, Guid profissionalId,
    CancellationToken cancellationToken);
    */
        Task<IReadOnlyList<HorarioProfissional>>
            ObterHorariosPorProfissionalAsync(
                Guid empresaId, Guid profissionalId,
                CancellationToken cancellationToken);
    }
}
