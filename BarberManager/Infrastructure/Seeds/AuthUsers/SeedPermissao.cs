using Domain.Entities.AuthUsers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Seeds.AuthUsers
{
    public static class SeedPermissao
    {
        #region EMPRESA

        public static readonly Guid EmpresaVisualizar =
            Guid.Parse("B0000000-0000-0000-0000-000000000013");

        public static readonly Guid EmpresaDetalhes =
            Guid.Parse("B0000000-0000-0000-0000-000000000014");

        public static readonly Guid EmpresaCriar =
            Guid.Parse("B0000000-0000-0000-0000-000000000015");

        public static readonly Guid EmpresaEditar =
            Guid.Parse("B0000000-0000-0000-0000-000000000016");

        public static readonly Guid EmpresaExcluir =
            Guid.Parse("B0000000-0000-0000-0000-000000000017");

        public static readonly Guid EmpresaAtivar =
            Guid.Parse("B0000000-0000-0000-0000-000000000018");

        public static readonly Guid EmpresaBloquear =
            Guid.Parse("B0000000-0000-0000-0000-000000000019");

        public static readonly Guid EmpresaDesbloquear =
            Guid.Parse("B0000000-0000-0000-0000-000000000020");

        public static readonly Guid EmpresaRenovar =
            Guid.Parse("B0000000-0000-0000-0000-000000000021");

        public static readonly Guid EmpresaEstatisticas =
            Guid.Parse("B0000000-0000-0000-0000-000000000022");

        public static readonly Guid EmpresaUsuarios =
            Guid.Parse("B0000000-0000-0000-0000-000000000023");

        #endregion

        #region Cliente

        public static readonly Guid ClienteVisualizar =
            Guid.Parse("80000000-0000-0000-0000-000000000002");

        public static readonly Guid ClienteCriar =
            Guid.Parse("80000000-0000-0000-0000-000000000003");

        public static readonly Guid ClienteEditar =
            Guid.Parse("80000000-0000-0000-0000-000000000004");

        public static readonly Guid ClienteExcluir =
            Guid.Parse("80000000-0000-0000-0000-000000000005");
        #endregion


        public static void SeedInicializarPermissoes(ModelBuilder builder)
        {
            builder.Entity<Permissao>().HasData(SetPermissoes());
        }

        private static List<Permissao> SetPermissoes()
        {
            return new List<Permissao>
            {
                new Permissao
                {
                    Id =EmpresaVisualizar ,
                    Codigo = "empresa.visualizar",
                    Nome = "Visualizar Empresas",
                    Descricao = "Permite listar empresas.",
                },new Permissao
                {
                    Id = EmpresaDetalhes,
                   Codigo = "empresa.detalhes",
                   Nome = "Detalhes Empresa",
                   Descricao = "Permite visualizar detalhes.",

                },new Permissao
                {
                    Id= EmpresaCriar,
                    Codigo ="empresa.criar",
                    Nome = "Cadastrar Empresa",
                    Descricao ="Permite cadastrar empresas.",

                },new Permissao
                {
                    Id =EmpresaEditar,
                    Codigo = "empresa.editar",
                    Nome = "Editar Empresa",
                    Descricao = "Permite editar empresas.",

                },new Permissao
                {
                   Id = EmpresaExcluir,
                    Codigo = "empresa.excluir",
                    Nome = "Excluir Empresa",
                    Descricao ="Permite excluir empresas.",

                },new Permissao
                {
                    Id= EmpresaAtivar,
                    Codigo ="empresa.ativar",
                    Nome ="Ativar Empresa",
                    Descricao ="Permite ativar empresas.",

                },new Permissao
                {
                    Id= EmpresaBloquear,
                    Codigo ="empresa.bloquear",
                    Nome ="Bloquear Empresa",
                    Descricao ="Permite bloquear empresas.",

                },new Permissao
                {
                    Id= EmpresaDesbloquear,
                    Codigo = "empresa.desbloquear",
                    Nome= "Desbloquear Empresa",
                    Descricao ="Permite desbloquear empresas.",

                },new Permissao
                {
                    Id = EmpresaRenovar,
                    Codigo ="empresa.renovar",
                    Nome ="Renovar Assinatura",
                    Descricao ="Permite renovar assinatura.",

                },new Permissao
                {
                    Id = EmpresaEstatisticas,
                    Codigo = "empresa.estatisticas",
                    Nome ="Estatísticas",
                    Descricao = "Permite visualizar estatísticas.",

                },new Permissao
                {
                  Id= EmpresaUsuarios,
                  Codigo = "empresa.usuarios",
                  Nome="Usuários",
                  Descricao= "Permite visualizar usuários da empresa.",

                },
                #region Clientes
                new Permissao
                {
                Id= ClienteVisualizar,
                Codigo = "cliente.visualizar",
                Nome = "Visualizar Clientes",
                Descricao = "Permite visualizar os clientes cadastrados.",
                },
                new Permissao
                {
                Id = ClienteCriar,
                Codigo = "cliente.criar",
                Nome = "Cadastrar Clientes",
                Descricao = "Permite cadastrar novos clientes.",
                },new Permissao
                {
                 Id = ClienteEditar,
                Codigo = "cliente.editar",
                Nome = "Editar Clientes",
                Descricao = "Permite editar clientes cadastrados.",
                },new Permissao
                {
                 Id = ClienteExcluir,
                Codigo = "cliente.excluir",
                Nome = "Excluir Clientes",
                Descricao = "Permite excluir clientes cadastrados.",
                }
                #endregion
              
            };
        }
    }
}