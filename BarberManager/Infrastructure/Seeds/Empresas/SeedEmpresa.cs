using Domain.Entities.Common.Enuns;
using Domain.Entities.Saas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.Empresas
{
    public static class SeedEmpresa
    {
        public static readonly Guid Empresa1 =
            Guid.Parse("50000000-0000-0000-0000-000000000001");

        public static readonly Guid Empresa2 =
            Guid.Parse("50000000-0000-0000-0000-000000000002");

        public static readonly Guid Empresa3 =
            Guid.Parse("50000000-0000-0000-0000-000000000003");

        public static readonly Guid Empresa4 =
            Guid.Parse("50000000-0000-0000-0000-000000000004");

        public static readonly Guid Empresa5 =
            Guid.Parse("50000000-0000-0000-0000-000000000005");

        public static readonly Guid Empresa6 =
            Guid.Parse("50000000-0000-0000-0000-000000000006");

        public static void InicializerSeed(ModelBuilder builder)
        {
            builder.Entity<Empresa>()
                   .HasData(SetEmpresas());
        }

        private static List<Empresa> SetEmpresas()
        {
            return
            [
                new Empresa
            {
                Id = Empresa1,
                RazaoSocial = "Barbearia Vintage LTDA",
                NomeFantasia = "Barbearia Vintage",
                Cnpj = "11.111.111/0001-11",
                Email = "contato@barbeariavintage.com.br",
                Telefone = "(51) 3333-1111",
                Ativa = StatusGeral.Ativo,
              //  ResponsavelId = SeedResponsavel.Responsavel1
            },

            new Empresa
            {
                Id = Empresa2,
                RazaoSocial = "Old School Barber LTDA",
                NomeFantasia = "Old School Barber",
                Cnpj = "22.222.222/0001-22",
                Email = "contato@oldschoolbarber.com.br",
                Telefone = "(51) 3333-2222",
                Ativa = StatusGeral.Ativo,
              //  ResponsavelId = SeedResponsavel.Responsavel2
            },

            new Empresa
            {
                Id = Empresa3,
                RazaoSocial = "Black Beard Barber LTDA",
                NomeFantasia = "Black Beard Barber",
                Cnpj = "33.333.333/0001-33",
                Email = "contato@blackbeard.com.br",
                Telefone = "(51) 3333-3333",
                Ativa = StatusGeral.Ativo,
              //  ResponsavelId = SeedResponsavel.Responsavel3
            },

            new Empresa
            {
                Id = Empresa4,
                RazaoSocial = "Império Barber LTDA",
                NomeFantasia = "Império Barber",
                Cnpj = "44.444.444/0001-44",
                Email = "contato@imperiobarber.com.br",
                Telefone = "(51) 3333-4444",
                Ativa = StatusGeral.Ativo,
              //  ResponsavelId = SeedResponsavel.Responsavel4
            },

            new Empresa
            {
                Id = Empresa5,
                RazaoSocial = "Dom Corte LTDA",
                NomeFantasia = "Dom Corte",
                Cnpj = "55.555.555/0001-55",
                Email = "contato@domcorte.com.br",
                Telefone = "(51) 3333-5555",
                Ativa = StatusGeral.Ativo,
              //  ResponsavelId = SeedResponsavel.Responsavel5
            },

            new Empresa
            {
                Id = Empresa6,
                RazaoSocial = "Prime Barber Club LTDA",
                NomeFantasia = "Prime Barber Club",
                Cnpj = "66.666.666/0001-66",
                Email = "contato@primebarber.com.br",
                Telefone = "(51) 3333-6666",
                Ativa = StatusGeral.Ativo,
               // ResponsavelId = SeedResponsavel.Responsavel6
            }
            ];
        }
    }

}
