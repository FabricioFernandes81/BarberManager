
using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using Infrastructure.Context;
using Infrastructure.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Api.Admin.BackgroundServices;

public sealed class OutboxOperacionalWorker : BackgroundService
{
    private const string TargetWorkflow = "NotificacaoWhatsApp";

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly N8nWebhookOptions _options;
    private readonly ILogger<OutboxOperacionalWorker> _logger;

    public OutboxOperacionalWorker(
        IServiceScopeFactory scopeFactory,
        IHttpClientFactory httpClientFactory,
        IOptions<N8nWebhookOptions> options,
        ILogger<OutboxOperacionalWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "OutboxOperacionalWorker iniciado.");

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
                    "Erro inesperado no processamento da Outbox operacional.");
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
            "OutboxOperacionalWorker encerrado.");
    }

    private async Task ProcessarLoteAsync(
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_options.WebhookSecret))
        {
            _logger.LogError(
                "N8n:WebhookSecret não configurado. " +
                "A publicação da Outbox operacional foi suspensa.");

            return;
        }

        if (string.IsNullOrWhiteSpace(
            _options.NotificacaoWhatsAppWebhookUrl))
        {
            _logger.LogError(
                "N8n:NotificacaoWhatsAppWebhookUrl não configurado. " +
                "A publicação da Outbox operacional foi suspensa.");

            return;
        }

        using var scope = _scopeFactory.CreateScope();

        var dbContext = scope.ServiceProvider
            .GetRequiredService<BarbeariaOperacionalDbContext>();

        var agora = DateTime.UtcNow;

        List<OutboxOperacionalMessage> mensagens;

        // A reserva é feita em uma transação curta.
        // O lock é liberado antes das chamadas HTTP.
        await using (var transaction =
            await dbContext.Database.BeginTransactionAsync(
                cancellationToken))
        {
            mensagens = await dbContext.OutboxMessages
                .FromSqlRaw(
                    """
                    SELECT TOP ({0}) *
                    FROM [dbo].[OutboxMessages]
                        WITH (UPDLOCK, READPAST, ROWLOCK)
                    WHERE
                        [TargetWorkflow] = {1}
                        AND
                        (
                            (
                                [Status] = {2}
                                AND [NextAttemptAtUtc] <= {3}
                            )
                            OR
                            (
                                [Status] = {4}
                                AND [LockedUntilUtc] <= {3}
                            )
                        )
                    ORDER BY [OccurredAtUtc], [Id]
                    """,
                    Math.Clamp(_options.BatchSize, 1, 100),
                    TargetWorkflow,
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
        BarbeariaOperacionalDbContext dbContext,
        OutboxOperacionalMessage mensagem,
        CancellationToken cancellationToken)
    {
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
                empresaId = mensagem.EmpresaId,
                aggregateId = mensagem.AggregateId,
                data = payloadDocument.RootElement.Clone()
            };

            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                _options.NotificacaoWhatsAppWebhookUrl);

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
                "Evento operacional {EventId} ({EventType}) " +
                "aceito pelo webhook n8n para a empresa {EmpresaId}.",
                mensagem.Id,
                mensagem.EventType,
                mensagem.EmpresaId);
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
            // Não marcar como entregue.
            // A reserva poderá ser recuperada após o vencimento do lease.
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
                "Falha ao publicar o evento operacional {EventId}.",
                mensagem.Id);
        }
    }

    private async Task RegistrarFalhaAsync(
        BarbeariaOperacionalDbContext dbContext,
        OutboxOperacionalMessage mensagem,
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
}
