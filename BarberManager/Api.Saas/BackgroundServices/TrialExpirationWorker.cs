using Application.Saas.Empresas;

namespace Api.Saas.BackgroundServices
{
    public class TrialExpirationWorker : BackgroundService
    {

        private static TimeSpan Intervalo = TimeSpan.FromMinutes(1);
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<TrialExpirationWorker> _logger;

        public TrialExpirationWorker(IServiceScopeFactory scopeFactory, ILogger<TrialExpirationWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
         CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Worker de expiração de Trial iniciado.");

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
                    // Não encerra o worker por uma falha transitória.
                    // O próximo ciclo tentará processar novamente.
                    _logger.LogError(
                        ex,
                        "Falha no ciclo de expiração de Trials. " +
                        "Uma nova tentativa ocorrerá no próximo ciclo.");
                }

                try
                {
                    await Task.Delay(Intervalo, stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }

            _logger.LogInformation(
                "Worker de expiração de Trial encerrado.");
        }

        private async Task ProcessarCicloAsync(
            CancellationToken cancellationToken)
        {
            await using var scope = _scopeFactory.CreateAsyncScope();

            var servico = scope.ServiceProvider
                .GetRequiredService<ITrialExpirationService>();

            var agoraUtc = DateTime.UtcNow;

            var quantidade = await servico.ExpirarTrialsVencidosAsync(
                agoraUtc,
                cancellationToken);

            if (quantidade > 0)
            {
                _logger.LogInformation(
                    "{Quantidade} assinatura(s) Trial expirada(s) em {AgoraUtc}.",
                    quantidade,
                    agoraUtc);
            }
            else
            {
                _logger.LogDebug(
                    "Nenhuma assinatura Trial vencida em {AgoraUtc}.",
                    agoraUtc);
            }
        }
    }
}
