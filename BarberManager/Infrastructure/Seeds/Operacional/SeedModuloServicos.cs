using Domain.Entities.Common.Enuns;
using Domain.Entities.Servicos;
using Infrastructure.Seeds.Empresas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.Operacional
{
    public static class SeedModuloServicos
    {
        public static readonly Guid CategoriaServico1 =
           Guid.Parse("22222222-2222-2222-2222-222222222222");
        public static readonly Guid CategoriaServico2 =
           Guid.Parse("33333333-3333-3333-3333-333333333333");

        public static readonly Guid CategoriaServico3 =
           Guid.Parse("44444444-4444-4444-4444-444444444444");


        public static readonly Guid Servico1 =
           Guid.Parse("11111111-1111-1111-1111-111111111111");

        public static readonly Guid Servico2 =
           Guid.Parse("11111111-1111-1111-1111-111111111112");

        public static readonly Guid Servico3 =
           Guid.Parse("11111111-1111-1111-1111-111111111113");

        public static readonly Guid Servico4 =
           Guid.Parse("11111111-1111-1111-1111-111111111114");

        public static readonly Guid Servico5 =
          Guid.Parse("11111111-1111-1111-1111-111111111115");

        public static readonly Guid Servico6 =
          Guid.Parse("11111111-1111-1111-1111-111111111116");

        public static readonly Guid Servico7 =
          Guid.Parse("11111111-1111-1111-1111-111111111117");

        public static readonly Guid Servico8 =
          Guid.Parse("11111111-1111-1111-1111-111111111118");
        public static readonly Guid Servico9 =
         Guid.Parse("11111111-1111-1111-1111-111111111119");


        public static void SeedIniciarServico(ModelBuilder builder)
        {
            builder.Entity<CategoriaServico>().HasData(SetCategoriasServico());
            builder.Entity<Servico>().HasData(SetServicos());
            builder.Entity<ProfissionalServicos>().HasData(SetProfissionalServico());

        }

        private static List<ProfissionalServicos> SetProfissionalServico()
        {
            return new List<ProfissionalServicos>
            {
                new ProfissionalServicos
                {
                    Id = Guid.NewGuid(),
                    ProfissionalId = SeedProfissionais.Profissional1,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ServicoId = Servico1,
                    ValorPersonalizado = 50,
                    DuracaoPersonalizada = 25,
                    Ativo = StatusGeral.Ativo
                },new ProfissionalServicos
                {
                    Id = Guid.NewGuid(),
                    ProfissionalId = SeedProfissionais.Profissional1,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ServicoId = Servico2,
                    ValorPersonalizado = 70,
                    DuracaoPersonalizada = 35,
                    Ativo = StatusGeral.Ativo
                },new ProfissionalServicos
                {
                    Id = Guid.NewGuid(),
                    ProfissionalId = SeedProfissionais.Profissional1,
                    ServicoId = Servico3,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ValorPersonalizado = 80,
                    DuracaoPersonalizada = 40,
                    Ativo = StatusGeral.Inativo
                },new ProfissionalServicos
                {
                    Id = Guid.NewGuid(),
                    ProfissionalId = SeedProfissionais.Profissional2,
                    ServicoId = Servico4,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ValorPersonalizado = 90,
                    DuracaoPersonalizada = 45,
                    Ativo = StatusGeral.Inativo,
                },new ProfissionalServicos
                {
                    Id = Guid.NewGuid(),
                    ProfissionalId = SeedProfissionais.Profissional2,
                    ServicoId = Servico5,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ValorPersonalizado = 90,
                    DuracaoPersonalizada = 45,
                    Ativo = StatusGeral.Inativo,
                },new ProfissionalServicos
                {
                    Id = Guid.NewGuid(),
                    ProfissionalId = SeedProfissionais.Profissional3,
                    ServicoId = Servico5,
                    ValorPersonalizado = 90,
                    EmpresaId = SeedEmpresa.Empresa1,
                    DuracaoPersonalizada = 45,
                    Ativo = StatusGeral.Inativo,
                },new ProfissionalServicos
                {
                    Id = Guid.NewGuid(),
                    ProfissionalId = SeedProfissionais.Profissional3,
                    ServicoId = Servico6,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ValorPersonalizado = 90,
                    DuracaoPersonalizada = 45,
                    Ativo = StatusGeral.Inativo,
                },//
                 new ProfissionalServicos
                {
                    Id = Guid.NewGuid(),
                    ProfissionalId = SeedProfissionais.Profissional11,
                    ServicoId = Servico1,
                    EmpresaId = SeedEmpresa.Empresa1,
                    ValorPersonalizado = 50,
                    DuracaoPersonalizada = 10,
                    Ativo = StatusGeral.Inativo
                }
            };
        }

        private static List<CategoriaServico> SetCategoriasServico()
        {
            return new List<CategoriaServico>
            {
                new CategoriaServico
                {
                    Id = CategoriaServico1,
                    Nome = "Cabelo",
                    Ativo = StatusGeral.Ativo,
                    Descricao = "Serviço de corte de cabelo",
                    Ordem = 1,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Icone = "content_cut",
                    Cor = "#FF5733",

                },
                new CategoriaServico
                {
                    Id = CategoriaServico2,
                    Nome = "Mão e Pés",
                    Ativo = StatusGeral.Ativo,
                    Descricao = "Serviço de categoria 2",
                    Ordem = 2,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Icone = "icone-categoria2",
                    Cor = "#33FF57"
                },new CategoriaServico
                {
                    Id = CategoriaServico3,
                    Nome = "Barba e Bigode",
                    Ativo = StatusGeral.Ativo,
                    Descricao = "Serviço de categoria 3",
                    Ordem = 3,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Icone = "icone-categoria3",
                    Cor = "#3357FF"
                }
            };
        }

        private static List<Servico> SetServicos()
        {
            return new List<Servico>
            {
                new Servico
                {
                    Id = Servico1,
                    Nome = "Corte de Cabelo",
                    PermiteAgendamentoOnline = true,
                    Ativo = StatusGeral.Ativo,
                    DuracaoMinutos = 30,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Custo = 1,
                    Descricao = "Serviço de corte de cabelo",
                    IntervaloMinutos = 5,
                    Valor = 60,
                    CategoriaServicoId = CategoriaServico1,
                    ExibirAplicativo = true,
                    PercentualComissao = 10,
                    TipoComissao = TipoComissaoServico.Percentual,
                    

                },
                new Servico
                {
                    Id = Servico2,
                    Nome = "Cabelo e Barba",
                    PermiteAgendamentoOnline = true,
                    Ativo = StatusGeral.Ativo,
                   
                    DuracaoMinutos = 60,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Custo = 1,
                    Descricao = "Serviço de corte de cabelo",
                    IntervaloMinutos = 5,
                    Valor = 60,
                    CategoriaServicoId = CategoriaServico1,
                    ExibirAplicativo = true,
                    PercentualComissao = 10,
                    TipoComissao = TipoComissaoServico.Percentual,

                },
                new Servico
                {
                    Id = Servico3,
                    Nome = "Botox Capilar",
                    PermiteAgendamentoOnline = true,
                    Ativo = StatusGeral.Ativo,
                    DuracaoMinutos = 30,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Custo = 1,
                    Descricao = "Serviço de corte de cabelo",
                    IntervaloMinutos = 5,
                    Valor = 60,
                    CategoriaServicoId = CategoriaServico1,
                    ExibirAplicativo = true,
                    PercentualComissao = 10,
                    TipoComissao = TipoComissaoServico.Percentual,

                },
                new Servico
                {
                    Id = Servico4,
                    Nome = "Corte + Barba + Botox Capilar",
                    PermiteAgendamentoOnline = true,
                    Ativo = StatusGeral.Ativo,
                    
                    DuracaoMinutos = 30,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Custo = 1,
                    Descricao = "Serviço de corte de cabelo",
                    IntervaloMinutos = 5,
                    Valor = 60,
                    CategoriaServicoId = CategoriaServico1,
                    ExibirAplicativo = true,
                    PercentualComissao = 10,
                    TipoComissao = TipoComissaoServico.Percentual,

                },new Servico
                {
                    Id = Servico5,
                    Nome = "Esmaltação - Francesinha",
                    PermiteAgendamentoOnline = true,
                    Ativo = StatusGeral.Ativo,
                    DuracaoMinutos = 30,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Custo = 1,
                    Descricao = "Serviço de corte de cabelo",
                    IntervaloMinutos = 5,
                    Valor = 60,
                    CategoriaServicoId = CategoriaServico3,
                    ExibirAplicativo = true,
                    PercentualComissao = 10,
                    TipoComissao = TipoComissaoServico.Percentual,

                },new Servico
                {
                    Id = Servico6,
                    Nome = "Esmaltação - Mãos",
                    PermiteAgendamentoOnline = true,
                    Ativo = StatusGeral.Ativo,
                    DuracaoMinutos = 30,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Custo = 1,
                    Descricao = "Serviço de corte de cabelo",
                    IntervaloMinutos = 5,
                    Valor = 60,
                    CategoriaServicoId = CategoriaServico2,
                    ExibirAplicativo = true,
                    PercentualComissao = 10,
                    TipoComissao = TipoComissaoServico.Percentual,

                },new Servico
                {
                    Id = Servico7,
                    Nome = "Esmaltação - Mãos",
                    PermiteAgendamentoOnline = true,
                    Ativo = StatusGeral.Suspenso,
                    
                    DuracaoMinutos = 30,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Custo = 1,
                    Descricao = "Serviço de corte de cabelo",
                    IntervaloMinutos = 5,
                    Valor = 60,
                    CategoriaServicoId = CategoriaServico2,
                    ExibirAplicativo = true,
                    PercentualComissao = 10,
                    TipoComissao = TipoComissaoServico.Percentual,

                },new Servico
                {
                    Id = Servico8,
                    Nome = "Esmaltação - Mãos",
                    PermiteAgendamentoOnline = true,
                    Ativo = StatusGeral.Ativo,
                    
                    DuracaoMinutos = 30,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Custo = 1,
                    Descricao = "Serviço de corte de cabelo",
                    IntervaloMinutos = 5,
                    Valor = 60,
                    CategoriaServicoId = CategoriaServico2,
                    ExibirAplicativo = true,
                    PercentualComissao = 10,
                    TipoComissao = TipoComissaoServico.Percentual,

                },new Servico
                {
                    Id = Servico9,
                    Nome = "Esmaltação - Mãos",
                    PermiteAgendamentoOnline = true,
                    Ativo = StatusGeral.Suspenso,
                    DuracaoMinutos = 30,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Custo = 1,
                    Descricao = "Serviço de corte de cabelo",
                    IntervaloMinutos = 5,
                    Valor = 60,
                    CategoriaServicoId = CategoriaServico2,
                    ExibirAplicativo = true,
                    PercentualComissao = 10,
                    TipoComissao = TipoComissaoServico.Percentual,

                },
            };
        }


    }
}
