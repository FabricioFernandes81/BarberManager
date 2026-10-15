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
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeFantasia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativa = table.Column<int>(type: "int", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataBloqueio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MotivoBloqueio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMensal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorAnual = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LimiteProfissionais = table.Column<int>(type: "int", nullable: false),
                    LimiteEmpresas = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assinaturas",
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
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assinaturas_Planos_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Planos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Faturas",
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
                        principalTable: "Assinaturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransacoesPagamento",
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
                        principalTable: "Faturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "Id", "Ativa", "Cnpj", "DataAtualizacao", "DataBloqueio", "DataCadastro", "Email", "LogoUrl", "MotivoBloqueio", "NomeFantasia", "RazaoSocial", "Telefone" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), 1, "11.111.111/0001-11", null, null, new DateTime(2026, 10, 15, 0, 30, 15, 431, DateTimeKind.Utc).AddTicks(8490), "contato@barbeariavintage.com.br", null, null, "Barbearia Vintage", "Barbearia Vintage LTDA", "(51) 3333-1111" },
                    { new Guid("50000000-0000-0000-0000-000000000002"), 1, "22.222.222/0001-22", null, null, new DateTime(2026, 10, 15, 0, 30, 15, 432, DateTimeKind.Utc).AddTicks(119), "contato@oldschoolbarber.com.br", null, null, "Old School Barber", "Old School Barber LTDA", "(51) 3333-2222" },
                    { new Guid("50000000-0000-0000-0000-000000000003"), 1, "33.333.333/0001-33", null, null, new DateTime(2026, 10, 15, 0, 30, 15, 432, DateTimeKind.Utc).AddTicks(122), "contato@blackbeard.com.br", null, null, "Black Beard Barber", "Black Beard Barber LTDA", "(51) 3333-3333" },
                    { new Guid("50000000-0000-0000-0000-000000000004"), 1, "44.444.444/0001-44", null, null, new DateTime(2026, 10, 15, 0, 30, 15, 432, DateTimeKind.Utc).AddTicks(124), "contato@imperiobarber.com.br", null, null, "Império Barber", "Império Barber LTDA", "(51) 3333-4444" },
                    { new Guid("50000000-0000-0000-0000-000000000005"), 1, "55.555.555/0001-55", null, null, new DateTime(2026, 10, 15, 0, 30, 15, 432, DateTimeKind.Utc).AddTicks(126), "contato@domcorte.com.br", null, null, "Dom Corte", "Dom Corte LTDA", "(51) 3333-5555" },
                    { new Guid("50000000-0000-0000-0000-000000000006"), 1, "66.666.666/0001-66", null, null, new DateTime(2026, 10, 15, 0, 30, 15, 432, DateTimeKind.Utc).AddTicks(136), "contato@primebarber.com.br", null, null, "Prime Barber Club", "Prime Barber Club LTDA", "(51) 3333-6666" }
                });

            migrationBuilder.InsertData(
                table: "Planos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "LimiteEmpresas", "LimiteProfissionais", "Nome", "ValorAnual", "ValorMensal" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), 1, null, new DateTime(2026, 10, 15, 0, 30, 15, 433, DateTimeKind.Utc).AddTicks(2540), 1, 2, "Trial", 0m, 0m },
                    { new Guid("20000000-0000-0000-0000-000000000002"), 1, null, new DateTime(2026, 10, 15, 0, 30, 15, 433, DateTimeKind.Utc).AddTicks(4237), 1, 3, "Starter", 0m, 49.90m },
                    { new Guid("20000000-0000-0000-0000-000000000003"), 1, null, new DateTime(2026, 10, 15, 0, 30, 15, 433, DateTimeKind.Utc).AddTicks(4351), 1, 5, "Basic", 49.90m, 89.90m },
                    { new Guid("20000000-0000-0000-0000-000000000004"), 1, null, new DateTime(2026, 10, 15, 0, 30, 15, 433, DateTimeKind.Utc).AddTicks(4353), 1, 10, "Professional", 130m, 149.90m },
                    { new Guid("20000000-0000-0000-0000-000000000005"), 1, null, new DateTime(2026, 10, 15, 0, 30, 15, 433, DateTimeKind.Utc).AddTicks(4361), 1, 25, "Premium", 211.50m, 249.90m },
                    { new Guid("20000000-0000-0000-0000-000000000006"), 1, null, new DateTime(2026, 10, 15, 0, 30, 15, 433, DateTimeKind.Utc).AddTicks(4369), 999, 999, "Enterprise", 480.65m, 499.90m }
                });

            migrationBuilder.InsertData(
                table: "Assinaturas",
                columns: new[] { "Id", "Ativa", "DataAtualizacao", "DataCadastro", "DataCancelamento", "DataFimTrial", "DataInicio", "DataUltimoPagamento", "DataVencimento", "DiaVencimento", "EmpresaId", "PeriodicidadeCobranca", "PlanoId", "RenovacaoAutomatica", "Trial", "Valor" },
                values: new object[,]
                {
                    { new Guid("60000000-0000-0000-0000-000000000001"), 2, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 15, 0, 30, 15, 434, DateTimeKind.Utc).AddTicks(219), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000001"), 1, new Guid("20000000-0000-0000-0000-000000000002"), false, false, 0m },
                    { new Guid("60000000-0000-0000-0000-000000000002"), 5, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 15, 0, 30, 15, 434, DateTimeKind.Utc).AddTicks(2412), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000002"), 1, new Guid("20000000-0000-0000-0000-000000000001"), true, false, 0m },
                    { new Guid("60000000-0000-0000-0000-000000000003"), 2, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 15, 0, 30, 15, 434, DateTimeKind.Utc).AddTicks(2416), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000003"), 1, new Guid("20000000-0000-0000-0000-000000000003"), true, false, 0m },
                    { new Guid("60000000-0000-0000-0000-000000000004"), 2, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 15, 0, 30, 15, 434, DateTimeKind.Utc).AddTicks(2420), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000004"), 1, new Guid("20000000-0000-0000-0000-000000000004"), true, false, 0m },
                    { new Guid("60000000-0000-0000-0000-000000000005"), 2, null, new DateTime(2026, 10, 15, 0, 30, 15, 434, DateTimeKind.Utc).AddTicks(2422), null, null, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("50000000-0000-0000-0000-000000000005"), 1, new Guid("20000000-0000-0000-0000-000000000005"), true, false, 0m },
                    { new Guid("60000000-0000-0000-0000-000000000006"), 2, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 15, 0, 30, 15, 434, DateTimeKind.Utc).AddTicks(2431), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000006"), 1, new Guid("20000000-0000-0000-0000-000000000006"), true, false, 0m }
                });

            migrationBuilder.InsertData(
                table: "Faturas",
                columns: new[] { "Id", "AssinaturaId", "DataAtualizacao", "DataCadastro", "DataPagamento", "PeriodoFim", "PeriodoInicio", "ReferenciaGateway", "Status", "Valor", "Vencimento" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 15, 0, 30, 15, 434, DateTimeKind.Utc).AddTicks(8195), new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TESTE-FATURA-001", 2, 99.90m, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 15, 0, 30, 15, 434, DateTimeKind.Utc).AddTicks(9732), new DateTime(2026, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TESTE-FATURA-002", 2, 99.90m, new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 15, 0, 30, 15, 434, DateTimeKind.Utc).AddTicks(9740), null, new DateTime(2026, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 99.90m, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 15, 0, 30, 15, 434, DateTimeKind.Utc).AddTicks(9750), null, new DateTime(2026, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, 99.90m, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 15, 0, 30, 15, 434, DateTimeKind.Utc).AddTicks(9753), null, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TESTE-FATURA-005", 5, 99.90m, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 15, 0, 30, 15, 434, DateTimeKind.Utc).AddTicks(9755), null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TESTE-FATURA-006", 6, 99.90m, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
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
                table: "Assinaturas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Assinaturas_PlanoId",
                table: "Assinaturas",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_AssinaturaId_PeriodoInicio_PeriodoFim",
                table: "Faturas",
                columns: new[] { "AssinaturaId", "PeriodoInicio", "PeriodoFim" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_ReferenciaGateway",
                table: "Faturas",
                column: "ReferenciaGateway",
                unique: true,
                filter: "[ReferenciaGateway] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_Vencimento",
                table: "Faturas",
                column: "Vencimento");

            migrationBuilder.CreateIndex(
                name: "IX_TransacoesPagamento_FaturaId_DataCadastro",
                table: "TransacoesPagamento",
                columns: new[] { "FaturaId", "DataCadastro" });

            migrationBuilder.CreateIndex(
                name: "IX_TransacoesPagamento_Provedor_ChaveIdempotencia",
                table: "TransacoesPagamento",
                columns: new[] { "Provedor", "ChaveIdempotencia" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransacoesPagamento_Provedor_ReferenciaGateway",
                table: "TransacoesPagamento",
                columns: new[] { "Provedor", "ReferenciaGateway" },
                unique: true,
                filter: "[ReferenciaGateway] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransacoesPagamento");

            migrationBuilder.DropTable(
                name: "Faturas");

            migrationBuilder.DropTable(
                name: "Assinaturas");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Planos");
        }
    }
}
