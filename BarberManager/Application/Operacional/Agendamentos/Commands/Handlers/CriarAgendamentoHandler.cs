using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Agendamentos;
using Application.Operacional.Agendamentos.Queries;
using Domain.Entities.Agendamentos;
using Domain.Entities.Common.Enuns;

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
            // 1. Validar o contexto da empresa.

            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw Erro(
                    "Não foi possível identificar a empresa do usuário.");
            }

            var empresaId = _tenantContext.EmpresaId;

            // 2. Validar os identificadores obrigatórios.

            if (command.ClienteId == Guid.Empty ||
                command.ProfissionalId == Guid.Empty ||
                command.ServicoId == Guid.Empty)
            {
                throw Erro(
                    "Cliente, profissional e serviço são obrigatórios.");
            }

            // 3. Validar a data e o horário.

            var inicio = command.DataHoraInicio;

            if (inicio <= DateTimeOffset.Now)
            {
                throw Erro(
                    "O horário do agendamento deve ser futuro.");
            }

            var data = DateOnly.FromDateTime(inicio.DateTime);
            var diaSemana = data.DayOfWeek;

            // 4. Validar o cliente.

            var cliente = await _repositorio.ObterClienteAsync(
                command.ClienteId,
                cancellationToken);

            if (cliente is null || !cliente.Ativo)
            {
                throw Erro("Cliente não encontrado ou inativo.");
            }

            var clienteVinculado =
                await _repositorio.ClienteVinculadoEmpresaAsync(
                    empresaId,
                    command.ClienteId,
                    cancellationToken);

            if (!clienteVinculado)
            {
                throw Erro(
                    "Cliente não encontrado ou não vinculado à empresa.");
            }

            // 5. Validar o profissional.

            var profissional = await _repositorio.ObterProfissionalAsync(
                empresaId,
                command.ProfissionalId,
                cancellationToken);

            if (profissional is null ||
                profissional.Status != StatusProfissional.Ativo)
            {
                throw Erro(
                    "Profissional não encontrado ou inativo.");
            }

            // 6. Validar o serviço.

            var servico = await _repositorio.ObterServicoAsync(
                empresaId,
                command.ServicoId,
                cancellationToken);

            if (servico is null ||
                servico.Ativo != StatusGeral.Ativo)
            {
                throw Erro("Serviço não encontrado ou inativo.");
            }

            // 7. Validar o vínculo entre profissional e serviço.

            var vinculo =
                await _repositorio.ObterVinculoProfissionalServicoAsync(
                    empresaId,
                    command.ProfissionalId,
                    command.ServicoId,
                    cancellationToken);

            if (vinculo is null /*|| !vinculo.Ativo*/)
            {
                throw Erro(
                    "O profissional não está habilitado para esse serviço.");
            }

            // 8. Determinar duração, valor e intervalo do serviço.

            var duracao = vinculo.DuracaoPersonalizada
                ?? servico.DuracaoMinutos;

            var valor = vinculo.ValorPersonalizado
                ?? servico.Valor;

            var intervalo = servico.IntervaloMinutos;

            if (duracao <= 0 || intervalo < 0 || valor < 0)
            {
                throw Erro(
                    "O serviço possui duração, intervalo ou valor inválido.");
            }

            var fim = inicio.AddMinutes(duracao);
            var fimOcupacao = fim.AddMinutes(intervalo);

            // 9. Impedir que o agendamento ultrapasse a meia-noite.

            if (DateOnly.FromDateTime(fimOcupacao.DateTime) != data)
            {
                throw Erro(
                    "O agendamento não pode ultrapassar o dia de atendimento.");
            }

            var horaInicio = TimeOnly.FromDateTime(inicio.DateTime);
            var horaFimOcupacao =
                TimeOnly.FromDateTime(fimOcupacao.DateTime);

            // 10. Obter os horários de funcionamento e a jornada.

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
            {
                throw Erro(
                    "Não há horário de funcionamento ou jornada nesse dia.");
            }

            // 11. Validar feriados.

            if (await _repositorio.ExisteFeriadoAsync(
                empresaId,
                data,
                cancellationToken))
            {
                throw Erro(
                    "Não é possível agendar em um feriado cadastrado.");
            }

            // 12. Obter a tolerância de encerramento.

            var configuracaoEmpresa =
                await _repositorio.ObterConfiguracaoAgendaEmpresaAsync(
                    empresaId,
                    cancellationToken);

            var toleranciaEmpresa =
                configuracaoEmpresa?.ToleranciaEncerramentoMinutos ?? 0;

            var toleranciaProfissional =
                profissional.ToleranciaEncerramentoMinutos;

            // Se o profissional não possuir valor próprio,
            // será utilizada a tolerância da empresa.
            var toleranciaMinutos =
                toleranciaProfissional ?? toleranciaEmpresa;

            if (toleranciaEmpresa < 0 ||
                toleranciaProfissional < 0 ||
                toleranciaMinutos < 0)
            {
                throw Erro(
                    "A tolerância de encerramento não pode ser negativa.");
            }

            // 13. Validar o expediente da empresa e a jornada profissional.
            //
            // A tolerância permite ultrapassar o horário final,
            // mas não permite iniciar antes do horário de abertura
            // nem ultrapassar a meia-noite.

            var dentroFuncionamento = funcionamento.Any(h =>
                h.Ativo &&
                EstaDentroDoHorarioComTolerancia(
                    horaInicio,
                    horaFimOcupacao,
                    h.HoraInicio,
                    h.HoraFim,
                    toleranciaMinutos));

            var dentroJornada = jornada.Any(h =>
                h.Ativo &&
                EstaDentroDoHorarioComTolerancia(
                    horaInicio,
                    horaFimOcupacao,
                    h.HoraInicio,
                    h.HoraFim,
                    toleranciaMinutos));

            if (!dentroFuncionamento || !dentroJornada)
            {
                throw Erro(
                    "O horário solicitado está fora do expediente ou da jornada, " +
                    "considerando a tolerância de encerramento configurada.");
            }

            // 14. Validar bloqueios de agenda.
            //
            // A validação considera o período total de ocupação,
            // incluindo o intervalo configurado para o serviço.

            var bloqueios = await _repositorio.ObterBloqueiosAsync(
                empresaId,
                command.ProfissionalId,
                inicio,
                fimOcupacao,
                cancellationToken);

            var bloqueado = bloqueios.Any(b =>
                b.Ativo &&
                (b.ProfissionalId is null ||
                 b.ProfissionalId == command.ProfissionalId) &&
                inicio < b.DataHoraFim &&
                fimOcupacao > b.DataHoraInicio);

            if (bloqueado)
            {
                throw Erro("O horário solicitado está bloqueado.");
            }

            // 15. Validar conflitos com outros agendamentos.

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
            {
                throw Erro(
                    "O profissional já possui um agendamento nesse intervalo.");
            }

            // 16. Criar o agendamento.

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

            // 17. Retornar o resultado.

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

        private static bool EstaDentroDoHorarioComTolerancia(
            TimeOnly horaInicio,
            TimeOnly horaFimOcupacao,
            TimeOnly inicioExpediente,
            TimeOnly fimExpediente,
            int toleranciaMinutos)
        {
            var inicioMinutos =
                horaInicio.Hour * 60 + horaInicio.Minute;

            var fimOcupacaoMinutos =
                horaFimOcupacao.Hour * 60 + horaFimOcupacao.Minute;

            var inicioExpedienteMinutos =
                inicioExpediente.Hour * 60 + inicioExpediente.Minute;

            var fimExpedienteMinutos =
                fimExpediente.Hour * 60 + fimExpediente.Minute;

            // O limite nunca pode ultrapassar a meia-noite.
            var limiteEncerramentoMinutos = Math.Min(
                fimExpedienteMinutos + toleranciaMinutos,
                24 * 60);

            return inicioMinutos >= inicioExpedienteMinutos &&
                   fimOcupacaoMinutos <= limiteEncerramentoMinutos;
        }

        private static CustomValidationException Erro(string mensagem)
            => new(new[] { mensagem });
    }
}