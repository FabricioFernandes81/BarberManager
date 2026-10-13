using Domain.Entities.Assinaturas;
using Domain.Entities.Common.Enuns;
using Infrastructure.Seeds.Empresas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.Operacional
{
    public static class SeedPlanosAssinatura
    {
        // IDs fixos para manter os seeds estáveis entre migrations.
        public static readonly Guid PlanoCorteMensal =
            Guid.Parse("A1000000-0000-0000-0000-000000000001");

        public static readonly Guid PlanoCorteBarba =
            Guid.Parse("A1000000-0000-0000-0000-000000000002");

        public static readonly Guid PlanoFamilia =
            Guid.Parse("A1000000-0000-0000-0000-000000000003");

        public static readonly Guid PlanoVip =
            Guid.Parse("A1000000-0000-0000-0000-000000000004");

        public static readonly Guid BeneficioDescontoProduto =
            Guid.Parse("A2000000-0000-0000-0000-000000000001");

        public static readonly Guid BeneficioCortesia =
            Guid.Parse("A2000000-0000-0000-0000-000000000002");

        public static void InicializarSeed(ModelBuilder builder)
        {
            builder.Entity<PlanoAssinatura>()
                .HasData(SetPlanos());

            builder.Entity<BeneficioPlano>()
                .HasData(SetBeneficios());
        }

        private static List<PlanoAssinatura> SetPlanos()
        {
            var dataCadastro = new DateTime(
                2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return new List<PlanoAssinatura>
        {
            new()
            {
                Id = PlanoCorteMensal,
                EmpresaId = SeedEmpresa.Empresa1,
                Nome = "Corte Mensal",
                Descricao = "Plano de teste com até quatro cortes por ciclo.",
                Valor = 99.90m,
                Periodicidade = PeriodicidadeAssinatura.Mensal,
                QuantidadeCiclos = 1,
                Ativo = StatusGeral.Ativo,
                DisponivelOnline = true,
                DataCadastro = dataCadastro
            },
            new()
            {
                Id = PlanoCorteBarba,
                EmpresaId = SeedEmpresa.Empresa1,
                Nome = "Corte e Barba",
                Descricao = "Plano de teste para corte e barba recorrentes.",
                Valor = 179.90m,
                Periodicidade = PeriodicidadeAssinatura.Mensal,
                QuantidadeCiclos = 1,
                Ativo = StatusGeral.Ativo,
                DisponivelOnline = true,
                DataCadastro = dataCadastro
            },
            new()
            {
                Id = PlanoFamilia,
                EmpresaId = SeedEmpresa.Empresa1,
                Nome = "Plano Família",
                Descricao = "Plano demonstrativo para contratação familiar.",
                Valor = 249.90m,
                Periodicidade = PeriodicidadeAssinatura.Mensal,
                QuantidadeCiclos = 1,
                Ativo = StatusGeral.Ativo,
                DisponivelOnline = true,
                DataCadastro = dataCadastro
            },
            new()
            {
                Id = PlanoVip,
                EmpresaId = SeedEmpresa.Empresa1,
                Nome = "Clube VIP",
                Descricao = "Plano demonstrativo com benefícios exclusivos.",
                Valor = 299.90m,
                Periodicidade = PeriodicidadeAssinatura.Mensal,
                QuantidadeCiclos = 1,
                Ativo = StatusGeral.Ativo,
                DisponivelOnline = true,
                DataCadastro = dataCadastro
            }
        };
        }

        private static List<BeneficioPlano> SetBeneficios()
        {
            var dataCadastro = new DateTime(
                2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return new List<BeneficioPlano>
        {
            new()
            {
                Id = BeneficioDescontoProduto,
                EmpresaId = SeedEmpresa.Empresa1,
                PlanoAssinaturaId = PlanoVip,
                Nome = "Desconto em cosméticos",
                Descricao = "Desconto demonstrativo de 10% em produtos elegíveis.",
                Tipo = TipoBeneficioPlano.DescontoPercentual,
                Valor = 10m,
                Ativo = true,
                DataCadastro = dataCadastro
            },
            new()
            {
                Id = BeneficioCortesia,
                EmpresaId = SeedEmpresa.Empresa1,
                PlanoAssinaturaId = PlanoVip,
                Nome = "Café de cortesia",
                Descricao = "Uma cortesia conforme as regras da barbearia.",
                Tipo = TipoBeneficioPlano.Cortesia,
                Valor = null,
                Ativo = true,
                DataCadastro = dataCadastro
            }
        };
        }
    }
}
