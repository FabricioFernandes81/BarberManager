using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class CriarCobrancasOperacionais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("0e3ec0a0-7162-4ca0-b949-aba15b5ca8b8"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("560c3fff-53aa-4b9f-acbd-bbfbf0be0792"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("66a4ce67-fb2a-4c18-a71a-18124b6fd942"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("79c44013-2f22-48d3-9d79-85a47d42c978"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("892d4b57-0bf5-47c9-8305-073bf7a98290"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("b7a4f942-bd5b-4b4a-8e6b-ec13360a85ab"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("bf5aafd6-ba83-4d88-8b3a-bd0d0b9dad66"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("e70ae7cd-4e6a-45c4-ac90-d87632d8ef38"));

            migrationBuilder.CreateTable(
                name: "CobrancasOperacionais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContaReceberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gateway = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ExternalReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GatewayPagamentoId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ChaveIdempotencia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CriadaEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConfirmadaEmUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobrancasOperacionais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CobrancasOperacionais_ContaReceber_ContaReceberId",
                        column: x => x.ContaReceberId,
                        principalTable: "ContaReceber",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 151, DateTimeKind.Utc).AddTicks(5846));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 151, DateTimeKind.Utc).AddTicks(8139));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 151, DateTimeKind.Utc).AddTicks(8143));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 155, DateTimeKind.Utc).AddTicks(2708));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 155, DateTimeKind.Utc).AddTicks(3430));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 155, DateTimeKind.Utc).AddTicks(3432));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 155, DateTimeKind.Utc).AddTicks(3433));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 155, DateTimeKind.Utc).AddTicks(3435));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 155, DateTimeKind.Utc).AddTicks(3436));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 157, DateTimeKind.Utc).AddTicks(8220));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 157, DateTimeKind.Utc).AddTicks(9184));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 157, DateTimeKind.Utc).AddTicks(9186));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 148, DateTimeKind.Utc).AddTicks(5819));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 149, DateTimeKind.Utc).AddTicks(8787));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 149, DateTimeKind.Utc).AddTicks(8819));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 149, DateTimeKind.Utc).AddTicks(8827));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 149, DateTimeKind.Utc).AddTicks(8854));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 149, DateTimeKind.Utc).AddTicks(8869));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 149, DateTimeKind.Utc).AddTicks(8898));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 149, DateTimeKind.Utc).AddTicks(8902));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 149, DateTimeKind.Utc).AddTicks(8906));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 149, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 149, DateTimeKind.Utc).AddTicks(8916));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("35e16792-6c95-4ac3-8da1-a1fcf5d2f1f0"), 3, null, new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(7809), new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(7808), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("72c99446-aa53-4c29-a220-ccc89df9d1ad"), 3, null, new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(7821), new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(7821), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("9b1bbeee-98be-4983-b62e-10f04ec7b66a"), 3, null, new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(7818), new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(7818), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("a7812b69-e5b0-41ab-b964-9c78d8266295"), 1, null, new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(5897), new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(5873), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("b6e35967-2013-4e60-9551-4ba09d0daa5f"), 3, null, new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(7831), new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(7831), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("b9178063-7044-4d3f-9b58-0f291717aa64"), 3, null, new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(7840), new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(7839), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("c0ce9643-f935-4ad5-b53d-98d3f63f0aa5"), 3, null, new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(7834), new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(7833), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("caf546a1-c85a-42c6-befb-73030c7ee9aa"), 1, null, new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(7804), new DateTime(2026, 11, 6, 1, 0, 51, 153, DateTimeKind.Utc).AddTicks(7799), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 152, DateTimeKind.Utc).AddTicks(5696));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 152, DateTimeKind.Utc).AddTicks(9081));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 152, DateTimeKind.Utc).AddTicks(9087));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 152, DateTimeKind.Utc).AddTicks(9090));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 152, DateTimeKind.Utc).AddTicks(9093));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 152, DateTimeKind.Utc).AddTicks(9109));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 152, DateTimeKind.Utc).AddTicks(9119));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 152, DateTimeKind.Utc).AddTicks(9123));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 1, 0, 51, 152, DateTimeKind.Utc).AddTicks(9126));

            migrationBuilder.CreateIndex(
                name: "IX_CobrancasOperacionais_ContaReceberId",
                table: "CobrancasOperacionais",
                column: "ContaReceberId");

            migrationBuilder.CreateIndex(
                name: "IX_CobrancasOperacionais_EmpresaId_ContaReceberId",
                table: "CobrancasOperacionais",
                columns: new[] { "EmpresaId", "ContaReceberId" });

            migrationBuilder.CreateIndex(
                name: "IX_CobrancasOperacionais_EmpresaId_Status",
                table: "CobrancasOperacionais",
                columns: new[] { "EmpresaId", "Status" });

            migrationBuilder.CreateIndex(
                name: "UX_CobrancasOperacionais_Empresa_Idempotencia",
                table: "CobrancasOperacionais",
                columns: new[] { "EmpresaId", "ChaveIdempotencia" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_CobrancasOperacionais_ExternalReference",
                table: "CobrancasOperacionais",
                columns: new[] { "EmpresaId", "ExternalReference" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_CobrancasOperacionais_Gateway_Pagamento",
                table: "CobrancasOperacionais",
                columns: new[] { "EmpresaId", "Gateway", "GatewayPagamentoId" },
                unique: true,
                filter: "[GatewayPagamentoId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CobrancasOperacionais");

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("35e16792-6c95-4ac3-8da1-a1fcf5d2f1f0"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("72c99446-aa53-4c29-a220-ccc89df9d1ad"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("9b1bbeee-98be-4983-b62e-10f04ec7b66a"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("a7812b69-e5b0-41ab-b964-9c78d8266295"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("b6e35967-2013-4e60-9551-4ba09d0daa5f"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("b9178063-7044-4d3f-9b58-0f291717aa64"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("c0ce9643-f935-4ad5-b53d-98d3f63f0aa5"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("caf546a1-c85a-42c6-befb-73030c7ee9aa"));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 387, DateTimeKind.Utc).AddTicks(8085));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 387, DateTimeKind.Utc).AddTicks(9974));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 387, DateTimeKind.Utc).AddTicks(9987));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 391, DateTimeKind.Utc).AddTicks(2907));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 391, DateTimeKind.Utc).AddTicks(3693));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 391, DateTimeKind.Utc).AddTicks(3695));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 391, DateTimeKind.Utc).AddTicks(3697));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 391, DateTimeKind.Utc).AddTicks(3720));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 391, DateTimeKind.Utc).AddTicks(3722));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 393, DateTimeKind.Utc).AddTicks(4147));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 393, DateTimeKind.Utc).AddTicks(5200));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 393, DateTimeKind.Utc).AddTicks(5202));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 385, DateTimeKind.Utc).AddTicks(1619));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5382));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5386));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5391));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5420));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5453));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5457));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5461));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5466));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5471));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("0e3ec0a0-7162-4ca0-b949-aba15b5ca8b8"), 3, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4352), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4352), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("560c3fff-53aa-4b9f-acbd-bbfbf0be0792"), 3, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4362), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4362), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("66a4ce67-fb2a-4c18-a71a-18124b6fd942"), 3, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4368), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4367), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("79c44013-2f22-48d3-9d79-85a47d42c978"), 3, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4370), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4370), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("892d4b57-0bf5-47c9-8305-073bf7a98290"), 3, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4359), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4354), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("b7a4f942-bd5b-4b4a-8e6b-ec13360a85ab"), 1, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4349), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4345), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("bf5aafd6-ba83-4d88-8b3a-bd0d0b9dad66"), 1, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(2971), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(2965), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("e70ae7cd-4e6a-45c4-ac90-d87632d8ef38"), 3, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4375), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4372), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(6139));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8134));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8138));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8141));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8143));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8155));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8158));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8167));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8170));
        }
    }
}
