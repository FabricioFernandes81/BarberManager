using Domain.Entities.Agendamentos;
using Domain.Entities.Assinaturas;
using Domain.Entities.Clientes;
using Domain.Entities.Common;
using Domain.Entities.Compras;
using Domain.Entities.Estoque;
using Domain.Entities.Financeiro;
using Domain.Entities.Produtos;
using Domain.Entities.Profissionais;
using Domain.Entities.Servicos;
using Domain.Entities.Vendas;
using Infrastructure.Configurations.Financeiro;
using Infrastructure.Configurations.Operacional;
using Infrastructure.Configurations.Operacional.Compras;
using Infrastructure.Configurations.Operacional.Estoque;
using Infrastructure.Configurations.Operacional.Produtos;
using Infrastructure.Configurations.Operacional.Vendas;
using Infrastructure.Configurations.Saas;
using Infrastructure.Seeds.Operacional;
using Infrastructure.Seeds.Operacional.Estoque;
using Infrastructure.Seeds.Operacional.Produtos;
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

        
        public DbSet<Venda> Vendas => Set<Venda>();

        public DbSet<VendaItem> VendaItens => Set<VendaItem>();

        public DbSet<CategoriaProduto> CategoriasProdutos =>
    Set<CategoriaProduto>();

        public DbSet<Marca> Marcas =>
            Set<Marca>();

        public DbSet<Produto> Produtos =>
            Set<Produto>();

        public DbSet<EstoqueProduto> EstoquesProdutos =>
            Set<EstoqueProduto>();

        public DbSet<MovimentacaoEstoque> MovimentacoesEstoque =>
            Set<MovimentacaoEstoque>();
        
        public DbSet<Fornecedor> Fornecedores => Set<Fornecedor>();

        public DbSet<Compra> Compras => Set<Compra>();

        public DbSet<CompraItem> CompraItens => Set<CompraItem>();

        public DbSet<RecebimentoCompra> RecebimentosCompras =>
    Set<RecebimentoCompra>();

        public DbSet<RecebimentoCompraItem> RecebimentosComprasItens =>
            Set<RecebimentoCompraItem>();
        public DbSet<ConfiguracaoAgendaEmpresa> ConfiguracoesAgendaEmpresa
    => Set<ConfiguracaoAgendaEmpresa>();

        public DbSet<ConfiguracaoAssasClient> ConfiguracoesAssasClient => Set<ConfiguracaoAssasClient>();

        public DbSet<EventoWebhookAsaas> EventosWebhookAsaas => Set<EventoWebhookAsaas>();
        public DbSet<CobrancaOperacional> CobrancasOperacionais
    => Set<CobrancaOperacional>();

        public DbSet<ClienteAsaasOperacional> ClientesAsaasOperacionais
    => Set<ClienteAsaasOperacional>();
        public DbSet<OutboxOperacionalMessage> OutboxMessages =>
      Set<OutboxOperacionalMessage>();

        public DbSet<DevolucaoCompra> DevolucoesCompras =>
    Set<DevolucaoCompra>();

        public DbSet<DevolucaoCompraItem> DevolucoesComprasItens =>
            Set<DevolucaoCompraItem>();
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
            modelBuilder.ApplyConfiguration(new VendaConfiguration());
            modelBuilder.ApplyConfiguration(new VendaItemConfiguration());

            
            
           
            modelBuilder.ApplyConfiguration(new FornecedorConfiguration());

            modelBuilder.ApplyConfiguration(
    new CategoriaProdutoConfiguration());

            modelBuilder.ApplyConfiguration(
                new MarcaConfiguration());

            modelBuilder.ApplyConfiguration(
                new ProdutoConfiguration());

            modelBuilder.ApplyConfiguration(
                new EstoqueProdutoConfiguration());

            modelBuilder.ApplyConfiguration(
                new MovimentacaoEstoqueConfiguration());


            modelBuilder.ApplyConfiguration(
                new ClienteEmpresaConfiguration());

            modelBuilder.ApplyConfiguration(new CompraConfiguration());
            
            modelBuilder.ApplyConfiguration(new CompraItemConfiguration());

            modelBuilder.ApplyConfiguration(
    new RecebimentoCompraConfiguration());

            modelBuilder.ApplyConfiguration(
                new RecebimentoCompraItemConfiguration());

            modelBuilder.ApplyConfiguration(new ConfiguracaoAssasClientConfiguration());
            modelBuilder.ApplyConfiguration(
    new EventoWebhookAsaasConfiguration());

            modelBuilder.ApplyConfiguration(
          new CobrancaOperacionalConfiguration());

            modelBuilder.ApplyConfiguration(
    new ClienteAsaasOperacionalConfiguration());
            modelBuilder.ApplyConfiguration(
    new OutboxOperacionalConfiguration());

            modelBuilder.ApplyConfiguration(
    new DevolucaoCompraConfiguration());

            modelBuilder.ApplyConfiguration(
                new DevolucaoCompraItemConfiguration());



            SeedProfissionais.SeedProfissial(modelBuilder);
            SeedModuloServicos.SeedIniciarServico(modelBuilder);
            SeedPlanosAssinatura.InicializarSeed(modelBuilder);


            SeedHorariosFuncionamento.InicializarSeed(modelBuilder);
            SeedHorariosProfissionais.InicializarSeed(modelBuilder);

            SeedCategoriasProdutos.InicializarSeed(modelBuilder);
            SeedMarcas.InicializarSeed(modelBuilder);
            SeedProdutos.InicializarSeed(modelBuilder);

            // Novos seeds do módulo Compras
            SeedFornecedores.InicializarSeed(modelBuilder);
            SeedCompras.InicializarSeed(modelBuilder);

            SeedEstoqueProdutos.InicializarSeed(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

    }
}
