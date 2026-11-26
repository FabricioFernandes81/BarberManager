using Application.Interfaces.Servicos;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Servicos;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Servicos
{
    public sealed class RepositorioServico : IRepositorioServico
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioServico(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<Servico?> ObterPorIdAsync(
            Guid empresaId,
            Guid servicoId,
            CancellationToken cancellationToken)
        {
            return await _context.Servicos
                    .AsNoTracking()
                    .SingleOrDefaultAsync(
                        servico =>
                            servico.Id == servicoId &&
                            servico.EmpresaId == empresaId,
                        cancellationToken);
        }

        public async Task<IReadOnlyCollection<Servico>>
    ListarDisponiveisWhatsAppAsync(
        Guid empresaId,
        string? searchTerm,
        CancellationToken cancellationToken)
        {
            var query = _context.Servicos
                .AsNoTracking()
                .Include(x => x.CategoriaServico)
                .Where(x =>
                    x.EmpresaId == empresaId &&
                    x.Ativo == StatusGeral.Ativo &&
                    x.PermiteAgendamentoOnline);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var termo = searchTerm.Trim();

                query = query.Where(x =>
                    x.Nome.Contains(termo) ||
                    (x.Descricao != null &&
                     x.Descricao.Contains(termo)));
            }

            return await query
                .OrderBy(x => x.Nome)
                .ToListAsync(cancellationToken);
        }
    }
}
