using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Agendamentos;
using Application.Operacional.Agendamentos.Queries;
using Domain.Entities.Agendamentos;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Agendamentos.Commands.Handlers
{
    public class CriarAgendamentoHandler
     : ICommandHandler<CriarAgendamentoCommand, CriarAgendamentoDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioCriarAgendamento _repositorio;

        public CriarAgendamentoHandler(
            ITenantContext tenantContext,
            IRepositorioCriarAgendamento repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<CriarAgendamentoDto> Handle(
            CriarAgendamentoCommand command,
            CancellationToken cancellationToken)
        {
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw Erro("Não foi possível identificar a empresa do usuário.");
            }

            if (command.ClienteId == Guid.Empty ||
                command.ProfissionalId == Guid.Empty ||
                command.ServicoId == Guid.Empty)
            {
                throw Erro("Cliente, profissional e serviço são obrigatórios.");
            }

           /* if (command.DataHoraInicio.Offset !=
                command.DataHoraInicio.Offset)
            {
                throw Erro("O fuso horário informado é inválido.");
            }*/

            var empresaId = _tenantContext.EmpresaId;
            var inicio = command.DataHoraInicio;
            var data = DateOnly.FromDateTime(inicio.DateTime);
            var diaSemana = data.DayOfWeek;

            if (inicio <= DateTimeOffset.Now)
                throw Erro("O horário do agendamento deve ser futuro.");

            var cliente = await _repositorio.ObterClienteAsync(
                command.ClienteId,
                cancellationToken);

            if (cliente is null || !cliente.Ativo)
                throw Erro("Cliente não encontrado ou inativo.");

            var clienteVinculado =
                await _repositorio.ClienteVinculadoEmpresaAsync(
                    empresaId,
                    command.ClienteId,
                    cancellationToken);

            if (!clienteVinculado)
                throw Erro("Cliente não encontrado ou não vinculado à empresa.");

            var profissional = await _repositorio.ObterProfissionalAsync(
                empresaId,
                command.ProfissionalId,
                cancellationToken);

            if (profissional is null ||
                profissional.Status != StatusProfissional.Ativo)
            {
                throw Erro("Profissional não encontrado ou inativo.");
            }

            var servico = await _repositorio.ObterServicoAsync(
                empresaId,
                command.ServicoId,
                cancellationToken);

            if (servico is null || servico.Ativo != StatusGeral.Ativo)
                throw Erro("Serviço não encontrado ou inativo.");

            var vinculo =
                await _repositorio.ObterVinculoProfissionalServicoAsync(
                    empresaId,
                    command.ProfissionalId,
                    command.ServicoId,
                    cancellationToken);

            if (vinculo is null)
                throw Erro("O profissional não está habilitado para esse serviço.");

            var duracao = vinculo.DuracaoPersonalizada
                ?? servico.DuracaoMinutos;

            var valor = vinculo.ValorPersonalizado
                ?? servico.Valor;

            var intervalo = servico.IntervaloMinutos;

            if (duracao <= 0 || intervalo < 0 || valor < 0)
                throw Erro("O serviço possui duração, intervalo ou valor inválido.");

            var fim = inicio.AddMinutes(duracao);
            var fimOcupacao = fim.AddMinutes(intervalo);

            var funcionamento = await _repositorio.ObterFuncionamentoAsync(
                empresaId,
                diaSemana,
                cancellationToken);

            var jornada = await _repositorio.ObterJornadaAsync(
                empresaId,
                command.ProfissionalId,
                diaSemana,
                cancellationToken);

            if (funcionamento.Count == 0 || jornada.Count == 0)
                throw Erro("Não há horário de funcionamento ou jornada nesse dia.");

            if (await _repositorio.ExisteFeriadoAsync(
                empresaId,
                data,
                cancellationToken))
            {
                throw Erro("Não é possível agendar em um feriado cadastrado.");
            }

            var horaInicio = TimeOnly.FromDateTime(inicio.DateTime);
            var horaFimOcupacao = TimeOnly.FromDateTime(fimOcupacao.DateTime);

            // Este fluxo não aceita agendamentos que cruzem a meia-noite.
            if (DateOnly.FromDateTime(fimOcupacao.DateTime) != data)
                throw Erro("O agendamento não pode ultrapassar o dia de atendimento.");

            var dentroFuncionamento = funcionamento.Any(h =>
                h.Ativo &&
                horaInicio >= h.HoraInicio &&
                horaFimOcupacao <= h.HoraFim);

            var dentroJornada = jornada.Any(h =>
                h.Ativo &&
                horaInicio >= h.HoraInicio &&
                horaFimOcupacao <= h.HoraFim);

            if (!dentroFuncionamento || !dentroJornada)
                throw Erro("O horário solicitado está fora do expediente ou da jornada.");

            var bloqueios = await _repositorio.ObterBloqueiosAsync(
                empresaId,
                command.ProfissionalId,
                inicio,
                fimOcupacao,
                cancellationToken);

            if (bloqueios.Any(b =>
                b.Ativo &&
                inicio < b.DataHoraFim &&
                fimOcupacao > b.DataHoraInicio))
            {
                throw Erro("O horário solicitado está bloqueado.");
            }

            var agendamentos = await _repositorio.ObterAgendamentosAsync(
                empresaId,
                command.ProfissionalId,
                inicio,
                fimOcupacao,
                cancellationToken);

            var conflito = agendamentos.Any(a =>
                a.Status != StatusAgendamento.Cancelado &&
                a.Status != StatusAgendamento.NaoCompareceu &&
                inicio < a.DataHoraFim &&
                fimOcupacao > a.DataHoraInicio);

            if (conflito)
                throw Erro("O profissional já possui um agendamento nesse intervalo.");

            var agendamento = new Agendamento
            {
                Id = Guid.NewGuid(),
                EmpresaId = empresaId,
                ClienteId = command.ClienteId,
                ProfissionalId = command.ProfissionalId,
                DataHoraInicio = inicio,
                DataHoraFim = fim,
                Status = StatusAgendamento.Pendente,
                ValorTotal = valor,
                Observacoes = command.Observacoes?.Trim(),
                Servicos = new List<AgendamentoServico>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    EmpresaId = empresaId,
                    ServicoId = servico.Id,
                    NomeServico = servico.Nome,
                    Valor = valor,
                    DuracaoMinutos = duracao,
                    Ordem = 1
                }
            }
            };

            await _repositorio.CriarAsync(
                agendamento,
                cancellationToken);

            return new CriarAgendamentoDto(
                agendamento.Id,
                agendamento.ClienteId,
                agendamento.ProfissionalId,
                servico.Id,
                agendamento.DataHoraInicio,
                agendamento.DataHoraFim,
                agendamento.Observacoes,
                agendamento.ValorTotal,
                agendamento.Status.ToString());
        }

        private static CustomValidationException Erro(string mensagem)
            => new(new[] { mensagem });
    }
}
