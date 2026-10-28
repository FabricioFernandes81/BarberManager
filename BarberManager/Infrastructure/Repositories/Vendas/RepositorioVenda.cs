using Application.Interfaces.Vendas;
using Domain.Entities.Vendas;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Vendas
{
    public class RepositorioVenda : IRepositorioVenda
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioVenda(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<Venda?> ObterPorAgendamentoAsync(
            Guid empresaId,
            Guid agendamentoId,
            CancellationToken cancellationToken)
        {
            return await _context.Vendas
                .AsNoTracking()
                .Include(v => v.Itens)
                .SingleOrDefaultAsync(
                    v => v.EmpresaId == empresaId
                      && v.AgendamentoId == agendamentoId,
                    cancellationToken);
        }

        public async Task<Venda?> ObterPorIdAsync(
            Guid empresaId,
            Guid vendaId,
            CancellationToken cancellationToken)
        {
            return await _context.Vendas
                .AsNoTracking()
                .Include(v => v.Itens)
                .SingleOrDefaultAsync(
                    v => v.EmpresaId == empresaId
                      && v.Id == vendaId,
                    cancellationToken);
        }

        public async Task<Venda?> ObterParaAtualizacaoAsync(
            Guid empresaId,
            Guid vendaId,
            CancellationToken cancellationToken)
        {
            return await _context.Vendas
                .Include(v => v.Itens)
                .SingleOrDefaultAsync(
                    v => v.EmpresaId == empresaId
                      && v.Id == vendaId,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            Venda venda,
            CancellationToken cancellationToken)
        {
            await _context.Vendas.AddAsync(
                venda,
                cancellationToken);

            // Não chamar SaveChangesAsync aqui.
            // A persistência será coordenada pela transação
            // da aplicação.
        }

        public Task RemoverItemAsync(Guid empresaId, Guid vendaId, Guid itemVendaId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
