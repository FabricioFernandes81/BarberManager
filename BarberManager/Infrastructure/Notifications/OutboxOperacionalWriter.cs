
using Application.Interfaces.Financeiro;
using Domain.Entities.Common;
using Infrastructure.Context;

using System.Text.Json;

namespace Infrastructure.Notifications;

public sealed class OutboxOperacionalWriter
    : IOutboxOperacionalWriter
{
    private readonly BarbeariaOperacionalDbContext _context;

    public OutboxOperacionalWriter(
        BarbeariaOperacionalDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync<TPayload>(
        string eventType,
        string targetWorkflow,
        TPayload payload,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(eventType);
        ArgumentException.ThrowIfNullOrWhiteSpace(targetWorkflow);
        ArgumentNullException.ThrowIfNull(payload);

        var mensagem = new OutboxMessage
        {
            EventType = eventType,
            TargetWorkflow = targetWorkflow,
            Payload = JsonSerializer.Serialize(
                payload,
                new JsonSerializerOptions(JsonSerializerDefaults.Web)),
            Status = OutboxMessageStatus.Pendente,
            Attempts = 0,
            OccurredAtUtc = DateTime.UtcNow,
            NextAttemptAtUtc = DateTime.UtcNow
        };

        await _context.OutboxMessages.AddAsync(
            mensagem,
            cancellationToken);
    }
}
