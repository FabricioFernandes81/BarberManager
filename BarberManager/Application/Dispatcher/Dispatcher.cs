using Application.Common.Middlewares;
using Application.Common.Middlewares.Handler;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<Dispatcher> logger;

        public Dispatcher(IServiceProvider serviceProvider, ILogger<Dispatcher> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;

        }
        public async Task<TResult> Command<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class
        {
            logger.LogInformation($"[DISPATCHER] Obter instância do handler para processar a consulta : {command} ");

            var handler = serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();

            // Constroi pipeline para comandos
            Func<Task<TResult>> pipeline = () => handler.Handle(command, cancellationToken);

            var middlewares = serviceProvider.GetServices<IDispatcherCommandMiddleware<TCommand, TResult>>()
                .Reverse();

            foreach (var middleware in middlewares)
            {
                var current = middleware;
                var next = pipeline;
                pipeline = () => current.Handle(command, next);
            }

            return await pipeline();

        }

        public async Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : class
        {
            logger.LogInformation($"[DISPATCHER] Obter instância do handler para processar a consulta : {query} ");
            var handler = serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();

            // Constroi pipeline para consultas
            Func<Task<TResult>> pipeline = () => handler.Handle(query, cancellationToken);

            var middlewares = serviceProvider.GetServices<IDispatcherQueryMiddleware<TQuery, TResult>>()
                .Reverse();

            foreach (var middleware in middlewares)
            {
                var current = middleware;
                var next = pipeline;
                pipeline = () => current.Handle(query, next);
            }

            return await pipeline();

        }
    }
}