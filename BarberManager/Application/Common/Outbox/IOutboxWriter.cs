using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Outbox
{
    public interface IOutboxWriter
    {
        Task AdicionarAsync<TPayload>(
        string eventType,
        string targetWorkflow,
        TPayload payload,
        CancellationToken cancellationToken = default);
    }
}
