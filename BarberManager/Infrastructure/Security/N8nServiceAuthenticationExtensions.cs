using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Security;

public static class N8nServiceAuthenticationExtensions
{
    public static IServiceCollection AddN8nServiceAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var token = configuration[
            "N8n:AdminApiServiceToken"];

        if (string.IsNullOrWhiteSpace(token) ||
            Encoding.UTF8.GetByteCount(token) < 32)
        {
            throw new InvalidOperationException(
                "N8n:AdminApiServiceToken deve estar configurado " +
                "e possuir pelo menos 32 bytes.");
        }

        services
            .AddAuthentication()
            .AddScheme<
                AuthenticationSchemeOptions,
                N8nServiceAuthenticationHandler>(
                    N8nServiceDefaults.AuthenticationScheme,
                    _ => { });

        return services;
    }
}