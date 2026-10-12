using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Security;
using Domain.Entities.AuthUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Common.Security
{
    public sealed class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Usuario> _userManager;

        public JwtTokenService(
            IConfiguration configuration,
            UserManager<Usuario> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<(string AccessToken, DateTime ExpiraEm)> GerarAsync(
     Usuario usuario,
     ContextoAutenticacao contexto,
     CancellationToken cancellationToken = default)
        {
            if (contexto.TipoUsuario == "Operacional" &&
                contexto.EmpresaId is null)
            {
                throw new InvalidOperationException(
                    "O token operacional exige uma empresa.");
            }

            var issuer = _configuration["JWT:ValidIssuer"]
                ?? throw new InvalidOperationException(
                    "JWT:ValidIssuer não configurado.");

            var audience = _configuration["JWT:ValidAudience"]
                ?? throw new InvalidOperationException(
                    "JWT:ValidAudience não configurado.");

            var secret = _configuration["JWT:Secret"]
                ?? throw new InvalidOperationException(
                    "JWT:Secret não configurado.");

            var roles = await _userManager.GetRolesAsync(usuario);

            var claims = new List<Claim>
    {
        new("usuarioId", usuario.Id.ToString()),
        new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
        new(JwtRegisteredClaimNames.Email, usuario.Email ?? string.Empty),
        new("tipoUsuario", contexto.TipoUsuario),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            if (contexto.EmpresaId.HasValue)
            {
                claims.Add(new Claim(
                    "EmpresaId",
                    contexto.EmpresaId.Value.ToString()));
            }

            //  claims.AddRange(
            //      roles.Select(role => new Claim(ClaimTypes.Role, role)));
            claims.AddRange(
                 contexto.Permissoes
                        .Distinct(StringComparer.Ordinal)
                                .Select(codigo => new Claim("permissao", codigo)));
           
            var chave = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secret));

            var credenciais = new SigningCredentials(
                chave,
                SecurityAlgorithms.HmacSha256);

            var agora = DateTime.UtcNow;
            var expiraEm = agora.AddMinutes(30);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: agora,
                expires: expiraEm,
                signingCredentials: credenciais);

            return (
                new JwtSecurityTokenHandler().WriteToken(token),
                expiraEm
            );
        }
    }
}
