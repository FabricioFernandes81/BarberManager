using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomValidationException ex)
            {
                var statusCode = ex.Errors.Any(e => e.Contains("não encontrado"))
                    ? StatusCodes.Status404NotFound
                    : StatusCodes.Status400BadRequest;

                _logger.LogWarning("Erro de validação: {Errors}", string.Join("; ", ex.Errors));
                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                var responseBody = JsonSerializer.Serialize(new { Message = "Erro na operação", Errors = ex.Errors });
                await context.Response.WriteAsync(responseBody);
            }
            catch (ConcurrencyConflictException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Conflito de concorrência durante a operação.");

                context.Response.StatusCode =
                    StatusCodes.Status409Conflict;

                context.Response.ContentType = "application/json";

                var responseBody = JsonSerializer.Serialize(new
                {
                    Message = "Conflito de concorrência",
                    Errors = new[]
                    {
            ex.Message
        }
                });

                await context.Response.WriteAsync(responseBody);
            }
            catch (IdempotencyConflictException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Conflito de chave de idempotência.");

                context.Response.StatusCode =
                    StatusCodes.Status409Conflict;

                context.Response.ContentType = "application/json";

                var responseBody = JsonSerializer.Serialize(new
                {
                    Message = "Conflito de idempotência",
                    Errors = new[] { ex.Message }
                });

                await context.Response.WriteAsync(responseBody);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado na API.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var responseBody = JsonSerializer.Serialize(new { Message = "Erro interno no servidor" });
                await context.Response.WriteAsync(responseBody);
            }
        }
    }

}
