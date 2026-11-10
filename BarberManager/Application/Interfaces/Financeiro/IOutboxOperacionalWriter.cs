
namespace Application.Interfaces.Financeiro;

public interface IOutboxOperacionalWriter
{
    Task AdicionarAsync<TPayload>(
        string eventType,
        string targetWorkflow,
        TPayload payload,
        CancellationToken cancellationToken = default);
}
