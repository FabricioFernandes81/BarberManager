using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Agendamentos;
using Application.Operacional.Agendamentos.DTOs;
using Domain.Entities.Common.Enuns;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.Queries.Handlers
{
    public class ObterDisponibilidadeAgendaHandler : IQueryHandler<ObterDisponibilidadeAgendaQuery, DisponibilidadeAgendaDto>
    {

        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioDisponibilidadeAgenda _repositorio;

        public ObterDisponibilidadeAgendaHandler(ITenantContext tenantContext, IRepositorioDisponibilidadeAgenda repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<DisponibilidadeAgendaDto> Handle(ObterDisponibilidadeAgendaQuery query, CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable)
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");

            if (query.ProfissionalId == Guid.Empty ||
                query.ServicoId == Guid.Empty)
                throw new ArgumentException(
                    "Profissional e serviço devem ser informados.");

            var empresaId = _tenantContext.EmpresaId;

            var profissional = await _repositorio.ObterProfissionalAsync(
                empresaId,
                query.ProfissionalId,
                cancellationToken);

            if (profissional is null ||
                profissional.Status != StatusProfissional.Ativo ||
                !profissional.AgendaOnline)
                throw new InvalidOperationException(
                    "Profissional inexistente ou indisponível para agendamento online.");

            var servico = await _repositorio.ObterServicoAsync(
                empresaId,
                query.ServicoId,
                cancellationToken);

            if (servico is null ||
                servico.Ativo != StatusGeral.Ativo ||
                !servico.PermiteAgendamentoOnline)
                throw new InvalidOperationException(
                    "Serviço inexistente ou indisponível para agendamento online.");

            var executaServico =
                await _repositorio.ProfissionalExecutaServicoAsync(
                    empresaId,
                    query.ProfissionalId,
                    query.ServicoId,
                    cancellationToken);

            if (!executaServico)
                throw new InvalidOperationException(
                    "O profissional não está habilitado para executar esse serviço.");

            if (query.Data < DateOnly.FromDateTime(DateTime.Today))
                throw new ArgumentException(
                    "Não é possível consultar disponibilidade em uma data passada.");

            var inicioDiaLocal = query.Data.ToDateTime(TimeOnly.MinValue);
            var fimDiaLocal = query.Data.AddDays(1).ToDateTime(TimeOnly.MinValue);

            var inicioDia = CriarDataHoraLocal(inicioDiaLocal);
            var fimDia = CriarDataHoraLocal(fimDiaLocal);

            var diaSemana = query.Data.DayOfWeek;

            var funcionamento = await _repositorio.ObterFuncionamentoAsync(
                empresaId, diaSemana, cancellationToken);

            var jornada = await _repositorio.ObterJornadaAsync(
                empresaId,
                query.ProfissionalId,
                diaSemana,
                cancellationToken);

            if (funcionamento.Count == 0 ||
                jornada.Count == 0 ||
                await _repositorio.ExisteFeriadoAsync(
                    empresaId, query.Data, cancellationToken))
            {
                return ResultadoVazio(query);
            }

            var bloqueios = await _repositorio.ObterBloqueiosAsync(
                empresaId,
                query.ProfissionalId,
                inicioDia,
                fimDia,
                cancellationToken);

            var agendamentos = await _repositorio.ObterAgendamentosAsync(
                empresaId,
                query.ProfissionalId,
                inicioDia,
                fimDia,
                cancellationToken);

            var duracao = servico.DuracaoMinutos;
            var intervalo = servico.IntervaloMinutos;

            if (duracao <= 0 || intervalo < 0)
                throw new InvalidOperationException(
                    "A duração ou o intervalo configurado para o serviço é inválido.");

            const int passoMinutos = 30;

            var horarios = new List<HorarioDisponivelDto>();

            foreach (var horarioEmpresa in funcionamento.Where(x => x.Ativo))
            {
                foreach (var horarioProfissional in jornada.Where(x => x.Ativo))
                {
                    var inicio = horarioEmpresa.HoraInicio >
                                 horarioProfissional.HoraInicio
                        ? horarioEmpresa.HoraInicio
                        : horarioProfissional.HoraInicio;

                    var fim = horarioEmpresa.HoraFim <
                              horarioProfissional.HoraFim
                        ? horarioEmpresa.HoraFim
                        : horarioProfissional.HoraFim;

                    if (fim <= inicio)
                        continue;

                    for (var hora = inicio;
                         hora.AddMinutes(duracao) <= fim;
                         hora = hora.AddMinutes(passoMinutos))
                    {
                        var inicioSlotLocal = query.Data.ToDateTime(hora);
                        var fimSlotLocal = inicioSlotLocal.AddMinutes(duracao);

                        var inicioSlot = CriarDataHoraLocal(inicioSlotLocal);
                        var fimSlot = CriarDataHoraLocal(fimSlotLocal);
                        var fimOcupacao = fimSlot.AddMinutes(intervalo);

                        var bloqueado = bloqueios.Any(b =>
                            b.Ativo &&
                            (b.ProfissionalId is null ||
                             b.ProfissionalId == query.ProfissionalId) &&
                            inicioSlot < b.DataHoraFim &&
                            fimOcupacao > b.DataHoraInicio);

                        var ocupado = agendamentos.Any(a =>
                            a.Status != StatusAgendamento.Cancelado &&
                            a.Status != StatusAgendamento.NaoCompareceu &&
                            inicioSlot < a.DataHoraFim &&
                            fimOcupacao > a.DataHoraInicio);

                        if (!bloqueado && !ocupado)
                        {
                            horarios.Add(
                                new HorarioDisponivelDto {
                                    DataHoraInicio = inicioSlot,
                                    DataHoraFim = fimSlot
                                });
                        }
                    }
                }
            }

            return new DisponibilidadeAgendaDto(
                query.ProfissionalId,
                query.ServicoId,
                query.Data,
                horarios
                    .DistinctBy(x => x.DataHoraInicio)
                    .OrderBy(x => x.DataHoraInicio)
                    .ToArray());
        }

        private static DateTimeOffset CriarDataHoraLocal(DateTime dataHora)
        {
            var local = DateTime.SpecifyKind(
                dataHora,
                DateTimeKind.Unspecified);

            var fuso = TimeZoneInfo.Local;

            return new DateTimeOffset(
                local,
                fuso.GetUtcOffset(local));
        }

        private static DisponibilidadeAgendaDto ResultadoVazio(
            ObterDisponibilidadeAgendaQuery query)
        {
            return new DisponibilidadeAgendaDto(
                query.ProfissionalId,
                query.ServicoId,
                query.Data,
                Array.Empty<HorarioDisponivelDto>());
        }
    }
}
