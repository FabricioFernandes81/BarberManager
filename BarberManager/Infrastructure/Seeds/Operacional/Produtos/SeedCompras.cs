
using Domain.Entities.Common.Enuns;
using Domain.Entities.Compras;
using Domain.Entities.Financeiro;
using Infrastructure.Seeds.Empresas;
using Infrastructure.Seeds.Operacional.Produtos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeds.Operacional
{
    public static class SeedCompras
    {
        public static readonly Guid Compra1 =
            Guid.Parse("a6000000-0000-0000-0000-000000000001");

        public static readonly Guid CompraItem1 =
            Guid.Parse("a6100000-0000-0000-0000-000000000001");

        public static readonly Guid CompraItem2 =
            Guid.Parse("a6100000-0000-0000-0000-000000000002");

        public static readonly Guid ContaPagar1 =
            Guid.Parse("a6200000-0000-0000-0000-000000000001");

        public static readonly Guid ContaPagar2 =
            Guid.Parse("a6200000-0000-0000-0000-000000000002");

        public static void InicializarSeed(ModelBuilder builder)
        {
            var dataCadastro = new DateTime(
                2026, 10, 1, 0, 0, 0, DateTimeKind.Utc);

            // =========================================
            // COMPRA
            // =========================================

            builder.Entity<Compra>().HasData(
                new
                {
                    Id = Compra1,
                    EmpresaId = SeedEmpresa.Empresa1,
                    FornecedorId = SeedFornecedores.Fornecedor1,

                    NumeroDocumento = "NF-TESTE-001",
                    SerieDocumento = "1",

                    DataEmissao = new DateTime(2026, 10, 1),
                    DataCompra = new DateTime(2026, 10, 1),

                    Subtotal = 325.00m,
                    Desconto = 0.00m,
                    Frete = 25.00m,
                    OutrasDespesas = 0.00m,
                    ValorTotal = 350.00m,

                    Observacao = "Compra inicial para testes do módulo.",

                    Status = StatusCompra.Pendente,

                    DataCadastro = dataCadastro,
                    DataAtualizacao = (DateTime?)null
                }
            );

            // =========================================
            // ITENS DA COMPRA
            // =========================================

            builder.Entity<CompraItem>().HasData(
                new
                {
                    Id = CompraItem1,
                    CompraId = Compra1,
                    ProdutoId = SeedProdutos.ProdutoPomadaMatte,

                    DescricaoProduto = "Pomada Efeito Matte",

                    Quantidade = 10.000m,
                    QuantidadeRecebida = 0.000m,

                    ValorUnitario = 20.00m,
                    Desconto = 0.00m,
                    ValorTotal = 200.00m,

                    DataCadastro = dataCadastro,
                    DataAtualizacao = (DateTime?)null
                },
                new
                {
                    Id = CompraItem2,
                    CompraId = Compra1,
                    ProdutoId = SeedProdutos.ProdutoShampoo,

                    DescricaoProduto = "Shampoo Profissional",

                    Quantidade = 5.000m,
                    QuantidadeRecebida = 0.000m,

                    ValorUnitario = 25.00m,
                    Desconto = 0.00m,
                    ValorTotal = 125.00m,

                    DataCadastro = dataCadastro,
                    DataAtualizacao = (DateTime?)null,

                }
            );

            // =========================================
            // CONTAS A PAGAR
            // =========================================

            builder.Entity<ContaPagar>().HasData(
                new
                {
                    Id = ContaPagar1,
                    EmpresaId = SeedEmpresa.Empresa1,
                    FornecedorId = SeedFornecedores.Fornecedor1,
                    CompraId = Compra1,

                    DataEmissao = new DateTime(2026, 10, 1),
                    DataVencimento = new DateTime(2026, 10, 10),

                    Valor = 175.00m,
                    ValorPago = 0.00m,

                    Status = StatusContas.Aberta,

                    Descricao = "Compra NF-TESTE-001 - Parcela 1/2",

                    NumeroParcela = 1,
                    TotalParcelas = 2,

                    DataCadastro = dataCadastro,
                    DataAtualizacao = (DateTime?)null,
                    ValorAbatidoDevolucao = 0m

                },
                new
                {
                    Id = ContaPagar2,
                    EmpresaId = SeedEmpresa.Empresa1,
                    FornecedorId = SeedFornecedores.Fornecedor1,
                    CompraId = Compra1,

                    DataEmissao = new DateTime(2026, 10, 1),
                    DataVencimento = new DateTime(2026, 11, 10),

                    Valor = 175.00m,
                    ValorPago = 0.00m,

                    Status = StatusContas.Aberta,

                    Descricao = "Compra NF-TESTE-001 - Parcela 2/2",

                    NumeroParcela = 2,
                    TotalParcelas = 2,

                    DataCadastro = dataCadastro,
                    DataAtualizacao = (DateTime?)null,
                    ValorAbatidoDevolucao = 0m
                }
            );
        }
    }
}
