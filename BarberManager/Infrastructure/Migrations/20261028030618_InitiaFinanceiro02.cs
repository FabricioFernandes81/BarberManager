using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitiaFinanceiro02 : Migration
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
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(6885));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(6887));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(6888));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(6889));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000013"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(5330));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000014"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(6768));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000015"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(6770));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000016"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(6862));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000017"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(6864));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000018"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(6873));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000019"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(6874));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000020"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(6876));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000021"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(6877));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000022"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(6880));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000023"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 18, 191, DateTimeKind.Utc).AddTicks(6881));
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
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(7744));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(7746));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(7747));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("80000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(7748));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000013"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(6164));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000014"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(7635));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000015"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(7638));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000016"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(7640));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000017"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(7641));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000018"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(7650));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000019"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(7652));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000020"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(7653));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000021"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(7658));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000022"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(7741));

            migrationBuilder.UpdateData(
                schema: "Auth",
                table: "Permissoes",
                keyColumn: "Id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000023"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 43, 383, DateTimeKind.Utc).AddTicks(7743));
        }
    }
}
