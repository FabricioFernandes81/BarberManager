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
    public static class SeedCategoriasProdutos
    {
        public static readonly Guid CategoriaPomadas =
            Guid.Parse("a1000000-0000-0000-0000-000000000001");

        public static readonly Guid CategoriaShampoos =
            Guid.Parse("a1000000-0000-0000-0000-000000000002");

        public static readonly Guid CategoriaAcessorios =
            Guid.Parse("a1000000-0000-0000-0000-000000000003");

        public static void InicializarSeed(ModelBuilder builder)
        {
            builder.Entity<CategoriaProduto>().HasData(
                new CategoriaProduto
                {
                    Id = CategoriaPomadas,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Pomadas e Finalizadores",
                    Descricao = "Produtos para finalização e modelagem capilar.",
                    Ativo = true,
                    DataCadastro = new DateTime(2026, 10, 1),
                    DataAtualizacao = null
                },
                new CategoriaProduto
                {
                    Id = CategoriaShampoos,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Shampoos e Higiene",
                    Descricao = "Produtos para limpeza e cuidados capilares.",
                    Ativo = true,
                    DataCadastro = new DateTime(2026, 10, 1),
                    DataAtualizacao = null
                },
                new CategoriaProduto
                {
                    Id = CategoriaAcessorios,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Acessórios",
                    Descricao = "Acessórios utilizados em barbearias.",
                    Ativo = true,
                    DataCadastro = new DateTime(2026, 10, 1),
                    DataAtualizacao = null
                }
            );
        }
    }
}
