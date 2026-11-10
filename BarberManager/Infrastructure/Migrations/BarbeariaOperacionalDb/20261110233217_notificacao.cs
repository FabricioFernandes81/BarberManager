using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class notificacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("2d75099a-d4ec-4ddf-9a23-0a0a0222f89b"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("58adb6be-c93f-4de8-9e4c-9bb898e46fa3"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("642c25d6-6899-4288-8be4-d64ce048b994"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("9c28dada-4365-47c1-aca8-ced877f93646"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("ae95858a-4448-480e-8b12-f33491bf0d64"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("c1fdb3c6-2d29-4c95-ae24-2fa480af019e"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("f5cc9dcd-e8a0-4ce1-b079-0af19575c861"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("f678f634-cdac-425b-852a-e65f86ad9ff7"));

            migrationBuilder.AddColumn<string>(
                name: "InvoiceUrl",
                table: "CobrancasOperacionais",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TargetWorkflow = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Attempts = table.Column<int>(type: "int", nullable: false),
                    OccurredAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextAttemptAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LockedUntilUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastError = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 503, DateTimeKind.Utc).AddTicks(2036));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 503, DateTimeKind.Utc).AddTicks(4135));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 503, DateTimeKind.Utc).AddTicks(4140));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 509, DateTimeKind.Utc).AddTicks(2994));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 509, DateTimeKind.Utc).AddTicks(3879));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 509, DateTimeKind.Utc).AddTicks(3895));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 509, DateTimeKind.Utc).AddTicks(3896));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 509, DateTimeKind.Utc).AddTicks(3897));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 509, DateTimeKind.Utc).AddTicks(3899));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 511, DateTimeKind.Utc).AddTicks(5950));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 511, DateTimeKind.Utc).AddTicks(7450));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 511, DateTimeKind.Utc).AddTicks(7453));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 499, DateTimeKind.Utc).AddTicks(8435));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 501, DateTimeKind.Utc).AddTicks(1671));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 501, DateTimeKind.Utc).AddTicks(1696));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 501, DateTimeKind.Utc).AddTicks(1700));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 501, DateTimeKind.Utc).AddTicks(1705));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 501, DateTimeKind.Utc).AddTicks(1733));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 501, DateTimeKind.Utc).AddTicks(1737));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 501, DateTimeKind.Utc).AddTicks(1742));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 501, DateTimeKind.Utc).AddTicks(1765));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 501, DateTimeKind.Utc).AddTicks(1770));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 501, DateTimeKind.Utc).AddTicks(1777));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("129ce30d-6860-48a1-a3a5-b7c381aad264"), 3, null, new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(5462), new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(5460), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("1c99dadd-829c-44ee-a158-1fbb1bf67589"), 3, null, new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(5404), new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(5402), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("414aa89c-6bbc-4755-a2e9-6d7214e9c435"), 3, null, new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(5423), new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(5406), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("5040e78f-2f7f-4cf8-9baa-7fe0ae3078f5"), 3, null, new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(5396), new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(5394), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("57cee7e2-ebea-48c4-b598-0ffb0dfcf7b6"), 1, null, new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(5382), new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(5338), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("7e7fb7cd-2dda-4029-bb08-0569acba120a"), 1, null, new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(214), new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(100), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("a86890cf-7744-4bbc-baff-9cb714cab204"), 3, null, new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(5449), new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(5448), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("e3b5186e-9df8-4e9c-abfc-54a173594104"), 3, null, new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(5456), new DateTime(2026, 11, 10, 23, 32, 16, 506, DateTimeKind.Utc).AddTicks(5452), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 504, DateTimeKind.Utc).AddTicks(4329));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 504, DateTimeKind.Utc).AddTicks(9001));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 504, DateTimeKind.Utc).AddTicks(9092));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 504, DateTimeKind.Utc).AddTicks(9099));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 504, DateTimeKind.Utc).AddTicks(9103));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 504, DateTimeKind.Utc).AddTicks(9151));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 504, DateTimeKind.Utc).AddTicks(9154));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 504, DateTimeKind.Utc).AddTicks(9157));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 10, 23, 32, 16, 504, DateTimeKind.Utc).AddTicks(9161));

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_Status_NextAttemptAtUtc_OccurredAtUtc",
                table: "OutboxMessages",
                columns: new[] { "Status", "NextAttemptAtUtc", "OccurredAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_TargetWorkflow_Status",
                table: "OutboxMessages",
                columns: new[] { "TargetWorkflow", "Status" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("129ce30d-6860-48a1-a3a5-b7c381aad264"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("1c99dadd-829c-44ee-a158-1fbb1bf67589"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("414aa89c-6bbc-4755-a2e9-6d7214e9c435"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("5040e78f-2f7f-4cf8-9baa-7fe0ae3078f5"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("57cee7e2-ebea-48c4-b598-0ffb0dfcf7b6"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("7e7fb7cd-2dda-4029-bb08-0569acba120a"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("a86890cf-7744-4bbc-baff-9cb714cab204"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("e3b5186e-9df8-4e9c-abfc-54a173594104"));

            migrationBuilder.DropColumn(
                name: "InvoiceUrl",
                table: "CobrancasOperacionais");

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 212, DateTimeKind.Utc).AddTicks(7670));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 212, DateTimeKind.Utc).AddTicks(9461));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 212, DateTimeKind.Utc).AddTicks(9464));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 216, DateTimeKind.Utc).AddTicks(3288));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 216, DateTimeKind.Utc).AddTicks(4043));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 216, DateTimeKind.Utc).AddTicks(4045));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 216, DateTimeKind.Utc).AddTicks(4046));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 216, DateTimeKind.Utc).AddTicks(4052));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 216, DateTimeKind.Utc).AddTicks(4054));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 217, DateTimeKind.Utc).AddTicks(7734));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 217, DateTimeKind.Utc).AddTicks(8722));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 217, DateTimeKind.Utc).AddTicks(8725));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 210, DateTimeKind.Utc).AddTicks(8962));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 211, DateTimeKind.Utc).AddTicks(7170));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 211, DateTimeKind.Utc).AddTicks(7179));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 211, DateTimeKind.Utc).AddTicks(7184));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 211, DateTimeKind.Utc).AddTicks(7189));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 211, DateTimeKind.Utc).AddTicks(7226));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 211, DateTimeKind.Utc).AddTicks(7230));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 211, DateTimeKind.Utc).AddTicks(7234));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 211, DateTimeKind.Utc).AddTicks(7239));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 211, DateTimeKind.Utc).AddTicks(7246));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 211, DateTimeKind.Utc).AddTicks(7251));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("2d75099a-d4ec-4ddf-9a23-0a0a0222f89b"), 3, null, new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(8305), new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(8304), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("58adb6be-c93f-4de8-9e4c-9bb898e46fa3"), 3, null, new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(8302), new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(8301), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("642c25d6-6899-4288-8be4-d64ce048b994"), 1, null, new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(6783), new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(6775), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("9c28dada-4365-47c1-aca8-ced877f93646"), 1, null, new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(8298), new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(8279), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("ae95858a-4448-480e-8b12-f33491bf0d64"), 3, null, new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(8321), new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(8320), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("c1fdb3c6-2d29-4c95-ae24-2fa480af019e"), 3, null, new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(8318), new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(8315), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("f5cc9dcd-e8a0-4ce1-b079-0af19575c861"), 3, null, new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(8324), new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(8323), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("f678f634-cdac-425b-852a-e65f86ad9ff7"), 3, null, new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(8308), new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(8307), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 213, DateTimeKind.Utc).AddTicks(8254));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(666));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(671));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(683));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(704));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(722));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(724));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(727));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 2, 4, 29, 214, DateTimeKind.Utc).AddTicks(730));
        }
    }
}
