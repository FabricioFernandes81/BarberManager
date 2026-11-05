using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class AddAssas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("2bd548d5-54eb-4cf8-9788-b653bdaa7c7e"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("2e482d1f-d68d-4b9d-89d2-8bd7beebf1c0"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("9aefd5a5-50a3-4566-9d43-1aa7b96eb1d7"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("b860f7f1-5636-46c4-a313-da5c337525bb"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("bffd8c44-fd78-47fc-9e46-b2084834608e"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("c272428b-fef5-4ad1-a518-fcc55596bb9c"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("c58ebe6e-0e3c-4c72-b13b-7e71a103b506"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("da1d134d-9f23-4e34-9209-9d11d3e3322b"));

            migrationBuilder.RenameColumn(
                name: "WebhooksToken",
                table: "ConfigurarcaoAssasCliente",
                newName: "WebhooksTokenProtegido");

            migrationBuilder.RenameColumn(
                name: "ApiToken",
                table: "ConfigurarcaoAssasCliente",
                newName: "ApiTokenProtegido");

            migrationBuilder.RenameIndex(
                name: "IX_ConfigurarcaoAssasCliente_EmpresaId",
                table: "ConfigurarcaoAssasCliente",
                newName: "UX_ConfigurarcaoAssasCliente_EmpresaId");

            migrationBuilder.AddColumn<int>(
                name: "Ambiente",
                table: "ConfigurarcaoAssasCliente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "ConfigurarcaoAssasCliente",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimaValidacao",
                table: "ConfigurarcaoAssasCliente",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 669, DateTimeKind.Utc).AddTicks(2301));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 669, DateTimeKind.Utc).AddTicks(4927));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 669, DateTimeKind.Utc).AddTicks(4932));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 673, DateTimeKind.Utc).AddTicks(2411));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 673, DateTimeKind.Utc).AddTicks(3179));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 673, DateTimeKind.Utc).AddTicks(3181));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 673, DateTimeKind.Utc).AddTicks(3182));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 673, DateTimeKind.Utc).AddTicks(3183));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 673, DateTimeKind.Utc).AddTicks(3184));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 674, DateTimeKind.Utc).AddTicks(9965));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 675, DateTimeKind.Utc).AddTicks(1144));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 675, DateTimeKind.Utc).AddTicks(1147));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 665, DateTimeKind.Utc).AddTicks(7236));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 667, DateTimeKind.Utc).AddTicks(6260));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 667, DateTimeKind.Utc).AddTicks(6292));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 667, DateTimeKind.Utc).AddTicks(6333));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 667, DateTimeKind.Utc).AddTicks(6338));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 667, DateTimeKind.Utc).AddTicks(6358));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 667, DateTimeKind.Utc).AddTicks(6362));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 667, DateTimeKind.Utc).AddTicks(6366));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 667, DateTimeKind.Utc).AddTicks(6371));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 667, DateTimeKind.Utc).AddTicks(6376));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 667, DateTimeKind.Utc).AddTicks(6382));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("08d41746-73a6-4e5e-8ebf-b1eb1e1ef6a4"), 3, null, new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(1376), new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(1373), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("11af0e5a-0f8b-4e56-8e5c-9a8f061d0998"), 3, null, new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(1371), new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(1370), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("2b34d3f5-635b-4bd0-a82d-1e4a78d461dd"), 3, null, new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(1379), new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(1378), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("4280e816-d762-4d25-83eb-b11e18e8dedf"), 1, null, new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(27), new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(9), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("436129ee-b917-4e18-be98-21bf4a7e72ac"), 3, null, new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(1358), new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(1342), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("64f17cf1-3e24-49db-8c32-6d506aa4a5c8"), 3, null, new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(1361), new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(1360), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("aa9a8505-1a04-49c1-ae18-0b2b10d0f96a"), 3, null, new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(1364), new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(1363), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("eedc9ae1-9b6f-4920-a4b0-7703a5c39024"), 1, null, new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(1339), new DateTime(2026, 11, 5, 18, 32, 55, 671, DateTimeKind.Utc).AddTicks(1336), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 670, DateTimeKind.Utc).AddTicks(2240));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 670, DateTimeKind.Utc).AddTicks(4426));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 670, DateTimeKind.Utc).AddTicks(4431));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 670, DateTimeKind.Utc).AddTicks(4433));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 670, DateTimeKind.Utc).AddTicks(4436));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 670, DateTimeKind.Utc).AddTicks(4454));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 670, DateTimeKind.Utc).AddTicks(4457));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 670, DateTimeKind.Utc).AddTicks(4459));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 32, 55, 670, DateTimeKind.Utc).AddTicks(4462));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("08d41746-73a6-4e5e-8ebf-b1eb1e1ef6a4"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("11af0e5a-0f8b-4e56-8e5c-9a8f061d0998"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("2b34d3f5-635b-4bd0-a82d-1e4a78d461dd"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("4280e816-d762-4d25-83eb-b11e18e8dedf"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("436129ee-b917-4e18-be98-21bf4a7e72ac"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("64f17cf1-3e24-49db-8c32-6d506aa4a5c8"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("aa9a8505-1a04-49c1-ae18-0b2b10d0f96a"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("eedc9ae1-9b6f-4920-a4b0-7703a5c39024"));

            migrationBuilder.DropColumn(
                name: "Ambiente",
                table: "ConfigurarcaoAssasCliente");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "ConfigurarcaoAssasCliente");

            migrationBuilder.DropColumn(
                name: "DataUltimaValidacao",
                table: "ConfigurarcaoAssasCliente");

            migrationBuilder.RenameColumn(
                name: "WebhooksTokenProtegido",
                table: "ConfigurarcaoAssasCliente",
                newName: "WebhooksToken");

            migrationBuilder.RenameColumn(
                name: "ApiTokenProtegido",
                table: "ConfigurarcaoAssasCliente",
                newName: "ApiToken");

            migrationBuilder.RenameIndex(
                name: "UX_ConfigurarcaoAssasCliente_EmpresaId",
                table: "ConfigurarcaoAssasCliente",
                newName: "IX_ConfigurarcaoAssasCliente_EmpresaId");

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 635, DateTimeKind.Utc).AddTicks(8400));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 636, DateTimeKind.Utc).AddTicks(282));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 636, DateTimeKind.Utc).AddTicks(285));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 638, DateTimeKind.Utc).AddTicks(9251));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 639, DateTimeKind.Utc).AddTicks(9));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 639, DateTimeKind.Utc).AddTicks(12));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 639, DateTimeKind.Utc).AddTicks(18));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 639, DateTimeKind.Utc).AddTicks(19));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 639, DateTimeKind.Utc).AddTicks(20));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 640, DateTimeKind.Utc).AddTicks(1414));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 640, DateTimeKind.Utc).AddTicks(2386));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 640, DateTimeKind.Utc).AddTicks(2395));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 634, DateTimeKind.Utc).AddTicks(2978));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 634, DateTimeKind.Utc).AddTicks(9372));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 634, DateTimeKind.Utc).AddTicks(9381));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 634, DateTimeKind.Utc).AddTicks(9385));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 634, DateTimeKind.Utc).AddTicks(9412));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 634, DateTimeKind.Utc).AddTicks(9427));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 634, DateTimeKind.Utc).AddTicks(9447));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 634, DateTimeKind.Utc).AddTicks(9451));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 634, DateTimeKind.Utc).AddTicks(9460));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 634, DateTimeKind.Utc).AddTicks(9466));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 634, DateTimeKind.Utc).AddTicks(9470));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("2bd548d5-54eb-4cf8-9788-b653bdaa7c7e"), 1, null, new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(4418), new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(4414), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("2e482d1f-d68d-4b9d-89d2-8bd7beebf1c0"), 3, null, new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(4422), new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(4421), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("9aefd5a5-50a3-4566-9d43-1aa7b96eb1d7"), 3, null, new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(4428), new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(4427), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("b860f7f1-5636-46c4-a313-da5c337525bb"), 3, null, new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(4442), new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(4441), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("bffd8c44-fd78-47fc-9e46-b2084834608e"), 1, null, new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(3149), new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(3144), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("c272428b-fef5-4ad1-a518-fcc55596bb9c"), 3, null, new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(4436), new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(4436), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("c58ebe6e-0e3c-4c72-b13b-7e71a103b506"), 3, null, new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(4425), new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(4424), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("da1d134d-9f23-4e34-9209-9d11d3e3322b"), 3, null, new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(4439), new DateTime(2026, 11, 5, 4, 32, 16, 637, DateTimeKind.Utc).AddTicks(4438), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 636, DateTimeKind.Utc).AddTicks(6254));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 636, DateTimeKind.Utc).AddTicks(8424));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 636, DateTimeKind.Utc).AddTicks(8437));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 636, DateTimeKind.Utc).AddTicks(8439));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 636, DateTimeKind.Utc).AddTicks(8442));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 636, DateTimeKind.Utc).AddTicks(8449));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 636, DateTimeKind.Utc).AddTicks(8452));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 636, DateTimeKind.Utc).AddTicks(8454));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 4, 32, 16, 636, DateTimeKind.Utc).AddTicks(8457));
        }
    }
}
