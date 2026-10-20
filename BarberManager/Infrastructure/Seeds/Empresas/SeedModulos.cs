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
    public static class SeedModulos
    {

        private static readonly DateTime DataSeed =
         new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static readonly Guid Clientes =
            Guid.Parse("70000000-0000-0000-0000-000000000001");

        public static readonly Guid Profissionais =
            Guid.Parse("70000000-0000-0000-0000-000000000002");

        public static readonly Guid Servicos =
            Guid.Parse("70000000-0000-0000-0000-000000000003");

        public static readonly Guid Agenda =
            Guid.Parse("70000000-0000-0000-0000-000000000004");

        public static readonly Guid Financeiro =
            Guid.Parse("70000000-0000-0000-0000-000000000005");

        public static readonly Guid Vendas =
            Guid.Parse("70000000-0000-0000-0000-000000000006");

        public static readonly Guid Estoque =
            Guid.Parse("70000000-0000-0000-0000-000000000007");

        public static readonly Guid Compras =
            Guid.Parse("70000000-0000-0000-0000-000000000008");

        public static readonly Guid Comissoes =
            Guid.Parse("70000000-0000-0000-0000-000000000009");

        public static readonly Guid Fidelidade =
            Guid.Parse("70000000-0000-0000-0000-000000000010");

        public static readonly Guid Avaliacoes =
            Guid.Parse("70000000-0000-0000-0000-000000000011");

        public static readonly Guid Relatorios =
            Guid.Parse("70000000-0000-0000-0000-000000000012");

        public static readonly Guid Configuracoes =
            Guid.Parse("70000000-0000-0000-0000-000000000013");

        public static void InicializerSeed(ModelBuilder builder)
        {
            builder.Entity<Modulo>().HasData(
                new Modulo
                {
                    Id = Clientes,
                    Nome = "Clientes",
                    Codigo = "CLIENTES",
                    Descricao = "Cadastro e gestão de clientes.",
                    Ativo = StatusGeral.Ativo,
                    DataCadastro = DataSeed
                },
                new Modulo
                {
                    Id = Profissionais,
                    Nome = "Profissionais",
                    Codigo = "PROFISSIONAIS",
                    Descricao = "Cadastro e gestão de profissionais.",
                    Ativo = StatusGeral.Ativo,
                    DataCadastro = DataSeed
                },
                new Modulo
                {
                    Id = Servicos,
                    Nome = "Serviços",
                    Codigo = "SERVICOS",
                    Descricao = "Catálogo de serviços e preços.",
                    Ativo = StatusGeral.Ativo,
                    DataCadastro = DataSeed
                },
                new Modulo
                {
                    Id = Agenda,
                    Nome = "Agenda",
                    Codigo = "AGENDA",
                    Descricao = "Agendamentos e disponibilidade.",
                    Ativo = StatusGeral.Ativo,
                    DataCadastro = DataSeed
                },
                new Modulo
                {
                    Id = Financeiro,
                    Nome = "Financeiro",
                    Codigo = "FINANCEIRO",
                    Descricao = "Gestão financeira da barbearia.",
                    Ativo = StatusGeral.Ativo,
                    DataCadastro = DataSeed
                },
                new Modulo
                {
                    Id = Vendas,
                    Nome = "Vendas",
                    Codigo = "VENDAS",
                    Descricao = "Vendas de serviços e produtos.",
                    Ativo = StatusGeral.Ativo,
                    DataCadastro = DataSeed
                },
                new Modulo
                {
                    Id = Estoque,
                    Nome = "Estoque",
                    Codigo = "ESTOQUE",
                    Descricao = "Controle de estoque e movimentações.",
                    Ativo = StatusGeral.Ativo,
                    DataCadastro = DataSeed
                },
                new Modulo
                {
                    Id = Compras,
                    Nome = "Compras",
                    Codigo = "COMPRAS",
                    Descricao = "Gestão de compras e fornecedores.",
                    Ativo = StatusGeral.Ativo,
                    DataCadastro = DataSeed
                },
                new Modulo
                {
                    Id = Comissoes,
                    Nome = "Comissões",
                    Codigo = "COMISSOES",
                    Descricao = "Apuração de comissões dos profissionais.",
                    Ativo = StatusGeral.Ativo,
                    DataCadastro = DataSeed
                },
                new Modulo
                {
                    Id = Fidelidade,
                    Nome = "Fidelidade",
                    Codigo = "FIDELIDADE",
                    Descricao = "Programas de fidelidade e benefícios.",
                    Ativo = StatusGeral.Ativo,
                    DataCadastro = DataSeed
                },
                new Modulo
                {
                    Id = Avaliacoes,
                    Nome = "Avaliações",
                    Codigo = "AVALIACOES",
                    Descricao = "Avaliações e feedback dos clientes.",
                    Ativo = StatusGeral.Ativo,
                    DataCadastro = DataSeed
                },
                new Modulo
                {
                    Id = Relatorios,
                    Nome = "Relatórios",
                    Codigo = "RELATORIOS",
                    Descricao = "Relatórios gerenciais.",
                    Ativo = StatusGeral.Ativo,
                    DataCadastro = DataSeed
                },
                new Modulo
                {
                    Id = Configuracoes,
                    Nome = "Configurações",
                    Codigo = "CONFIGURACOES",
                    Descricao = "Configurações operacionais da barbearia.",
                    Ativo = StatusGeral.Ativo,
                    DataCadastro = DataSeed
                }
            );
        }
    }
}
