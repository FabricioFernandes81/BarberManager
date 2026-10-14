using Application.Common;
using Application.Common.Transactions;
using Infrastructure.Context;

namespace Infrastructure.Transactions;

public sealed class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly BarbeariaSaasDbContext _saasContext;
    private readonly BarbeariaOperacionalDbContext _operacionalContext;
    private readonly AuthDbContext _authContext;

    public UnitOfWorkFactory(
        BarbeariaSaasDbContext saasContext,
        BarbeariaOperacionalDbContext operacionalContext,
        AuthDbContext authContext)
    {
        _saasContext = saasContext;
        _operacionalContext = operacionalContext;
        _authContext = authContext;
    }

    public IUnitOfWork Create(TransactionContext context)
    {
        return context switch
        {
            TransactionContext.SaaS =>
                new EfUnitOfWork<BarbeariaSaasDbContext>(
                    _saasContext),

            TransactionContext.Operacional =>
                new EfUnitOfWork<BarbeariaOperacionalDbContext>(
                    _operacionalContext),

            TransactionContext.Auth =>
                new EfUnitOfWork<AuthDbContext>(
                    _authContext),

            _ => throw new ArgumentOutOfRangeException(
                nameof(context),
                context,
                "Contexto transacional não suportado.")
        };
    }
}