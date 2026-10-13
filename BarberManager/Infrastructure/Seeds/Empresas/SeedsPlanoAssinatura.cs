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
    public class SeedsPlanoAssinatura
    {
        #region Ids de Planos 
        public static readonly Guid Trial =
            Guid.Parse("20000000-0000-0000-0000-000000000001");

        public static readonly Guid Starter =
            Guid.Parse("20000000-0000-0000-0000-000000000002");

        public static readonly Guid Basic =
            Guid.Parse("20000000-0000-0000-0000-000000000003");

        public static readonly Guid Professional =
            Guid.Parse("20000000-0000-0000-0000-000000000004");

        public static readonly Guid Premium =
            Guid.Parse("20000000-0000-0000-0000-000000000005");

        public static readonly Guid Enterprise =
            Guid.Parse("20000000-0000-0000-0000-000000000006");

        #endregion
        #region Ids para assinaturas 

        public static readonly Guid Assinatura1 =
            Guid.Parse("60000000-0000-0000-0000-000000000001");

        public static readonly Guid Assinatura2 =
            Guid.Parse("60000000-0000-0000-0000-000000000002");

        public static readonly Guid Assinatura3 =
            Guid.Parse("60000000-0000-0000-0000-000000000003");

        public static readonly Guid Assinatura4 =
            Guid.Parse("60000000-0000-0000-0000-000000000004");

        public static readonly Guid Assinatura5 =
            Guid.Parse("60000000-0000-0000-0000-000000000005");

        public static readonly Guid Assinatura6 =
            Guid.Parse("60000000-0000-0000-0000-000000000006");

        #endregion

        public static void InicializerSeed(ModelBuilder builder)
        {
            builder.Entity<Plano>().HasData(SetPlanos());

            builder.Entity<Assinatura>().HasData(SetAssinatura());


        }

        private static List<Assinatura> SetAssinatura()
        {
            return new List<Assinatura>
        {
            new Assinatura
            {
                Id = Assinatura1,
                EmpresaId = SeedEmpresa.Empresa1,
                PlanoId = Trial,
                DataInicio = new DateTime(2026, 1, 1),
                DataVencimento = new DateTime(2026, 1, 15),
                Trial = true,
                RenovacaoAutomatica = false,
                Ativa = StatusAssinaturaCliente.Ativa,
                DataAtualizacao = new DateTime(2026,12,15),
                DataFimTrial = new DateTime(2026,10,15),
                DiaVencimento = 15,
                Valor = 0,
                
                
            },

            new Assinatura
            {
                Id = Assinatura2,
                EmpresaId = SeedEmpresa.Empresa2,
                PlanoId = Starter,
                DataInicio = new DateTime(2026, 1, 10),
                DataVencimento = new DateTime(2027, 1, 10),
                Trial = false,
                RenovacaoAutomatica = true,
                Ativa = StatusAssinaturaCliente.Expirada,
                DataAtualizacao = new DateTime(2026,12,15),
                DataFimTrial = new DateTime(2026,10,15),
                DiaVencimento = 15,
                Valor = 0,
            },

            new Assinatura
            {
                Id = Assinatura3,
                EmpresaId = SeedEmpresa.Empresa3,
                PlanoId = Basic,
                DataInicio = new DateTime(2026, 2, 1),
                DataVencimento = new DateTime(2027, 2, 1),
                Trial = false,
                RenovacaoAutomatica = true,
                Ativa = StatusAssinaturaCliente.Ativa,
                DataAtualizacao = new DateTime(2026,12,15),
                DataFimTrial = new DateTime(2026,10,15),
                DiaVencimento = 15,
                Valor = 0,
            },

            new Assinatura
            {
                Id = Assinatura4,
                EmpresaId = SeedEmpresa.Empresa4,
                PlanoId = Professional,
                DataInicio = new DateTime(2026, 3, 1),
                DataVencimento = new DateTime(2027, 3, 1),
                Trial = false,
                RenovacaoAutomatica = true,
                Ativa = StatusAssinaturaCliente.Ativa,
                DataAtualizacao = new DateTime(2026,12,15),
                DataFimTrial = new DateTime(2026,10,15),
                DiaVencimento = 15,
                Valor = 0,
            },

            new Assinatura
            {
                Id = Assinatura5,
                EmpresaId = SeedEmpresa.Empresa5,
                PlanoId = Premium,
                DataInicio = new DateTime(2026, 4, 1),
                DataVencimento = new DateTime(2027, 4, 1),
                Trial = false,
                RenovacaoAutomatica = true,
                Ativa = StatusAssinaturaCliente.Ativa,
            },

            new Assinatura
            {
                Id = Assinatura6,
                EmpresaId = SeedEmpresa.Empresa6,
                PlanoId = Enterprise,
                DataInicio = new DateTime(2026, 5, 1),
                DataVencimento = new DateTime(2027, 5, 1),
                Trial = false,
                RenovacaoAutomatica = true,
                Ativa = StatusAssinaturaCliente.Ativa,
                DataAtualizacao = new DateTime(2026,12,15),
                DataFimTrial = new DateTime(2026,10,15),
                DiaVencimento = 15,
                Valor = 0,
            }
        };
        }

        private static List<Plano> SetPlanos()
        {
            return new List<Plano>
            {
                new Plano
                {
                    Id = Trial,
                    Nome = "Trial",
                    ValorMensal = 0,
                    LimiteProfissionais = 2,
                    LimiteEmpresas = 1,
                    Ativo = StatusGeral.Ativo,
                    ValorAnual = 0,
                },new Plano
                {
                    Id = Starter,
                    Nome = "Starter",
                    ValorMensal = 49.90m,
                    LimiteProfissionais = 3,
                    LimiteEmpresas = 1,
                    Ativo = StatusGeral.Ativo,
                    ValorAnual = 0,
                },new Plano
                {
                     Id = Basic,
                    Nome = "Basic",
                    ValorMensal = 89.90m,
                    LimiteProfissionais = 5,
                    LimiteEmpresas = 1,
                    Ativo = StatusGeral.Ativo,
                    ValorAnual = 49.90m,
                },new Plano
                {
                    Id = Professional,
                    Nome = "Professional",
                    ValorMensal = 149.90m,
                    LimiteProfissionais = 10,
                    LimiteEmpresas = 1,
                    Ativo = StatusGeral.Ativo,
                    ValorAnual = 130m,
                },new Plano
            {
                Id = Premium,
                Nome = "Premium",
                ValorMensal = 249.90m,
                LimiteProfissionais = 25,
                LimiteEmpresas = 1,
                Ativo = StatusGeral.Ativo,
                ValorAnual = 211.50m,
            },

            new Plano
            {
                Id = Enterprise,
                Nome = "Enterprise",
                ValorMensal = 499.90m,
                LimiteProfissionais = 999,
                LimiteEmpresas = 999,
                Ativo = StatusGeral.Ativo,
                ValorAnual = 480.65m,
            }
           };
        }
    }
}
