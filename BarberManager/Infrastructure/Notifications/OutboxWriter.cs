using Application.Common.Outbox;
using Domain.Entities.Common;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Notifications
{
    public class OutboxWriter : IOutboxWriter
    {
        private static readonly JsonSerializerOptions JsonOptions =
        new(JsonSerializerDefaults.Web);

        private readonly BarbeariaSaasDbContext _dbContext;

        public OutboxWriter(BarbeariaSaasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AdicionarAsync<TPayload>(string eventType, string targetWorkflow, TPayload payload, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(eventType);
            ArgumentException.ThrowIfNullOrWhiteSpace(targetWorkflow);
            ArgumentNullException.ThrowIfNull(payload);

            var json = JsonSerializer.Serialize(payload, JsonOptions);

            var mensagem = new OutboxMessage
            {
                EventType = eventType,
                TargetWorkflow = targetWorkflow,
                Payload = json,
                Status = OutboxMessageStatus.Pendente,
                Attempts = 0,
                OccurredAtUtc = DateTime.UtcNow,
                NextAttemptAtUtc = DateTime.UtcNow
            };

            // Não executar SaveChanges aqui.
            // O UnitOfWork da operação de negócio fará o commit
            // da fatura e da mensagem na mesma transação.
            await _dbContext.OutboxMessages.AddAsync(
                mensagem,
                cancellationToken);
        }
    
    }
}
