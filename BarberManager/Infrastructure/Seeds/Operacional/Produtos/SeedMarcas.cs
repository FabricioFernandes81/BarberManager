using Domain.Entities.Produtos;
using Infrastructure.Seeds.Empresas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.Operacional.Produtos
{
    public static class SeedMarcas
    {
        public static readonly Guid MarcaBarberPro =
            Guid.Parse("a2000000-0000-0000-0000-000000000001");

        public static readonly Guid MarcaClassic =
            Guid.Parse("a2000000-0000-0000-0000-000000000002");

        public static readonly Guid MarcaPremium =
            Guid.Parse("a2000000-0000-0000-0000-000000000003");

        public static void InicializarSeed(ModelBuilder builder)
        {
            builder.Entity<Marca>().HasData(
                new Marca
                {
                    Id = MarcaBarberPro,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Barber Pro",
                    Descricao = "Linha profissional para barbearias.",
                    Ativo = true,
                  //  DataCadastro = new DateTime(2026, 10, 1),
                    DataAtualizacao = null
                },
                new Marca
                {
                    Id = MarcaClassic,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Classic Grooming",
                    Descricao = "Produtos clássicos para cuidados pessoais.",
                    Ativo = true,
                  //  DataCadastro = new DateTime(2026, 10, 1),
                    DataAtualizacao = null
                },
                new Marca
                {
                    Id = MarcaPremium,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Premium Care",
                    Descricao = "Linha premium de cuidados capilares.",
                    Ativo = true,
                   // DataCadastro = new DateTime(2026, 10, 1),
                    DataAtualizacao = null
                }
            );
        }
    }
}
