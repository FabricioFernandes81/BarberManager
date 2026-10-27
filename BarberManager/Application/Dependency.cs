using Application.Common.Middlewares;
using Application.Common.Middlewares.Handler;
using Application.Common.Pagination;
using Application.Common.Security;
using Application.Interfaces;
using Application.Interfaces.Empresas.Pagamentos;
using Application.Interfaces.Financeiro;
using Application.Saas.Pagamentos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Dependency
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        var assembly = typeof(Dependency).Assembly;

        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(c =>
                c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(c =>
                c.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(c =>
                c.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddScoped(
            typeof(IDispatcherCommandMiddleware<,>),
            typeof(CommandLoggingMiddleware<,>));

        services.AddScoped(
            typeof(IDispatcherQueryMiddleware<,>),
            typeof(QueryLoggingMiddleware<,>));

        services.AddScoped(
            typeof(IDispatcherCommandMiddleware<,>),
            typeof(TransactionMiddleware<,>));

        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IUrlService, UrlService>();
        services.AddScoped<IPagamentoService, PagamentoService>();


        
        return services;
    }

    public static IServiceCollection AddPermissions(
        this IServiceCollection services)
    {
        services.AddSingleton<
            IAuthorizationPolicyProvider,
            PermissionPolicyProvider>();

        services.AddScoped<
            IAuthorizationHandler,
            PermissionHandler>();

        return services;
    }
}