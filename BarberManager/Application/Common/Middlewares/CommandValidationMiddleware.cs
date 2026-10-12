using Application.Common.Exceptions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Application.Common.Middlewares
{
    public class CommandValidationMiddleware<TCommand, TResult> : IDispatcherCommandMiddleware<TCommand, TResult>
    {
        private readonly IValidator<TCommand>? _validator;
        private readonly ILogger<CommandValidationMiddleware<TCommand, TResult>> _logger;

        public CommandValidationMiddleware(
            ILogger<CommandValidationMiddleware<TCommand, TResult>> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            if (HasProperties(typeof(TCommand)))
            {
                _validator = serviceProvider.GetService<IValidator<TCommand>>();
            }

        }


        public async Task<TResult> Handle(TCommand request, Func<Task<TResult>> next)
        {

            _logger.LogDebug(">>> Validando comando {CommandType}", typeof(TCommand).Name);

            var result = await _validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                _logger.LogWarning(">>> Validação falhou para {CommandType}", typeof(TCommand).Name);
                throw new CustomValidationException(result.Errors);
            }
           return await next();

        }

        private static bool HasProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                       .Any(p => p.CanRead);
        }

    }

}
