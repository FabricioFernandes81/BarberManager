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
        public DbSet<Responsavel> Responsaveis => Set<Responsavel>();
        public DbSet<Plano> Planos { get; set; }
        public DbSet<Assinatura> Assinaturas { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<TransacaoPagamento> TransacoesPagamento { get; set; }

        public DbSet<PlanoModulo> PlanoModulos { set; get; }
        public DbSet<EmpresaModulo> EmpresaModulos { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Saas");
            modelBuilder.ApplyConfiguration(new EmpresaConfigurations());
            modelBuilder.ApplyConfiguration( new ResponsavelConfigurations());
            modelBuilder.ApplyConfiguration(new PlanoConfigurations());

            modelBuilder.ApplyConfiguration(new AssinaturaConfigurations());
            modelBuilder.ApplyConfiguration(new FaturaConfigurations());
            modelBuilder.ApplyConfiguration(new TransacaoPagamentoConfigurations());

            modelBuilder.ApplyConfiguration(new EmpresaModuloConfigurations());
            modelBuilder.ApplyConfiguration(new ModuloConfigurations());
            modelBuilder.ApplyConfiguration(new PlanoModuloConfigurations());


            
            SeedEmpresa.InicializerSeed(modelBuilder);
            SeedResponsaveis.Seed(modelBuilder);

            SeedsPlanoAssinatura.InicializerSeed(modelBuilder);
            SeedFatura.InicializerSeed(modelBuilder);
            SeedTransacaoPagamento.InicializerSeed(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
