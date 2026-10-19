using Application;
using Application.Common.Exceptions;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Dispatcher;
using Domain.Entities.AuthUsers;
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.Seeds.AuthUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Threading.RateLimiting;

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
    "Saas");

// ==========================================
// 4. Autorização e aplicação
// ==========================================

builder.Services.AddPermissions();
builder.Services.AddApplication();

builder.Services.AddScoped<IDispatcher, Application.Dispatcher.Dispatcher>();

// ==========================================
// 5. Contexto do usuário e tenant
// ==========================================

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUsuarioLogado, UsuarioLogado>();
builder.Services.AddScoped<ITenantContext, TenantContext>();

// ==========================================
// 6. Controllers e OpenAPI
// ==========================================

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Barber Shop API Saas"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Informe Bearer seguido do token JWT.",
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
// 7. Limitação de requisições no cadastro público
// ==========================================
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("cadastro-empresa", context =>
        RateLimitPartition.GetFixedWindowLimiter(partitionKey:
         context.Connection.RemoteIpAddress?.ToString()
         ?? "ip-desconhecido",
         factory: _ => new FixedWindowRateLimiterOptions
         {
             PermitLimit = 5,
             Window = TimeSpan.FromMinutes(1),
             QueueLimit = 0,
             AutoReplenishment = true
         }));

});

// ==========================================
// 8. Pipeline HTTP
// ==========================================

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    await SeederVinculos.InicializarAsync(app.Services);
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "Barber Shop API Saas v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();