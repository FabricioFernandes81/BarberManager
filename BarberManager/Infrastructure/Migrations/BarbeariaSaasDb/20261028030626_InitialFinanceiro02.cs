using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.BarbeariaSaasDb
{
    /// <inheritdoc />
    public partial class InitialFinanceiro02 : Migration
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
                value: new DateTime(2026, 10, 28, 3, 6, 25, 380, DateTimeKind.Utc).AddTicks(870));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 380, DateTimeKind.Utc).AddTicks(9320));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 380, DateTimeKind.Utc).AddTicks(9327));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 380, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 380, DateTimeKind.Utc).AddTicks(9333));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Assinaturas",
                keyColumn: "Id",
                keyValue: new Guid("60000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 380, DateTimeKind.Utc).AddTicks(9346));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 376, DateTimeKind.Utc).AddTicks(7257));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 377, DateTimeKind.Utc).AddTicks(882));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 377, DateTimeKind.Utc).AddTicks(888));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 377, DateTimeKind.Utc).AddTicks(892));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 377, DateTimeKind.Utc).AddTicks(911));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Empresas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 377, DateTimeKind.Utc).AddTicks(918));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 381, DateTimeKind.Utc).AddTicks(5982));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 381, DateTimeKind.Utc).AddTicks(7417));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 381, DateTimeKind.Utc).AddTicks(7428));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 381, DateTimeKind.Utc).AddTicks(7431));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 381, DateTimeKind.Utc).AddTicks(7434));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Faturas",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 381, DateTimeKind.Utc).AddTicks(7436));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 378, DateTimeKind.Utc).AddTicks(9181));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 379, DateTimeKind.Utc).AddTicks(1228));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 379, DateTimeKind.Utc).AddTicks(3819));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 379, DateTimeKind.Utc).AddTicks(3823));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 379, DateTimeKind.Utc).AddTicks(3826));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Planos",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 379, DateTimeKind.Utc).AddTicks(3847));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 378, DateTimeKind.Utc).AddTicks(652));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 378, DateTimeKind.Utc).AddTicks(2679));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 378, DateTimeKind.Utc).AddTicks(2683));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 378, DateTimeKind.Utc).AddTicks(2685));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 378, DateTimeKind.Utc).AddTicks(2686));

            migrationBuilder.UpdateData(
                schema: "Saas",
                table: "Responsaveis",
                keyColumn: "Id",
                keyValue: new Guid("30000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 25, 378, DateTimeKind.Utc).AddTicks(2688));
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
    }
}
