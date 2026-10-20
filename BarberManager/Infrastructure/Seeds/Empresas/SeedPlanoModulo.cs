using Domain.Entities.Saas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.Empresas
{
    public static class SeedPlanoModulo
    {
        public static void InicializerSeed(ModelBuilder builder)
        {
            var modulosBasicos = new[]
            {
            SeedModulos.Clientes,
            SeedModulos.Profissionais,
            SeedModulos.Servicos,
            SeedModulos.Agenda,
            SeedModulos.Configuracoes
        };

            var modulosBasic = modulosBasicos
                .Concat(new[]
                {
                SeedModulos.Financeiro,
                SeedModulos.Vendas
                }).ToArray();

            var modulosProfessional = modulosBasic
                .Concat(new[]
                {
                SeedModulos.Estoque,
                SeedModulos.Compras,
                SeedModulos.Comissoes,
                SeedModulos.Relatorios
                }).ToArray();

            var modulosPremium = modulosProfessional
                .Concat(new[]
                {
                SeedModulos.Fidelidade,
                SeedModulos.Avaliacoes
                }).ToArray();

            var modulosEnterprise = modulosPremium;

            var dados = new List<PlanoModulo>();

            Adicionar(dados, SeedsPlanoAssinatura.Trial, modulosBasicos);
            Adicionar(dados, SeedsPlanoAssinatura.Starter, modulosBasicos);
            Adicionar(dados, SeedsPlanoAssinatura.Basic, modulosBasic);
            Adicionar(dados, SeedsPlanoAssinatura.Professional, modulosProfessional);
            Adicionar(dados, SeedsPlanoAssinatura.Premium, modulosPremium);
            Adicionar(dados, SeedsPlanoAssinatura.Enterprise, modulosEnterprise);

            builder.Entity<PlanoModulo>().HasData(dados);
        }

        private static void Adicionar(
            ICollection<PlanoModulo> destino,
            Guid planoId,
            IEnumerable<Guid> moduloIds)
        {
            foreach (var moduloId in moduloIds)
            {
                destino.Add(new PlanoModulo
                {
                    PlanoId = planoId,
                    ModuloId = moduloId
                });
            }
        }
    }
}
 
