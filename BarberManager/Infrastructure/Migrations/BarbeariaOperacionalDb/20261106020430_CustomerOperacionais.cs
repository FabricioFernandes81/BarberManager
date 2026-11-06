using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class CustomerOperacionais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "ClientesAsaasOperacionais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteAsaasId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataCriacaoGatewayUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacaoGatewayUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesAsaasOperacionais", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "UX_ClientesAsaasOperacionais_EmpresaId_ClienteAsaasId",
                table: "ClientesAsaasOperacionais",
                columns: new[] { "EmpresaId", "ClienteAsaasId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_ClientesAsaasOperacionais_EmpresaId_ClienteId",
                table: "ClientesAsaasOperacionais",
                columns: new[] { "EmpresaId", "ClienteId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientesAsaasOperacionais");

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
        }
    }
}
