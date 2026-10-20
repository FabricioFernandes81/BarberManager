using Application;
using Application.Clientes.Commands;
using Application.Clientes.Commands.Handlers;
using Application.Clientes.DTOs;
using Application.Clientes.Queries;
using Application.Clientes.Queries.Handlers;
using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Dispatcher;
using Domain.Entities.AuthUsers;
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.Seeds.AuthUsers;
using Infrastructure.Seeds.Clientes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Dispatcher = Application.Dispatcher.Dispatcher;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. Infrastructure e banco de dados
// ==========================================

builder.Services.AddInfrastructure(builder.Configuration);

// ==========================================
// 2. Identity
// ==========================================

builder.Services
    .AddIdentityCore<Usuario>()
    .AddRoles<Roles>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// ==========================================
// 3. Autenticação JWT
// ==========================================

builder.Services.AddJwtAuthentication(
    builder.Configuration,
    "Cliente");

// ==========================================
// 4. Autorização e aplicação
// ==========================================

builder.Services.AddPermissions();
builder.Services.AddApplication();

builder.Services.AddScoped<IDispatcher, Dispatcher>();

// ==========================================
// 5. Contexto do usuário e tenant
// ==========================================

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUsuarioLogado, UsuarioLogado>();
builder.Services.AddScoped<ITenantContext, TenantContext>();

// ==========================================
// 6. Controllers e Swagger
// ==========================================


builder.Services.AddScoped<
    ICommandHandler<
        CriarPerfilClienteCommand,
        CriarPerfilClienteResultadoDto>,
    CriarPerfilClienteHandler>();

builder.Services.AddScoped<
    IQueryHandler<
        ObterMeuPerfilClienteQuery,
        PerfilClienteDto?>,
    ObterMeuPerfilClienteHandler>();



builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Barber Shop API Cliente"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Informe o token JWT usando o esquema Bearer.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ==========================================
// 7. Pipeline HTTP
// ==========================================

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    await SeederClientes.InicializarAsync(app.Services);
  //  await SeederVinculos.InicializarAsync(app.Services);
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "Barber Shop API Cliente v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();