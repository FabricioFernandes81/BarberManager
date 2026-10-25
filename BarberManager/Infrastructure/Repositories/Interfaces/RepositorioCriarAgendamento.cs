using Application.Interfaces.Agendamentos;
using Domain.Entities.Agendamentos;
using Domain.Entities.Clientes;
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
    public class RepositorioCriarAgendamento
      : IRepositorioCriarAgendamento
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioCriarAgendamento(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ClienteVinculadoEmpresaAsync(
            Guid empresaId,
            Guid clienteId,
            CancellationToken cancellationToken)
        {
            return await _context.ClientesEmpresas
                .AsNoTracking()
                .AnyAsync(
                    x => x.EmpresaId == empresaId
                      && x.ClienteId == clienteId
                      && x.Ativo,
                    cancellationToken);
        }

        public Task<Cliente?> ObterClienteAsync(
            Guid clienteId,
            CancellationToken cancellationToken)
        {
            return _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.Id == clienteId,
                    cancellationToken);
        }

        public Task<Profissional?> ObterProfissionalAsync(
            Guid empresaId,
            Guid profissionalId,
            CancellationToken cancellationToken)
        {
            return _context.Profissionais
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.EmpresaId == empresaId
                      && x.Id == profissionalId,
                    cancellationToken);
        }

        public async Task<Servico?> ObterServicoAsync(
            Guid empresaId,
            Guid servicoId,
            CancellationToken cancellationToken)
        {
            return await _context.Servicos
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.EmpresaId == empresaId
                      && x.Id == servicoId,
                    cancellationToken);
        }

        public async Task<ProfissionalServicos?>
            ObterVinculoProfissionalServicoAsync(
                Guid empresaId,
                Guid profissionalId,
                Guid servicoId,
                CancellationToken cancellationToken)
        {
            return await _context.ProfissionalServicos
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.EmpresaId == empresaId
                      && x.ProfissionalId == profissionalId
                      && x.ServicoId == servicoId
                      && x.Ativo == StatusGeral.Ativo,
                    cancellationToken);
        }

        public async Task<List<HorarioFuncionamento>> ObterFuncionamentoAsync(
            Guid empresaId,
            DayOfWeek diaSemana,
            CancellationToken cancellationToken)
        {
            return await _context.HorariosFuncionamento
                .AsNoTracking()
                .Where(
                    x => x.EmpresaId == empresaId
                      && x.DiaSemana == diaSemana
                      && x.Ativo)
                .OrderBy(x => x.HoraInicio)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<HorarioProfissional>> ObterJornadaAsync(
            Guid empresaId,
            Guid profissionalId,
            DayOfWeek diaSemana,
            CancellationToken cancellationToken)
        {
            return await _context.HorariosProfissionais
                .AsNoTracking()
                .Where(
                    x => x.EmpresaId == empresaId
                      && x.ProfissionalId == profissionalId
                      && x.DiaSemana == diaSemana
                      && x.Ativo)
                .OrderBy(x => x.HoraInicio)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExisteFeriadoAsync(
            Guid empresaId,
            DateOnly data,
            CancellationToken cancellationToken)
        {
            return await _context.Feriados
                .AsNoTracking()
                .AnyAsync(
                    x => x.EmpresaId == empresaId
                      && x.Data == data
                      && x.Ativo,
                    cancellationToken);
        }

        public async Task<List<BloqueioAgenda>> ObterBloqueiosAsync(
            Guid empresaId,
            Guid profissionalId,
            DateTimeOffset inicio,
            DateTimeOffset fim,
            CancellationToken cancellationToken)
        {
            return await _context.BloqueiosAgenda
                .AsNoTracking()
                .Where(
                    x => x.EmpresaId == empresaId
                      && x.Ativo
                      && (
                          x.ProfissionalId == null
                          || x.ProfissionalId == profissionalId
                      )
                      && x.DataHoraInicio < fim
                      && x.DataHoraFim > inicio)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Agendamento>> ObterAgendamentosAsync(
            Guid empresaId,
            Guid profissionalId,
            DateTimeOffset inicio,
            DateTimeOffset fim,
            CancellationToken cancellationToken)
        {
            return await _context.Agendamentos
                .AsNoTracking()
                .Where(
                    x => x.EmpresaId == empresaId
                      && x.ProfissionalId == profissionalId
                      && x.DataHoraInicio < fim
                      && x.DataHoraFim > inicio)
                .ToListAsync(cancellationToken);
        }

        public async Task CriarAsync(
            Agendamento agendamento,
            CancellationToken cancellationToken)
        {
            await _context.Agendamentos.AddAsync(
                agendamento,
                cancellationToken);

            // A gravação é confirmada pelo TransactionMiddleware
            // ao finalizar a transação operacional.
        }

        public async Task<ConfiguracaoAgendaEmpresa?> ObterConfiguracaoAgendaEmpresaAsync(Guid empresaId, CancellationToken cancellationToken)
        {
            return await _context.ConfiguracoesAgendaEmpresa
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.EmpresaId == empresaId, cancellationToken);
        }
    }
}
