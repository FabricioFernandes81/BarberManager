using Domain.Entities.AuthUsers;
using Infrastructure.Configurations.AuthUsers;
using Infrastructure.Seeds.AuthUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Context
{
    public class AuthDbContext : IdentityDbContext<
        Usuario,
        Roles,
        Guid,
     IdentityUserClaim<Guid>,
     IdentityUserRole<Guid>,
     IdentityUserLogin<Guid>,
     IdentityRoleClaim<Guid>,
     IdentityUserToken<Guid>>{


        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<PermissaoGrupo> PermissaoGrupo { get; set; }
        public DbSet<Permissao> Permissoes { get; set; }
        public DbSet<RolePermissao> RolePermissaos { get; set; }
        public DbSet<UsuarioPermissao> UsuarioPermissaos { get; set; }

        public DbSet<GrupoPermissaoPermissao> GrupoPermissaoPermissaos { get; set; }
        public DbSet<UsuarioEmpresa> UsuarioEmpresa { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Auth");

            builder.ApplyConfiguration(new PermissaoGrupoConfiguration());

            builder.ApplyConfiguration(new PermissaoConfiguration());

            builder.ApplyConfiguration(new RolePermissaoConfiguration());
            // ggggggggggggggggggggggggggggggggg
            builder.ApplyConfiguration(new UsuarioPermissaoConfiguration());
            builder.ApplyConfiguration(new GrupoPermissaoPermissaoConfigurations());
            builder.ApplyConfiguration(new UsuarioEmpresaConfiguration());

          //  SeedUser.IniciarSeed(builder);
            SeedRole.SeedInicializerRoles(builder);
            SeedPermissao.SeedInicializarPermissoes(builder);
            SeedRolePermissao.SeedInicializer(builder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
