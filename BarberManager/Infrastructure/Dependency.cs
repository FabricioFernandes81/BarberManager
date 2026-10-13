using Application.Interfaces;
using Application.Interfaces.Empresas;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Seeds.Operacional;
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


            // Repositórios e serviços de autenticação
            services.AddScoped<
                IValidadorEmpresaAutenticacao,
                ValidadorEmpresaAutenticacao>();

            services.AddScoped<
                IRepositorioUsuarioEmpresa,
                RepositorioUsuarioEmpresa>();

            services.AddScoped<IRepositorioEmpresa, RepositorioEmpresa>();

            services.AddScoped<IRepositorioProfissionalAutenticacao, RepositorioProfissionalAutenticacao>();

            return services;
        }
    }
}
