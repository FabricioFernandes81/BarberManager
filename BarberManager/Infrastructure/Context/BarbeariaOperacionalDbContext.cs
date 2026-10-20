using Domain.Entities.Assinaturas;
using Domain.Entities.Clientes;
using Domain.Entities.Profissionais;
using Domain.Entities.Servicos;
using Infrastructure.Configurations.Operacional;
using Infrastructure.Seeds.Operacional;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Context
{
    public class BarbeariaOperacionalDbContext : DbContext
    {

        public BarbeariaOperacionalDbContext(DbContextOptions<BarbeariaOperacionalDbContext> options)
            : base(options)
        {

        }

        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Servico> Servicos { get; set; }

        public DbSet<CategoriaServico> CategoriasServicos { get; set; }

        public DbSet<ProfissionalServicos> ProfissionalServicos { get; set; }


        public DbSet<PlanoAssinatura> PlanosAssinatura
    => Set<PlanoAssinatura>();

        public DbSet<PlanoAssinaturaServico> PlanoAssinaturaServicos
            => Set<PlanoAssinaturaServico>();

        public DbSet<BeneficioPlano> BeneficiosPlano
            => Set<BeneficioPlano>();

        public DbSet<AssinaturaCliente> AssinaturasClientes
            => Set<AssinaturaCliente>();

        public DbSet<BeneficiarioAssinatura> BeneficiariosAssinatura
            => Set<BeneficiarioAssinatura>();

        public DbSet<UtilizacaoAssinatura> UtilizacoesAssinatura
            => Set<UtilizacaoAssinatura>();

        public DbSet<Cliente> Clientes => Set<Cliente>();

        public DbSet<ClienteEmpresa> ClientesEmpresas
            => Set<ClienteEmpresa>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProfissionalConfiguration());
            modelBuilder.ApplyConfiguration(new ServicoConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriaServicoConfiguration());
            modelBuilder.ApplyConfiguration(new ProfissionalServicoConfiguration());
            modelBuilder.ApplyConfiguration(new PlanoAssinaturaConfiguration());
            modelBuilder.ApplyConfiguration(new PlanoAssinaturaServicoConfiguration());
            modelBuilder.ApplyConfiguration(new BeneficioPlanoConfiguration());
            modelBuilder.ApplyConfiguration(new AssinaturaClienteConfiguration());
            modelBuilder.ApplyConfiguration(new BeneficiarioAssinaturaConfiguration());
            modelBuilder.ApplyConfiguration(new UtilizacaoAssinaturaConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());

            modelBuilder.ApplyConfiguration(
                new ClienteEmpresaConfiguration());

            SeedProfissionais.SeedProfissial(modelBuilder);
            SeedModuloServicos.SeedIniciarServico(modelBuilder);
            SeedPlanosAssinatura.InicializarSeed(modelBuilder);
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

    }
}
