using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class InitialFinanceiro01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("0e744c45-c78b-416a-9e0d-1c3e9ae9c390"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("1251d200-a7ef-4886-9702-d833ea627e1b"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("24295a83-0af9-42c1-9b5f-0619118f9a79"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("3e23dc94-fe65-40c2-a977-ad6312c77052"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("7b4b88b9-6286-4ee7-b262-ee4c48478042"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("9a778cb2-f8d2-49ba-90d2-19e213afea47"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("da58c59e-f493-4137-98f7-14a3c57db728"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("e070aadb-7661-46c8-876f-99d9cf6298b9"));

            migrationBuilder.AddColumn<decimal>(
                name: "DiferencaFechamento",
                table: "Caixa",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObservacaoFechamento",
                table: "Caixa",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SaldoEsperadoFechamento",
                table: "Caixa",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 645, DateTimeKind.Utc).AddTicks(4563));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 645, DateTimeKind.Utc).AddTicks(6284));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 645, DateTimeKind.Utc).AddTicks(6287));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 648, DateTimeKind.Utc).AddTicks(7556));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 648, DateTimeKind.Utc).AddTicks(8267));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 648, DateTimeKind.Utc).AddTicks(8275));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 648, DateTimeKind.Utc).AddTicks(8277));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 648, DateTimeKind.Utc).AddTicks(8278));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 648, DateTimeKind.Utc).AddTicks(8279));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 644, DateTimeKind.Utc).AddTicks(55));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 644, DateTimeKind.Utc).AddTicks(6044));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 644, DateTimeKind.Utc).AddTicks(6052));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 644, DateTimeKind.Utc).AddTicks(6070));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 644, DateTimeKind.Utc).AddTicks(6097));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 644, DateTimeKind.Utc).AddTicks(6107));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 644, DateTimeKind.Utc).AddTicks(6111));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 644, DateTimeKind.Utc).AddTicks(6131));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 644, DateTimeKind.Utc).AddTicks(6135));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 644, DateTimeKind.Utc).AddTicks(6140));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 644, DateTimeKind.Utc).AddTicks(6144));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("01a10247-a6b4-4cb6-b335-31f379a45ca9"), 3, null, new DateTime(2026, 10, 27, 23, 25, 56, 647, DateTimeKind.Utc).AddTicks(3039), new DateTime(2026, 10, 27, 23, 25, 56, 647, DateTimeKind.Utc).AddTicks(3038), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("33142333-4f0a-484e-9842-3477b14d351a"), 3, null, new DateTime(2026, 10, 27, 23, 25, 56, 647, DateTimeKind.Utc).AddTicks(3017), new DateTime(2026, 10, 27, 23, 25, 56, 647, DateTimeKind.Utc).AddTicks(3010), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("552b2965-eac8-4232-9589-ae005e772c0b"), 3, null, new DateTime(2026, 10, 27, 23, 25, 56, 647, DateTimeKind.Utc).AddTicks(3008), new DateTime(2026, 10, 27, 23, 25, 56, 647, DateTimeKind.Utc).AddTicks(3008), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("64b0e2a9-0ba6-4a8a-8821-4ed23e4f03b3"), 3, null, new DateTime(2026, 10, 27, 23, 25, 56, 647, DateTimeKind.Utc).AddTicks(3045), new DateTime(2026, 10, 27, 23, 25, 56, 647, DateTimeKind.Utc).AddTicks(3044), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("7e64906f-62c5-497c-8bc1-97a94b815454"), 3, null, new DateTime(2026, 10, 27, 23, 25, 56, 647, DateTimeKind.Utc).AddTicks(3006), new DateTime(2026, 10, 27, 23, 25, 56, 647, DateTimeKind.Utc).AddTicks(3005), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("a03331b5-caf0-4c3d-925f-49b86c97ba65"), 3, null, new DateTime(2026, 10, 27, 23, 25, 56, 647, DateTimeKind.Utc).AddTicks(3042), new DateTime(2026, 10, 27, 23, 25, 56, 647, DateTimeKind.Utc).AddTicks(3041), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("b8bf082a-4999-4f2b-86b0-7c0f01387257"), 1, null, new DateTime(2026, 10, 27, 23, 25, 56, 646, DateTimeKind.Utc).AddTicks(9022), new DateTime(2026, 10, 27, 23, 25, 56, 646, DateTimeKind.Utc).AddTicks(9011), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("b8fbe99e-8999-40e5-99de-8ddc88621da6"), 1, null, new DateTime(2026, 10, 27, 23, 25, 56, 647, DateTimeKind.Utc).AddTicks(3001), new DateTime(2026, 10, 27, 23, 25, 56, 647, DateTimeKind.Utc).AddTicks(2989), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 646, DateTimeKind.Utc).AddTicks(2475));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 646, DateTimeKind.Utc).AddTicks(4331));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 646, DateTimeKind.Utc).AddTicks(4336));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 646, DateTimeKind.Utc).AddTicks(4339));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 646, DateTimeKind.Utc).AddTicks(4341));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 646, DateTimeKind.Utc).AddTicks(4348));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 646, DateTimeKind.Utc).AddTicks(4351));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 646, DateTimeKind.Utc).AddTicks(4353));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 23, 25, 56, 646, DateTimeKind.Utc).AddTicks(4356));

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_EmpresaId_UsuarioId",
                table: "Caixa",
                columns: new[] { "EmpresaId", "UsuarioId" },
                unique: true,
                filter: "[Status] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Caixa_EmpresaId_UsuarioId",
                table: "Caixa");

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("01a10247-a6b4-4cb6-b335-31f379a45ca9"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("33142333-4f0a-484e-9842-3477b14d351a"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("552b2965-eac8-4232-9589-ae005e772c0b"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("64b0e2a9-0ba6-4a8a-8821-4ed23e4f03b3"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("7e64906f-62c5-497c-8bc1-97a94b815454"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("a03331b5-caf0-4c3d-925f-49b86c97ba65"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("b8bf082a-4999-4f2b-86b0-7c0f01387257"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("b8fbe99e-8999-40e5-99de-8ddc88621da6"));

            migrationBuilder.DropColumn(
                name: "DiferencaFechamento",
                table: "Caixa");

            migrationBuilder.DropColumn(
                name: "ObservacaoFechamento",
                table: "Caixa");

            migrationBuilder.DropColumn(
                name: "SaldoEsperadoFechamento",
                table: "Caixa");

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 661, DateTimeKind.Utc).AddTicks(998));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 661, DateTimeKind.Utc).AddTicks(2918));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 661, DateTimeKind.Utc).AddTicks(2921));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 664, DateTimeKind.Utc).AddTicks(2152));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 664, DateTimeKind.Utc).AddTicks(3003));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 664, DateTimeKind.Utc).AddTicks(3005));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 664, DateTimeKind.Utc).AddTicks(3007));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 664, DateTimeKind.Utc).AddTicks(3008));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 664, DateTimeKind.Utc).AddTicks(3009));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 659, DateTimeKind.Utc).AddTicks(6453));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 660, DateTimeKind.Utc).AddTicks(2769));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 660, DateTimeKind.Utc).AddTicks(2794));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 660, DateTimeKind.Utc).AddTicks(2798));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 660, DateTimeKind.Utc).AddTicks(2803));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 660, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 660, DateTimeKind.Utc).AddTicks(2842));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 660, DateTimeKind.Utc).AddTicks(2846));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 660, DateTimeKind.Utc).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 660, DateTimeKind.Utc).AddTicks(2856));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 660, DateTimeKind.Utc).AddTicks(2860));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("0e744c45-c78b-416a-9e0d-1c3e9ae9c390"), 3, null, new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(7521), new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(7521), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("1251d200-a7ef-4886-9702-d833ea627e1b"), 1, null, new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(7517), new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(7512), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("24295a83-0af9-42c1-9b5f-0619118f9a79"), 3, null, new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(7544), new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(7543), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("3e23dc94-fe65-40c2-a977-ad6312c77052"), 3, null, new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(7524), new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(7523), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("7b4b88b9-6286-4ee7-b262-ee4c48478042"), 3, null, new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(7541), new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(7540), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("9a778cb2-f8d2-49ba-90d2-19e213afea47"), 3, null, new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(7533), new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(7532), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("da58c59e-f493-4137-98f7-14a3c57db728"), 3, null, new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(7538), new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(7537), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("e070aadb-7661-46c8-876f-99d9cf6298b9"), 1, null, new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(6256), new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(6250), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 661, DateTimeKind.Utc).AddTicks(8856));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(1396));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(1400));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(1402));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(1405));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(1412));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(1415));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(1417));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 27, 20, 38, 54, 662, DateTimeKind.Utc).AddTicks(1428));
        }
    }
}
