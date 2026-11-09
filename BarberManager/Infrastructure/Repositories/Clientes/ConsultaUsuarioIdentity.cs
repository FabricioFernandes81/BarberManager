using Application.Interfaces.Clientes;
using Domain.Entities.AuthUsers;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public sealed class ConsultaUsuarioIdentity
        : IConsultaUsuarioIdentity
    {
        private readonly UserManager<Usuario> _userManager;

        public ConsultaUsuarioIdentity(
            UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DadosContatoUsuarioDto?> ObterContatoAsync(
            Guid usuarioId,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var usuario = await _userManager.FindByIdAsync(
                usuarioId.ToString());

            if (usuario is null)
                return null;

            return new DadosContatoUsuarioDto(
                usuario.Email,
                usuario.PhoneNumber);
        }
    }
}