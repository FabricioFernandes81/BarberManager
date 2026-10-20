using Domain.Entities.Common;
using Infrastructure.Context;
using Infrastructure.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Api.Saas.BackgroundServices
{
    public class OutboxPublisherWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly N8nWebhookOptions _options;
        private readonly ILogger<OutboxPublisherWorker> _logger;

        public OutboxPublisherWorker(
            IServiceScopeFactory scopeFactory,
            IHttpClientFactory httpClientFactory,
            IOptions<N8nWebhookOptions> options,
            ILogger<OutboxPublisherWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
            _logger = logger;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
               "OutboxPublisherWorker iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessarLoteAsync(stoppingToken);
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
                        "Erro inesperado no publicador da Outbox.");
                }

                try
                {
                    await Task.Delay(
                        TimeSpan.FromSeconds(
                            Math.Max(1, _options.PollIntervalSeconds)),
                        stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }

            _logger.LogInformation(
                "OutboxPublisherWorker encerrado.");
        }

        private async Task ProcessarLoteAsync(
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(_options.WebhookSecret))
            {
                _logger.LogError(
                    "N8n:WebhookSecret não configurado. " +
                    "A publicação da Outbox foi suspensa.");

                return;
            }
            using var scope = _scopeFactory.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<BarbeariaSaasDbContext>();

            var agora = DateTime.UtcNow;

            List<OutboxMessage> mensagens;

            // Transação curta: reserva as mensagens e libera os locks
            // antes de qualquer chamada HTTP.
            await using (var transaction =
                await dbContext.Database.BeginTransactionAsync(
                    cancellationToken))
            {
                mensagens = await dbContext.OutboxMessages
                    .FromSqlRaw(
                        """
                    SELECT TOP ({0}) *
                    FROM [Saas].[OutboxMessages]
                        WITH (UPDLOCK, READPAST, ROWLOCK)
                    WHERE
                        (
                            [Status] = {1}
                            AND [NextAttemptAtUtc] <= {2}
                        )
                        OR
                        (
                            [Status] = {3}
                            AND [LockedUntilUtc] <= {2}
                        )
                    ORDER BY [OccurredAtUtc], [Id]
                    """,
                        Math.Clamp(_options.BatchSize, 1, 100),
                        (int)OutboxMessageStatus.Pendente,
                        agora,
                        (int)OutboxMessageStatus.Processando)
                    .ToListAsync(cancellationToken);

                foreach (var mensagem in mensagens)
                {
                    mensagem.Status = OutboxMessageStatus.Processando;

                    mensagem.LockedUntilUtc = agora.AddSeconds(
                        Math.Max(30, _options.LeaseDurationSeconds));
                }

                await dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }

            foreach (var mensagem in mensagens)
            {
                cancellationToken.ThrowIfCancellationRequested();

                await PublicarMensagemAsync(
                    dbContext,
                    mensagem,
                    cancellationToken);
            }
        }

        private async Task PublicarMensagemAsync(
            BarbeariaSaasDbContext dbContext,
            OutboxMessage mensagem,
            CancellationToken cancellationToken)
        {
            var webhookUrl = ObterWebhookUrl(mensagem.TargetWorkflow);

            if (string.IsNullOrWhiteSpace(webhookUrl))
            {
                await RegistrarFalhaAsync(
                    dbContext,
                    mensagem,
                    $"Webhook não configurado para o destino " +
                    $"'{mensagem.TargetWorkflow}'.",
                    cancellationToken);

                return;
            }

            try
            {
                using var payloadDocument =
                    JsonDocument.Parse(mensagem.Payload);

                var envelope = new
                {
                    eventId = mensagem.Id,
                    eventType = mensagem.EventType,
                    occurredAtUtc = mensagem.OccurredAtUtc,
                    targetWorkflow = mensagem.TargetWorkflow,
                    data = payloadDocument.RootElement.Clone()
                };

                using var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    webhookUrl);

                request.Headers.Add(
                    "X-Webhook-Secret",
                    _options.WebhookSecret);

                request.Headers.Add(
                    "X-Event-Id",
                    mensagem.Id.ToString());

                request.Content = JsonContent.Create(envelope);

                var client = _httpClientFactory
                    .CreateClient("N8nWebhook");

                using var response = await client.SendAsync(
                    request,
                    HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"n8n respondeu com HTTP {(int)response.StatusCode}.");
                }

                mensagem.Status = OutboxMessageStatus.Entregue;
                mensagem.ProcessedAtUtc = DateTime.UtcNow;
                mensagem.LockedUntilUtc = null;
                mensagem.LastError = null;

                await dbContext.SaveChangesAsync(cancellationToken);

                _logger.LogInformation(
                    "Evento {EventId} ({EventType}) entregue ao destino {Target}.",
                    mensagem.Id,
                    mensagem.EventType,
                    mensagem.TargetWorkflow);
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                // Não marcar como entregue. A reserva expirará e poderá
                // ser recuperada em uma execução futura.
                throw;
            }
            catch (Exception ex)
            {
                await RegistrarFalhaAsync(
                    dbContext,
                    mensagem,
                    ex.Message,
                    cancellationToken);

                _logger.LogWarning(
                    ex,
                    "Falha ao publicar o evento {EventId}.",
                    mensagem.Id);
            }
        }

        private async Task RegistrarFalhaAsync(
            BarbeariaSaasDbContext dbContext,
            OutboxMessage mensagem,
            string erro,
            CancellationToken cancellationToken)
        {
            mensagem.Attempts++;
            mensagem.LastError = erro.Length <= 2000
                ? erro
                : erro[..2000];

            mensagem.LockedUntilUtc = null;

            if (mensagem.Attempts >= Math.Max(1, _options.MaxAttempts))
            {
                mensagem.Status = OutboxMessageStatus.FalhaPermanente;
            }
            else
            {
                mensagem.Status = OutboxMessageStatus.Pendente;

                // Backoff exponencial limitado a uma hora.
                var expoente = Math.Min(mensagem.Attempts - 1, 10);
                var segundos = Math.Min(
                    3600,
                    5 * Math.Pow(2, expoente));

                mensagem.NextAttemptAtUtc =
                    DateTime.UtcNow.AddSeconds(segundos);
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        private string? ObterWebhookUrl(string targetWorkflow)
        {
            return targetWorkflow switch
            {
                "proprietario" =>
                    _options.ProprietarioWebhookUrl,

                "atendimento" =>
                    _options.AtendimentoWebhookUrl,

                _ => null
            };
        }
    }
}
