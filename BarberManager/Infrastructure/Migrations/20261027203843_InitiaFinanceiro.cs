using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitiaFinanceiro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(7012));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(7014));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(7015));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(7017));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000013"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(5522));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000014"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(6981));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000015"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(6984));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000016"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(6985));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000017"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(6987));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000018"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(6998));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000019"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(6999));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000020"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(7001));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000021"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(7007));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000022"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(7009));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000023"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 43, 221, DateTimeKind.Utc).AddTicks(7010));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(5356));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(5358));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(5360));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(5362));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000013"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(3551));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000014"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(5302));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000015"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(5307));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000016"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(5309));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000017"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(5311));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000018"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(5333));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000019"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(5335));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000020"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(5337));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000021"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(5348));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000022"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(5353));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000023"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 51, 243, DateTimeKind.Utc).AddTicks(5355));
        }
    }
}
