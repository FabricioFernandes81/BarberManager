using Domain.Entities.Saas;
using Infrastructure.Configurations.Saas;
using Infrastructure.Seeds.Empresas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class BarbeariaSaasDbContext : DbContext
    {

        public BarbeariaSaasDbContext(DbContextOptions<BarbeariaSaasDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Empresa> Empresas => Set<Empresa>();
        public DbSet<Plano> Planos { get; set; }
        public DbSet<Assinatura> Assinaturas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmpresaConfigurations());

            SeedEmpresa.InicializerSeed(modelBuilder);

            SeedsPlanoAssinatura.InicializerSeed(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
