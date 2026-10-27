using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.BarbeariaSaasDb
{
    /// <inheritdoc />
    public partial class InitialFinanceiro01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 240, DateTimeKind.Utc).AddTicks(8894));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 241, DateTimeKind.Utc).AddTicks(3256));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 241, DateTimeKind.Utc).AddTicks(3264));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 241, DateTimeKind.Utc).AddTicks(3267));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 241, DateTimeKind.Utc).AddTicks(3270));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 241, DateTimeKind.Utc).AddTicks(3285));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 238, DateTimeKind.Utc).AddTicks(3077));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 238, DateTimeKind.Utc).AddTicks(5576));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 238, DateTimeKind.Utc).AddTicks(5580));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 238, DateTimeKind.Utc).AddTicks(5598));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 238, DateTimeKind.Utc).AddTicks(5601));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 238, DateTimeKind.Utc).AddTicks(5603));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 241, DateTimeKind.Utc).AddTicks(9685));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 242, DateTimeKind.Utc).AddTicks(1640));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 242, DateTimeKind.Utc).AddTicks(1646));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 242, DateTimeKind.Utc).AddTicks(1657));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 242, DateTimeKind.Utc).AddTicks(1660));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 242, DateTimeKind.Utc).AddTicks(1663));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 240, DateTimeKind.Utc).AddTicks(964));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 240, DateTimeKind.Utc).AddTicks(2837));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 240, DateTimeKind.Utc).AddTicks(2923));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 240, DateTimeKind.Utc).AddTicks(2926));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 240, DateTimeKind.Utc).AddTicks(2928));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 240, DateTimeKind.Utc).AddTicks(2934));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 239, DateTimeKind.Utc).AddTicks(3976));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 239, DateTimeKind.Utc).AddTicks(5487));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 239, DateTimeKind.Utc).AddTicks(5490));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 239, DateTimeKind.Utc).AddTicks(5491));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 239, DateTimeKind.Utc).AddTicks(5541));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 51, 239, DateTimeKind.Utc).AddTicks(5552));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 868, DateTimeKind.Utc).AddTicks(8697));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 869, DateTimeKind.Utc).AddTicks(5813));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 869, DateTimeKind.Utc).AddTicks(5820));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 869, DateTimeKind.Utc).AddTicks(5823));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 869, DateTimeKind.Utc).AddTicks(5826));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 869, DateTimeKind.Utc).AddTicks(5902));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 865, DateTimeKind.Utc).AddTicks(8986));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 866, DateTimeKind.Utc).AddTicks(1727));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 866, DateTimeKind.Utc).AddTicks(1731));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 866, DateTimeKind.Utc).AddTicks(1733));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 866, DateTimeKind.Utc).AddTicks(1735));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 866, DateTimeKind.Utc).AddTicks(1737));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 870, DateTimeKind.Utc).AddTicks(1397));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 870, DateTimeKind.Utc).AddTicks(2760));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 870, DateTimeKind.Utc).AddTicks(2776));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 870, DateTimeKind.Utc).AddTicks(2780));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 870, DateTimeKind.Utc).AddTicks(2783));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 870, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 867, DateTimeKind.Utc).AddTicks(7538));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 867, DateTimeKind.Utc).AddTicks(9528));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 868, DateTimeKind.Utc).AddTicks(2619));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 868, DateTimeKind.Utc).AddTicks(2622));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 868, DateTimeKind.Utc).AddTicks(2624));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 868, DateTimeKind.Utc).AddTicks(2641));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 867, DateTimeKind.Utc).AddTicks(526));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 867, DateTimeKind.Utc).AddTicks(2096));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 867, DateTimeKind.Utc).AddTicks(2110));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 867, DateTimeKind.Utc).AddTicks(2112));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 867, DateTimeKind.Utc).AddTicks(2114));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 49, 867, DateTimeKind.Utc).AddTicks(2115));
        }
    }
}
