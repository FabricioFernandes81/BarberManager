
using Application.Interfaces.Financeiro;
using Domain.Entities.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
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
        Guid empresaId,
        Guid aggregateId,
        string eventType,
        string targetWorkflow,
        TPayload payload,
        CancellationToken cancellationToken = default,
        DateTimeOffset? scheduledAtUtc = null)
    {
        if (empresaId == Guid.Empty)
            throw new ArgumentException(
                "A empresa do evento é obrigatória.",
                nameof(empresaId));

        if (aggregateId == Guid.Empty)
            throw new ArgumentException(
                "O agregado de origem é obrigatório.",
                nameof(aggregateId));

        ArgumentException.ThrowIfNullOrWhiteSpace(eventType);
        ArgumentException.ThrowIfNullOrWhiteSpace(targetWorkflow);
        ArgumentNullException.ThrowIfNull(payload);

        var agora = DateTime.UtcNow;

        var dataProgramada = scheduledAtUtc?
            .UtcDateTime ?? agora;

        if (dataProgramada < agora)
            throw new ArgumentException(
                "A data programada não pode estar no passado.",
                nameof(scheduledAtUtc));

        var eventoExistente = await _context.OutboxMessages
            .AnyAsync(x =>
                x.EmpresaId == empresaId &&
                x.AggregateId == aggregateId &&
                x.EventType == eventType &&
                x.TargetWorkflow == targetWorkflow,
                cancellationToken);

        if (eventoExistente)
            return;

        var mensagem = new OutboxOperacionalMessage
        {
            EmpresaId = empresaId,
            AggregateId = aggregateId,
            EventType = eventType,
            TargetWorkflow = targetWorkflow,

            Payload = JsonSerializer.Serialize(
                payload,
                new JsonSerializerOptions(
                    JsonSerializerDefaults.Web)),

            Status = OutboxMessageStatus.Pendente,

            Attempts = 0,
            OccurredAtUtc = agora,
            NextAttemptAtUtc = dataProgramada
        };

        await _context.OutboxMessages.AddAsync(
            mensagem,
            cancellationToken);
    }
}
