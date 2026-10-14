using Domain.Entities.Common.Enuns;
using Domain.Entities.Saas;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeds.Empresas
{
    public static class SeedTransacaoPagamento
    {
        public static void InicializerSeed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransacaoPagamento>().HasData(
                new TransacaoPagamento
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000001"),
                    FaturaId = Guid.Parse("50000000-0000-0000-0000-000000000001"),
                    Provedor = "MercadoPago",
                    ReferenciaGateway = "TESTE-TRANSACAO-001",
                    ChaveIdempotencia = "SEED-FATURA-001-TENTATIVA-001",
                    Valor = 99.90m,
                    Status = StatusTransacaoPagamento.Aprovada,
                    DataProcessamento = new DateTime(2026, 1, 8, 12, 0, 0, DateTimeKind.Utc),
                    DataCadastro = new DateTime(2026, 1, 8, 11, 55, 0, DateTimeKind.Utc)
                },
                new TransacaoPagamento
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000002"),
                    FaturaId = Guid.Parse("50000000-0000-0000-0000-000000000002"),
                    Provedor = "MercadoPago",
                    ReferenciaGateway = "TESTE-TRANSACAO-002",
                    ChaveIdempotencia = "SEED-FATURA-002-TENTATIVA-001",
                    Valor = 99.90m,
                    Status = StatusTransacaoPagamento.Aprovada,
                    DataProcessamento = new DateTime(2026, 2, 9, 12, 0, 0, DateTimeKind.Utc),
                    DataCadastro = new DateTime(2026, 2, 9, 11, 55, 0, DateTimeKind.Utc)
                },
                new TransacaoPagamento
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000003"),
                    FaturaId = Guid.Parse("50000000-0000-0000-0000-000000000003"),
                    Provedor = "MercadoPago",
                    ReferenciaGateway = null,
                    ChaveIdempotencia = "SEED-FATURA-003-TENTATIVA-001",
                    Valor = 99.90m,
                    Status = StatusTransacaoPagamento.Recusada,
                    CodigoErro = "PAYMENT_DECLINED",
                    MensagemErro = "Pagamento recusado no cenário de teste.",
                    DataProcessamento = new DateTime(2026, 3, 10, 12, 0, 0, DateTimeKind.Utc),
                    DataCadastro = new DateTime(2026, 3, 10, 11, 55, 0, DateTimeKind.Utc)
                },
                new TransacaoPagamento
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000004"),
                    FaturaId = Guid.Parse("50000000-0000-0000-0000-000000000004"),
                    Provedor = "MercadoPago",
                    ReferenciaGateway = null,
                    ChaveIdempotencia = "SEED-FATURA-004-TENTATIVA-001",
                    Valor = 99.90m,
                    Status = StatusTransacaoPagamento.Cancelada,
                    DataProcessamento = new DateTime(2026, 4, 10, 12, 0, 0, DateTimeKind.Utc),
                    DataCadastro = new DateTime(2026, 4, 10, 11, 55, 0, DateTimeKind.Utc)
                },
                new TransacaoPagamento
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000005"),
                    FaturaId = Guid.Parse("50000000-0000-0000-0000-000000000005"),
                    Provedor = "MercadoPago",
                    ReferenciaGateway = "TESTE-TRANSACAO-005",
                    ChaveIdempotencia = "SEED-FATURA-005-TENTATIVA-001",
                    Valor = 99.90m,
                    Status = StatusTransacaoPagamento.EmProcessamento,
                    DataProcessamento = null,
                    DataCadastro = new DateTime(2026, 5, 10, 11, 55, 0, DateTimeKind.Utc)
                },
                new TransacaoPagamento
                {
                    Id = Guid.Parse("70000000-0000-0000-0000-000000000006"),
                    FaturaId = Guid.Parse("50000000-0000-0000-0000-000000000006"),
                    Provedor = "MercadoPago",
                    ReferenciaGateway = "TESTE-TRANSACAO-006",
                    ChaveIdempotencia = "SEED-FATURA-006-TENTATIVA-001",
                    Valor = 99.90m,
                    Status = StatusTransacaoPagamento.Expirada,
                    DataProcessamento = new DateTime(2026, 6, 10, 12, 0, 0, DateTimeKind.Utc),
                    DataCadastro = new DateTime(2026, 6, 10, 11, 55, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}