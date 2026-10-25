using Domain.Entities.Agendamentos;
using Domain.Entities.Clientes;
using Domain.Entities.Profissionais;
using Domain.Entities.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Agendamentos
{
    public interface IRepositorioCriarAgendamento
    {
        Task<bool> ClienteVinculadoEmpresaAsync(
        Guid empresaId,
        Guid clienteId,
        CancellationToken cancellationToken);

        Task<Profissional?> ObterProfissionalAsync(
            Guid empresaId,
            Guid profissionalId,
            CancellationToken cancellationToken);

        Task<Cliente?> ObterClienteAsync(
            Guid clienteId,
            CancellationToken cancellationToken);

        Task<Servico?> ObterServicoAsync(
            Guid empresaId,
            Guid servicoId,
            CancellationToken cancellationToken);

        Task<ProfissionalServicos?> ObterVinculoProfissionalServicoAsync(
            Guid empresaId,
            Guid profissionalId,
            Guid servicoId,
            CancellationToken cancellationToken);

        Task<List<HorarioFuncionamento>> ObterFuncionamentoAsync(
            Guid empresaId,
            DayOfWeek diaSemana,
            CancellationToken cancellationToken);

        Task<List<HorarioProfissional>> ObterJornadaAsync(
            Guid empresaId,
            Guid profissionalId,
            DayOfWeek diaSemana,
            CancellationToken cancellationToken);

        Task<bool> ExisteFeriadoAsync(
            Guid empresaId,
            DateOnly data,
            CancellationToken cancellationToken);

        Task<List<BloqueioAgenda>> ObterBloqueiosAsync(
            Guid empresaId,
            Guid profissionalId,
            DateTimeOffset inicio,
            DateTimeOffset fim,
            CancellationToken cancellationToken);

        Task<List<Agendamento>> ObterAgendamentosAsync(
            Guid empresaId,
            Guid profissionalId,
            DateTimeOffset inicio,
            DateTimeOffset fim,
            CancellationToken cancellationToken);

        Task CriarAsync(
            Agendamento agendamento,
            CancellationToken cancellationToken);

        Task<ConfiguracaoAgendaEmpresa?> ObterConfiguracaoAgendaEmpresaAsync(
    Guid empresaId,
    CancellationToken cancellationToken);
    }
}
