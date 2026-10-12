using Domain.Entities.AuthUsers;
using Domain.Entities.Common.Enuns;
using Infrastructure.Context;
using Infrastructure.Seeds.Empresas;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.AuthUsers
{
    public static class SeederVinculos
    {
        private static readonly Guid IdVinculoBarbearia =
       Guid.Parse("A0000000-0000-0000-0000-000000000001");

        public static async Task InicializarAsync(
            IServiceProvider services,
            CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();

            var userManager =
                scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();

            var roleManager =
                scope.ServiceProvider.GetRequiredService<RoleManager<Roles>>();

            var authContext =
                scope.ServiceProvider.GetRequiredService<AuthDbContext>();

            var saasContext =
                scope.ServiceProvider.GetRequiredService<BarbeariaSaasDbContext>();

            // A empresa precisa existir e estar ativa antes de criar o vínculo.
            var empresaExiste = await saasContext.Empresas
                .AsNoTracking()
                .AnyAsync(
                    empresa =>
                        empresa.Id == SeedEmpresa.Empresa1 &&
                        empresa.Ativa == StatusGeral.Ativo,
                    cancellationToken);

            if (!empresaExiste)
            {
                throw new InvalidOperationException(
                    "A empresa de teste não existe ou está inativa. " +
                    "Aplique as migrations do contexto SaaS.");
            }

            // As roles são mantidas no seed de modelo do AuthDbContext.
            var roleSaas = await roleManager.FindByNameAsync("Administrador");
            var roleOperacional =
                await roleManager.FindByNameAsync("AdministradorBarber");

            if (roleSaas is null || roleOperacional is null)
            {
                throw new InvalidOperationException(
                    "As roles de teste não foram encontradas. " +
                    "Aplique as migrations do AuthDbContext.");
            }

            if (!roleSaas.Ativo || !roleOperacional.Ativo)
            {
                throw new InvalidOperationException(
                    "As roles de teste precisam estar ativas.");
            }

            var administrador = await ObterOuCriarUsuarioAsync(
                userManager,
                SeedUser.UsuarioAdministradorMaster,
                SeedUser.EmailAdministrador,
                SeedUser.SenhaAdministrador,
                "(51)99999-0001");

            var administradorBarbearia = await ObterOuCriarUsuarioAsync(
                userManager,
                SeedUser.UsuarioBarbeariaAdministrador,
                SeedUser.EmailAdministradorBarbearia,
                SeedUser.SenhaAdministradorBarbearia,
                "+55(51)999635987");

            await GarantirRoleAsync(
                userManager,
                administrador,
                "Administrador");

            await GarantirRoleAsync(
                userManager,
                administradorBarbearia,
                "AdministradorBarber");

            await GarantirVinculoEmpresaAsync(
                authContext,
                administradorBarbearia,
                cancellationToken);
        }

        private static async Task<Usuario> ObterOuCriarUsuarioAsync(
            UserManager<Usuario> userManager,
            Guid usuarioId,
            string email,
            string senha,
            string telefone)
        {
            var usuario = await userManager.FindByEmailAsync(email);

            if (usuario is null)
            {
                usuario = new Usuario
                {
                    Id = usuarioId,
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    PhoneNumber = telefone,
                    PhoneNumberConfirmed = true,
                    LockoutEnabled = false,
                    TwoFactorEnabled = false
                };

                var resultado = await userManager.CreateAsync(usuario, senha);

                if (!resultado.Succeeded)
                {
                    var erros = string.Join(
                        "; ",
                        resultado.Errors.Select(erro => erro.Description));

                    throw new InvalidOperationException(
                        $"Falha ao criar o usuário de teste {email}: {erros}");
                }
            }
            else if (!await userManager.CheckPasswordAsync(usuario, senha))
            {
                // Os usuários reservados para testes são mantidos com
                // as credenciais definidas pelo seed de desenvolvimento.
                var token = await userManager.GeneratePasswordResetTokenAsync(
                    usuario);

                var resultado = await userManager.ResetPasswordAsync(
                    usuario,
                    token,
                    senha);

                if (!resultado.Succeeded)
                {
                    var erros = string.Join(
                        "; ",
                        resultado.Errors.Select(erro => erro.Description));

                    throw new InvalidOperationException(
                        $"Falha ao atualizar a senha de teste de {email}: {erros}");
                }
            }

            return usuario;
        }

        private static async Task GarantirRoleAsync(
            UserManager<Usuario> userManager,
            Usuario usuario,
            string nomeRole)
        {
            if (await userManager.IsInRoleAsync(usuario, nomeRole))
                return;

            var resultado = await userManager.AddToRoleAsync(
                usuario,
                nomeRole);

            if (!resultado.Succeeded)
            {
                var erros = string.Join(
                    "; ",
                    resultado.Errors.Select(erro => erro.Description));

                throw new InvalidOperationException(
                    $"Falha ao associar a role {nomeRole}: {erros}");
            }
        }

        private static async Task GarantirVinculoEmpresaAsync(
            AuthDbContext context,
            Usuario usuario,
            CancellationToken cancellationToken)
        {
            var vinculo = await context.UsuarioEmpresa
                .SingleOrDefaultAsync(
                    item =>
                        item.UsuarioId == usuario.Id &&
                        item.EmpresaId == SeedEmpresa.Empresa1,
                    cancellationToken);

            if (vinculo is null)
            {
                context.UsuarioEmpresa.Add(new UsuarioEmpresa
                {
                    Id = IdVinculoBarbearia,
                    UsuarioId = usuario.Id,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Ativo = true,
                    DataVinculo = DateTime.UtcNow
                });
            }
            else
            {
                // Garante um vínculo utilizável no ambiente de testes.
                vinculo.Ativo = true;
                vinculo.DataDesvinculo = null;
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
