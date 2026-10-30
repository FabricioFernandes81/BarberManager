using Domain.Entities.Common.Enuns;
using Domain.Entities.Estoque;
using Infrastructure.Seeds.Empresas;
using Infrastructure.Seeds.Operacional.Produtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.Operacional.Estoque
{
    public static class SeedEstoqueProdutos
    {
        public static readonly Guid EstoquePomadaMatte =
            Guid.Parse("a4000000-0000-0000-0000-000000000001");

        public static readonly Guid EstoquePomadaBrilho =
            Guid.Parse("a4000000-0000-0000-0000-000000000002");

        public static readonly Guid EstoqueShampoo =
            Guid.Parse("a4000000-0000-0000-0000-000000000003");

        public static readonly Guid EstoquePente =
            Guid.Parse("a4000000-0000-0000-0000-000000000004");

        public static readonly Guid EstoqueEscovaBarba =
            Guid.Parse("a4000000-0000-0000-0000-000000000005");

        public static readonly Guid MovimentacaoPomadaMatte =
            Guid.Parse("a5000000-0000-0000-0000-000000000001");

        public static readonly Guid MovimentacaoPomadaBrilho =
            Guid.Parse("a5000000-0000-0000-0000-000000000002");

        public static readonly Guid MovimentacaoShampoo =
            Guid.Parse("a5000000-0000-0000-0000-000000000003");

        public static readonly Guid MovimentacaoPente =
            Guid.Parse("a5000000-0000-0000-0000-000000000004");

        public static readonly Guid MovimentacaoEscovaBarba =
            Guid.Parse("a5000000-0000-0000-0000-000000000005");

        public static void InicializarSeed(ModelBuilder builder)
        {
            var dataCadastro = new DateTime(2026, 10, 1);
            var dataMovimento = new DateTimeOffset(
                2026, 10, 1, 12, 0, 0, TimeSpan.Zero);

            // Saldos iniciais.
            builder.Entity<EstoqueProduto>().HasData(
                new
                {
                    Id = EstoquePomadaMatte,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ProdutoId = SeedProdutos.ProdutoPomadaMatte,
                    QuantidadeAtual = 15m,
                    EstoqueMinimo = 3m,
                    DataAtualizacaoEstoque = dataCadastro,
                    DataCadastro = dataCadastro
                },
                new
                {
                    Id = EstoquePomadaBrilho,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ProdutoId = SeedProdutos.ProdutoPomadaBrilho,
                    QuantidadeAtual = 10m,
                    EstoqueMinimo = 2m,
                    DataAtualizacaoEstoque = dataCadastro,
                   DataCadastro = dataCadastro
                },
                new
                {
                    Id = EstoqueShampoo,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ProdutoId = SeedProdutos.ProdutoShampoo,
                    QuantidadeAtual = 8m,
                    EstoqueMinimo = 2m,
                    DataAtualizacaoEstoque = dataCadastro,
                    DataCadastro = dataCadastro
                },
                new
                {
                    Id = EstoquePente,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ProdutoId = SeedProdutos.ProdutoPente,
                    QuantidadeAtual = 20m,
                    EstoqueMinimo = 5m,
                    DataAtualizacaoEstoque = dataCadastro,
                   DataCadastro = dataCadastro
                },
                new
                {
                    Id = EstoqueEscovaBarba,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ProdutoId = SeedProdutos.ProdutoEscovaBarba,
                    QuantidadeAtual = 12m,
                    EstoqueMinimo = 3m,
                    DataAtualizacaoEstoque = dataCadastro,
                    DataCadastro = dataCadastro
                }
            );

            // Movimentações que justificam os saldos iniciais.
            builder.Entity<MovimentacaoEstoque>().HasData(
                new
                {
                    Id = MovimentacaoPomadaMatte,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ProdutoId = SeedProdutos.ProdutoPomadaMatte,
                    Tipo = TipoMovimentacaoEstoque.Entrada,
                    Origem = OrigemMovimentacaoEstoque.CadastroInicial,
                    Quantidade = 15m,
                    SaldoAnterior = 0m,
                    SaldoPosterior = 15m,
                    DocumentoOrigemId = (Guid?)null,
                    UsuarioResponsavelId = (Guid?)null,
                    DataMovimento = dataMovimento,
                    Observacao = "Estoque inicial para testes.",
                    DataCadastro = dataCadastro
                    //   DataAtualizacao = (DateTime?)null
                },
                new
                {
                    Id = MovimentacaoPomadaBrilho,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ProdutoId = SeedProdutos.ProdutoPomadaBrilho,
                    Tipo = TipoMovimentacaoEstoque.Entrada,
                    Origem = OrigemMovimentacaoEstoque.CadastroInicial,
                    Quantidade = 10m,
                    SaldoAnterior = 0m,
                    SaldoPosterior = 10m,
                    DocumentoOrigemId = (Guid?)null,
                    UsuarioResponsavelId = (Guid?)null,
                    DataMovimento = dataMovimento,
                    Observacao = "Estoque inicial para testes.",
                    DataCadastro = dataCadastro
                    //   DataAtualizacao = (DateTime?)null
                },
                new
                {
                    Id = MovimentacaoShampoo,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ProdutoId = SeedProdutos.ProdutoShampoo,
                    Tipo = TipoMovimentacaoEstoque.Entrada,
                    Origem = OrigemMovimentacaoEstoque.CadastroInicial,
                    Quantidade = 8m,
                    SaldoAnterior = 0m,
                    SaldoPosterior = 8m,
                    DocumentoOrigemId = (Guid?)null,
                    UsuarioResponsavelId = (Guid?)null,
                    DataMovimento = dataMovimento,
                    Observacao = "Estoque inicial para testes.",
                    DataCadastro = dataCadastro
                    //   DataAtualizacao = (DateTime?)null
                },
                new
                {
                    Id = MovimentacaoPente,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ProdutoId = SeedProdutos.ProdutoPente,
                    Tipo = TipoMovimentacaoEstoque.Entrada,
                    Origem = OrigemMovimentacaoEstoque.CadastroInicial,
                    Quantidade = 20m,
                    SaldoAnterior = 0m,
                    SaldoPosterior = 20m,
                    DocumentoOrigemId = (Guid?)null,
                    UsuarioResponsavelId = (Guid?)null,
                    DataMovimento = dataMovimento,
                    Observacao = "Estoque inicial para testes.",
                    DataCadastro = dataCadastro
                    //   DataAtualizacao = (DateTime?)null
                },
                new
                {
                    Id = MovimentacaoEscovaBarba,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ProdutoId = SeedProdutos.ProdutoEscovaBarba,
                    Tipo = TipoMovimentacaoEstoque.Entrada,
                    Origem = OrigemMovimentacaoEstoque.CadastroInicial,
                    Quantidade = 12m,
                    SaldoAnterior = 0m,
                    SaldoPosterior = 12m,
                    DocumentoOrigemId = (Guid?)null,
                    UsuarioResponsavelId = (Guid?)null,
                    DataMovimento = dataMovimento,
                    Observacao = "Estoque inicial para testes.",
                    DataCadastro = dataCadastro,
                 //   DataAtualizacao = (DateTime?)null
                }
            );
        }
    }
}
