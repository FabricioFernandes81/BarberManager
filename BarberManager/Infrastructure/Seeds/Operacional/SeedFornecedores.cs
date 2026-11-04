
using Domain.Entities.Compras;
using Infrastructure.Seeds.Empresas;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeds.Operacional
{
    public static class SeedFornecedores
    {
        public static readonly Guid Fornecedor1 =
            Guid.Parse("a3000000-0000-0000-0000-000000000010");

        public static readonly Guid Fornecedor2 =
            Guid.Parse("a3000000-0000-0000-0000-000000000011");

        public static readonly Guid Fornecedor3 =
            Guid.Parse("a3000000-0000-0000-0000-000000000012");

        public static readonly Guid Fornecedor4 =
            Guid.Parse("a3000000-0000-0000-0000-000000000013");

        public static void InicializarSeed(ModelBuilder builder)
        {
            var dataCadastro = new DateTime(
                2026, 10, 1, 0, 0, 0, DateTimeKind.Utc);

            builder.Entity<Fornecedor>().HasData(
                new
                {
                    Id = Fornecedor1,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Distribuidora Barber Pro",
                    NomeFantasia = "Barber Pro Distribuidora",
                    Documento = "12345678000190",
                    Email = "contato@barberpro.com.br",
                    Telefone = "51999990001",
                    Observacao = "Fornecedor de produtos profissionais.",
                    Ativo = true,
                    DataCadastro = dataCadastro,
                    DataAtualizacao = (DateTime?)null
                },
                new
                {
                    Id = Fornecedor2,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Cosmeticos Premium LTDA",
                    NomeFantasia = "Premium Cosméticos",
                    Documento = "23456789000101",
                    Email = "vendas@premium.com.br",
                    Telefone = "51999990002",
                    Observacao = "Fornecedor de cosméticos e shampoos.",
                    Ativo = true,
                    DataCadastro = dataCadastro,
                    DataAtualizacao = (DateTime?)null
                },
                new
                {
                    Id = Fornecedor3,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Acessorios Classic LTDA",
                    NomeFantasia = "Classic Acessórios",
                    Documento = "34567890000112",
                    Email = "contato@classic.com.br",
                    Telefone = "51999990003",
                    Observacao = "Fornecedor de acessórios para barbearia.",
                    Ativo = true,
                    DataCadastro = dataCadastro,
                    DataAtualizacao = (DateTime?)null
                },
                new
                {
                    Id = Fornecedor4,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Distribuidora Nacional",
                    NomeFantasia = "Nacional Distribuidora",
                    Documento = "45678901000123",
                    Email = "vendas@nacional.com.br",
                    Telefone = "51999990004",
                    Observacao = "Fornecedor geral para testes.",
                    Ativo = true,
                    DataCadastro = dataCadastro,
                    DataAtualizacao = (DateTime?)null
                }
            );
        }
    }
}
