using Domain.Entities.AuthUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.AuthUsers
{
    public static class SeedUser
    {

        public static readonly Guid UsuarioAdministradorMaster =
         Guid.Parse("D0000000-0000-0000-0000-000000000001");

        public static readonly Guid UsuarioBarbeariaAdministrador =
            Guid.Parse("90000000-0000-0000-0000-000000000001");

        public const string EmailAdministrador =
            "admin@barberpro.com.br";

        public const string SenhaAdministrador =
            "Admin@123";

        public const string EmailAdministradorBarbearia =
            "barbearia@barbeariavintage.com.br";

        public const string SenhaAdministradorBarbearia =
            "vintAge@123";

        public static void IniciarSeed(ModelBuilder builder)
        {
            builder.Entity<Usuario>().HasData(SeedUsuarios());

        }

        private static List<Usuario> SeedUsuarios()
        {
            var hasher = new PasswordHasher<Usuario>();

            var Usuarios = new List<Usuario>();

            var administrador = new Usuario
            {
                Id = UsuarioAdministradorMaster,
                UserName = "admin@barberpro.com.br",
                NormalizedUserName = "admin@barberpro.com.br".ToUpperInvariant(),

                Email = "admin@barberpro.com.br",
                NormalizedEmail = "admin@barberpro.com.br".ToUpperInvariant(),

                EmailConfirmed = true,

                PhoneNumber = "(51)99999-0001",
                PhoneNumberConfirmed = true,

                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),

                LockoutEnabled = true,
                AccessFailedCount = 0,
                TwoFactorEnabled = false,

            };

            administrador.PasswordHash =
                hasher.HashPassword(administrador, "Admin@123");

            Usuarios.Add(administrador);

            var userBarberAdmin = new Usuario
            {
                Id = UsuarioBarbeariaAdministrador,
                UserName = "barbearia@barbeariavintage.com.br",
                NormalizedUserName = "BARBEARIA@BARBEARIAVINTAGE.COM.BR",
                Email = "barbearia@barbeariavintage.com.br",
                NormalizedEmail = "BARBEARIA@BARBEARIAVINTAGE.COM.BR",
                EmailConfirmed = true,
                PhoneNumber = "+55(51)999635987",
                PhoneNumberConfirmed = true,
                SecurityStamp = "8D1525C2-4C67-4B52-87A3-9A96D466F6A1",
                ConcurrencyStamp = "D3F28503-6258-4D4B-A6C7-93C29E65D1A2",
                LockoutEnabled = false,
                AccessFailedCount = 0,
                TwoFactorEnabled = false,
               
            };
            userBarberAdmin.PasswordHash = hasher.HashPassword(userBarberAdmin, "vintAge@123");
            Usuarios.Add(userBarberAdmin);

            return Usuarios;
        }
    }
}
