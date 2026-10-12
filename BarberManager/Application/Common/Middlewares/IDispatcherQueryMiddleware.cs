namespace Application.Common.Middlewares
{
    public interface IDispatcherQueryMiddleware<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next);
    }
}
