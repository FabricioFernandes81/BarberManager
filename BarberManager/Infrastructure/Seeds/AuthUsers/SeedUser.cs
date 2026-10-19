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

        public static readonly Guid UsuarioBarbeariaAdministrador1 =
            Guid.Parse("90000000-0000-0000-0000-000000000002");
        public static readonly Guid UsuarioBarbeariaAdministrador2 =
            Guid.Parse("90000000-0000-0000-0000-000000000003");
        public static readonly Guid UsuarioBarbeariaAdministrador3 =
            Guid.Parse("90000000-0000-0000-0000-000000000004");
        public static readonly Guid UsuarioBarbeariaAdministrador4 =
            Guid.Parse("90000000-0000-0000-0000-000000000005");

        public static readonly Guid UsuarioBarbeariaAdministrador5 =
            Guid.Parse("90000000-0000-0000-0000-000000000006");


        public const string EmailAdministrador =
            "admin@barberpro.com.br";

        public const string SenhaAdministrador =
            "Admin@123";

        public const string SenhaAdministradorBarbeariaTeste =
           "Teste@123";

        public const string EmailAdministradorBarbearia =
            "barbearia@barbeariavintage.com.br";

        public const string SenhaAdministradorBarbearia =
            "vintAge@123";

        public static readonly Guid UsuarioBarbeiroTeste =
              Guid.Parse("90000000-0000-0000-0000-000000000008");

        public const string EmailBarbeiroTeste =
            "barbeiro.teste@barberpro.local";

        public const string SenhaBarbeiroTeste =
            "Barbeiro@123";
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

            var userBarberAdmin1 = new Usuario
            {
                Id = UsuarioBarbeariaAdministrador1,
                UserName = "contato@oldschoolbarber.com.br",
                NormalizedUserName = "BARBEARIA@BARBEARIAVINTAGE.COM.BR".ToLower(),
                Email = "contato@oldschoolbarber.com.br",
                NormalizedEmail = "contato@oldschoolbarber.com.br".ToLower(),
                EmailConfirmed = true,
                PhoneNumber = "+55(51)999635988",
                PhoneNumberConfirmed = true,
                SecurityStamp = "8D1525C2-4C67-4B52-87A3-9A96D466F6A2",
                ConcurrencyStamp = "D3F28503-6258-4D4B-A6C7-93C29E65D1A3",
                LockoutEnabled = false,
                AccessFailedCount = 0,
                TwoFactorEnabled = false,

            };
            userBarberAdmin1.PasswordHash = hasher.HashPassword(userBarberAdmin1, "Teste@123");
            Usuarios.Add(userBarberAdmin1);

            var userBarberAdmin2 = new Usuario
            {
                Id = UsuarioBarbeariaAdministrador2,
                UserName = "contato@imperiobarber.com.br",
                NormalizedUserName = "contato@imperiobarber.com.br".ToLower(),
                Email = "contato@imperiobarber.com.br",
                NormalizedEmail = "contato@imperiobarber.com.br".ToLower(),
                EmailConfirmed = true,
                PhoneNumber = "+55(51)999635918",
                PhoneNumberConfirmed = true,
                SecurityStamp = "8D1525C2-4C67-4B52-87A3-9A96D466F6A4",
                ConcurrencyStamp = "D3F28503-6258-4D4B-A6C7-93C29E65D1A5",
                LockoutEnabled = false,
                AccessFailedCount = 0,
                TwoFactorEnabled = false,

            };
            userBarberAdmin2.PasswordHash = hasher.HashPassword(userBarberAdmin2, "Teste@123");
            Usuarios.Add(userBarberAdmin2);

            var userBarberAdmin3 = new Usuario
            {
                Id = UsuarioBarbeariaAdministrador3,
                UserName = "contato@domcorte.com.br",
                NormalizedUserName = "contato@domcorte.com.br".ToLower(),
                Email = "contato@domcorte.com.br",
                NormalizedEmail = "contato@domcorte.com.br".ToLower(),
                EmailConfirmed = true,
                PhoneNumber = "+55(51)999635918",
                PhoneNumberConfirmed = true,
                SecurityStamp = "8D1525C2-4C67-4B52-87A3-9A96D466F6A5",
                ConcurrencyStamp = "D3F28503-6258-4D4B-A6C7-93C29E65D1A6",
                LockoutEnabled = false,
                AccessFailedCount = 0,
                TwoFactorEnabled = false,

            };
            userBarberAdmin3.PasswordHash = hasher.HashPassword(userBarberAdmin3, "Teste@123");
            Usuarios.Add(userBarberAdmin3);

            var userBarberAdmin4 = new Usuario
            {
                Id = UsuarioBarbeariaAdministrador4,
                UserName = "contato@primebarber.com.br",
                NormalizedUserName = "contato@primebarber.com.br".ToLower(),
                Email = "contato@primebarber.com.br",
                NormalizedEmail = "contato@primebarber.com.br".ToLower(),
                EmailConfirmed = true,
                PhoneNumber = "+55(51)999635918",
                PhoneNumberConfirmed = true,
                SecurityStamp = "8D1525C2-4C67-4B52-87A3-9A96D466F6A6",
                ConcurrencyStamp = "D3F28503-6258-4D4B-A6C7-93C29E65D1A7",
                LockoutEnabled = false,
                AccessFailedCount = 0,
                TwoFactorEnabled = false,

            };
            userBarberAdmin4.PasswordHash = hasher.HashPassword(userBarberAdmin4, "Teste@123");
            Usuarios.Add(userBarberAdmin4);

            var userBarberAdmin5 = new Usuario
            {
                Id = UsuarioBarbeariaAdministrador5,
                UserName = "contato@blackbeard.com.br",
                NormalizedUserName = "contato@blackbeard.com.br".ToLower(),
                Email = "contato@blackbeard.com.br",
                NormalizedEmail = "contato@blackbeard.com.br".ToLower(),
                EmailConfirmed = true,
                PhoneNumber = "+55(51)999635918",
                PhoneNumberConfirmed = true,
                SecurityStamp = "8D1525C2-4C67-4B52-87A3-9A96D466F6A8",
                ConcurrencyStamp = "D3F28503-6258-4D4B-A6C7-93C29E65D1A8",
                LockoutEnabled = false,
                AccessFailedCount = 0,
                TwoFactorEnabled = false,

            };
            userBarberAdmin5.PasswordHash = hasher.HashPassword(userBarberAdmin5, "Teste@123");
            Usuarios.Add(userBarberAdmin5);



            return Usuarios;
        }
    }
}
