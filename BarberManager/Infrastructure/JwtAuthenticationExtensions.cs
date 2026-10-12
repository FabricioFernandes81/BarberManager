using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Infrastructure;

public static class JwtAuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration,
        string tipoUsuario)
    {
        var issuer = configuration["JWT:ValidIssuer"]
            ?? throw new InvalidOperationException(
                "JWT:ValidIssuer não foi configurado.");

        var audience = configuration["JWT:ValidAudience"]
            ?? throw new InvalidOperationException(
                "JWT:ValidAudience não foi configurado.");

        var secret = configuration["JWT:Secret"]
            ?? throw new InvalidOperationException(
                "JWT:Secret não foi configurado.");

        if (Encoding.UTF8.GetByteCount(secret) < 32)
        {
            throw new InvalidOperationException(
                "JWT:Secret deve possuir pelo menos 32 bytes.");
        }

        var signingKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secret));

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;
                options.SaveToken = false;
                options.RequireHttpsMetadata = true;

                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = issuer,

                        ValidateAudience = true,
                        ValidAudience = audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = signingKey,

                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        RequireSignedTokens = true,

                        ValidAlgorithms =
                        [
                            SecurityAlgorithms.HmacSha256
                        ],

                        ClockSkew = TimeSpan.FromMinutes(1),

                        NameClaimType = "email",
                        RoleClaimType = ClaimTypes.Role
                    };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var tipoToken = context.Principal?
                            .FindFirst("tipoUsuario")?.Value;

                        if (!string.Equals(
                            tipoToken,
                            tipoUsuario,
                            StringComparison.Ordinal))
                        {
                            context.Fail(
                                "Tipo de token não autorizado para esta API.");
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy =
                new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
        });

        return services;
    }
}