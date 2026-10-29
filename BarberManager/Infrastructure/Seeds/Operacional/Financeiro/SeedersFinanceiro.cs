using Domain.Entities.Common.Enuns;
using Domain.Entities.Financeiro;
using Infrastructure.Context;
using Infrastructure.Seeds.Empresas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.Operacional.Financeiro
{
    public static class SeedersFinanceiro
    {
        public static readonly Guid FormaDinheiroId =
       Guid.Parse("F1000000-0000-0000-0000-000000000001");

        public static readonly Guid FormaPixId =
            Guid.Parse("F1000000-0000-0000-0000-000000000002");

        public static readonly Guid FormaCartaoCreditoId =
            Guid.Parse("F1000000-0000-0000-0000-000000000003");

        public static readonly Guid ContaParcialId =
            Guid.Parse("F2000000-0000-0000-0000-000000000001");

        public static readonly Guid ContaSucessivaId =
            Guid.Parse("F2000000-0000-0000-0000-000000000002");

        public static readonly Guid ContaVencidaId =
            Guid.Parse("F2000000-0000-0000-0000-000000000003");

        public static async Task InicializarAsync(
        IServiceProvider services,
        CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider
                .GetRequiredService<BarbeariaOperacionalDbContext>();

            var empresaId = SeedEmpresa.Empresa1;

            await CriarFormasPagamentoAsync(
                context,
                empresaId,
                cancellationToken);

            await CriarContasReceberAsync(
                context,
                empresaId,
                cancellationToken);
        }

        private static async Task CriarFormasPagamentoAsync(
            BarbeariaOperacionalDbContext context,
            Guid empresaId,
            CancellationToken cancellationToken)
        {
            var formas = new[]
            {
            new FormaPagamento
            {
                Id = FormaDinheiroId,
                EmpresaId = empresaId,
                Nome = "Dinheiro - Teste",
                PermiteParcelamento = false,
                GeraTroco = true,
                Ativo = true,
                Tipo = TipoFormaPagamento.Dinheiro,
                
            },
            new FormaPagamento
            {
                Id = FormaPixId,
                EmpresaId = empresaId,
                Nome = "PIX - Teste",
                PermiteParcelamento = false,
                GeraTroco = false,
                Ativo = true,
                 Tipo = TipoFormaPagamento.Pix,
            },
            new FormaPagamento
            {
                Id = FormaCartaoCreditoId,
                EmpresaId = empresaId,
                Nome = "Cartão de Crédito - Teste",
                PermiteParcelamento = true,
                GeraTroco = false,
                Ativo = true,
                Tipo = TipoFormaPagamento.CartaoCredito,
            }
        };

            var ids = formas.Select(x => x.Id).ToArray();

            var existentes = await context.FormaPagamentos
                .Where(x => ids.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            var idsExistentes = existentes.ToHashSet();

            var novasFormas = formas
                .Where(x => !idsExistentes.Contains(x.Id))
                .ToArray();

            if (novasFormas.Length == 0)
                return;

            await context.FormaPagamentos.AddRangeAsync(
                novasFormas,
                cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        private static async Task CriarContasReceberAsync(
            BarbeariaOperacionalDbContext context,
            Guid empresaId,
            CancellationToken cancellationToken)
        {
            var hoje = DateTime.Today;

            var contas = new[]
            {
            new ContaReceber(
                empresaId,
                clienteId: null,
                dataEmissao: hoje,
                dataVencimento: hoje.AddDays(15),
                valor: 100m,
                descricao: "TESTE RECEBIMENTO PARCIAL E TOTAL")
            {
                Id = ContaParcialId
            },

            new ContaReceber(
                empresaId,
                clienteId: null,
                dataEmissao: hoje,
                dataVencimento: hoje.AddDays(15),
                valor: 250m,
                descricao: "TESTE RECEBIMENTOS SUCESSIVOS")
            {
                Id = ContaSucessivaId
            },

            new ContaReceber(
                empresaId,
                clienteId: null,
                dataEmissao: hoje.AddDays(-20),
                dataVencimento: hoje.AddDays(-10),
                valor: 120m,
                descricao: "TESTE RECEBIMENTO CONTA VENCIDA")
            {
                Id = ContaVencidaId
            }
        };

            var ids = contas.Select(x => x.Id).ToArray();

            var existentes = await context.ContasReceber
                .Where(x => ids.Contains(x.Id))
                .Select(x => x.Id)
                .ToListAsync(cancellationToken);

            var idsExistentes = existentes.ToHashSet();

            var novasContas = contas
                .Where(x => !idsExistentes.Contains(x.Id))
                .ToArray();

            if (novasContas.Length == 0)
                return;

            await context.ContasReceber.AddRangeAsync(
                novasContas,
                cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
