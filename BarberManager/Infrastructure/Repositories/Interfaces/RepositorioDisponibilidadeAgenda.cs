using Application.Interfaces.Agendamentos;
using Domain.Entities.Agendamentos;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Profissionais;
using Domain.Entities.Servicos;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public class RepositorioDisponibilidadeAgenda : IRepositorioDisponibilidadeAgenda
    {
        private readonly BarbeariaOperacionalDbContext _dbContext;

        public RepositorioDisponibilidadeAgenda(BarbeariaOperacionalDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<bool> ExisteFeriadoAsync(Guid empresaId, DateOnly data, CancellationToken cancellationToken)
        {
            return await _dbContext.Feriados.AsNoTracking()
                .AnyAsync(x => x.EmpresaId == empresaId
                && x.Data == data && x.Ativo, cancellationToken);
        }

        public async Task<List<Agendamento>> ObterAgendamentosAsync(Guid empresaId, Guid profissionalId, DateTimeOffset inicio, DateTimeOffset fim, CancellationToken cancellationToken)
        {
            return await _dbContext.Agendamentos
                 .AsNoTracking()
                 .Where(x => x.EmpresaId == empresaId &&
                 x.ProfissionalId == profissionalId &&
                 x.DataHoraInicio < fim &&
                 x.DataHoraFim > inicio)
                 .ToListAsync(cancellationToken);
        }

        public async Task<List<BloqueioAgenda>> ObterBloqueiosAsync(Guid empresaId, Guid profissionalId, DateTimeOffset inicio, DateTimeOffset fim, CancellationToken cancellationToken)
        {
            return await _dbContext.BloqueiosAgenda
                 .AsNoTracking()
                 .Where(x => x.EmpresaId == empresaId && x.Ativo && (x.ProfissionalId == null ||
                 x.ProfissionalId == profissionalId) && x.DataHoraInicio < fim && x.DataHoraFim > inicio)
                 .ToListAsync(cancellationToken);
        }

        public async Task<ConfiguracaoAgendaEmpresa?> ObterConfiguracaoAgendaEmpresaAsync(Guid empresaId, CancellationToken cancellationToken)
        {
            return await _dbContext.ConfiguracoesAgendaEmpresa
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.EmpresaId == empresaId, cancellationToken);
        }

        public async Task<List<HorarioFuncionamento>> ObterFuncionamentoAsync(Guid empresaId, DayOfWeek diaSemana, CancellationToken cancellationToken)
        {
            return await _dbContext.HorariosFuncionamento
                .AsNoTracking()
                .Where(x => x.EmpresaId == empresaId && 
                       x.DiaSemana == diaSemana && x.Ativo)
                .OrderBy(x => x.HoraInicio)
                    .ToListAsync(cancellationToken);

        }

        public async Task<List<HorarioProfissional>> ObterJornadaAsync(Guid empresaId, Guid profissionalId, DayOfWeek diaSemana, CancellationToken cancellationToken)
        {
            return await _dbContext.HorariosProfissionais
                .AsNoTracking()
                    .Where(x => x.EmpresaId == empresaId &&
                    x.ProfissionalId == profissionalId &&
                    x.DiaSemana == diaSemana &&
                    x.Ativo)
                    .OrderBy(x => x.HoraInicio).ToListAsync(cancellationToken);
        }

        public async Task<Profissional?> ObterProfissionalAsync(Guid empresaId, Guid profissionalId, CancellationToken cancellationToken)
        {
            return await _dbContext.Profissionais
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == profissionalId &&
                x.EmpresaId == empresaId, cancellationToken);
        }

        public async Task<Servico?> ObterServicoAsync(Guid empresaId, Guid servicoId, CancellationToken cancellationToken)
        {
            return await _dbContext.Servicos
                .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == servicoId &&
                    x.EmpresaId == empresaId, cancellationToken);
        }

        public async Task<ProfissionalServicos?> ObterVinculoProfissionalServicoAsync(Guid empresaId, Guid profissionalId, Guid servicoId, CancellationToken cancellationToken)
        {
            return await _dbContext.ProfissionalServicos
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x =>
                 x.EmpresaId == empresaId && x.ProfissionalId == profissionalId &&
                 x.ServicoId == servicoId && x.Ativo == StatusGeral.Ativo, cancellationToken);
        }

        public async Task<bool> ProfissionalExecutaServicoAsync(Guid empresaId, Guid profissionalId, Guid servicoId, CancellationToken cancellationToken)
        {
            return await _dbContext.ProfissionalServicos
                .AsNoTracking()
                    .AnyAsync(x => x.EmpresaId == empresaId && x.ProfissionalId == profissionalId
                    && x.ServicoId == servicoId && x.Ativo == StatusGeral.Ativo, cancellationToken);
        }
    }
}
