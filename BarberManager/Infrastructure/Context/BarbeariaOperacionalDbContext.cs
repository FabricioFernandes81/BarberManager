using Domain.Entities.Agendamentos;
using Domain.Entities.Assinaturas;
using Domain.Entities.Clientes;
using Domain.Entities.Financeiro;
using Domain.Entities.Profissionais;
using Domain.Entities.Servicos;
using Infrastructure.Configurations.Operacional;
using Infrastructure.Configurations.Saas;
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

        public DbSet<Agendamento> Agendamentos => Set<Agendamento>();
        public DbSet<AgendamentoServico> AgendamentoServicos => Set<AgendamentoServico>();
        public DbSet<HorarioFuncionamento> HorariosFuncionamento => Set<HorarioFuncionamento>();
        public DbSet<HorarioProfissional> HorariosProfissionais => Set<HorarioProfissional>();
        public DbSet<BloqueioAgenda> BloqueiosAgenda => Set<BloqueioAgenda>();
        
        public DbSet<Feriado> Feriados => Set<Feriado>();

        public DbSet<Caixa> Caixas => Set<Caixa>();
        public DbSet<MovimentoCaixa> MovimentoCaixas => Set<MovimentoCaixa>();

        public DbSet<FormaPagamento> FormaPagamentos => Set<FormaPagamento>();

        public DbSet<Recebimento> Recebimentos => Set<Recebimento>();


        public DbSet<ContaPagar> ContasPagar => Set<ContaPagar>();
        public DbSet<ContaReceber> ContasReceber => Set<ContaReceber>();
        public DbSet<Pagamento> Pagamentos => Set<Pagamento>();





        public DbSet<ConfiguracaoAgendaEmpresa> ConfiguracoesAgendaEmpresa
    => Set<ConfiguracaoAgendaEmpresa>();
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
            modelBuilder.ApplyConfiguration(new AgendamentoConfiguration());
            modelBuilder.ApplyConfiguration(new AgendamentoServicoConfiguration());
            modelBuilder.ApplyConfiguration(new HorarioFuncionamentoConfiguration());
            modelBuilder.ApplyConfiguration(new HorarioProfissionalConfiguration());
            modelBuilder.ApplyConfiguration(new BloqueioAgendaConfiguration());
            modelBuilder.ApplyConfiguration(new FeriadoConfiguration());

            modelBuilder.ApplyConfiguration(new ConfiguracaoAgendaEmpresaConfiguration());
            modelBuilder.ApplyConfiguration(new CaixaConfiguration());
            modelBuilder.ApplyConfiguration(new MovimentoCaixaConfiguration());
            modelBuilder.ApplyConfiguration(new FormaPagamentoConfiguration());
            modelBuilder.ApplyConfiguration(new RecebimentoConfiguration());

            modelBuilder.ApplyConfiguration(new ContaPagarConfiguration());
            modelBuilder.ApplyConfiguration(new ContaReceberConfiguration());
            modelBuilder.ApplyConfiguration(new PagamentoConfiguration());


            modelBuilder.ApplyConfiguration(
                new ClienteEmpresaConfiguration());

            SeedProfissionais.SeedProfissial(modelBuilder);
            SeedModuloServicos.SeedIniciarServico(modelBuilder);
            SeedPlanosAssinatura.InicializarSeed(modelBuilder);


            SeedHorariosFuncionamento.InicializarSeed(modelBuilder);
            SeedHorariosProfissionais.InicializarSeed(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

    }
}
