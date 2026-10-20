using Domain.Entities.Saas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.Empresas
{
    public static class SeedEmpresaModulo
    {
        public static void InicializerSeed(ModelBuilder builder)
        {
            var basicos = new[]
            {
            SeedModulos.Clientes,
            SeedModulos.Profissionais,
            SeedModulos.Servicos,
            SeedModulos.Agenda,
            SeedModulos.Configuracoes
        };

            var basic = basicos.Concat(new[]
            {
            SeedModulos.Financeiro,
            SeedModulos.Vendas
        }).ToArray();

            var professional = basic.Concat(new[]
            {
            SeedModulos.Estoque,
            SeedModulos.Compras,
            SeedModulos.Comissoes,
            SeedModulos.Relatorios
        }).ToArray();

            var premium = professional.Concat(new[]
            {
            SeedModulos.Fidelidade,
            SeedModulos.Avaliacoes
        }).ToArray();

            var dados = new List<EmpresaModulo>();

            Adicionar(dados, SeedEmpresa.Empresa1, basicos);
            Adicionar(dados, SeedEmpresa.Empresa2, basicos);
            Adicionar(dados, SeedEmpresa.Empresa3, basic);
            Adicionar(dados, SeedEmpresa.Empresa4, professional);
            Adicionar(dados, SeedEmpresa.Empresa5, premium);
            Adicionar(dados, SeedEmpresa.Empresa6, premium);

            builder.Entity<EmpresaModulo>().HasData(dados);
        }

        private static void Adicionar(
            ICollection<EmpresaModulo> destino,
            Guid empresaId,
            IEnumerable<Guid> moduloIds)
        {
            foreach (var moduloId in moduloIds)
            {
                destino.Add(new EmpresaModulo
                {
                    EmpresaId = empresaId,
                    ModuloId = moduloId
                });
            }
        }
    }
}
