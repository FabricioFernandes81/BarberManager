using Application.Common;
using Application.Common.Outbox;
using Application.Interfaces;
using Application.Interfaces.Agendamentos;
using Application.Interfaces.Clientes;
using Application.Interfaces.Empresas;
using Application.Interfaces.Empresas.Pagamentos;
using Application.Interfaces.Profissionais;
using Application.Saas.Empresas;
using Application.Saas.Pagamentos.Services;
using Infrastructure.Context;
using Infrastructure.Notifications;
using Infrastructure.Payments;
using Infrastructure.Payments.Asaas;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Clientes;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Seeds.Operacional;
using Infrastructure.Services;
using Infrastructure.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Dependency
    {
        public static IServiceCollection AddInfrastructure(
     this IServiceCollection services,
     IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "ConnectionStrings:DefaultConnection não configurada.");

            // Contexto de autenticação e Identity
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Contexto SaaS: empresas, planos e assinaturas
            services.AddDbContext<BarbeariaSaasDbContext>(options =>
                options.UseSqlServer(connectionString));


            services.AddDbContext<BarbeariaOperacionalDbContext>(options =>
                options.UseSqlServer(connectionString));




            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();




            // Repositórios e serviços de autenticação
            services.AddScoped<
                IValidadorEmpresaAutenticacao,
                ValidadorEmpresaAutenticacao>();

            services.AddScoped<
                IRepositorioUsuarioEmpresa,
                RepositorioUsuarioEmpresa>();

            services.AddScoped<IRepositorioEmpresa, RepositorioEmpresa>();

            services.AddScoped<IRepositorioProfissionalAutenticacao, RepositorioProfissionalAutenticacao>();
            services.AddScoped<IRepositórioPlano, RepositórioPlano>();
            services.AddScoped<IRepositorioAssinatura, RepositorioAssinatura>();
            services.AddScoped<IRepositorioPagamento, RepositorioPagamento>();

            services.AddScoped<IRepositorioFatura, RepositorioFatura>();

            services.Configure<AsaasOptions>(
                    configuration.GetSection(AsaasOptions.SectionName)
                );

            services.AddHttpClient<IAsaasGateway, AsaasGateway>(
                client =>
                {
                    client.Timeout = TimeSpan.FromSeconds(70);
                });

            services.AddScoped<
                            IAsaasWebhookService, AsaasWebhookService>();

            services.AddScoped<ITrialExpirationService, TrialExpirationService>();

            services.AddScoped<IOutboxWriter, OutboxWriter>();



            services.AddScoped<IRepositorioCliente, RepositorioCliente>();

            services.AddScoped<IRepositorioClienteAutenticacao, RepositorioClienteAutenticacao>();

            services.AddScoped<IRepositorioDisponibilidadeAgenda, RepositorioDisponibilidadeAgenda>();

            services.AddScoped<IRepositorioCriarAgendamento, RepositorioCriarAgendamento>();
            services.AddScoped<IRepositorioProfissionais, RepositorioProfissionais>();


            return services;
        }
    }
}
