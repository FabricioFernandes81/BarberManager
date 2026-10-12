using Domain.Entities.AuthUsers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeds.AuthUsers;

public static class SeedRolePermissao
{
    public static readonly Guid PermisaoGrupSettings =
        Guid.Parse("C0000000-0000-0000-0000-000000000001");

    public static readonly Guid PermisaoGrupFINANCEIRO =
        Guid.Parse("C0000000-0000-0000-0000-000000000007");

    public static readonly Guid PermissaoGrupoClientes =
        Guid.Parse("C0000000-0000-0000-0000-000000000008");

    private static readonly DateTime DataSeed =
        new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static void SeedInicializer(ModelBuilder builder)
    {
        builder.Entity<PermissaoGrupo>()
            .HasData(SetPermissoesGrupo());

        builder.Entity<RolePermissao>()
            .HasData(SetRolePermissoes());

        builder.Entity<GrupoPermissaoPermissao>()
            .HasData(SetGrupoPermissaoPermissoes());
    }

    private static List<PermissaoGrupo> SetPermissoesGrupo()
    {
        return
        [
            new PermissaoGrupo
            {
                Id = PermisaoGrupSettings,
                Nome = "settings.manage",
                Descricao = "Gerenciar configurações da plataforma.",
                DataCadastro = DataSeed
            },
            new PermissaoGrupo
            {
                Id = PermisaoGrupFINANCEIRO,
                Nome = "financeiro.manage",
                Descricao = "Gerenciar operações financeiras.",
                DataCadastro = DataSeed
            },
            new PermissaoGrupo
            {
                Id = PermissaoGrupoClientes,
                Nome = "clientes.manage",
                Descricao = "Gerenciar clientes da barbearia.",
                DataCadastro = DataSeed
            }
        ];
    }

    private static List<RolePermissao> SetRolePermissoes()
    {
        return
        [
            new RolePermissao
            {
                RoleId = SeedRole.RoleAdministrador,
                PermissaoGupoId = PermisaoGrupSettings
            },
            new RolePermissao
            {
                RoleId = SeedRole.RoleBarberAdministrador,
                PermissaoGupoId = PermissaoGrupoClientes
            }
        ];
    }

    private static List<GrupoPermissaoPermissao>
        SetGrupoPermissaoPermissoes()
    {
        return
        [
            new GrupoPermissaoPermissao
            {
                PermissaoGrupoId = PermisaoGrupSettings,
                PermissaoId = SeedPermissao.EmpresaDetalhes
            },
            new GrupoPermissaoPermissao
            {
                PermissaoGrupoId = PermisaoGrupSettings,
                PermissaoId = SeedPermissao.EmpresaEditar
            },
            new GrupoPermissaoPermissao
            {
                PermissaoGrupoId = PermissaoGrupoClientes,
                PermissaoId = SeedPermissao.ClienteVisualizar
            },
            new GrupoPermissaoPermissao
            {
                PermissaoGrupoId = PermissaoGrupoClientes,
                PermissaoId = SeedPermissao.ClienteCriar
            },
            new GrupoPermissaoPermissao
            {
                PermissaoGrupoId = PermissaoGrupoClientes,
                PermissaoId = SeedPermissao.ClienteEditar
            },
            new GrupoPermissaoPermissao
            {
                PermissaoGrupoId = PermissaoGrupoClientes,
                PermissaoId = SeedPermissao.ClienteExcluir
            }
        ];
    }
}