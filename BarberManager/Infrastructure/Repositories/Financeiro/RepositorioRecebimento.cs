using Application.Interfaces.Financeiro;
using Domain.Entities.Financeiro;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Financeiro
{
    public class RepositorioRecebimento : IRepositorioRecebimento
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioRecebimento(
            BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<ContaReceber?> ObterContaParaRecebimentoAsync(
            Guid empresaId,
            Guid contaReceberId,
            CancellationToken cancellationToken)
        {
            // A conta permanece rastreada pelo EF Core,
            // pois será atualizada pelo handler.
            return await _context.ContasReceber
                .FirstOrDefaultAsync(
                    x => x.EmpresaId == empresaId
                      && x.Id == contaReceberId,
                    cancellationToken);
        }

        public async Task<FormaPagamento?> ObterFormaPagamentoAtivaAsync(
            Guid empresaId,
            Guid formaPagamentoId,
            CancellationToken cancellationToken)
        {
            return await _context.FormaPagamentos
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.EmpresaId == empresaId
                      && x.Id == formaPagamentoId
                      && x.Ativo,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            Recebimento recebimento,
            CancellationToken cancellationToken)
        {
            await _context.Recebimentos.AddAsync(
                recebimento,
                cancellationToken);

            // Não chamar SaveChangesAsync aqui.
            // O TransactionMiddleware confirma a transação.
        }
    }
}
