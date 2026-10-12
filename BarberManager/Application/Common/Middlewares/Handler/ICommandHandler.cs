using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Middlewares.Handler
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : class
    {
        Task<TResult> Handle(TCommand command, CancellationToken cancellationToken);
    }

}
