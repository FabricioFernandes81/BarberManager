using Domain.Entities.Common.Enuns;
using Domain.Entities.Profissionais;
using Infrastructure.Seeds.Empresas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.Operacional
{
    public static class SeedProfissionais
    {
        public static readonly Guid Profissional1 =
            Guid.Parse("91000000-0000-0000-0000-000000000001");
        public static readonly Guid Profissional2 =
            Guid.Parse("91000000-0000-0000-0000-000000000002");
        public static readonly Guid Profissional3 =
            Guid.Parse("91000000-0000-0000-0000-000000000003");
        public static readonly Guid Profissional4 =
            Guid.Parse("91000000-0000-0000-0000-000000000004");
        public static readonly Guid Profissional5 =
           Guid.Parse("91000000-0000-0000-0000-000000000005");
        public static readonly Guid Profissional6 =
           Guid.Parse("91000000-0000-0000-0000-000000000006");
        public static readonly Guid Profissional7 =
           Guid.Parse("91000000-0000-0000-0000-000000000007");

        public static readonly Guid Profissional8 =
           Guid.Parse("91000000-0000-0000-0000-000000000008");

        public static readonly Guid Profissional9 =
          Guid.Parse("91000000-0000-0000-0000-000000000009");

        public static readonly Guid Profissional10 =
          Guid.Parse("91000000-0000-0000-0000-000000000010");

        public static readonly Guid Profissional11 =
         Guid.Parse("91000000-0000-0000-0000-000000000011");

        public static void SeedProfissial(ModelBuilder builder)
        {
            builder.Entity<Profissional>().HasData(SetProfissionais());
        }
        private static List<Profissional> SetProfissionais()
        {
            return new List<Profissional>
            {
                new Profissional
                {
                    Id = Profissional1,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Carlos Henrique",
                    Email = "carlos.henrique@example.com",
                    Cpf = "123 ",
                    ValorTipoPagamento = 10.0m,
                    PermiteEncaixe = true,
                    RecebePagamentoDireto = true,
                    AgendaOnline = true,
                    Celular = "11999999999",
                    Cargo = "Barbeiro",
                    Matricula = "123456",
                    Especialidade = "Cortes de cabelo e barba",
                    AvaliacaoMedia = 4.5m,
                    DataNascimento = new DateTime(1990, 5, 15),
                    Destaque = true,
                    IntervaloAtendimento = 30,
                    Status = StatusProfissional.Ativo,
                    ExibirAgendamentoOnline = true,
                    Sexo = EnumSexo.Masculino,
                    Telefone = "51515151515",
                    Rg = "22222222222",
                    OrdemAgenda = 1,
                    Sobrenome = "ffffffffffff",
                    TipoComissao = TipoPagamento.ValorFixo,
                  //  UsuarioOperacionalId = SeedUserOperacional.UsuarioAdministrador


                },new Profissional
                {
                    Id = Profissional2,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Alexandre =",
                    Sobrenome = "Fernandes",
                    Email = "alexandre.fernandes@example.com",
                    Cpf = "123 ",
                    ValorTipoPagamento = 10.0m,
                    PermiteEncaixe = true,
                    RecebePagamentoDireto = true,
                    AgendaOnline = true,
                    Celular = "11999999999",
                    Cargo = "Barbeiro",
                    Matricula = "123456",
                    Especialidade = "Cortes de cabelo e barba",
                    AvaliacaoMedia = 4.5m,
                    DataNascimento = new DateTime(1990, 5, 15),
                    Destaque = true,
                    IntervaloAtendimento = 30,
                    Status = StatusProfissional.Ativo,
                    ExibirAgendamentoOnline = true,
                    Sexo = EnumSexo.Masculino,
                    Telefone = "51515151515",
                    Rg = "22222222222",
                    OrdemAgenda = 1,
                    TipoComissao = TipoPagamento.ValorFixo,
                   // UsuarioOperacionalId = SeedUserOperacional.UsuarioBarbeiro,

                },new Profissional
                {
                    Id = Profissional3,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Roberto Almeida",
                    Sobrenome = "Fernandes",
                    Email = "alexandre.fernandes@example.com",
                    Cpf = "123 ",
                    ValorTipoPagamento = 10.0m,
                    PermiteEncaixe = true,
                    RecebePagamentoDireto = true,
                    AgendaOnline = true,
                    Celular = "11999999999",
                    Cargo = "Administrador",
                    Matricula = "0000000",
                    Especialidade = "Cortes de cabelo e barba",
                    AvaliacaoMedia = 4.5m,
                    DataNascimento = new DateTime(1990, 5, 15),
                    Destaque = true,
                    IntervaloAtendimento = 30,
                    Status = StatusProfissional.Ativo,
                    ExibirAgendamentoOnline = true,
                    Sexo = EnumSexo.Masculino,
                    Telefone = "51515151515",
                    Rg = "22222222222",
                    OrdemAgenda = 1,
                    TipoComissao = TipoPagamento.Nenhuma,
                  //  UsuarioOperacionalId = SeedUserOperacional.UsuarioAdministrador2
                },new Profissional
                {
                    Id = Profissional4,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Bete Carvalho",
                    Sobrenome = "Faaaaaaaaa",
                    Email = "alexandre.fernandes@example.com",
                    Cpf = "123 ",
                    ValorTipoPagamento = 10.0m,
                    PermiteEncaixe = true,
                    RecebePagamentoDireto = true,
                    AgendaOnline = true,
                    Celular = "11999999999",
                    Cargo = "Recepcionista",
                    Matricula = "123456",
                    Especialidade = "Cortes de cabelo e barba",
                    AvaliacaoMedia = 4.5m,
                    DataNascimento = new DateTime(1990, 5, 15),
                    Destaque = true,
                    IntervaloAtendimento = 30,
                    Status = StatusProfissional.Ativo,
                    ExibirAgendamentoOnline = true,
                    Sexo = EnumSexo.Masculino,
                    Telefone = "51515151515",
                    Rg = "22222222222",
                    OrdemAgenda = 1,
                    TipoComissao = TipoPagamento.Nenhuma,
                 //   UsuarioOperacionalId = SeedUserOperacional.UsuarioRecepcionista
                },new Profissional
                {
                    Id = Profissional5,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Alexandre11111111 = aaaaa",
                    Sobrenome = "Fernandes",
                    Email = "alexandre.fernandes@example.com",
                    Cpf = "123 ",
                    ValorTipoPagamento = 10.0m,
                    PermiteEncaixe = true,
                    RecebePagamentoDireto = true,
                    AgendaOnline = true,
                    Celular = "11999999999",
                    Cargo = "Caixa",
                    Matricula = "123456",
                    Especialidade = "Cortes de cabelo e barba",
                    AvaliacaoMedia = 4.5m,
                    DataNascimento = new DateTime(1990, 5, 15),
                    Destaque = true,
                    IntervaloAtendimento = 30,
                    Status = StatusProfissional.Ativo,
                    ExibirAgendamentoOnline = true,
                    Sexo = EnumSexo.Masculino,
                    Telefone = "51515151515",
                    Rg = "22222222222",
                    OrdemAgenda = 1,
                    TipoComissao = TipoPagamento.ValorFixo,
                 //   UsuarioOperacionalId = SeedUserOperacional.UsuarioCaixa,

                },new Profissional
                {
                    Id = Profissional6,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Agenor Rodrigues",
                    Sobrenome = "Fernandes",
                    Email = "alexandre.fernandes@example.com",
                    Cpf = "123 ",
                    ValorTipoPagamento = 10.0m,
                    PermiteEncaixe = true,
                    RecebePagamentoDireto = true,
                    AgendaOnline = true,
                    Celular = "11999999999",
                    Cargo = "Estoquita",
                    Matricula = "123456",
                    Especialidade = "Cortes de cabelo e barba",
                    AvaliacaoMedia = 4.5m,
                    DataNascimento = new DateTime(1990, 5, 15),
                    Destaque = true,
                    IntervaloAtendimento = 30,
                    Status = StatusProfissional.Ativo,
                    ExibirAgendamentoOnline = true,
                    Sexo = EnumSexo.Masculino,
                    Telefone = "51515151515",
                    Rg = "22222222222",
                    OrdemAgenda = 1,
                    TipoComissao = TipoPagamento.Nenhuma,
                  //  UsuarioOperacionalId = SeedUserOperacional.UsuarioEstoquista
                },new Profissional
                {
                    Id = Profissional7,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Paulo Silva",
                    Sobrenome = "a",
                    Email = "alexandre.fernandes@example.com",
                    Cpf = "123 ",
                    ValorTipoPagamento = 10.0m,
                    PermiteEncaixe = true,
                    RecebePagamentoDireto = true,
                    AgendaOnline = true,
                    Celular = "11999999999",
                    Cargo = "Financeiro",
                    Matricula = "123456",
                    Especialidade = "Cortes de cabelo e barba",
                    AvaliacaoMedia = 4.5m,
                    DataNascimento = new DateTime(1990, 5, 15),
                    Destaque = true,
                    IntervaloAtendimento = 30,
                    Status = StatusProfissional.Ativo,
                    ExibirAgendamentoOnline = true,
                    Sexo = EnumSexo.Masculino,
                    Telefone = "51515151515",
                    Rg = "22222222222",
                    OrdemAgenda = 1,
                    TipoComissao = TipoPagamento.ValorFixo,
                  //  UsuarioOperacionalId = SeedUserOperacional.UsuarioFinanceiro
                },new Profissional
                {
                    Id = Profissional8,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Paula Mota",
                    Sobrenome = "Fernandes",
                    Email = "alexandre.fernandes@example.com",
                    Cpf = "123 ",
                    ValorTipoPagamento = 10.0m,
                    PermiteEncaixe = true,
                    RecebePagamentoDireto = true,
                    AgendaOnline = true,
                    Celular = "11999999999",
                    Cargo = "Barbeiro",
                    Matricula = "123456",
                    Especialidade = "Cortes de cabelo e barba",
                    AvaliacaoMedia = 4.5m,
                    DataNascimento = new DateTime(1990, 5, 15),
                    Destaque = true,
                    IntervaloAtendimento = 30,
                    Status = StatusProfissional.Ativo,
                    ExibirAgendamentoOnline = true,
                    Sexo = EnumSexo.Masculino,
                    Telefone = "51515151515",
                    Rg = "22222222222",
                    OrdemAgenda = 1,
                    TipoComissao = TipoPagamento.ValorFixo,
                   // UsuarioOperacionalId = SeedUserOperacional.UsuarioBarbeiro2
                },new Profissional
                {
                    Id = Profissional9,
                    EmpresaId = SeedEmpresa.Empresa2,
                    Nome = "Fabricio Machado",
                    Sobrenome = "Fernandes",
                    Email = "fabricio.fernandes@example.com",
                    Cpf = "123 ",
                    ValorTipoPagamento = 10.0m,
                    PermiteEncaixe = true,
                    RecebePagamentoDireto = true,
                    AgendaOnline = true,
                    Celular = "11999999999",
                    Cargo = "Barbeiro",
                    Matricula = "123456",
                    Especialidade = "Cortes de cabelo e barba",
                    AvaliacaoMedia = 4.5m,
                    DataNascimento = new DateTime(1990, 5, 15),
                    Destaque = true,
                    IntervaloAtendimento = 30,
                    Status = StatusProfissional.Ativo,
                    ExibirAgendamentoOnline = true,
                    Sexo = EnumSexo.Masculino,
                    Telefone = "51515151515",
                    Rg = "22222222222",
                    OrdemAgenda = 1,
                    TipoComissao = TipoPagamento.ValorFixo,
                  //  UsuarioOperacionalId = null,
                },new Profissional
                {
                    Id = Profissional10,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Paula Mota",
                    Sobrenome = "Fernandes",
                    Email = "alexandre.fernandes@example.com",
                    Cpf = "123 ",
                    ValorTipoPagamento = 10.0m,
                    PermiteEncaixe = true,
                    RecebePagamentoDireto = true,
                    AgendaOnline = true,
                    Celular = "11999999999",
                    Cargo = "Barbeiro",
                    Matricula = "123456",
                    Especialidade = "Cortes de cabelo e barba",
                    AvaliacaoMedia = 4.5m,
                    DataNascimento = new DateTime(1990, 5, 15),
                    Destaque = true,
                    IntervaloAtendimento = 30,
                    Status = StatusProfissional.Ativo,
                    ExibirAgendamentoOnline = true,
                    Sexo = EnumSexo.Feminino,
                    Telefone = "51515151515",
                    Rg = "22222222222",
                    OrdemAgenda = 1,
                    TipoComissao = TipoPagamento.ValorFixo,
                   // UsuarioOperacionalId = null,
                },new Profissional
                {
                    Id = Profissional11,
                    EmpresaId = SeedEmpresa.Empresa1,
                    Nome = "Rebeca Fernandes",
                    Sobrenome = "Fernandes",
                    Email = "rebeca.fernandes@example.com",
                    Cpf = "123 ",
                    ValorTipoPagamento = 10.0m,
                    PermiteEncaixe = true,
                    RecebePagamentoDireto = true,
                    AgendaOnline = true,
                    Celular = "11999999999",
                    Cargo = "Barbeira",
                    Matricula = "123456",
                    Especialidade = "Cortes de cabelo e barba",
                    AvaliacaoMedia = 4.5m,
                    DataNascimento = new DateTime(1990, 5, 15),
                    Destaque = true,
                    IntervaloAtendimento = 30,
                    Status = StatusProfissional.Ativo,
                    ExibirAgendamentoOnline = true,
                    Sexo = EnumSexo.Masculino,
                    Telefone = "51515151515",
                    Rg = "22222222222",
                    OrdemAgenda = 1,
                    TipoComissao = TipoPagamento.ValorFixo,
                  
                }
            };
        }
    }

}
