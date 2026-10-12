using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Middlewares
{
    public class QueryLoggingMiddleware<TQuery, TResult> : IDispatcherQueryMiddleware<TQuery, TResult>
    {
        private readonly ILogger<QueryLoggingMiddleware<TQuery, TResult>> _logger;


        public QueryLoggingMiddleware(ILogger<QueryLoggingMiddleware<TQuery, TResult>> logger)
        {
            _logger = logger;
        }

        public async Task<TResult> Handle(TQuery request, Func<Task<TResult>> next)
        {
            var queryName = typeof(TQuery).Name;

            _logger.LogInformation("[INÍCIO] Processando consulta {QueryName}", queryName);
            _logger.LogDebug("Parâmetros da consulta: {@Query}", request);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                var result = await next();
                stopwatch.Stop();

                _logger.LogInformation("[SUCESSO] Consulta {QueryName} concluída em {ElapsedMs}ms",
                    queryName, stopwatch.ElapsedMilliseconds);
                _logger.LogDebug("Resultado: {@Result}", result);

                return result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "[ERRO] Consulta {QueryName} falhou após {ElapsedMs}ms",
                    queryName, stopwatch.ElapsedMilliseconds);
                throw;
            }
        }
    }

}
