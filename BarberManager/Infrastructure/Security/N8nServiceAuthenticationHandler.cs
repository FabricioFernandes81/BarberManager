using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Security;

public static class N8nServiceDefaults
{
    public const string AuthenticationScheme = "N8nService";
    public const string HeaderName = "X-N8N-Service-Token";
}

public sealed class N8nServiceAuthenticationHandler
    : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IConfiguration _configuration;

    public N8nServiceAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IConfiguration configuration)
        : base(options, logger, encoder)
    {
        _configuration = configuration;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var expectedToken =
            _configuration["N8n:AdminApiServiceToken"];

        if (string.IsNullOrWhiteSpace(expectedToken))
        {
            Logger.LogError(
                "Credencial de serviço do n8n não configurada.");

            return Task.FromResult(
                AuthenticateResult.Fail(
                    "Autenticação do serviço não configurada."));
        }

        if (!Request.Headers.TryGetValue(
                N8nServiceDefaults.HeaderName,
                out var providedHeader) ||
            providedHeader.Count != 1)
        {
            return Task.FromResult(
                AuthenticateResult.NoResult());
        }

        var providedToken = providedHeader.ToString();

        var expectedBytes = Encoding.UTF8.GetBytes(expectedToken);
        var providedBytes = Encoding.UTF8.GetBytes(providedToken);

        if (!CryptographicOperations.FixedTimeEquals(
                expectedBytes,
                providedBytes))
        {
            return Task.FromResult(
                AuthenticateResult.Fail(
                    "Credencial de serviço inválida."));
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "n8n-service"),
            new Claim(ClaimTypes.Name, "n8n"),
            new Claim("service", "n8n")
        };

        var identity = new ClaimsIdentity(
            claims,
            N8nServiceDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        var ticket = new AuthenticationTicket(
            principal,
            N8nServiceDefaults.AuthenticationScheme);

        return Task.FromResult(
            AuthenticateResult.Success(ticket));
    }
}