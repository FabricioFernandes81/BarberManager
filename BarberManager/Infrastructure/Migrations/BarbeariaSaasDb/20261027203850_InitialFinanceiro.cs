using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.BarbeariaSaasDb
{
    /// <inheritdoc />
    public partial class InitialFinanceiro : Migration
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 439, DateTimeKind.Utc).AddTicks(3361));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 439, DateTimeKind.Utc).AddTicks(5361));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 439, DateTimeKind.Utc).AddTicks(5475));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 439, DateTimeKind.Utc).AddTicks(5478));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 439, DateTimeKind.Utc).AddTicks(5487));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 439, DateTimeKind.Utc).AddTicks(5499));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 436, DateTimeKind.Utc).AddTicks(7893));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 437, DateTimeKind.Utc).AddTicks(394));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 437, DateTimeKind.Utc).AddTicks(397));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 437, DateTimeKind.Utc).AddTicks(399));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 437, DateTimeKind.Utc).AddTicks(401));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 437, DateTimeKind.Utc).AddTicks(403));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 440, DateTimeKind.Utc).AddTicks(617));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 440, DateTimeKind.Utc).AddTicks(1919));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 440, DateTimeKind.Utc).AddTicks(1924));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 440, DateTimeKind.Utc).AddTicks(1927));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 440, DateTimeKind.Utc).AddTicks(1930));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 440, DateTimeKind.Utc).AddTicks(1933));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 438, DateTimeKind.Utc).AddTicks(5545));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 438, DateTimeKind.Utc).AddTicks(7370));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 438, DateTimeKind.Utc).AddTicks(7462));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 438, DateTimeKind.Utc).AddTicks(7465));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 438, DateTimeKind.Utc).AddTicks(7467));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 438, DateTimeKind.Utc).AddTicks(7473));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 437, DateTimeKind.Utc).AddTicks(8622));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 438, DateTimeKind.Utc).AddTicks(197));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 438, DateTimeKind.Utc).AddTicks(199));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 438, DateTimeKind.Utc).AddTicks(201));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 438, DateTimeKind.Utc).AddTicks(203));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 55, 438, DateTimeKind.Utc).AddTicks(205));
        }
    }
}
