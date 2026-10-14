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
    public static class SeedFatura
    {
        public static void InicializerSeed(ModelBuilder modelBuilder)
        {
            // IMPORTANTE:
            // Substitua pelo ID FIXO de uma Assinatura
            // já cadastrada no Seed de Assinatura.
            var assinaturaId = SeedsPlanoAssinatura.Assinatura1;

            modelBuilder.Entity<Fatura>().HasData(
                new Fatura
                {
                    Id = Guid.Parse(
                        "50000000-0000-0000-0000-000000000001"
                    ),
                    AssinaturaId = assinaturaId,
                    Valor = 99.90m,
                    PeriodoInicio = new DateTime(2026, 1, 1),
                    PeriodoFim = new DateTime(2026, 1, 31),
                    Vencimento = new DateTime(2026, 1, 10),
                    DataPagamento = new DateTime(2026, 1, 8),
                    Status = StatusFatura.Paga,
                    ReferenciaGateway = "TESTE-FATURA-001"
                },
                new Fatura
                {
                    Id = Guid.Parse(
                        "50000000-0000-0000-0000-000000000002"
                    ),
                    AssinaturaId = assinaturaId,
                    Valor = 99.90m,
                    PeriodoInicio = new DateTime(2026, 2, 1),
                    PeriodoFim = new DateTime(2026, 2, 28),
                    Vencimento = new DateTime(2026, 2, 10),
                    DataPagamento = new DateTime(2026, 2, 9),
                    Status = StatusFatura.Paga,
                    ReferenciaGateway = "TESTE-FATURA-002"
                },
                new Fatura
                {
                    Id = Guid.Parse(
                        "50000000-0000-0000-0000-000000000003"
                    ),
                    AssinaturaId = assinaturaId,
                    Valor = 99.90m,
                    PeriodoInicio = new DateTime(2026, 3, 1),
                    PeriodoFim = new DateTime(2026, 3, 31),
                    Vencimento = new DateTime(2026, 3, 10),
                    DataPagamento = null,
                    Status = StatusFatura.Vencida,
                    ReferenciaGateway = null
                },
                new Fatura
                {
                    Id = Guid.Parse(
                        "50000000-0000-0000-0000-000000000004"
                    ),
                    AssinaturaId = assinaturaId,
                    Valor = 99.90m,
                    PeriodoInicio = new DateTime(2026, 4, 1),
                    PeriodoFim = new DateTime(2026, 4, 30),
                    Vencimento = new DateTime(2026, 4, 10),
                    DataPagamento = null,
                    Status = StatusFatura.Cancelada,
                    ReferenciaGateway = null
                },
                new Fatura
                {
                    Id = Guid.Parse(
                        "50000000-0000-0000-0000-000000000005"
                    ),
                    AssinaturaId = assinaturaId,
                    Valor = 99.90m,
                    PeriodoInicio = new DateTime(2026, 5, 1),
                    PeriodoFim = new DateTime(2026, 5, 31),
                    Vencimento = new DateTime(2026, 5, 10),
                    DataPagamento = null,
                    Status = StatusFatura.EmProcessamento,
                    ReferenciaGateway = "TESTE-FATURA-005"
                },
                new Fatura
                {
                    Id = Guid.Parse(
                        "50000000-0000-0000-0000-000000000006"
                    ),
                    AssinaturaId = assinaturaId,
                    Valor = 99.90m,
                    PeriodoInicio = new DateTime(2026, 6, 1),
                    PeriodoFim = new DateTime(2026, 6, 30),
                    Vencimento = new DateTime(2026, 6, 10),
                    DataPagamento = null,
                    Status = StatusFatura.FalhaPagamento,
                    ReferenciaGateway = "TESTE-FATURA-006"
                }
            );
        }
    }
}
