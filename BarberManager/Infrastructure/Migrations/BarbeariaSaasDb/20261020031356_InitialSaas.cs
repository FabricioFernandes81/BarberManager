using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaSaasDb
{
    /// <inheritdoc />
    public partial class InitialSaas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Saas");

            migrationBuilder.CreateTable(
                name: "Modulo",
                schema: "Saas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planos",
                schema: "Saas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMensal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorAnual = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LimiteProfissionais = table.Column<int>(type: "int", nullable: false),
                    LimiteEmpresas = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<int>(type: "int", nullable: false),
                    EhTrial = table.Column<bool>(type: "bit", nullable: false),
                    DuracaoTrialDias = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Responsaveis",
                schema: "Saas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsaveis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanoModulos",
                schema: "Saas",
                columns: table => new
                {
                    PlanoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuloId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoModulos", x => new { x.PlanoId, x.ModuloId });
                    table.ForeignKey(
                        name: "FK_PlanoModulos_Modulo_ModuloId",
                        column: x => x.ModuloId,
                        principalSchema: "Saas",
                        principalTable: "Modulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanoModulos_Planos_PlanoId",
                        column: x => x.PlanoId,
                        principalSchema: "Saas",
                        principalTable: "Planos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                schema: "Saas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeFantasia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativa = table.Column<int>(type: "int", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataBloqueio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MotivoBloqueio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenciaClienteAsaas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsavelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empresas_Responsaveis_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalSchema: "Saas",
                        principalTable: "Responsaveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assinaturas",
                schema: "Saas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RenovacaoAutomatica = table.Column<bool>(type: "bit", nullable: false),
                    DataUltimoPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCancelamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiaVencimento = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Trial = table.Column<bool>(type: "bit", nullable: false),
                    DataFimTrial = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativa = table.Column<int>(type: "int", nullable: false),
                    PeriodicidadeCobranca = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assinaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assinaturas_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalSchema: "Saas",
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assinaturas_Planos_PlanoId",
                        column: x => x.PlanoId,
                        principalSchema: "Saas",
                        principalTable: "Planos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaModulos",
                schema: "Saas",
                columns: table => new
                {
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuloId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaModulos", x => new { x.EmpresaId, x.ModuloId });
                    table.ForeignKey(
                        name: "FK_EmpresaModulos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalSchema: "Saas",
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresaModulos_Modulo_ModuloId",
                        column: x => x.ModuloId,
                        principalSchema: "Saas",
                        principalTable: "Modulo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faturas",
                schema: "Saas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssinaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Vencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    PeriodoInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodoFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferenciaGateway = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faturas_Assinaturas_AssinaturaId",
                        column: x => x.AssinaturaId,
                        principalSchema: "Saas",
                        principalTable: "Assinaturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransacoesPagamento",
                schema: "Saas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Provedor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReferenciaGateway = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ReferenciaClienteAsaas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChaveIdempotencia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataProcessamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CodigoErro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MensagemErro = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    MetodoPagamento = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransacoesPagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransacoesPagamento_Faturas_FaturaId",
                        column: x => x.FaturaId,
                        principalSchema: "Saas",
                        principalTable: "Faturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Saas",
                table: "Modulo",
                columns: new[] { "Id", "Ativo", "Codigo", "DataAtualizacao", "DataCadastro", "Descricao", "Nome" },
                values: new object[,]
                {
                    { new Guid("70000000-0000-0000-0000-000000000001"), 1, "CLIENTES", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cadastro e gestão de clientes.", "Clientes" },
                    { new Guid("70000000-0000-0000-0000-000000000002"), 1, "PROFISSIONAIS", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cadastro e gestão de profissionais.", "Profissionais" },
                    { new Guid("70000000-0000-0000-0000-000000000003"), 1, "SERVICOS", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Catálogo de serviços e preços.", "Serviços" },
                    { new Guid("70000000-0000-0000-0000-000000000004"), 1, "AGENDA", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Agendamentos e disponibilidade.", "Agenda" },
                    { new Guid("70000000-0000-0000-0000-000000000005"), 1, "FINANCEIRO", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gestão financeira da barbearia.", "Financeiro" },
                    { new Guid("70000000-0000-0000-0000-000000000006"), 1, "VENDAS", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Vendas de serviços e produtos.", "Vendas" },
                    { new Guid("70000000-0000-0000-0000-000000000007"), 1, "ESTOQUE", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Controle de estoque e movimentações.", "Estoque" },
                    { new Guid("70000000-0000-0000-0000-000000000008"), 1, "COMPRAS", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gestão de compras e fornecedores.", "Compras" },
                    { new Guid("70000000-0000-0000-0000-000000000009"), 1, "COMISSOES", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Apuração de comissões dos profissionais.", "Comissões" },
                    { new Guid("70000000-0000-0000-0000-000000000010"), 1, "FIDELIDADE", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Programas de fidelidade e benefícios.", "Fidelidade" },
                    { new Guid("70000000-0000-0000-0000-000000000011"), 1, "AVALIACOES", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Avaliações e feedback dos clientes.", "Avaliações" },
                    { new Guid("70000000-0000-0000-0000-000000000012"), 1, "RELATORIOS", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Relatórios gerenciais.", "Relatórios" },
                    { new Guid("70000000-0000-0000-0000-000000000013"), 1, "CONFIGURACOES", null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Configurações operacionais da barbearia.", "Configurações" }
                });

            migrationBuilder.InsertData(
                schema: "Saas",
                table: "Planos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DuracaoTrialDias", "EhTrial", "LimiteEmpresas", "LimiteProfissionais", "Nome", "ValorAnual", "ValorMensal" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), 1, null, new DateTime(2026, 10, 20, 3, 13, 55, 522, DateTimeKind.Utc).AddTicks(7143), 30, true, 1, 2, "Trial", 0m, 0m },
                    { new Guid("20000000-0000-0000-0000-000000000002"), 1, null, new DateTime(2026, 10, 20, 3, 13, 55, 522, DateTimeKind.Utc).AddTicks(9114), 0, false, 1, 3, "Starter", 0m, 49.90m },
                    { new Guid("20000000-0000-0000-0000-000000000003"), 1, null, new DateTime(2026, 10, 20, 3, 13, 55, 522, DateTimeKind.Utc).AddTicks(9190), 0, false, 1, 5, "Basic", 49.90m, 89.90m },
                    { new Guid("20000000-0000-0000-0000-000000000004"), 1, null, new DateTime(2026, 10, 20, 3, 13, 55, 522, DateTimeKind.Utc).AddTicks(9193), 0, false, 1, 10, "Professional", 130.00m, 149.90m },
                    { new Guid("20000000-0000-0000-0000-000000000005"), 1, null, new DateTime(2026, 10, 20, 3, 13, 55, 522, DateTimeKind.Utc).AddTicks(9195), 0, false, 1, 25, "Premium", 211.50m, 249.90m },
                    { new Guid("20000000-0000-0000-0000-000000000006"), 1, null, new DateTime(2026, 10, 20, 3, 13, 55, 522, DateTimeKind.Utc).AddTicks(9201), 0, false, 999, 999, "Enterprise", 480.65m, 499.90m }
                });

            migrationBuilder.InsertData(
                schema: "Saas",
                table: "Responsaveis",
                columns: new[] { "Id", "Cpf", "DataAtualizacao", "DataCadastro", "Email", "Nome", "Telefone", "UsuarioId" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), "111.111.111-11", null, new DateTime(2026, 10, 20, 3, 13, 55, 522, DateTimeKind.Utc).AddTicks(136), "joao.silva@barberpro.com.br", "João Carlos Silva", "(51) 99991-0001", new Guid("90000000-0000-0000-0000-000000000001") },
                    { new Guid("30000000-0000-0000-0000-000000000002"), "222.222.222-22", null, new DateTime(2026, 10, 20, 3, 13, 55, 522, DateTimeKind.Utc).AddTicks(1516), "marcos.oliveira@barberpro.com.br", "Marcos Oliveira", "(51) 99991-0002", new Guid("90000000-0000-0000-0000-000000000002") },
                    { new Guid("30000000-0000-0000-0000-000000000003"), "333.333.333-33", null, new DateTime(2026, 10, 20, 3, 13, 55, 522, DateTimeKind.Utc).AddTicks(1519), "pedro.souza@barberpro.com.br", "Pedro Henrique Souza", "(51) 99991-0003", new Guid("90000000-0000-0000-0000-000000000003") },
                    { new Guid("30000000-0000-0000-0000-000000000004"), "444.444.444-44", null, new DateTime(2026, 10, 20, 3, 13, 55, 522, DateTimeKind.Utc).AddTicks(1521), "lucas.ferreira@barberpro.com.br", "Lucas Ferreira", "(51) 99991-0004", new Guid("90000000-0000-0000-0000-000000000004") },
                    { new Guid("30000000-0000-0000-0000-000000000005"), "555.555.555-55", null, new DateTime(2026, 10, 20, 3, 13, 55, 522, DateTimeKind.Utc).AddTicks(1522), "rafael.almeida@barberpro.com.br", "Rafael Almeida", "(51) 99991-0005", new Guid("90000000-0000-0000-0000-000000000005") },
                    { new Guid("30000000-0000-0000-0000-000000000006"), "666.666.666-66", null, new DateTime(2026, 10, 20, 3, 13, 55, 522, DateTimeKind.Utc).AddTicks(1524), "gabriel.martins@barberpro.com.br", "Gabriel Martins", "(51) 99991-0006", new Guid("90000000-0000-0000-0000-000000000006") }
                });

            migrationBuilder.InsertData(
                schema: "Saas",
                table: "Empresas",
                columns: new[] { "Id", "Ativa", "Cnpj", "DataAtualizacao", "DataBloqueio", "DataCadastro", "Email", "LogoUrl", "MotivoBloqueio", "NomeFantasia", "RazaoSocial", "ReferenciaClienteAsaas", "ResponsavelId", "Telefone" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), 1, "85.804.648/0001-73", null, null, new DateTime(2026, 10, 20, 3, 13, 55, 520, DateTimeKind.Utc).AddTicks(7147), "contato@barbeariavintage.com.br", null, null, "Barbearia Vintage", "Barbearia Vintage LTDA", null, new Guid("30000000-0000-0000-0000-000000000001"), "(51) 3333-1111" },
                    { new Guid("50000000-0000-0000-0000-000000000002"), 1, "80.856.888/0001-06", null, null, new DateTime(2026, 10, 20, 3, 13, 55, 521, DateTimeKind.Utc).AddTicks(1243), "contato@oldschoolbarber.com.br", null, null, "Old School Barber", "Old School Barber LTDA", null, new Guid("30000000-0000-0000-0000-000000000002"), "(51) 3333-2222" },
                    { new Guid("50000000-0000-0000-0000-000000000003"), 1, "21.416.493/0001-15", null, null, new DateTime(2026, 10, 20, 3, 13, 55, 521, DateTimeKind.Utc).AddTicks(1247), "contato@blackbeard.com.br", null, null, "Black Beard Barber", "Black Beard Barber LTDA", null, new Guid("30000000-0000-0000-0000-000000000003"), "(51) 3333-3333" },
                    { new Guid("50000000-0000-0000-0000-000000000004"), 1, "01.985.559/0001-80", null, null, new DateTime(2026, 10, 20, 3, 13, 55, 521, DateTimeKind.Utc).AddTicks(1249), "contato@imperiobarber.com.br", null, null, "Império Barber", "Império Barber LTDA", null, new Guid("30000000-0000-0000-0000-000000000004"), "(51) 3333-4444" },
                    { new Guid("50000000-0000-0000-0000-000000000005"), 1, "38.170.265/0001-00", null, null, new DateTime(2026, 10, 20, 3, 13, 55, 521, DateTimeKind.Utc).AddTicks(1264), "contato@domcorte.com.br", null, null, "Dom Corte", "Dom Corte LTDA", null, new Guid("30000000-0000-0000-0000-000000000005"), "(51) 3333-5555" },
                    { new Guid("50000000-0000-0000-0000-000000000006"), 1, "76.676.405/0001-06", null, null, new DateTime(2026, 10, 20, 3, 13, 55, 521, DateTimeKind.Utc).AddTicks(1267), "contato@primebarber.com.br", null, null, "Prime Barber Club", "Prime Barber Club LTDA", null, new Guid("30000000-0000-0000-0000-000000000006"), "(51) 3333-6666" }
                });

            migrationBuilder.InsertData(
                schema: "Saas",
                table: "PlanoModulos",
                columns: new[] { "ModuloId", "PlanoId" },
                values: new object[,]
                {
                    { new Guid("70000000-0000-0000-0000-000000000001"), new Guid("20000000-0000-0000-0000-000000000001") },
                    { new Guid("70000000-0000-0000-0000-000000000002"), new Guid("20000000-0000-0000-0000-000000000001") },
                    { new Guid("70000000-0000-0000-0000-000000000003"), new Guid("20000000-0000-0000-0000-000000000001") },
                    { new Guid("70000000-0000-0000-0000-000000000004"), new Guid("20000000-0000-0000-0000-000000000001") },
                    { new Guid("70000000-0000-0000-0000-000000000013"), new Guid("20000000-0000-0000-0000-000000000001") },
                    { new Guid("70000000-0000-0000-0000-000000000001"), new Guid("20000000-0000-0000-0000-000000000002") },
                    { new Guid("70000000-0000-0000-0000-000000000002"), new Guid("20000000-0000-0000-0000-000000000002") },
                    { new Guid("70000000-0000-0000-0000-000000000003"), new Guid("20000000-0000-0000-0000-000000000002") },
                    { new Guid("70000000-0000-0000-0000-000000000004"), new Guid("20000000-0000-0000-0000-000000000002") },
                    { new Guid("70000000-0000-0000-0000-000000000013"), new Guid("20000000-0000-0000-0000-000000000002") },
                    { new Guid("70000000-0000-0000-0000-000000000001"), new Guid("20000000-0000-0000-0000-000000000003") },
                    { new Guid("70000000-0000-0000-0000-000000000002"), new Guid("20000000-0000-0000-0000-000000000003") },
                    { new Guid("70000000-0000-0000-0000-000000000003"), new Guid("20000000-0000-0000-0000-000000000003") },
                    { new Guid("70000000-0000-0000-0000-000000000004"), new Guid("20000000-0000-0000-0000-000000000003") },
                    { new Guid("70000000-0000-0000-0000-000000000005"), new Guid("20000000-0000-0000-0000-000000000003") },
                    { new Guid("70000000-0000-0000-0000-000000000006"), new Guid("20000000-0000-0000-0000-000000000003") },
                    { new Guid("70000000-0000-0000-0000-000000000013"), new Guid("20000000-0000-0000-0000-000000000003") },
                    { new Guid("70000000-0000-0000-0000-000000000001"), new Guid("20000000-0000-0000-0000-000000000004") },
                    { new Guid("70000000-0000-0000-0000-000000000002"), new Guid("20000000-0000-0000-0000-000000000004") },
                    { new Guid("70000000-0000-0000-0000-000000000003"), new Guid("20000000-0000-0000-0000-000000000004") },
                    { new Guid("70000000-0000-0000-0000-000000000004"), new Guid("20000000-0000-0000-0000-000000000004") },
                    { new Guid("70000000-0000-0000-0000-000000000005"), new Guid("20000000-0000-0000-0000-000000000004") },
                    { new Guid("70000000-0000-0000-0000-000000000006"), new Guid("20000000-0000-0000-0000-000000000004") },
                    { new Guid("70000000-0000-0000-0000-000000000007"), new Guid("20000000-0000-0000-0000-000000000004") },
                    { new Guid("70000000-0000-0000-0000-000000000008"), new Guid("20000000-0000-0000-0000-000000000004") },
                    { new Guid("70000000-0000-0000-0000-000000000009"), new Guid("20000000-0000-0000-0000-000000000004") },
                    { new Guid("70000000-0000-0000-0000-000000000012"), new Guid("20000000-0000-0000-0000-000000000004") },
                    { new Guid("70000000-0000-0000-0000-000000000013"), new Guid("20000000-0000-0000-0000-000000000004") },
                    { new Guid("70000000-0000-0000-0000-000000000001"), new Guid("20000000-0000-0000-0000-000000000005") },
                    { new Guid("70000000-0000-0000-0000-000000000002"), new Guid("20000000-0000-0000-0000-000000000005") },
                    { new Guid("70000000-0000-0000-0000-000000000003"), new Guid("20000000-0000-0000-0000-000000000005") },
                    { new Guid("70000000-0000-0000-0000-000000000004"), new Guid("20000000-0000-0000-0000-000000000005") },
                    { new Guid("70000000-0000-0000-0000-000000000005"), new Guid("20000000-0000-0000-0000-000000000005") },
                    { new Guid("70000000-0000-0000-0000-000000000006"), new Guid("20000000-0000-0000-0000-000000000005") },
                    { new Guid("70000000-0000-0000-0000-000000000007"), new Guid("20000000-0000-0000-0000-000000000005") },
                    { new Guid("70000000-0000-0000-0000-000000000008"), new Guid("20000000-0000-0000-0000-000000000005") },
                    { new Guid("70000000-0000-0000-0000-000000000009"), new Guid("20000000-0000-0000-0000-000000000005") },
                    { new Guid("70000000-0000-0000-0000-000000000010"), new Guid("20000000-0000-0000-0000-000000000005") },
                    { new Guid("70000000-0000-0000-0000-000000000011"), new Guid("20000000-0000-0000-0000-000000000005") },
                    { new Guid("70000000-0000-0000-0000-000000000012"), new Guid("20000000-0000-0000-0000-000000000005") },
                    { new Guid("70000000-0000-0000-0000-000000000013"), new Guid("20000000-0000-0000-0000-000000000005") },
                    { new Guid("70000000-0000-0000-0000-000000000001"), new Guid("20000000-0000-0000-0000-000000000006") },
                    { new Guid("70000000-0000-0000-0000-000000000002"), new Guid("20000000-0000-0000-0000-000000000006") },
                    { new Guid("70000000-0000-0000-0000-000000000003"), new Guid("20000000-0000-0000-0000-000000000006") },
                    { new Guid("70000000-0000-0000-0000-000000000004"), new Guid("20000000-0000-0000-0000-000000000006") },
                    { new Guid("70000000-0000-0000-0000-000000000005"), new Guid("20000000-0000-0000-0000-000000000006") },
                    { new Guid("70000000-0000-0000-0000-000000000006"), new Guid("20000000-0000-0000-0000-000000000006") },
                    { new Guid("70000000-0000-0000-0000-000000000007"), new Guid("20000000-0000-0000-0000-000000000006") },
                    { new Guid("70000000-0000-0000-0000-000000000008"), new Guid("20000000-0000-0000-0000-000000000006") },
                    { new Guid("70000000-0000-0000-0000-000000000009"), new Guid("20000000-0000-0000-0000-000000000006") },
                    { new Guid("70000000-0000-0000-0000-000000000010"), new Guid("20000000-0000-0000-0000-000000000006") },
                    { new Guid("70000000-0000-0000-0000-000000000011"), new Guid("20000000-0000-0000-0000-000000000006") },
                    { new Guid("70000000-0000-0000-0000-000000000012"), new Guid("20000000-0000-0000-0000-000000000006") },
                    { new Guid("70000000-0000-0000-0000-000000000013"), new Guid("20000000-0000-0000-0000-000000000006") }
                });

            migrationBuilder.InsertData(
                schema: "Saas",
                table: "Assinaturas",
                columns: new[] { "Id", "Ativa", "DataAtualizacao", "DataCadastro", "DataCancelamento", "DataFimTrial", "DataInicio", "DataUltimoPagamento", "DataVencimento", "DiaVencimento", "EmpresaId", "PeriodicidadeCobranca", "PlanoId", "RenovacaoAutomatica", "Trial", "Valor" },
                values: new object[,]
                {
                    { new Guid("60000000-0000-0000-0000-000000000001"), 2, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 20, 3, 13, 55, 523, DateTimeKind.Utc).AddTicks(4701), null, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000001"), 1, new Guid("20000000-0000-0000-0000-000000000002"), false, false, 49.90m },
                    { new Guid("60000000-0000-0000-0000-000000000002"), 5, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 20, 3, 13, 55, 523, DateTimeKind.Utc).AddTicks(6954), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000002"), 1, new Guid("20000000-0000-0000-0000-000000000001"), false, true, 0m },
                    { new Guid("60000000-0000-0000-0000-000000000003"), 2, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 20, 3, 13, 55, 523, DateTimeKind.Utc).AddTicks(6969), null, null, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000003"), 1, new Guid("20000000-0000-0000-0000-000000000003"), true, false, 0m },
                    { new Guid("60000000-0000-0000-0000-000000000004"), 2, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 20, 3, 13, 55, 523, DateTimeKind.Utc).AddTicks(6975), null, null, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000004"), 1, new Guid("20000000-0000-0000-0000-000000000004"), true, false, 0m },
                    { new Guid("60000000-0000-0000-0000-000000000005"), 2, null, new DateTime(2026, 10, 20, 3, 13, 55, 523, DateTimeKind.Utc).AddTicks(6996), null, null, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("50000000-0000-0000-0000-000000000005"), 1, new Guid("20000000-0000-0000-0000-000000000005"), true, false, 249.90m },
                    { new Guid("60000000-0000-0000-0000-000000000006"), 2, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 20, 3, 13, 55, 523, DateTimeKind.Utc).AddTicks(7003), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000006"), 1, new Guid("20000000-0000-0000-0000-000000000006"), true, false, 499.90m }
                });

            migrationBuilder.InsertData(
                schema: "Saas",
                table: "EmpresaModulos",
                columns: new[] { "EmpresaId", "ModuloId" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), new Guid("70000000-0000-0000-0000-000000000001") },
                    { new Guid("50000000-0000-0000-0000-000000000001"), new Guid("70000000-0000-0000-0000-000000000002") },
                    { new Guid("50000000-0000-0000-0000-000000000001"), new Guid("70000000-0000-0000-0000-000000000003") },
                    { new Guid("50000000-0000-0000-0000-000000000001"), new Guid("70000000-0000-0000-0000-000000000004") },
                    { new Guid("50000000-0000-0000-0000-000000000001"), new Guid("70000000-0000-0000-0000-000000000013") },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new Guid("70000000-0000-0000-0000-000000000001") },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new Guid("70000000-0000-0000-0000-000000000002") },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new Guid("70000000-0000-0000-0000-000000000003") },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new Guid("70000000-0000-0000-0000-000000000004") },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new Guid("70000000-0000-0000-0000-000000000013") },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new Guid("70000000-0000-0000-0000-000000000001") },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new Guid("70000000-0000-0000-0000-000000000002") },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new Guid("70000000-0000-0000-0000-000000000003") },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new Guid("70000000-0000-0000-0000-000000000004") },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new Guid("70000000-0000-0000-0000-000000000005") },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new Guid("70000000-0000-0000-0000-000000000006") },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new Guid("70000000-0000-0000-0000-000000000013") },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("70000000-0000-0000-0000-000000000001") },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("70000000-0000-0000-0000-000000000002") },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("70000000-0000-0000-0000-000000000003") },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("70000000-0000-0000-0000-000000000004") },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("70000000-0000-0000-0000-000000000005") },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("70000000-0000-0000-0000-000000000006") },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("70000000-0000-0000-0000-000000000007") },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("70000000-0000-0000-0000-000000000008") },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("70000000-0000-0000-0000-000000000009") },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("70000000-0000-0000-0000-000000000012") },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("70000000-0000-0000-0000-000000000013") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("70000000-0000-0000-0000-000000000001") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("70000000-0000-0000-0000-000000000002") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("70000000-0000-0000-0000-000000000003") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("70000000-0000-0000-0000-000000000004") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("70000000-0000-0000-0000-000000000005") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("70000000-0000-0000-0000-000000000006") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("70000000-0000-0000-0000-000000000007") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("70000000-0000-0000-0000-000000000008") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("70000000-0000-0000-0000-000000000009") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("70000000-0000-0000-0000-000000000010") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("70000000-0000-0000-0000-000000000011") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("70000000-0000-0000-0000-000000000012") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("70000000-0000-0000-0000-000000000013") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("70000000-0000-0000-0000-000000000001") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("70000000-0000-0000-0000-000000000002") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("70000000-0000-0000-0000-000000000003") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("70000000-0000-0000-0000-000000000004") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("70000000-0000-0000-0000-000000000005") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("70000000-0000-0000-0000-000000000006") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("70000000-0000-0000-0000-000000000007") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("70000000-0000-0000-0000-000000000008") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("70000000-0000-0000-0000-000000000009") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("70000000-0000-0000-0000-000000000010") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("70000000-0000-0000-0000-000000000011") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("70000000-0000-0000-0000-000000000012") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("70000000-0000-0000-0000-000000000013") }
                });

            migrationBuilder.InsertData(
                schema: "Saas",
                table: "Faturas",
                columns: new[] { "Id", "AssinaturaId", "DataAtualizacao", "DataCadastro", "DataPagamento", "PeriodoFim", "PeriodoInicio", "ReferenciaGateway", "Status", "Valor", "Vencimento" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 20, 3, 13, 55, 524, DateTimeKind.Utc).AddTicks(2156), new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TESTE-FATURA-001", 2, 99.90m, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 20, 3, 13, 55, 524, DateTimeKind.Utc).AddTicks(3427), new DateTime(2026, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TESTE-FATURA-002", 2, 99.90m, new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 20, 3, 13, 55, 524, DateTimeKind.Utc).AddTicks(3432), null, new DateTime(2026, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 99.90m, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 20, 3, 13, 55, 524, DateTimeKind.Utc).AddTicks(3435), null, new DateTime(2026, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, 99.90m, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 20, 3, 13, 55, 524, DateTimeKind.Utc).AddTicks(3445), null, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TESTE-FATURA-005", 5, 99.90m, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 20, 3, 13, 55, 524, DateTimeKind.Utc).AddTicks(3448), null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TESTE-FATURA-006", 6, 99.90m, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                schema: "Saas",
                table: "TransacoesPagamento",
                columns: new[] { "Id", "ChaveIdempotencia", "CodigoErro", "DataAtualizacao", "DataCadastro", "DataProcessamento", "FaturaId", "MensagemErro", "MetodoPagamento", "Provedor", "ReferenciaClienteAsaas", "ReferenciaGateway", "Status", "Valor" },
                values: new object[,]
                {
                    { new Guid("70000000-0000-0000-0000-000000000001"), "SEED-FATURA-001-TENTATIVA-001", null, null, new DateTime(2026, 1, 8, 11, 55, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 8, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000001"), null, 0, "MercadoPago", null, "TESTE-TRANSACAO-001", 4, 99.90m },
                    { new Guid("70000000-0000-0000-0000-000000000002"), "SEED-FATURA-002-TENTATIVA-001", null, null, new DateTime(2026, 2, 9, 11, 55, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 9, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000002"), null, 0, "MercadoPago", null, "TESTE-TRANSACAO-002", 4, 99.90m },
                    { new Guid("70000000-0000-0000-0000-000000000003"), "SEED-FATURA-003-TENTATIVA-001", "PAYMENT_DECLINED", null, new DateTime(2026, 3, 10, 11, 55, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 10, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000003"), "Pagamento recusado no cenário de teste.", 0, "MercadoPago", null, null, 5, 99.90m },
                    { new Guid("70000000-0000-0000-0000-000000000004"), "SEED-FATURA-004-TENTATIVA-001", null, null, new DateTime(2026, 4, 10, 11, 55, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 10, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000004"), null, 0, "MercadoPago", null, null, 6, 99.90m },
                    { new Guid("70000000-0000-0000-0000-000000000005"), "SEED-FATURA-005-TENTATIVA-001", null, null, new DateTime(2026, 5, 10, 11, 55, 0, 0, DateTimeKind.Utc), null, new Guid("50000000-0000-0000-0000-000000000005"), null, 0, "MercadoPago", null, "TESTE-TRANSACAO-005", 3, 99.90m },
                    { new Guid("70000000-0000-0000-0000-000000000006"), "SEED-FATURA-006-TENTATIVA-001", null, null, new DateTime(2026, 6, 10, 11, 55, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 10, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000006"), null, 0, "MercadoPago", null, "TESTE-TRANSACAO-006", 8, 99.90m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assinaturas_EmpresaId",
                schema: "Saas",
                table: "Assinaturas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Assinaturas_PlanoId",
                schema: "Saas",
                table: "Assinaturas",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaModulos_ModuloId",
                schema: "Saas",
                table: "EmpresaModulos",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_ResponsavelId",
                schema: "Saas",
                table: "Empresas",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_AssinaturaId_PeriodoInicio_PeriodoFim",
                schema: "Saas",
                table: "Faturas",
                columns: new[] { "AssinaturaId", "PeriodoInicio", "PeriodoFim" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_ReferenciaGateway",
                schema: "Saas",
                table: "Faturas",
                column: "ReferenciaGateway",
                unique: true,
                filter: "[ReferenciaGateway] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_Vencimento",
                schema: "Saas",
                table: "Faturas",
                column: "Vencimento");

            migrationBuilder.CreateIndex(
                name: "IX_PlanoModulos_ModuloId",
                schema: "Saas",
                table: "PlanoModulos",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_Responsaveis_UsuarioId",
                schema: "Saas",
                table: "Responsaveis",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransacoesPagamento_FaturaId_DataCadastro",
                schema: "Saas",
                table: "TransacoesPagamento",
                columns: new[] { "FaturaId", "DataCadastro" });

            migrationBuilder.CreateIndex(
                name: "IX_TransacoesPagamento_Provedor_ChaveIdempotencia",
                schema: "Saas",
                table: "TransacoesPagamento",
                columns: new[] { "Provedor", "ChaveIdempotencia" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransacoesPagamento_Provedor_ReferenciaGateway",
                schema: "Saas",
                table: "TransacoesPagamento",
                columns: new[] { "Provedor", "ReferenciaGateway" },
                unique: true,
                filter: "[ReferenciaGateway] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpresaModulos",
                schema: "Saas");

            migrationBuilder.DropTable(
                name: "PlanoModulos",
                schema: "Saas");

            migrationBuilder.DropTable(
                name: "TransacoesPagamento",
                schema: "Saas");

            migrationBuilder.DropTable(
                name: "Modulo",
                schema: "Saas");

            migrationBuilder.DropTable(
                name: "Faturas",
                schema: "Saas");

            migrationBuilder.DropTable(
                name: "Assinaturas",
                schema: "Saas");

            migrationBuilder.DropTable(
                name: "Empresas",
                schema: "Saas");

            migrationBuilder.DropTable(
                name: "Planos",
                schema: "Saas");

            migrationBuilder.DropTable(
                name: "Responsaveis",
                schema: "Saas");
        }
    }
}
