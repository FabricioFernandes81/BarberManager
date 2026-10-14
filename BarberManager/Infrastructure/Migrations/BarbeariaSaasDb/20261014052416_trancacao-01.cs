using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaSaasDb
{
    /// <inheritdoc />
    public partial class trancacao01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransacoesPagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Provedor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReferenciaGateway = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ChaveIdempotencia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataProcessamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CodigoErro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MensagemErro = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
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

            migrationBuilder.UpdateData(
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 109, DateTimeKind.Utc).AddTicks(3904));

            migrationBuilder.UpdateData(
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 109, DateTimeKind.Utc).AddTicks(5930));

            migrationBuilder.UpdateData(
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 109, DateTimeKind.Utc).AddTicks(5935));

            migrationBuilder.UpdateData(
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 109, DateTimeKind.Utc).AddTicks(5939));

            migrationBuilder.UpdateData(
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 109, DateTimeKind.Utc).AddTicks(5958));

            migrationBuilder.UpdateData(
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 109, DateTimeKind.Utc).AddTicks(5968));

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 107, DateTimeKind.Utc).AddTicks(4312));

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 107, DateTimeKind.Utc).AddTicks(6408));

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 107, DateTimeKind.Utc).AddTicks(6411));

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 107, DateTimeKind.Utc).AddTicks(6413));

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 107, DateTimeKind.Utc).AddTicks(6415));

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 107, DateTimeKind.Utc).AddTicks(6417));

            migrationBuilder.UpdateData(
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 110, DateTimeKind.Utc).AddTicks(5909));

            migrationBuilder.UpdateData(
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 110, DateTimeKind.Utc).AddTicks(7273));

            migrationBuilder.UpdateData(
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 110, DateTimeKind.Utc).AddTicks(7279));

            migrationBuilder.UpdateData(
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 110, DateTimeKind.Utc).AddTicks(7282));

            migrationBuilder.UpdateData(
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 110, DateTimeKind.Utc).AddTicks(7285));

            migrationBuilder.UpdateData(
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 110, DateTimeKind.Utc).AddTicks(7287));

            migrationBuilder.UpdateData(
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 108, DateTimeKind.Utc).AddTicks(6185));

            migrationBuilder.UpdateData(
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 108, DateTimeKind.Utc).AddTicks(7934));

            migrationBuilder.UpdateData(
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 108, DateTimeKind.Utc).AddTicks(8042));

            migrationBuilder.UpdateData(
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 108, DateTimeKind.Utc).AddTicks(8044));

            migrationBuilder.UpdateData(
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 108, DateTimeKind.Utc).AddTicks(8057));

            migrationBuilder.UpdateData(
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 24, 16, 108, DateTimeKind.Utc).AddTicks(8066));

            migrationBuilder.InsertData(
                table: "TransacoesPagamento",
                columns: new[] { "Id", "ChaveIdempotencia", "CodigoErro", "DataAtualizacao", "DataCadastro", "DataProcessamento", "FaturaId", "MensagemErro", "Provedor", "ReferenciaGateway", "Status", "Valor" },
                values: new object[,]
                {
                    { new Guid("70000000-0000-0000-0000-000000000001"), "SEED-FATURA-001-TENTATIVA-001", null, null, new DateTime(2026, 1, 8, 11, 55, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 8, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000001"), null, "MercadoPago", "TESTE-TRANSACAO-001", 4, 99.90m },
                    { new Guid("70000000-0000-0000-0000-000000000002"), "SEED-FATURA-002-TENTATIVA-001", null, null, new DateTime(2026, 2, 9, 11, 55, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 9, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000002"), null, "MercadoPago", "TESTE-TRANSACAO-002", 4, 99.90m },
                    { new Guid("70000000-0000-0000-0000-000000000003"), "SEED-FATURA-003-TENTATIVA-001", "PAYMENT_DECLINED", null, new DateTime(2026, 3, 10, 11, 55, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 10, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000003"), "Pagamento recusado no cenário de teste.", "MercadoPago", null, 5, 99.90m },
                    { new Guid("70000000-0000-0000-0000-000000000004"), "SEED-FATURA-004-TENTATIVA-001", null, null, new DateTime(2026, 4, 10, 11, 55, 0, 0, DateTimeKind.Utc), new DateTime(2026, 4, 10, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000004"), null, "MercadoPago", null, 6, 99.90m },
                    { new Guid("70000000-0000-0000-0000-000000000005"), "SEED-FATURA-005-TENTATIVA-001", null, null, new DateTime(2026, 5, 10, 11, 55, 0, 0, DateTimeKind.Utc), null, new Guid("50000000-0000-0000-0000-000000000005"), null, "MercadoPago", "TESTE-TRANSACAO-005", 3, 99.90m },
                    { new Guid("70000000-0000-0000-0000-000000000006"), "SEED-FATURA-006-TENTATIVA-001", null, null, new DateTime(2026, 6, 10, 11, 55, 0, 0, DateTimeKind.Utc), new DateTime(2026, 6, 10, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("50000000-0000-0000-0000-000000000006"), null, "MercadoPago", "TESTE-TRANSACAO-006", 8, 99.90m }
                });

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

            migrationBuilder.UpdateData(
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 856, DateTimeKind.Utc).AddTicks(5329));

            migrationBuilder.UpdateData(
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 856, DateTimeKind.Utc).AddTicks(7249));

            migrationBuilder.UpdateData(
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 856, DateTimeKind.Utc).AddTicks(7253));

            migrationBuilder.UpdateData(
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 856, DateTimeKind.Utc).AddTicks(7257));

            migrationBuilder.UpdateData(
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 856, DateTimeKind.Utc).AddTicks(7267));

            migrationBuilder.UpdateData(
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 856, DateTimeKind.Utc).AddTicks(7276));

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 854, DateTimeKind.Utc).AddTicks(2879));

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 854, DateTimeKind.Utc).AddTicks(5037));

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 854, DateTimeKind.Utc).AddTicks(5040));

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 854, DateTimeKind.Utc).AddTicks(5041));

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 854, DateTimeKind.Utc).AddTicks(5043));

            migrationBuilder.UpdateData(
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 854, DateTimeKind.Utc).AddTicks(5045));

            migrationBuilder.UpdateData(
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 857, DateTimeKind.Utc).AddTicks(2859));

            migrationBuilder.UpdateData(
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 857, DateTimeKind.Utc).AddTicks(4469));

            migrationBuilder.UpdateData(
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 857, DateTimeKind.Utc).AddTicks(4476));

            migrationBuilder.UpdateData(
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 857, DateTimeKind.Utc).AddTicks(4479));

            migrationBuilder.UpdateData(
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 857, DateTimeKind.Utc).AddTicks(4482));

            migrationBuilder.UpdateData(
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 857, DateTimeKind.Utc).AddTicks(4484));

            migrationBuilder.UpdateData(
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 855, DateTimeKind.Utc).AddTicks(6188));

            migrationBuilder.UpdateData(
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 855, DateTimeKind.Utc).AddTicks(8243));

            migrationBuilder.UpdateData(
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 855, DateTimeKind.Utc).AddTicks(8392));

            migrationBuilder.UpdateData(
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 855, DateTimeKind.Utc).AddTicks(8394));

            migrationBuilder.UpdateData(
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 855, DateTimeKind.Utc).AddTicks(8403));

            migrationBuilder.UpdateData(
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 46, 855, DateTimeKind.Utc).AddTicks(8414));
        }
    }
}
