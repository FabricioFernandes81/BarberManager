
using Application.Interfaces.Agendamentos;
using Application.Operacional.Agendamentos.DTOs;
using Domain.Entities.Common.Enuns;

namespace Application.Operacional.Agendamentos.Services;

public sealed class DisponibilidadeAgendaService
    : IDisponibilidadeAgendaService
{
    private readonly IRepositorioDisponibilidadeAgenda _repositorio;

    public DisponibilidadeAgendaService(
        IRepositorioDisponibilidadeAgenda repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<DisponibilidadeAgendaDto> ObterAsync(
        Guid empresaId,
        Guid profissionalId,
        Guid servicoId,
        DateOnly data,
        CancellationToken cancellationToken)
    {
        if (empresaId == Guid.Empty ||
            profissionalId == Guid.Empty ||
            servicoId == Guid.Empty)
        {
            throw new ArgumentException(
                "Empresa, profissional e serviço devem ser informados.");
        }

        var profissional = await _repositorio.ObterProfissionalAsync(
            empresaId,
            profissionalId,
            cancellationToken);

        if (profissional is null ||
            profissional.Status != StatusProfissional.Ativo ||
            !profissional.AgendaOnline ||
            !profissional.ExibirAgendamentoOnline)
        {
            throw new InvalidOperationException(
                "Profissional inexistente ou indisponível para agendamento online.");
        }

        var servico = await _repositorio.ObterServicoAsync(
            empresaId,
            servicoId,
            cancellationToken);

        if (servico is null ||
            servico.Ativo != StatusGeral.Ativo ||
            !servico.PermiteAgendamentoOnline)
        {
            throw new InvalidOperationException(
                "Serviço inexistente ou indisponível para agendamento online.");
        }

        var vinculo =
            await _repositorio.ObterVinculoProfissionalServicoAsync(
                empresaId,
                profissionalId,
                servicoId,
                cancellationToken);

        if (vinculo is null ||
            vinculo.Ativo != StatusGeral.Ativo)
        {
            throw new InvalidOperationException(
                "O profissional não está habilitado para executar esse serviço.");
        }

        if (data < DateOnly.FromDateTime(DateTime.Today))
        {
            throw new ArgumentException(
                "Não é possível consultar disponibilidade em uma data passada.");
        }

        var inicioDiaLocal = data.ToDateTime(TimeOnly.MinValue);
        var fimDiaLocal = data.AddDays(1).ToDateTime(TimeOnly.MinValue);

        var inicioDia = CriarDataHoraLocal(inicioDiaLocal);
        var fimDia = CriarDataHoraLocal(fimDiaLocal);

        var diaSemana = data.DayOfWeek;

        var funcionamento = await _repositorio.ObterFuncionamentoAsync(
            empresaId,
            diaSemana,
            cancellationToken);

        var jornada = await _repositorio.ObterJornadaAsync(
            empresaId,
            profissionalId,
            diaSemana,
            cancellationToken);

        if (funcionamento.Count == 0 ||
            jornada.Count == 0 ||
            await _repositorio.ExisteFeriadoAsync(
                empresaId,
                data,
                cancellationToken))
        {
            return ResultadoVazio(
                profissionalId,
                servicoId,
                data);
        }

        var bloqueios = await _repositorio.ObterBloqueiosAsync(
            empresaId,
            profissionalId,
            inicioDia,
            fimDia,
            cancellationToken);

        var agendamentos = await _repositorio.ObterAgendamentosAsync(
            empresaId,
            profissionalId,
            inicioDia,
            fimDia,
            cancellationToken);

        var duracao = vinculo.DuracaoPersonalizada
            ?? servico.DuracaoMinutos;

        var intervalo = servico.IntervaloMinutos;

        if (duracao <= 0 || intervalo < 0)
        {
            throw new InvalidOperationException(
                "A duração ou o intervalo configurado para o serviço é inválido.");
        }

        var configuracaoEmpresa =
            await _repositorio.ObterConfiguracaoAgendaEmpresaAsync(
                empresaId,
                cancellationToken);

        var toleranciaEmpresa =
            configuracaoEmpresa?.ToleranciaEncerramentoMinutos ?? 0;

        var toleranciaMinutos =
            profissional.ToleranciaEncerramentoMinutos
            ?? toleranciaEmpresa;

        if (toleranciaMinutos < 0)
        {
            throw new InvalidOperationException(
                "A tolerância de encerramento não pode ser negativa.");
        }

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

                var inicioMinutos =
                    inicio.Hour * 60 + inicio.Minute;

                var fimMinutos =
                    fim.Hour * 60 + fim.Minute;

                var limiteEncerramentoMinutos = Math.Min(
                    fimMinutos + toleranciaMinutos,
                    24 * 60);

                for (var minuto = inicioMinutos;
                     minuto + duracao + intervalo <= limiteEncerramentoMinutos;
                     minuto += passoMinutos)
                {
                    var hora = TimeOnly.MinValue.AddMinutes(minuto);

                    var inicioSlotLocal = data.ToDateTime(hora);
                    var fimSlotLocal = inicioSlotLocal.AddMinutes(duracao);

                    var inicioSlot = CriarDataHoraLocal(inicioSlotLocal);
                    var fimSlot = CriarDataHoraLocal(fimSlotLocal);

                    var fimOcupacao = fimSlot.AddMinutes(intervalo);

                    if (DateOnly.FromDateTime(
                        fimOcupacao.DateTime) != data)
                    {
                        continue;
                    }

                    var bloqueado = bloqueios.Any(b =>
                        b.Ativo &&
                        (b.ProfissionalId is null ||
                         b.ProfissionalId == profissionalId) &&
                        inicioSlot < b.DataHoraFim &&
                        fimOcupacao > b.DataHoraInicio);

                    var ocupado = agendamentos.Any(a =>
                        a.Status != StatusAgendamento.Cancelado &&
                        a.Status != StatusAgendamento.NaoCompareceu &&
                        inicioSlot < a.DataHoraFim &&
                        fimOcupacao > a.DataHoraInicio);

                    if (!bloqueado && !ocupado)
                    {
                        horarios.Add(new HorarioDisponivelDto
                        {
                            DataHoraInicio = inicioSlot,
                            DataHoraFim = fimSlot
                        });
                    }
                }
            }
        }

        return new DisponibilidadeAgendaDto(
            profissionalId,
            servicoId,
            data,
            horarios
                .DistinctBy(x => x.DataHoraInicio)
                .OrderBy(x => x.DataHoraInicio)
                .ToArray());
    }

    private static DateTimeOffset CriarDataHoraLocal(
        DateTime dataHora)
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
        Guid profissionalId,
        Guid servicoId,
        DateOnly data)
    {
        return new DisponibilidadeAgendaDto(
            profissionalId,
            servicoId,
            data,
            Array.Empty<HorarioDisponivelDto>());
    }
}
