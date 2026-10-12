using Microsoft.AspNetCore.Http;

namespace Application.Common.Tenancy;

public sealed class TenantContext : ITenantContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAvailable
    {
        get
        {
            var value = ObterEmpresaId();

            return Guid.TryParse(value, out var empresaId)
                   && empresaId != Guid.Empty;
        }
    }

    public Guid EmpresaId
    {
        get
        {
            var value = ObterEmpresaId();

            if (!Guid.TryParse(value, out var empresaId) ||
                empresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "O contexto do tenant não possui uma EmpresaId válida.");
            }

            return empresaId;
        }
    }

    private string? ObterEmpresaId()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true)
            return null;

        var claim = user.Claims.FirstOrDefault(x =>
            x.Type.Equals("EmpresaId", StringComparison.OrdinalIgnoreCase));

        return claim?.Value;
    }
}
