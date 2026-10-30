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
    public static class SeedProdutos
    {
        public static readonly Guid ProdutoPomadaMatte =
            Guid.Parse("a3000000-0000-0000-0000-000000000001");

        public static readonly Guid ProdutoPomadaBrilho =
            Guid.Parse("a3000000-0000-0000-0000-000000000002");

        public static readonly Guid ProdutoShampoo =
            Guid.Parse("a3000000-0000-0000-0000-000000000003");

        public static readonly Guid ProdutoPente =
            Guid.Parse("a3000000-0000-0000-0000-000000000004");

        public static readonly Guid ProdutoEscovaBarba =
            Guid.Parse("a3000000-0000-0000-0000-000000000005");

        public static void InicializarSeed(ModelBuilder builder)
        {
            var dataCadastro = new DateTime(2026, 10, 1);

            builder.Entity<Produto>().HasData(
                new
                {
                    Id = ProdutoPomadaMatte,
                    EmpresaId = SeedEmpresa.Empresa1,
                    CategoriaProdutoId =
                        SeedCategoriasProdutos.CategoriaPomadas,
                    MarcaId = SeedMarcas.MarcaBarberPro,
                    Nome = "Pomada Efeito Matte",
                    Descricao = "Pomada modeladora com efeito matte.",
                    CodigoInterno = "PROD-001",
                    CodigoBarras = "7891000000001",
                    ValorVenda = 39.90m,
                    Custo = 20.00m,
                    ControlaEstoque = true,
                    Ativo = true,
                    DataCadastro = dataCadastro,
                    DataAtualizacao = (DateTime?)null
                },
                new
                {
                    Id = ProdutoPomadaBrilho,
                    EmpresaId = SeedEmpresa.Empresa1,
                    CategoriaProdutoId =
                        SeedCategoriasProdutos.CategoriaPomadas,
                    MarcaId = SeedMarcas.MarcaClassic,
                    Nome = "Pomada Efeito Brilho",
                    Descricao = "Pomada modeladora com efeito brilho.",
                    CodigoInterno = "PROD-002",
                    CodigoBarras = "7891000000002",
                    ValorVenda = 34.90m,
                    Custo = 17.50m,
                    ControlaEstoque = true,
                    Ativo = true,
                    DataCadastro = dataCadastro,
                    DataAtualizacao = (DateTime?)null
                },
                new
                {
                    Id = ProdutoShampoo,
                    EmpresaId = SeedEmpresa.Empresa1,
                    CategoriaProdutoId =
                        SeedCategoriasProdutos.CategoriaShampoos,
                    MarcaId = SeedMarcas.MarcaPremium,
                    Nome = "Shampoo Profissional",
                    Descricao = "Shampoo para uso profissional.",
                    CodigoInterno = "PROD-003",
                    CodigoBarras = "7891000000003",
                    ValorVenda = 49.90m,
                    Custo = 25.00m,
                    ControlaEstoque = true,
                    Ativo = true,
                   DataCadastro = dataCadastro,
                    DataAtualizacao = (DateTime?)null
                },
                new
                {
                    Id = ProdutoPente,
                    EmpresaId = SeedEmpresa.Empresa1,
                    CategoriaProdutoId =
                        SeedCategoriasProdutos.CategoriaAcessorios,
                    MarcaId = SeedMarcas.MarcaBarberPro,
                    Nome = "Pente Profissional",
                    Descricao = "Pente para uso profissional.",
                    CodigoInterno = "PROD-004",
                    CodigoBarras = "7891000000004",
                    ValorVenda = 12.90m,
                    Custo = 5.00m,
                    ControlaEstoque = true,
                    Ativo = true,
                   DataCadastro = dataCadastro,
                    DataAtualizacao = (DateTime?)null
                },
                new
                {
                    Id = ProdutoEscovaBarba,
                    EmpresaId = SeedEmpresa.Empresa1,
                    CategoriaProdutoId =
                        SeedCategoriasProdutos.CategoriaAcessorios,
                    MarcaId = SeedMarcas.MarcaClassic,
                    Nome = "Escova para Barba",
                    Descricao = "Escova para cuidados com a barba.",
                    CodigoInterno = "PROD-005",
                    CodigoBarras = "7891000000005",
                    ValorVenda = 24.90m,
                    Custo = 10.00m,
                    ControlaEstoque = true,
                    Ativo = true,
                    DataCadastro = dataCadastro,
                    DataAtualizacao = (DateTime?)null
                }
            );
        }
    }
}
