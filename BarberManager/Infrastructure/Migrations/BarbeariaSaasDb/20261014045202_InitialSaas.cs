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

            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "Id", "Ativa", "Cnpj", "DataAtualizacao", "DataBloqueio", "DataCadastro", "Email", "LogoUrl", "MotivoBloqueio", "NomeFantasia", "RazaoSocial", "Telefone" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), 1, "11.111.111/0001-11", null, null, new DateTime(2026, 10, 14, 4, 52, 1, 441, DateTimeKind.Utc).AddTicks(8417), "contato@barbeariavintage.com.br", null, null, "Barbearia Vintage", "Barbearia Vintage LTDA", "(51) 3333-1111" },
                    { new Guid("50000000-0000-0000-0000-000000000002"), 1, "22.222.222/0001-22", null, null, new DateTime(2026, 10, 14, 4, 52, 1, 442, DateTimeKind.Utc).AddTicks(199), "contato@oldschoolbarber.com.br", null, null, "Old School Barber", "Old School Barber LTDA", "(51) 3333-2222" },
                    { new Guid("50000000-0000-0000-0000-000000000003"), 1, "33.333.333/0001-33", null, null, new DateTime(2026, 10, 14, 4, 52, 1, 442, DateTimeKind.Utc).AddTicks(202), "contato@blackbeard.com.br", null, null, "Black Beard Barber", "Black Beard Barber LTDA", "(51) 3333-3333" },
                    { new Guid("50000000-0000-0000-0000-000000000004"), 1, "44.444.444/0001-44", null, null, new DateTime(2026, 10, 14, 4, 52, 1, 442, DateTimeKind.Utc).AddTicks(204), "contato@imperiobarber.com.br", null, null, "Império Barber", "Império Barber LTDA", "(51) 3333-4444" },
                    { new Guid("50000000-0000-0000-0000-000000000005"), 1, "55.555.555/0001-55", null, null, new DateTime(2026, 10, 14, 4, 52, 1, 442, DateTimeKind.Utc).AddTicks(206), "contato@domcorte.com.br", null, null, "Dom Corte", "Dom Corte LTDA", "(51) 3333-5555" },
                    { new Guid("50000000-0000-0000-0000-000000000006"), 1, "66.666.666/0001-66", null, null, new DateTime(2026, 10, 14, 4, 52, 1, 442, DateTimeKind.Utc).AddTicks(219), "contato@primebarber.com.br", null, null, "Prime Barber Club", "Prime Barber Club LTDA", "(51) 3333-6666" }
                });

            migrationBuilder.InsertData(
                table: "Planos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "LimiteEmpresas", "LimiteProfissionais", "Nome", "ValorAnual", "ValorMensal" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), 1, null, new DateTime(2026, 10, 14, 4, 52, 1, 443, DateTimeKind.Utc).AddTicks(178), 1, 2, "Trial", 0m, 0m },
                    { new Guid("20000000-0000-0000-0000-000000000002"), 1, null, new DateTime(2026, 10, 14, 4, 52, 1, 443, DateTimeKind.Utc).AddTicks(1874), 1, 3, "Starter", 0m, 49.90m },
                    { new Guid("20000000-0000-0000-0000-000000000003"), 1, null, new DateTime(2026, 10, 14, 4, 52, 1, 443, DateTimeKind.Utc).AddTicks(1983), 1, 5, "Basic", 49.90m, 89.90m },
                    { new Guid("20000000-0000-0000-0000-000000000004"), 1, null, new DateTime(2026, 10, 14, 4, 52, 1, 443, DateTimeKind.Utc).AddTicks(1985), 1, 10, "Professional", 130m, 149.90m },
                    { new Guid("20000000-0000-0000-0000-000000000005"), 1, null, new DateTime(2026, 10, 14, 4, 52, 1, 443, DateTimeKind.Utc).AddTicks(1996), 1, 25, "Premium", 211.50m, 249.90m },
                    { new Guid("20000000-0000-0000-0000-000000000006"), 1, null, new DateTime(2026, 10, 14, 4, 52, 1, 443, DateTimeKind.Utc).AddTicks(2004), 999, 999, "Enterprise", 480.65m, 499.90m }
                });

            migrationBuilder.InsertData(
                table: "Assinaturas",
                columns: new[] { "Id", "Ativa", "DataAtualizacao", "DataCadastro", "DataCancelamento", "DataFimTrial", "DataInicio", "DataUltimoPagamento", "DataVencimento", "DiaVencimento", "EmpresaId", "PlanoId", "RenovacaoAutomatica", "Trial", "Valor" },
                values: new object[,]
                {
                    { new Guid("60000000-0000-0000-0000-000000000001"), 2, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 14, 4, 52, 1, 443, DateTimeKind.Utc).AddTicks(7997), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("20000000-0000-0000-0000-000000000001"), false, true, 0m },
                    { new Guid("60000000-0000-0000-0000-000000000002"), 5, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 14, 4, 52, 1, 443, DateTimeKind.Utc).AddTicks(9879), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000002"), new Guid("20000000-0000-0000-0000-000000000002"), true, false, 0m },
                    { new Guid("60000000-0000-0000-0000-000000000003"), 2, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 14, 4, 52, 1, 443, DateTimeKind.Utc).AddTicks(9883), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000003"), new Guid("20000000-0000-0000-0000-000000000003"), true, false, 0m },
                    { new Guid("60000000-0000-0000-0000-000000000004"), 2, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 14, 4, 52, 1, 443, DateTimeKind.Utc).AddTicks(9886), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000004"), new Guid("20000000-0000-0000-0000-000000000004"), true, false, 0m },
                    { new Guid("60000000-0000-0000-0000-000000000005"), 2, null, new DateTime(2026, 10, 14, 4, 52, 1, 443, DateTimeKind.Utc).AddTicks(9889), null, null, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("50000000-0000-0000-0000-000000000005"), new Guid("20000000-0000-0000-0000-000000000005"), true, false, 0m },
                    { new Guid("60000000-0000-0000-0000-000000000006"), 2, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 14, 4, 52, 1, 443, DateTimeKind.Utc).AddTicks(9897), null, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2027, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, new Guid("50000000-0000-0000-0000-000000000006"), new Guid("20000000-0000-0000-0000-000000000006"), true, false, 0m }
                });

            migrationBuilder.InsertData(
                table: "Faturas",
                columns: new[] { "Id", "AssinaturaId", "DataAtualizacao", "DataCadastro", "DataPagamento", "PeriodoFim", "PeriodoInicio", "ReferenciaGateway", "Status", "Valor", "Vencimento" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 14, 4, 52, 1, 444, DateTimeKind.Utc).AddTicks(5843), new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TESTE-FATURA-001", 2, 99.90m, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000002"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 14, 4, 52, 1, 444, DateTimeKind.Utc).AddTicks(7208), new DateTime(2026, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TESTE-FATURA-002", 2, 99.90m, new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000003"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 14, 4, 52, 1, 444, DateTimeKind.Utc).AddTicks(7215), null, new DateTime(2026, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 99.90m, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000004"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 14, 4, 52, 1, 444, DateTimeKind.Utc).AddTicks(7241), null, new DateTime(2026, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 4, 99.90m, new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000005"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 14, 4, 52, 1, 444, DateTimeKind.Utc).AddTicks(7244), null, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TESTE-FATURA-005", 5, 99.90m, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("50000000-0000-0000-0000-000000000006"), new Guid("60000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 14, 4, 52, 1, 444, DateTimeKind.Utc).AddTicks(7246), null, new DateTime(2026, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TESTE-FATURA-006", 6, 99.90m, new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
