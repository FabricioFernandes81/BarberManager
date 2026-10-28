using Application.Interfaces.Agendamentos;
using Application.Operacional.Agendamentos.DTOs;
using Application.Operacional.Agendamentos.Queries;
using Domain.Entities.Agendamentos;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public class RepositorioGerenciarAgendamento : IRepositorioGerenciarAgendamento
    {
        private readonly BarbeariaOperacionalDbContext _dbContext;

        public RepositorioGerenciarAgendamento(BarbeariaOperacionalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ObterAgendaPeriodoDto> ObterAgendaPeriodoAsync(Guid empresaId, ObterAgendaPeriodoQuery query, CancellationToken cancellationToken)
        {
            var consulta = _dbContext.Agendamentos
     .AsNoTracking()
     .Where(a => a.EmpresaId == empresaId);

            consulta = consulta.Where(a =>
                a.DataHoraInicio >= query.Inicio &&
                a.DataHoraInicio <= query.Fim);

            if (query.ProfissionalId.HasValue)
            {
                consulta = consulta.Where(a =>
                    a.ProfissionalId == query.ProfissionalId.Value);
            }

            if (query.ClienteId.HasValue)
            {
                consulta = consulta.Where(a =>
                    a.ClienteId == query.ClienteId.Value);
            }

            if (!string.IsNullOrWhiteSpace(query.Status))
            {
                if (!Enum.TryParse<Domain.Entities.Common.Enuns.StatusAgendamento>(
                        query.Status,
                        true,
                        out var status) ||
                    !Enum.IsDefined(typeof(
                        Domain.Entities.Common.Enuns.StatusAgendamento), status))
                {
                    throw new Application.Common.Exceptions.CustomValidationException(
                        new[] { "O status informado é inválido." });
                }

                consulta = consulta.Where(a => a.Status == status);
            }

            var totalRegistros = await consulta.CountAsync(
                cancellationToken);

            var ordenacao = (query.SortBy ?? "DataHoraInicio")
                .Trim()
                .ToLowerInvariant();

            consulta = ordenacao switch
            {
                "datahorainicio" => consulta.OrderBy(a => a.DataHoraInicio),
                "datahorafim" => consulta.OrderBy(a => a.DataHoraFim),
                "status" => consulta.OrderBy(a => a.Status),
                "valortotal" => consulta.OrderBy(a => a.ValorTotal),
                _ => consulta.OrderBy(a => a.DataHoraInicio)
            };

            var itens = await consulta
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(a => new AgendamentoListaDto(
                    a.Id,
                    a.ClienteId,
                    a.Cliente.Nome,
                    a.ProfissionalId,
                    a.Profissional.Nome,
                    a.DataHoraInicio,
                    a.DataHoraFim,
                    a.Status,
                    a.ValorTotal,
                    a.Observacoes))
                .ToListAsync(cancellationToken);

            var totalPaginas = (int)Math.Ceiling(
                totalRegistros / (double)query.PageSize);

            return new ObterAgendaPeriodoDto(
                itens,
                query.PageNumber,
                query.PageSize,
                totalRegistros,
                totalPaginas);
        }

        public async Task<Agendamento?> ObterParaAtualizacaoAsync(Guid empresaId, Guid agendamentoId, CancellationToken cancellationToken)
        {
            return await _dbContext.Agendamentos
        .Include(a => a.Servicos)
        .SingleOrDefaultAsync(
            a => a.EmpresaId == empresaId
              && a.Id == agendamentoId,
            cancellationToken);
        }
    }
}
