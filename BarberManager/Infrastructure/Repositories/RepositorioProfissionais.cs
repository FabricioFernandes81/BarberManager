using Application.Interfaces.Profissionais;
using Domain.Entities.Agendamentos;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Profissionais;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositorioProfissionais : IRepositorioProfissionais
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioProfissionais(BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task CriarAsync(Profissional profissional, IReadOnlyCollection<HorarioProfissional> horarios, CancellationToken cancellationToken)
        {
            await _context.Profissionais.AddAsync(profissional, cancellationToken);

            if(horarios.Count > 0)
            {
                await _context.HorariosProfissionais.AddRangeAsync(
                    horarios, cancellationToken);
            }

        }

        public async Task<bool> ExisteCpfAsync(Guid empresaId, string cpf, CancellationToken cancellationToken)
        {
            return await _context.Profissionais.AsNoTracking()
                .AnyAsync(p => p.EmpresaId == empresaId && p.Cpf == cpf, cancellationToken);
        }

        public async Task<bool> ExisteMatriculaAsync(Guid empresaId, string matricula, CancellationToken cancellationToken)
        {
            return await _context.Profissionais.AsNoTracking()
                 .AnyAsync(p => p.EmpresaId == empresaId && p.Matricula == matricula, cancellationToken);
        }

        public async Task<(IReadOnlyList<Profissional> Itens, int TotalRegistros)> ListarAsync(Guid empresaId, int pageNumber, int pageSize, string? searchTerm, StatusProfissional? status, CancellationToken cancellationToken)
        {
            IQueryable<Profissional> query = _context.Profissionais
            .AsNoTracking()
            .Where(p => p.EmpresaId == empresaId);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var termo = searchTerm.Trim();

                query = query.Where(p =>
                    p.Nome.Contains(termo) ||
                    (p.Sobrenome != null &&
                        p.Sobrenome.Contains(termo)) ||
                    p.Celular.Contains(termo) ||
                    p.Email.Contains(termo) ||
                    p.Matricula.Contains(termo) ||
                    p.Cargo.Contains(termo) ||
                    (p.Especialidade != null &&
                        p.Especialidade.Contains(termo)));
            }

            if (status.HasValue)
            {
                query = query.Where(p =>
                    p.Status == status.Value);
            }

            var totalRegistros = await query.CountAsync(
                cancellationToken);

            var itens = await query
                .OrderBy(p => p.OrdemAgenda)
                .ThenBy(p => p.Nome)
                .ThenBy(p => p.Sobrenome)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (itens, totalRegistros);
        }

        public async Task<IReadOnlyList<HorarioProfissional>> ObterHorariosPorProfissionalAsync(Guid empresaId, Guid profissionalId, CancellationToken cancellationToken)
        {
            return await _context.HorariosProfissionais
                .AsNoTracking()
                 .Where(h => h.EmpresaId == empresaId && h.ProfissionalId == profissionalId)
                    .OrderBy(h => h.DiaSemana)
                        .ThenBy(h => h.HoraInicio)
                             .ToListAsync(cancellationToken);
        }

        public async Task<Profissional?> ObterPorIdAsync(Guid empresaId, Guid profissionalId, CancellationToken cancellationToken)
        {
            return await _context.Profissionais
                 .AsNoTracking()
                 .AsSingleQuery()
                 .Where(p =>
                     p.EmpresaId == empresaId && p.Id == profissionalId)
                 .Include(p => p.Servicos)
                 .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
