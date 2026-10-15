using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class InitialOperacional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriasServicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasServicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanosAssinatura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Periodicidade = table.Column<int>(type: "int", nullable: false),
                    QuantidadeCiclos = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<int>(type: "int", nullable: false),
                    DisponivelOnline = table.Column<bool>(type: "bit", nullable: false),
                    DataInicioVigencia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFimVigencia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosAssinatura", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profissionais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAdmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDemissao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TipoComissao = table.Column<int>(type: "int", nullable: false),
                    ValorTipoPagamento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IntervaloAtendimento = table.Column<int>(type: "int", nullable: false),
                    PermiteEncaixe = table.Column<bool>(type: "bit", nullable: false),
                    AgendaOnline = table.Column<bool>(type: "bit", nullable: false),
                    Biografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ExibirAgendamentoOnline = table.Column<bool>(type: "bit", nullable: false),
                    RecebePagamentoDireto = table.Column<bool>(type: "bit", nullable: false),
                    OrdemAgenda = table.Column<int>(type: "int", nullable: false),
                    AvaliacaoMedia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAvaliacoes = table.Column<int>(type: "int", nullable: false),
                    TotalComissaoMes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Especialidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destaque = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissionais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Custo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DuracaoMinutos = table.Column<int>(type: "int", nullable: false),
                    IntervaloMinutos = table.Column<int>(type: "int", nullable: false),
                    TipoComissao = table.Column<int>(type: "int", nullable: false),
                    PercentualComissao = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PermiteAgendamentoOnline = table.Column<bool>(type: "bit", nullable: false),
                    ExibirAplicativo = table.Column<bool>(type: "bit", nullable: false),
                    Ativo = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicos_CategoriasServicos_CategoriaServicoId",
                        column: x => x.CategoriaServicoId,
                        principalTable: "CategoriasServicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssinaturasClientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanoAssinaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorContratado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Periodicidade = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFimCicloAtual = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataProximaCobranca = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCancelamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RenovacaoAutomatica = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ReferenciaGateway = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssinaturasClientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssinaturasClientes_PlanosAssinatura_PlanoAssinaturaId",
                        column: x => x.PlanoAssinaturaId,
                        principalTable: "PlanosAssinatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BeneficiosPlano",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanoAssinaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiosPlano", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeneficiosPlano_PlanosAssinatura_PlanoAssinaturaId",
                        column: x => x.PlanoAssinaturaId,
                        principalTable: "PlanosAssinatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanoAssinaturaServicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanoAssinaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantidadePorCiclo = table.Column<int>(type: "int", nullable: true),
                    LimitePorDia = table.Column<int>(type: "int", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoAssinaturaServicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanoAssinaturaServicos_PlanosAssinatura_PlanoAssinaturaId",
                        column: x => x.PlanoAssinaturaId,
                        principalTable: "PlanosAssinatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfissionalServicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorPersonalizado = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DuracaoPersonalizada = table.Column<int>(type: "int", nullable: true),
                    DataVinculo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfissionalServicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfissionalServicos_Profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfissionalServicos_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BeneficiariosAssinatura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssinaturaClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titular = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DataVinculo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDesvinculo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiariosAssinatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeneficiariosAssinatura_AssinaturasClientes_AssinaturaClienteId",
                        column: x => x.AssinaturaClienteId,
                        principalTable: "AssinaturasClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UtilizacoesAssinatura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssinaturaClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeneficiarioAssinaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgendamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataUtilizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Observacao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilizacoesAssinatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UtilizacoesAssinatura_AssinaturasClientes_AssinaturaClienteId",
                        column: x => x.AssinaturaClienteId,
                        principalTable: "AssinaturasClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UtilizacoesAssinatura_BeneficiariosAssinatura_BeneficiarioAssinaturaId",
                        column: x => x.BeneficiarioAssinaturaId,
                        principalTable: "BeneficiariosAssinatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CategoriasServicos",
                columns: new[] { "Id", "Ativo", "Cor", "DataAtualizacao", "DataCadastro", "Descricao", "EmpresaId", "Icone", "Nome", "Ordem" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), 1, "#FF5733", null, new DateTime(2026, 10, 15, 20, 1, 58, 554, DateTimeKind.Utc).AddTicks(5715), "Serviço de corte de cabelo", new Guid("50000000-0000-0000-0000-000000000001"), "content_cut", "Cabelo", 1 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 1, "#33FF57", null, new DateTime(2026, 10, 15, 20, 1, 58, 554, DateTimeKind.Utc).AddTicks(7648), "Serviço de categoria 2", new Guid("50000000-0000-0000-0000-000000000001"), "icone-categoria2", "Mão e Pés", 2 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 1, "#3357FF", null, new DateTime(2026, 10, 15, 20, 1, 58, 554, DateTimeKind.Utc).AddTicks(7651), "Serviço de categoria 3", new Guid("50000000-0000-0000-0000-000000000001"), "icone-categoria3", "Barba e Bigode", 3 }
                });

            migrationBuilder.InsertData(
                table: "PlanosAssinatura",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataFimVigencia", "DataInicioVigencia", "Descricao", "DisponivelOnline", "EmpresaId", "Nome", "Periodicidade", "QuantidadeCiclos", "Valor" },
                values: new object[,]
                {
                    { new Guid("a1000000-0000-0000-0000-000000000001"), 1, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Plano de teste com até quatro cortes por ciclo.", true, new Guid("50000000-0000-0000-0000-000000000001"), "Corte Mensal", 1, 1, 99.90m },
                    { new Guid("a1000000-0000-0000-0000-000000000002"), 1, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Plano de teste para corte e barba recorrentes.", true, new Guid("50000000-0000-0000-0000-000000000001"), "Corte e Barba", 1, 1, 179.90m },
                    { new Guid("a1000000-0000-0000-0000-000000000003"), 1, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Plano demonstrativo para contratação familiar.", true, new Guid("50000000-0000-0000-0000-000000000001"), "Plano Família", 1, 1, 249.90m },
                    { new Guid("a1000000-0000-0000-0000-000000000004"), 1, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Plano demonstrativo com benefícios exclusivos.", true, new Guid("50000000-0000-0000-0000-000000000001"), "Clube VIP", 1, 1, 299.90m }
                });

            migrationBuilder.InsertData(
                table: "Profissionais",
                columns: new[] { "Id", "AgendaOnline", "AvaliacaoMedia", "Biografia", "Cargo", "Celular", "Cpf", "DataAdmissao", "DataAtualizacao", "DataCadastro", "DataDemissao", "DataNascimento", "Destaque", "Email", "EmpresaId", "Especialidade", "ExibirAgendamentoOnline", "Facebook", "Foto", "Instagram", "IntervaloAtendimento", "Matricula", "Nome", "OrdemAgenda", "PermiteEncaixe", "RecebePagamentoDireto", "Rg", "Sexo", "Sobrenome", "Status", "Telefone", "TipoComissao", "TotalAvaliacoes", "TotalComissaoMes", "UsuarioId", "ValorTipoPagamento" },
                values: new object[,]
                {
                    { new Guid("91000000-0000-0000-0000-000000000001"), true, 4.5m, null, "Barbeiro", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 15, 20, 1, 58, 552, DateTimeKind.Utc).AddTicks(9130), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "carlos.henrique@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Carlos Henrique", 1, true, true, "22222222222", 1, "ffffffffffff", 1, "51515151515", 4, 0, 0m, new Guid("90000000-0000-0000-0000-000000000002"), 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000002"), true, 4.5m, null, "Barbeiro", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 15, 20, 1, 58, 553, DateTimeKind.Utc).AddTicks(4952), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Alexandre =", 1, true, true, "22222222222", 1, "Fernandes", 1, "51515151515", 4, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000003"), true, 4.5m, null, "Administrador", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 15, 20, 1, 58, 553, DateTimeKind.Utc).AddTicks(4980), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "0000000", "Roberto Almeida", 1, true, true, "22222222222", 1, "Fernandes", 1, "51515151515", 0, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000004"), true, 4.5m, null, "Recepcionista", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 15, 20, 1, 58, 553, DateTimeKind.Utc).AddTicks(4985), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Bete Carvalho", 1, true, true, "22222222222", 1, "Faaaaaaaaa", 1, "51515151515", 0, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000005"), true, 4.5m, null, "Caixa", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 15, 20, 1, 58, 553, DateTimeKind.Utc).AddTicks(4989), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Alexandre11111111 = aaaaa", 1, true, true, "22222222222", 1, "Fernandes", 1, "51515151515", 4, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000006"), true, 4.5m, null, "Estoquita", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 15, 20, 1, 58, 553, DateTimeKind.Utc).AddTicks(5001), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Agenor Rodrigues", 1, true, true, "22222222222", 1, "Fernandes", 1, "51515151515", 0, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000007"), true, 4.5m, null, "Financeiro", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 15, 20, 1, 58, 553, DateTimeKind.Utc).AddTicks(5025), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Paulo Silva", 1, true, true, "22222222222", 1, "a", 1, "51515151515", 4, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000008"), true, 4.5m, null, "Barbeiro", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 15, 20, 1, 58, 553, DateTimeKind.Utc).AddTicks(5029), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Paula Mota", 1, true, true, "22222222222", 1, "Fernandes", 1, "51515151515", 4, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000009"), true, 4.5m, null, "Barbeiro", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 15, 20, 1, 58, 553, DateTimeKind.Utc).AddTicks(5033), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "fabricio.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000002"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Fabricio Machado", 1, true, true, "22222222222", 1, "Fernandes", 1, "51515151515", 4, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000010"), true, 4.5m, null, "Barbeiro", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 15, 20, 1, 58, 553, DateTimeKind.Utc).AddTicks(5039), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Paula Mota", 1, true, true, "22222222222", 2, "Fernandes", 1, "51515151515", 4, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000011"), true, 4.5m, null, "Barbeira", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 10, 15, 20, 1, 58, 553, DateTimeKind.Utc).AddTicks(5043), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "rebeca.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Rebeca Fernandes", 1, true, true, "22222222222", 1, "Fernandes", 1, "51515151515", 4, 0, 0m, null, 10.0m }
                });

            migrationBuilder.InsertData(
                table: "BeneficiosPlano",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "Descricao", "EmpresaId", "Nome", "PlanoAssinaturaId", "Tipo", "Valor" },
                values: new object[,]
                {
                    { new Guid("a2000000-0000-0000-0000-000000000001"), true, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Desconto demonstrativo de 10% em produtos elegíveis.", new Guid("50000000-0000-0000-0000-000000000001"), "Desconto em cosméticos", new Guid("a1000000-0000-0000-0000-000000000004"), 1, 10m },
                    { new Guid("a2000000-0000-0000-0000-000000000002"), true, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Uma cortesia conforme as regras da barbearia.", new Guid("50000000-0000-0000-0000-000000000001"), "Café de cortesia", new Guid("a1000000-0000-0000-0000-000000000004"), 4, null }
                });

            migrationBuilder.InsertData(
                table: "Servicos",
                columns: new[] { "Id", "Ativo", "CategoriaServicoId", "Custo", "DataAtualizacao", "DataCadastro", "Descricao", "DuracaoMinutos", "EmpresaId", "ExibirAplicativo", "IntervaloMinutos", "Nome", "Observacao", "PercentualComissao", "PermiteAgendamentoOnline", "TipoComissao", "Valor" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 1, new Guid("22222222-2222-2222-2222-222222222222"), 1m, null, new DateTime(2026, 10, 15, 20, 1, 58, 555, DateTimeKind.Utc).AddTicks(3632), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Corte de Cabelo", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111112"), 1, new Guid("22222222-2222-2222-2222-222222222222"), 1m, null, new DateTime(2026, 10, 15, 20, 1, 58, 555, DateTimeKind.Utc).AddTicks(5457), "Serviço de corte de cabelo", 60, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Cabelo e Barba", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111113"), 1, new Guid("22222222-2222-2222-2222-222222222222"), 1m, null, new DateTime(2026, 10, 15, 20, 1, 58, 555, DateTimeKind.Utc).AddTicks(5461), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Botox Capilar", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111114"), 1, new Guid("22222222-2222-2222-2222-222222222222"), 1m, null, new DateTime(2026, 10, 15, 20, 1, 58, 555, DateTimeKind.Utc).AddTicks(5464), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Corte + Barba + Botox Capilar", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111115"), 1, new Guid("44444444-4444-4444-4444-444444444444"), 1m, null, new DateTime(2026, 10, 15, 20, 1, 58, 555, DateTimeKind.Utc).AddTicks(5466), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Esmaltação - Francesinha", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111116"), 1, new Guid("33333333-3333-3333-3333-333333333333"), 1m, null, new DateTime(2026, 10, 15, 20, 1, 58, 555, DateTimeKind.Utc).AddTicks(5473), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Esmaltação - Mãos", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111117"), 2, new Guid("33333333-3333-3333-3333-333333333333"), 1m, null, new DateTime(2026, 10, 15, 20, 1, 58, 555, DateTimeKind.Utc).AddTicks(5476), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Esmaltação - Mãos", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111118"), 1, new Guid("33333333-3333-3333-3333-333333333333"), 1m, null, new DateTime(2026, 10, 15, 20, 1, 58, 555, DateTimeKind.Utc).AddTicks(5478), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Esmaltação - Mãos", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111119"), 2, new Guid("33333333-3333-3333-3333-333333333333"), 1m, null, new DateTime(2026, 10, 15, 20, 1, 58, 555, DateTimeKind.Utc).AddTicks(5485), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Esmaltação - Mãos", null, 10m, true, 1, 60m }
                });

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("00d40f39-7bc9-4d16-be01-a6da49493bf0"), 3, null, new DateTime(2026, 10, 15, 20, 1, 58, 556, DateTimeKind.Utc).AddTicks(1074), new DateTime(2026, 10, 15, 20, 1, 58, 556, DateTimeKind.Utc).AddTicks(1074), 45, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("0c23d34b-2182-40f6-a9a5-8636ca15d82f"), 3, null, new DateTime(2026, 10, 15, 20, 1, 58, 556, DateTimeKind.Utc).AddTicks(1085), new DateTime(2026, 10, 15, 20, 1, 58, 556, DateTimeKind.Utc).AddTicks(1084), 10, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("0c31ad5a-6b1d-4dd6-b382-b3691159cc64"), 3, null, new DateTime(2026, 10, 15, 20, 1, 58, 556, DateTimeKind.Utc).AddTicks(1063), new DateTime(2026, 10, 15, 20, 1, 58, 556, DateTimeKind.Utc).AddTicks(1062), 45, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("31bb4e94-1b72-4873-940b-5f2f79df6db9"), 3, null, new DateTime(2026, 10, 15, 20, 1, 58, 556, DateTimeKind.Utc).AddTicks(1079), new DateTime(2026, 10, 15, 20, 1, 58, 556, DateTimeKind.Utc).AddTicks(1079), 45, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("3732a165-f9b9-4345-b1f0-129dfa7dec2d"), 3, null, new DateTime(2026, 10, 15, 20, 1, 58, 556, DateTimeKind.Utc).AddTicks(1082), new DateTime(2026, 10, 15, 20, 1, 58, 556, DateTimeKind.Utc).AddTicks(1081), 45, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("8dce3053-fc6a-483a-8c1f-11cc8eb0b5c5"), 1, null, new DateTime(2026, 10, 15, 20, 1, 58, 555, DateTimeKind.Utc).AddTicks(9945), new DateTime(2026, 10, 15, 20, 1, 58, 555, DateTimeKind.Utc).AddTicks(9938), 25, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("a5c2c9f7-d5cb-4512-b678-b19aeac9636e"), 3, null, new DateTime(2026, 10, 15, 20, 1, 58, 556, DateTimeKind.Utc).AddTicks(1060), new DateTime(2026, 10, 15, 20, 1, 58, 556, DateTimeKind.Utc).AddTicks(1059), 40, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("d27a5802-eb3c-44b5-9ecf-3c47d7800a88"), 1, null, new DateTime(2026, 10, 15, 20, 1, 58, 556, DateTimeKind.Utc).AddTicks(1056), new DateTime(2026, 10, 15, 20, 1, 58, 556, DateTimeKind.Utc).AddTicks(1052), 35, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasClientes_EmpresaId_ClienteId",
                table: "AssinaturasClientes",
                columns: new[] { "EmpresaId", "ClienteId" });

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasClientes_EmpresaId_Status_DataProximaCobranca",
                table: "AssinaturasClientes",
                columns: new[] { "EmpresaId", "Status", "DataProximaCobranca" });

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasClientes_PlanoAssinaturaId",
                table: "AssinaturasClientes",
                column: "PlanoAssinaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasClientes_ReferenciaGateway",
                table: "AssinaturasClientes",
                column: "ReferenciaGateway",
                unique: true,
                filter: "[ReferenciaGateway] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiariosAssinatura_AssinaturaClienteId_ClienteId",
                table: "BeneficiariosAssinatura",
                columns: new[] { "AssinaturaClienteId", "ClienteId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiariosAssinatura_EmpresaId_ClienteId_Ativo",
                table: "BeneficiariosAssinatura",
                columns: new[] { "EmpresaId", "ClienteId", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiosPlano_EmpresaId_PlanoAssinaturaId",
                table: "BeneficiosPlano",
                columns: new[] { "EmpresaId", "PlanoAssinaturaId" });

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiosPlano_PlanoAssinaturaId",
                table: "BeneficiosPlano",
                column: "PlanoAssinaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasServicos_EmpresaId_Nome",
                table: "CategoriasServicos",
                columns: new[] { "EmpresaId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanoAssinaturaServicos_EmpresaId_PlanoAssinaturaId_Ativo",
                table: "PlanoAssinaturaServicos",
                columns: new[] { "EmpresaId", "PlanoAssinaturaId", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanoAssinaturaServicos_PlanoAssinaturaId_ServicoId",
                table: "PlanoAssinaturaServicos",
                columns: new[] { "PlanoAssinaturaId", "ServicoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanosAssinatura_EmpresaId_Nome",
                table: "PlanosAssinatura",
                columns: new[] { "EmpresaId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_Cpf",
                table: "Profissionais",
                column: "Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_EmpresaId",
                table: "Profissionais",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_UsuarioId",
                table: "Profissionais",
                column: "UsuarioId",
                unique: true,
                filter: "[UsuarioId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProfissionalServicos_EmpresaId_ProfissionalId_Ativo",
                table: "ProfissionalServicos",
                columns: new[] { "EmpresaId", "ProfissionalId", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfissionalServicos_ProfissionalId_ServicoId",
                table: "ProfissionalServicos",
                columns: new[] { "ProfissionalId", "ServicoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfissionalServicos_ServicoId",
                table: "ProfissionalServicos",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_CategoriaServicoId",
                table: "Servicos",
                column: "CategoriaServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_EmpresaId_Nome",
                table: "Servicos",
                columns: new[] { "EmpresaId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_UtilizacoesAssinatura_AgendamentoId",
                table: "UtilizacoesAssinatura",
                column: "AgendamentoId",
                unique: true,
                filter: "[AgendamentoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UtilizacoesAssinatura_AssinaturaClienteId",
                table: "UtilizacoesAssinatura",
                column: "AssinaturaClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilizacoesAssinatura_BeneficiarioAssinaturaId",
                table: "UtilizacoesAssinatura",
                column: "BeneficiarioAssinaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilizacoesAssinatura_EmpresaId_AssinaturaClienteId_DataUtilizacao",
                table: "UtilizacoesAssinatura",
                columns: new[] { "EmpresaId", "AssinaturaClienteId", "DataUtilizacao" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BeneficiosPlano");

            migrationBuilder.DropTable(
                name: "PlanoAssinaturaServicos");

            migrationBuilder.DropTable(
                name: "ProfissionalServicos");

            migrationBuilder.DropTable(
                name: "UtilizacoesAssinatura");

            migrationBuilder.DropTable(
                name: "Profissionais");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "BeneficiariosAssinatura");

            migrationBuilder.DropTable(
                name: "CategoriasServicos");

            migrationBuilder.DropTable(
                name: "AssinaturasClientes");

            migrationBuilder.DropTable(
                name: "PlanosAssinatura");
        }
    }
}
