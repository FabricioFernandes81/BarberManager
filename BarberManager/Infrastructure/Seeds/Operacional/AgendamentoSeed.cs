using Domain.Entities.Agendamentos;
using Domain.Entities.Common.Enuns;
using Infrastructure.Context;
using Infrastructure.Seeds.Empresas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.Operacional
{
    public static class AgendamentoSeed
    {
        private static readonly Guid ProfissionalTesteId =
            SeedProfissionais.Profissional1;

        private static readonly Guid ServicoTesteId =
            SeedModuloServicos.Servico1;

        private sealed record AgendamentoTeste(
            int Numero,
            int DiasEmRelacaoAHoje,
            int Hora,
            int Minuto,
            StatusAgendamento Status,
            string? Observacoes,
            string? MotivoCancelamento = null);

        private static readonly AgendamentoTeste[] Dados =
        {
            // 3 agendamentos concluídos
            new(
                1, -8, 9, 0,
                StatusAgendamento.Concluido,
                "Atendimento de teste concluído."),

            new(
                2, -7, 10, 0,
                StatusAgendamento.Concluido,
                "Corte realizado com sucesso."),

            new(
                3, -6, 11, 0,
                StatusAgendamento.Concluido,
                "Atendimento finalizado."),

            // 2 agendamentos cancelados
            new(
                4, -5, 9, 0,
                StatusAgendamento.Cancelado,
                "Cancelamento utilizado para teste.",
                "Cliente solicitou o cancelamento."),

            new(
                5, -4, 10, 0,
                StatusAgendamento.Cancelado,
                "Cancelamento utilizado para teste.",
                "Imprevisto na agenda do cliente."),

            // 2 clientes não compareceram
            new(
                6, -3, 9, 0,
                StatusAgendamento.NaoCompareceu,
                "Cliente não compareceu ao atendimento."),

            new(
                7, -2, 10, 0,
                StatusAgendamento.NaoCompareceu,
                "Ausência registrada para teste."),

            // 4 agendamentos pendentes
            new(
                8, 1, 9, 0,
                StatusAgendamento.Pendente,
                "Agendamento de demonstração pendente."),

            new(
                9, 2, 10, 0,
                StatusAgendamento.Pendente,
                "Aguardando confirmação do cliente."),

            new(
                10, 3, 11, 0,
                StatusAgendamento.Pendente,
                "Agendamento pendente de confirmação."),

            new(
                11, 4, 9, 0,
                StatusAgendamento.Pendente,
                "Reserva aguardando confirmação."),

            // 4 agendamentos confirmados
            new(
                12, 5, 9, 0,
                StatusAgendamento.Confirmado,
                "Agendamento confirmado para demonstração."),

            new(
                13, 6, 10, 0,
                StatusAgendamento.Confirmado,
                "Cliente confirmou o horário."),

            new(
                14, 7, 11, 0,
                StatusAgendamento.Confirmado,
                "Atendimento confirmado."),

            new(
                15, 8, 9, 0,
                StatusAgendamento.Confirmado,
                "Reserva confirmada para teste.")
        };

        public static async Task InicializarAsync(
            IServiceProvider services,
            CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider
                .GetRequiredService<BarbeariaOperacionalDbContext>();

            var empresaId = SeedEmpresa.Empresa1;

            // Confirma que o profissional de demonstração existe.
            var profissional = await context.Profissionais
                .SingleOrDefaultAsync(
                    p => p.Id == ProfissionalTesteId
                         && p.EmpresaId == empresaId,
                    cancellationToken);

            if (profissional is null)
            {
                throw new InvalidOperationException(
                    "O profissional de demonstração não foi encontrado. " +
                    "Execute os seeds de profissionais antes dos agendamentos.");
            }

            // Confirma que o serviço de demonstração existe e está ativo.
            var servico = await context.Servicos
                .SingleOrDefaultAsync(
                    s => s.Id == ServicoTesteId
                         && s.EmpresaId == empresaId
                         && s.Ativo == StatusGeral.Ativo,
                    cancellationToken);

            if (servico is null)
            {
                throw new InvalidOperationException(
                    "O serviço de demonstração não foi encontrado ou está " +
                    "inativo. Execute os seeds de serviços antes dos agendamentos.");
            }

            // O profissional precisa estar associado ao serviço.
            var profissionalPossuiServico =
                await context.ProfissionalServicos
                    .AnyAsync(
                        ps => ps.ProfissionalId == profissional.Id
                              && ps.ServicoId == servico.Id
                              && ps.Ativo == StatusGeral.Ativo,
                        cancellationToken);

            if (!profissionalPossuiServico)
            {
                throw new InvalidOperationException(
                    "O profissional de demonstração não possui o serviço " +
                    "ativo associado. Verifique o seed de serviços.");
            }

            // Somente clientes vinculados à empresa de demonstração.
            var clientes = await context.Clientes
                .Where(c =>
                    c.Ativo &&
                    c.Empresas.Any(v =>
                        v.EmpresaId == empresaId && v.Ativo))
                .OrderBy(c => c.Id)
                .ToListAsync(cancellationToken);

            if (clientes.Count == 0)
            {
                throw new InvalidOperationException(
                    "Nenhum cliente ativo foi encontrado para a empresa de " +
                    "demonstração. Execute primeiro o seed de clientes.");
            }

            // IDs fixos tornam o seed idempotente.
            var idsAgendamentos = Dados
                .Select(d => CriarIdAgendamento(d.Numero))
                .ToArray();

            var idsExistentes = await context.Agendamentos
                .Where(a => idsAgendamentos.Contains(a.Id))
                .Select(a => a.Id)
                .ToHashSetAsync(cancellationToken);

            var hoje = DateTime.Today;
            var indiceCliente = 0;
            var quantidadeCriada = 0;

            foreach (var dado in Dados)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var agendamentoId =
                    CriarIdAgendamento(dado.Numero);

                // Não cria novamente um agendamento já inserido.
                if (idsExistentes.Contains(agendamentoId))
                    continue;

                var cliente = clientes[
                    indiceCliente % clientes.Count];

                indiceCliente++;

                var dataHoraLocal = hoje
                    .AddDays(dado.DiasEmRelacaoAHoje)
                    .AddHours(dado.Hora)
                    .AddMinutes(dado.Minuto);

                var offset = TimeZoneInfo.Local
                    .GetUtcOffset(dataHoraLocal);

                var inicio = new DateTimeOffset(
                    dataHoraLocal,
                    offset);

                var duracaoMinutos = servico.DuracaoMinutos;

                var fim = inicio.AddMinutes(duracaoMinutos);

                var agendamento = new Agendamento
                {
                    Id = agendamentoId,
                    EmpresaId = empresaId,
                    ClienteId = cliente.Id,
                    ProfissionalId = profissional.Id,
                    DataHoraInicio = inicio,
                    DataHoraFim = fim,
                    Status = dado.Status,
                    ValorTotal = servico.Valor,
                    Observacoes = dado.Observacoes,
                    DataCancelamento =
                        dado.Status == StatusAgendamento.Cancelado
                            ? inicio.AddHours(-2)
                            : null,
                    MotivoCancelamento =
                        dado.Status == StatusAgendamento.Cancelado
                            ? dado.MotivoCancelamento
                            : null,
                    CanceladoPorUsuarioId = null
                };

                var agendamentoServico =
                    new AgendamentoServico
                    {
                        Id = CriarIdAgendamentoServico(
                            dado.Numero),

                        EmpresaId = empresaId,
                        AgendamentoId = agendamento.Id,
                        ServicoId = servico.Id,
                        NomeServico = servico.Nome,
                        Valor = servico.Valor,
                        DuracaoMinutos = duracaoMinutos,
                        Ordem = 1,
                        Agendamento = agendamento,
                        Servico = servico
                    };

                agendamento.Servicos.Add(agendamentoServico);

                context.Agendamentos.Add(agendamento);

                quantidadeCriada++;
            }

            if (quantidadeCriada > 0)
            {
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        private static Guid CriarIdAgendamento(int numero)
        {
            return Guid.Parse(
                $"A1000000-0000-0000-0000-{numero:000000000000}");
        }

        private static Guid CriarIdAgendamentoServico(int numero)
        {
            return Guid.Parse(
                $"A2000000-0000-0000-0000-{numero:000000000000}");
        }
    }
}
