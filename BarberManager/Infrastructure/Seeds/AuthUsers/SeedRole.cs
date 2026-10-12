using Domain.Entities.AuthUsers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.AuthUsers
{
    public static class SeedRole
    {
        public static readonly Guid RoleAdministrador =
            Guid.Parse("C0000000-0000-0000-0000-000000000001");

        public static readonly Guid RoleBarberAdministrador =
          Guid.Parse("70000000-0000-0000-0000-000000000001");

        public static void SeedInicializerRoles(ModelBuilder builder)
        {
            builder.Entity<Roles>().HasData(SetRoles());
        }

        private static List<Roles> SetRoles()
        {
            return new List<Roles>
            {
                new Roles
                {
                Id = RoleAdministrador,
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR",
                ConcurrencyStamp = "C0000000-0000-0000-0000-000000000001",
                Descricao = "Administrador da plataforma SaaS.",
                Ativo = true
                },new Roles
                {
                Id = RoleBarberAdministrador,
                Name = "AdministradorBarber",
                NormalizedName = "ADMINISTRADORBARBER",
                Descricao = "Administrador da Barbearia",
                Ativo = true
            
                }
            };
        }
    }
}
