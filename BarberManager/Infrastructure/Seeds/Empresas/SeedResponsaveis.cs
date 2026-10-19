using Domain.Entities.Saas;
using Infrastructure.Seeds.AuthUsers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.Empresas
{
    public static class SeedResponsaveis
    {
        public static readonly Guid Responsavel1 =
           Guid.Parse("30000000-0000-0000-0000-000000000001");

        public static readonly Guid Responsavel2 =
            Guid.Parse("30000000-0000-0000-0000-000000000002");

        public static readonly Guid Responsavel3 =
            Guid.Parse("30000000-0000-0000-0000-000000000003");

        public static readonly Guid Responsavel4 =
            Guid.Parse("30000000-0000-0000-0000-000000000004");

        public static readonly Guid Responsavel5 =
            Guid.Parse("30000000-0000-0000-0000-000000000005");

        public static readonly Guid Responsavel6 =
            Guid.Parse("30000000-0000-0000-0000-000000000006");


        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Responsavel>()
                .HasData(SetResponsaveis());
        }

        private static List<Responsavel> SetResponsaveis()
        {
            return
            [
                new Responsavel
            {
                Id = Responsavel1,
                Nome = "João Carlos Silva",
                Email = "joao.silva@barberpro.com.br",
                Telefone = "(51) 99991-0001",
                Cpf = "111.111.111-11",
                UsuarioId = SeedUser.UsuarioBarbeariaAdministrador
                
            },

            new Responsavel
            {
                Id = Responsavel2,
                Nome = "Marcos Oliveira",
                Email = "marcos.oliveira@barberpro.com.br",
                Telefone = "(51) 99991-0002",
                Cpf = "222.222.222-22",
                UsuarioId = SeedUser.UsuarioBarbeariaAdministrador1
            },

            new Responsavel
            {
                Id = Responsavel3,
                Nome = "Pedro Henrique Souza",
                Email = "pedro.souza@barberpro.com.br",
                Telefone = "(51) 99991-0003",
                Cpf = "333.333.333-33",
                UsuarioId = SeedUser.UsuarioBarbeariaAdministrador2
            },

            new Responsavel
            {
                Id = Responsavel4,
                Nome = "Lucas Ferreira",
                Email = "lucas.ferreira@barberpro.com.br",
                Telefone = "(51) 99991-0004",
                Cpf = "444.444.444-44",
                UsuarioId = SeedUser.UsuarioBarbeariaAdministrador3
            },

            new Responsavel
            {
                Id = Responsavel5,
                Nome = "Rafael Almeida",
                Email = "rafael.almeida@barberpro.com.br",
                Telefone = "(51) 99991-0005",
                Cpf = "555.555.555-55",
                UsuarioId = SeedUser.UsuarioBarbeariaAdministrador4
            },

            new Responsavel
            {
                Id = Responsavel6,
                Nome = "Gabriel Martins",
                Email = "gabriel.martins@barberpro.com.br",
                Telefone = "(51) 99991-0006",
                Cpf = "666.666.666-66",
                UsuarioId = SeedUser.UsuarioBarbeariaAdministrador5
            }
            ];
        }
    }

}

