using Application.Dispatcher;
using Application.Interfaces.Empresas;
using Application.Saas.Pagamentos.Commands;
using Application.Saas.Pagamentos.DTOs;
using Domain.Entities.Common.Enuns;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Api.Saas.BackgroundServices
{
    public class FaturamentoAutomaticoWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<FaturamentoAutomaticoWorker> _logger;

        private static readonly TimeSpan IntervaloExecucao =
            TimeSpan.FromHours(6);
        public FaturamentoAutomaticoWorker(
       IServiceScopeFactory scopeFactory,
       ILogger<FaturamentoAutomaticoWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
           CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessarCicloAsync(stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Falha no ciclo de faturamento automático.");
                }

                try
                {
                    await Task.Delay(
                        IntervaloExecucao,
                        stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        private async Task ProcessarCicloAsync(
            CancellationToken cancellationToken)
        {
            // Inclui todo o quinto dia e recupera execuções perdidas.
            var limiteVencimento =
                DateTime.UtcNow.Date.AddDays(6);

            IReadOnlyList<Domain.Entities.Saas.Assinatura> assinaturas;

            await using (var consultaScope =
                _scopeFactory.CreateAsyncScope())
            {
                var repositorio = consultaScope.ServiceProvider
                    .GetRequiredService<IRepositorioFatura>();

                assinaturas =
                    await repositorio.ObterAssinaturasParaFaturamentoAsync(
                        limiteVencimento,
                        cancellationToken);
            }

            foreach (var assinatura in assinaturas)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    await ProcessarAssinaturaAsync(
                        assinatura,
                        cancellationToken);
                }
                catch (OperationCanceledException)
                    when (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
                catch (DbUpdateException ex)
                    when (EhViolacaoUnicidade(ex))
                {
                    // Outra execução pode ter criado a mesma fatura.
                    // O índice único no banco é a proteção definitiva.
                    _logger.LogInformation(
                        "Fatura da assinatura {AssinaturaId} já foi " +
                        "criada por outra execução.",
                        assinatura.Id);
                }
                catch (Exception ex)
                {
                    // Uma assinatura com erro não interrompe as demais.
                    _logger.LogError(
                        ex,
                        "Falha ao gerar fatura da assinatura {AssinaturaId}.",
                        assinatura.Id);
                }
            }
        }

        private async Task ProcessarAssinaturaAsync(
            Domain.Entities.Saas.Assinatura assinatura,
            CancellationToken cancellationToken)
        {
            // Não faturar Trial, mesmo se o worker for chamado
            // por outro caminho no futuro.
            if (assinatura.Trial)
                return;

            var vencimento = assinatura.DataVencimento.Date;

            // A fatura representa o ciclo que começa no vencimento.
            var periodoInicio = vencimento;

            var periodoFim = assinatura.PeriodicidadeCobranca switch
            {
                PeriodicidadeCobranca.Mensal =>
                    vencimento.AddMonths(1),

                PeriodicidadeCobranca.Anual =>
                    vencimento.AddYears(1),

                _ => throw new InvalidOperationException(
                    $"Periodicidade não suportada: " +
                    $"{assinatura.PeriodicidadeCobranca}.")
            };

            await using var scope = _scopeFactory.CreateAsyncScope();

            var repositorio = scope.ServiceProvider
                .GetRequiredService<IRepositorioFatura>();

            // Evita chamadas desnecessárias ao handler em execuções repetidas.
            var existente = await repositorio.ObterPorPeriodoAsync(
                assinatura.Id,
                periodoInicio,
                periodoFim,
                cancellationToken);

            if (existente is not null)
            {
                return;
            }

            var dispatcher = scope.ServiceProvider
                .GetRequiredService<IDispatcher>();

            var command = new GerarFaturaCommand(
                assinatura.Id,
                periodoInicio,
                periodoFim,
                vencimento);

            var resultado = await dispatcher.Command<
                GerarFaturaCommand,
                GerarFaturaDto>(
                    command,
                    cancellationToken);

            if (resultado.Ignorada)
            {
                _logger.LogInformation(
                    "Faturamento ignorado para assinatura {AssinaturaId}: {Motivo}",
                    assinatura.Id,
                    resultado.Motivo);

                return;
            }

            _logger.LogInformation(
                "Fatura {FaturaId} processada para assinatura {AssinaturaId}. " +
                "Já existia: {JaExistia}.",
                resultado.FaturaId,
                assinatura.Id,
                resultado.JaExistia);
        }

        private static bool EhViolacaoUnicidade(
            DbUpdateException exception)
        {
            return exception.GetBaseException() is SqlException sqlException
                && sqlException.Number is 2601 or 2627;
        }
    }
}
