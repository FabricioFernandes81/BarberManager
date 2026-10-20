using Domain.Entities.AuthUsers;
using Domain.Entities.Clientes;
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

namespace Infrastructure.Seeds.Clientes
{
    public static class SeederClientes
    {
        private static readonly Guid UsuarioAnaId =
            Guid.Parse("C1000000-0000-0000-0000-000000000001");

        private static readonly Guid UsuarioBrunoId =
            Guid.Parse("C1000000-0000-0000-0000-000000000002");

        private static readonly Guid VinculoAnaVintageId =
            Guid.Parse("C2000000-0000-0000-0000-000000000001");

        private static readonly Guid VinculoAnaOldSchoolId =
            Guid.Parse("C2000000-0000-0000-0000-000000000002");

        private static readonly Guid VinculoBrunoVintageId =
            Guid.Parse("C2000000-0000-0000-0000-000000000003");

        private sealed record ClienteTeste(
            Guid UsuarioId,
            string Email,
            string Senha,
            string Nome,
            string Sobrenome,
            DateTime DataNascimento);

        public static async Task InicializarAsync(
            IServiceProvider services,
            CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();

            var userManager =
                scope.ServiceProvider
                    .GetRequiredService<UserManager<Usuario>>();

            var authContext =
                scope.ServiceProvider
                    .GetRequiredService<AuthDbContext>();

            var saasContext =
                scope.ServiceProvider
                    .GetRequiredService<BarbeariaSaasDbContext>();

            var operacionalContext =
                scope.ServiceProvider
                    .GetRequiredService<BarbeariaOperacionalDbContext>();

            // Os bancos precisam estar preparados antes da execução.
            await authContext.Database.CanConnectAsync(cancellationToken);
            await saasContext.Database.CanConnectAsync(cancellationToken);
            await operacionalContext.Database.CanConnectAsync(
                cancellationToken);

            // Confirma que as empresas de demonstração existem e estão ativas.
            var empresasNecessarias = new[]
            {
            SeedEmpresa.Empresa1,
            SeedEmpresa.Empresa2
        };

            var empresasEncontradas = await saasContext.Empresas
                .AsNoTracking()
                .Where(e => empresasNecessarias.Contains(e.Id)
                    && e.Ativa == StatusGeral.Ativo)
                .Select(e => e.Id)
                .ToListAsync(cancellationToken);

            if (empresasEncontradas.Count != empresasNecessarias.Length)
            {
                throw new InvalidOperationException(
                    "As empresas de demonstração não estão disponíveis. " +
                    "Verifique as migrations e os seeds do contexto SaaS.");
            }

            var clientesTeste = new[]
            {
            new ClienteTeste(
                UsuarioAnaId,
                "ana.cliente@barbermanager.local",
                "Dev@Ana2026!",
                "Ana",
                "Souza",
                new DateTime(1995, 5, 12)),

            new ClienteTeste(
                UsuarioBrunoId,
                "bruno.cliente@barbermanager.local",
                "Dev@Bruno2026!",
                "Bruno",
                "Lima",
                new DateTime(1990, 8, 24))
        };

            foreach (var dados in clientesTeste)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var usuario = await ObterOuCriarUsuarioAsync(
                    userManager,
                    dados,
                    cancellationToken);

                var cliente = await operacionalContext.Clientes
                    .SingleOrDefaultAsync(
                        c => c.UsuarioId == usuario.Id,
                        cancellationToken);

                if (cliente is null)
                {
                    cliente = new Cliente
                    {
                        UsuarioId = usuario.Id,
                        Nome = dados.Nome,
                        Sobrenome = dados.Sobrenome,
                        DataNascimento = dados.DataNascimento,
                        Ativo = true
                    };

                    operacionalContext.Clientes.Add(cliente);
                    await operacionalContext.SaveChangesAsync(
                        cancellationToken);
                }

                // Ana possui vínculos com duas empresas.
                // Bruno possui vínculo apenas com a primeira.
                if (dados.UsuarioId == UsuarioAnaId)
                {
                    await GarantirVinculoAsync(
                        operacionalContext,
                        cliente.Id,
                        SeedEmpresa.Empresa1,
                        VinculoAnaVintageId,
                        cancellationToken);

                    await GarantirVinculoAsync(
                        operacionalContext,
                        cliente.Id,
                        SeedEmpresa.Empresa2,
                        VinculoAnaOldSchoolId,
                        cancellationToken);
                }
                else
                {
                    await GarantirVinculoAsync(
                        operacionalContext,
                        cliente.Id,
                        SeedEmpresa.Empresa1,
                        VinculoBrunoVintageId,
                        cancellationToken);
                }
            }
        }

        private static async Task<Usuario> ObterOuCriarUsuarioAsync(
            UserManager<Usuario> userManager,
            ClienteTeste dados,
            CancellationToken cancellationToken)
        {
            var usuario = await userManager.FindByEmailAsync(dados.Email);

            if (usuario is not null)
            {
                // Não altera senhas ou dados de contas preexistentes.
                return usuario;
            }

            usuario = new Usuario
            {
                Id = dados.UsuarioId,
                UserName = dados.Email,
                Email = dados.Email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                LockoutEnabled = false,
                TwoFactorEnabled = false
            };

            var resultado = await userManager.CreateAsync(
                usuario,
                dados.Senha);

            if (!resultado.Succeeded)
            {
                var erros = string.Join(
                    "; ",
                    resultado.Errors.Select(e => e.Description));

                // Uma execução concorrente pode ter criado a conta
                // entre a consulta inicial e a tentativa de inserção.
                var usuarioConcorrente =
                    await userManager.FindByEmailAsync(dados.Email);

                if (usuarioConcorrente is not null)
                    return usuarioConcorrente;

                cancellationToken.ThrowIfCancellationRequested();

                throw new InvalidOperationException(
                    $"Falha ao criar a conta de demonstração " +
                    $"{dados.Email}: {erros}");
            }

            return usuario;
        }

        private static async Task GarantirVinculoAsync(
            BarbeariaOperacionalDbContext context,
            Guid clienteId,
            Guid empresaId,
            Guid vinculoId,
            CancellationToken cancellationToken)
        {
            var vinculo = await context.ClientesEmpresas
                .SingleOrDefaultAsync(
                    x => x.ClienteId == clienteId
                        && x.EmpresaId == empresaId,
                    cancellationToken);

            if (vinculo is null)
            {
                context.ClientesEmpresas.Add(new ClienteEmpresa
                {
                    Id = vinculoId,
                    ClienteId = clienteId,
                    EmpresaId = empresaId,
                    DataVinculo = DateTime.UtcNow,
                    Ativo = true
                });
            }
            else
            {
                // Reativa o vínculo de demonstração sem duplicá-lo.
                vinculo.Ativo = true;
            }

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
