
namespace Application.Interfaces.Financeiro;

public interface IOutboxOperacionalWriter
{
    Task AdicionarAsync<T>(
       Guid empresaId,
       Guid aggregateId,
       string eventType,
       string targetWorkflow,
       T payload,
       CancellationToken cancellationToken = default);
}
