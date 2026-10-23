using Application.Interfaces.Profissionais;
using Domain.Entities.Agendamentos;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Profissionais;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RepositorioProfissionais : IRepositorioProfissionais
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioProfissionais(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // CADASTRO
        // =====================================================

        public async Task CriarAsync(
            Profissional profissional,
            IReadOnlyCollection<HorarioProfissional> horarios,
            CancellationToken cancellationToken)
        {
            await _context.Profissionais.AddAsync(
                profissional,
                cancellationToken);

            if (horarios.Count > 0)
            {
                await _context.HorariosProfissionais.AddRangeAsync(
                    horarios,
                    cancellationToken);
            }
        }

        // =====================================================
        // VERIFICAÇÃO DE CPF
        // =====================================================

        public async Task<bool> ExisteCpfAsync(
            Guid empresaId,
            string cpf,
            CancellationToken cancellationToken,
            Guid? profissionalIdIgnorado = null)
        {
            IQueryable<Profissional> query =
                _context.Profissionais
                    .AsNoTracking()
                    .Where(p =>
                        p.EmpresaId == empresaId &&
                        p.Cpf == cpf);

            if (profissionalIdIgnorado.HasValue)
            {
                query = query.Where(p =>
                    p.Id != profissionalIdIgnorado.Value);
            }

            return await query.AnyAsync(cancellationToken);
        }

        // =====================================================
        // VERIFICAÇÃO DE MATRÍCULA
        // =====================================================

        public async Task<bool> ExisteMatriculaAsync(
            Guid empresaId,
            string matricula,
            CancellationToken cancellationToken,
            Guid? profissionalIdIgnorado = null)
        {
            IQueryable<Profissional> query =
                _context.Profissionais
                    .AsNoTracking()
                    .Where(p =>
                        p.EmpresaId == empresaId &&
                        p.Matricula == matricula);

            if (profissionalIdIgnorado.HasValue)
            {
                query = query.Where(p =>
                    p.Id != profissionalIdIgnorado.Value);
            }

            return await query.AnyAsync(cancellationToken);
        }

        // =====================================================
        // LISTAGEM PAGINADA
        // =====================================================

        public async Task<(
            IReadOnlyList<Profissional> Itens,
            int TotalRegistros)> ListarAsync(
                Guid empresaId,
                int pageNumber,
                int pageSize,
                string? searchTerm,
                StatusProfissional? status,
                CancellationToken cancellationToken)
        {
            IQueryable<Profissional> query =
                _context.Profissionais
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

        // =====================================================
        // CONSULTA DOS HORÁRIOS DO PROFISSIONAL
        // =====================================================

        public async Task<IReadOnlyList<HorarioProfissional>>
            ObterHorariosPorProfissionalAsync(
                Guid empresaId,
                Guid profissionalId,
                CancellationToken cancellationToken,
                bool rastrearAlteracoes = false)
        {
            IQueryable<HorarioProfissional> query =
                _context.HorariosProfissionais
                    .Where(h =>
                        h.EmpresaId == empresaId &&
                        h.ProfissionalId == profissionalId);

            if (!rastrearAlteracoes)
            {
                query = query.AsNoTracking();
            }

            return await query
                .OrderBy(h => h.DiaSemana)
                .ThenBy(h => h.HoraInicio)
                .ToListAsync(cancellationToken);
        }

        // =====================================================
        // CONSULTA DO PROFISSIONAL POR ID
        // =====================================================

        public async Task<Profissional?> ObterPorIdAsync(
            Guid empresaId,
            Guid profissionalId,
            CancellationToken cancellationToken,
            bool rastrearAlteracoes = false)
        {
            IQueryable<Profissional> query =
                _context.Profissionais
                    .Where(p =>
                        p.EmpresaId == empresaId &&
                        p.Id == profissionalId);

            if (!rastrearAlteracoes)
            {
                query = query.AsNoTracking();
            }

            return await query
                .Include(p => p.Servicos)
                .AsSingleQuery()
                .SingleOrDefaultAsync(cancellationToken);
        }

        // =====================================================
        // ADICIONAR NOVO HORÁRIO
        // =====================================================

        public async Task AdicionarHorarioAsync(
            HorarioProfissional horario,
            CancellationToken cancellationToken)
        {
            await _context.HorariosProfissionais.AddAsync(
                horario,
                cancellationToken);
        }

        public async Task<Profissional?> ObterPorIdAsync(Guid empresaId, Guid profissionalId, CancellationToken cancellationToken)
        {
            return await _context.Profissionais
        .AsNoTracking()
        .AsSplitQuery()
        .Where(p =>
            p.EmpresaId == empresaId &&
            p.Id == profissionalId)
        .Include(p => p.Servicos)
            .ThenInclude(ps => ps.Servico)
        .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<HorarioProfissional>> ObterHorariosPorProfissionalAsync(Guid empresaId, Guid profissionalId, CancellationToken cancellationToken)
        {
            return await _context.HorariosProfissionais
       .AsNoTracking()
       .Where(h =>
           h.EmpresaId == empresaId &&
           h.ProfissionalId == profissionalId)
       .OrderBy(h => h.DiaSemana)
       .ThenBy(h => h.HoraInicio)
       .ToListAsync(cancellationToken);
        }
    }
}