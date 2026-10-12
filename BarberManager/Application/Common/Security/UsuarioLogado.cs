using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Security;

public sealed class UsuarioLogado : IUsuarioLogado
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioLogado(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal User =>
        _httpContextAccessor.HttpContext?.User
        ?? new ClaimsPrincipal(new ClaimsIdentity());

    public bool IsAuthenticated =>
        User.Identity?.IsAuthenticated == true;

    public Guid UsuarioId
    {
        get
        {
            var value = ObterClaim(
                "usuarioId",
                ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(value, out var usuarioId))
                throw new UnauthorizedAccessException(
                    "O usuário autenticado não possui um identificador válido.");

            return usuarioId;
        }
    }

    public string? TipoUsuario =>
        ObterClaim("tipoUsuario");

    public IReadOnlyCollection<string> Roles =>
        User.Claims
            .Where(x =>
                x.Type == ClaimTypes.Role ||
                x.Type.Equals("role", StringComparison.OrdinalIgnoreCase))
            .Select(x => x.Value)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

    public bool TemRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            return false;

        return Roles.Any(x =>
            x.Equals(role, StringComparison.OrdinalIgnoreCase));
    }

    private string? ObterClaim(params string[] claimTypes)
    {
        foreach (var claimType in claimTypes)
        {
            var claim = User.Claims.FirstOrDefault(x =>
                x.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase));

            if (claim is not null)
                return claim.Value;
        }

        return null;
    }
}
