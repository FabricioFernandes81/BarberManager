using Application.Interfaces.Clientes;
using Domain.Entities.AuthUsers;
using Domain.Entities.Clientes;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public sealed class ConsultaUsuarioIdentity
        : IConsultaUsuarioIdentity
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly BarbeariaOperacionalDbContext _context;
        public ConsultaUsuarioIdentity(
            UserManager<Usuario> userManager,
            BarbeariaOperacionalDbContext context)
        {
            _userManager = userManager;
            _context = context;
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

        public async Task<Guid?> ObterUsuarioIdPorTelefoneAsync(
    string telefone,
    CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                return null;

            cancellationToken.ThrowIfCancellationRequested();

            var telefoneNormalizado = new string(
                telefone.Where(char.IsDigit).ToArray());

            if (string.IsNullOrWhiteSpace(telefoneNormalizado))
                return null;

            var usuario = await _userManager.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(
                    x => x.PhoneNumber == telefoneNormalizado,
                    cancellationToken);

            return usuario?.Id;
        }

     
    }
}