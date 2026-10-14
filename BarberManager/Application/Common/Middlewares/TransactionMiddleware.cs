using Application.Common.Transactions;
using Microsoft.Extensions.Logging;

namespace Application.Common.Middlewares;

public sealed class TransactionMiddleware<TCommand, TResult>
    : IDispatcherCommandMiddleware<TCommand, TResult>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly ILogger<TransactionMiddleware<TCommand, TResult>>
        _logger;

    public TransactionMiddleware(
        IUnitOfWorkFactory unitOfWorkFactory,
        ILogger<TransactionMiddleware<TCommand, TResult>> logger)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _logger = logger;
    }

    public async Task<TResult> Handle(
        TCommand command,
        Func<Task<TResult>> next)
    {
        // Comandos sem marcador seguem sem transação automática.
        if (command is not ITransactionalCommand transactionalCommand)
            return await next();

        await using var unitOfWork = _unitOfWorkFactory.Create(
            transactionalCommand.TransactionContext);

        await unitOfWork.BeginTransactionAsync();

        try
        {
            var resultado = await next();

            await unitOfWork.CommitTransactionAsync();

            _logger.LogDebug(
                "Transação confirmada para {CommandType}, contexto {Contexto}.",
                typeof(TCommand).Name,
                transactionalCommand.TransactionContext);

            return resultado;
        }
        catch
        {
            try
            {
                // Não reutiliza um token possivelmente cancelado
                // para tentar reverter a transação.
                await unitOfWork.RollbackTransactionAsync(
                    CancellationToken.None);
            }
            catch (Exception rollbackException)
            {
                _logger.LogError(
                    rollbackException,
                    "Falha ao reverter a transação do comando {CommandType}.",
                    typeof(TCommand).Name);
            }

            throw;
        }
    }

    
}