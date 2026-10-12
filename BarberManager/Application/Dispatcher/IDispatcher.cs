namespace Application.Dispatcher
{
    public interface IDispatcher
    {
        Task<TResult> Command<TCommand, TResult>(TCommand command,
       CancellationToken cancellationToken = default) where TCommand : class;

        Task<TResult> Query<TQuery, TResult>(TQuery query,
            CancellationToken cancellationToken = default) where TQuery : class;



    }
}
