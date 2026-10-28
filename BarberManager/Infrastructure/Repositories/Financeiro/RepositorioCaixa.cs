using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.DTOs;
using Domain.Entities.Common.Enuns;
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
    public class RepositorioCaixa : IRepositorioCaixa
    {
        private readonly BarbeariaOperacionalDbContext _context;

        public RepositorioCaixa(BarbeariaOperacionalDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteCaixaAbertoAsync(
            Guid empresaId,
            Guid usuarioId,
            CancellationToken cancellationToken)
        {
            return await _context.Caixas.AnyAsync(
                x => x.EmpresaId == empresaId
                  && x.UsuarioId == usuarioId
                  && x.Status == StatusCaixa.Aberto,
                cancellationToken);
        }

        public async Task<Caixa?> ObterCaixaAbertoParaAtualizacaoAsync(
            Guid empresaId,
            Guid usuarioId,
            CancellationToken cancellationToken)
        {
            return await _context.Caixas.FirstOrDefaultAsync(
                x => x.EmpresaId == empresaId
                  && x.UsuarioId == usuarioId
                  && x.Status == StatusCaixa.Aberto,
                cancellationToken);
        }

        public async Task<Caixa?> ObterCaixaAbertoComMovimentosAsync(
            Guid empresaId,
            Guid usuarioId,
            CancellationToken cancellationToken)
        {
            return await _context.Caixas
                .AsNoTracking()
                .Include(x => x.Movimentos)
                .FirstOrDefaultAsync(
                    x => x.EmpresaId == empresaId
                      && x.UsuarioId == usuarioId
                      && x.Status == StatusCaixa.Aberto,
                    cancellationToken);
        }

        public async Task AdicionarAsync(
            Caixa caixa,
            CancellationToken cancellationToken)
        {
            await _context.Caixas.AddAsync(caixa, cancellationToken);
        }

        public async Task<ListarMovimentosCaixaDto> ListarMovimentosAsync(
            Guid empresaId,
            Guid usuarioId,
            DateTime? dataInicio,
            DateTime? dataFim,
            TipoMovimentoCaixa? tipoMovimento,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken)
        {
            var consulta = _context.MovimentoCaixas
                .AsNoTracking()
                .Where(m =>
                    m.Caixa.EmpresaId == empresaId &&
                    m.Caixa.UsuarioId == usuarioId);

            if (dataInicio.HasValue)
            {
                var inicio = dataInicio.Value.Date;
                consulta = consulta.Where(m => m.DataMovimento >= inicio);
            }

            if (dataFim.HasValue)
            {
                // Limite exclusivo: inclui todos os horários do último dia.
                var fimExclusivo = dataFim.Value.Date.AddDays(1);
                consulta = consulta.Where(m => m.DataMovimento < fimExclusivo);
            }

            if (tipoMovimento.HasValue)
            {
                consulta = consulta.Where(
                    m => m.TipoMovimento == tipoMovimento.Value);
            }

            var totalRegistros = await consulta.CountAsync(cancellationToken);

            var itens = await consulta
                .OrderByDescending(m => m.DataMovimento)
                .ThenByDescending(m => m.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(m => new MovimentoCaixaListaDto(
                    m.Id,
                    m.DataMovimento,
                    m.TipoMovimento,
                    m.FormaPagamentoId,
                    m.FormaPagamento.Nome,
                    m.Valor,
                    m.Historico,
                    m.Documento))
                .ToListAsync(cancellationToken);

            var totalPaginas = (int)Math.Ceiling(
                totalRegistros / (double)pageSize);

            return new ListarMovimentosCaixaDto(
                itens,
                pageNumber,
                pageSize,
                totalRegistros,
                totalPaginas);
        }

        public async Task<Caixa?> ObterCaixaAbertoComMovimentosParaAtualizacaoAsync(Guid empresaId, Guid usuarioId, CancellationToken cancellationToken)
        {
            return await _context.Caixas
       .Include(x => x.Movimentos)
       .FirstOrDefaultAsync(
           x => x.EmpresaId == empresaId
             && x.UsuarioId == usuarioId
             && x.Status == StatusCaixa.Aberto,
           cancellationToken);
        }

        public async Task BloquearOperacaoCaixaAsync(Guid empresaId, Guid usuarioId, CancellationToken cancellationToken)
        {
            if (empresaId == Guid.Empty)
                throw new ArgumentException(
                    "A empresa deve ser informada.",
                    nameof(empresaId));

            if (usuarioId == Guid.Empty)
                throw new ArgumentException(
                    "O usuário deve ser informado.",
                    nameof(usuarioId));

            var recurso = $"BarberManager:Caixa:{empresaId:N}:{usuarioId:N}";

            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"""
        DECLARE @resultado INT;

        EXEC @resultado = sys.sp_getapplock
            @Resource = {recurso},
            @LockMode = 'Exclusive',
            @LockOwner = 'Transaction',
            @LockTimeout = 10000;

        IF @resultado < 0
        BEGIN
            THROW 51000, 'Não foi possível obter o bloqueio do caixa.', 1;
        END;
        """,
                cancellationToken);
        }
    }
}
