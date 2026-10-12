using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Middlewares
{
    public class CommandLoggingMiddleware<TCommand, TResult> : IDispatcherCommandMiddleware<TCommand, TResult>
    {
        private readonly ILogger<CommandLoggingMiddleware<TCommand, TResult>> _logger;

        public CommandLoggingMiddleware(ILogger<CommandLoggingMiddleware<TCommand, TResult>> logger)
        {
            _logger = logger;
        }


        public async Task<TResult> Handle(TCommand command, Func<Task<TResult>> next)
        {
            var commandName = typeof(TCommand).Name;
            _logger.LogInformation("[INÍCIO] Processando comando {CommandName}", commandName);
            _logger.LogDebug("Dados do comando: {@Command}", command);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                var result = await next();
                stopwatch.Stop();

                /* _logger.LogInformation("[SUCESSO] Comando {CommandName} concluído em {ElapsedMs}ms",
                     commandName, stopwatch.ElapsedMilliseconds);*/
                _logger.LogInformation("[SUCESSO] Comando {CommandName} concluída em {ElapsedMs}ms",
                commandName, stopwatch.ElapsedMilliseconds);
                _logger.LogDebug("Resultado: {@Result}", result);

                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "[ERRO] Comando {CommandName} falhou após {ElapsedMs}ms",
                    commandName, stopwatch.ElapsedMilliseconds);
                throw;
            }
        }
    }

}

