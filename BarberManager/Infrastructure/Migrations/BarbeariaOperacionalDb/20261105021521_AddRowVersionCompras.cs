using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class AddRowVersionCompras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("0b35ec26-0832-4bb0-b823-eb1ef0f5c6e4"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("3cb8df4b-2861-4e4a-ac55-111cb9979d8e"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("4d39ca7e-6194-4998-a27f-b5fac8755e1a"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("81ef9fd1-fb1e-4052-a749-e3583d10ee1a"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("c2a1fd1e-fa08-4b70-bea3-56345171dfd5"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("cbde7633-6947-4fa1-849a-6c05c1ed88f2"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("cc646e89-02a4-46ce-a59d-032ee9c36588"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("f55ab7fc-e5cd-4ab4-800e-438e1d9e5da9"));

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Compras",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CompraItens",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 210, DateTimeKind.Utc).AddTicks(8873));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 211, DateTimeKind.Utc).AddTicks(751));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 211, DateTimeKind.Utc).AddTicks(754));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 213, DateTimeKind.Utc).AddTicks(9156));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 213, DateTimeKind.Utc).AddTicks(9872));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 213, DateTimeKind.Utc).AddTicks(9874));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 213, DateTimeKind.Utc).AddTicks(9875));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 213, DateTimeKind.Utc).AddTicks(9877));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 213, DateTimeKind.Utc).AddTicks(9883));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 215, DateTimeKind.Utc).AddTicks(3234));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 215, DateTimeKind.Utc).AddTicks(4183));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 215, DateTimeKind.Utc).AddTicks(4185));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 208, DateTimeKind.Utc).AddTicks(8963));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 209, DateTimeKind.Utc).AddTicks(6940));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 209, DateTimeKind.Utc).AddTicks(6965));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 209, DateTimeKind.Utc).AddTicks(6971));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 209, DateTimeKind.Utc).AddTicks(6975));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 209, DateTimeKind.Utc).AddTicks(7018));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 209, DateTimeKind.Utc).AddTicks(7022));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 209, DateTimeKind.Utc).AddTicks(7026));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 209, DateTimeKind.Utc).AddTicks(7030));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 209, DateTimeKind.Utc).AddTicks(7051));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 209, DateTimeKind.Utc).AddTicks(7058));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("42de63e2-b6e5-4c3f-b3b1-f8ba83ea41ad"), 3, null, new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(5036), new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(5036), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("452e6b9a-2d99-430a-be41-9e0db07c8643"), 3, null, new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(5033), new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(5033), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("606cd257-af50-4d19-9c09-787903fcac2d"), 3, null, new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(5050), new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(5049), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("61b575af-34d2-408e-b0c4-63e240b2c7c4"), 1, null, new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(5025), new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(5020), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("9e5657cb-13bf-488e-8d94-06a2e0d92582"), 3, null, new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(5039), new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(5038), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("c91929aa-fea6-44be-bebf-91de2b394205"), 3, null, new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(5053), new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(5052), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("ca975f17-522a-42ea-887f-ce69a53110ee"), 1, null, new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(3681), new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(3674), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("d827bf83-8b54-4ed9-b72f-6b487a3c5d28"), 3, null, new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(5045), new DateTime(2026, 11, 5, 2, 15, 20, 212, DateTimeKind.Utc).AddTicks(5044), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 211, DateTimeKind.Utc).AddTicks(6903));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 211, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 211, DateTimeKind.Utc).AddTicks(8840));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 211, DateTimeKind.Utc).AddTicks(8842));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 211, DateTimeKind.Utc).AddTicks(8858));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 211, DateTimeKind.Utc).AddTicks(8868));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 211, DateTimeKind.Utc).AddTicks(8870));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 211, DateTimeKind.Utc).AddTicks(8873));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 2, 15, 20, 211, DateTimeKind.Utc).AddTicks(8876));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("42de63e2-b6e5-4c3f-b3b1-f8ba83ea41ad"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("452e6b9a-2d99-430a-be41-9e0db07c8643"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("606cd257-af50-4d19-9c09-787903fcac2d"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("61b575af-34d2-408e-b0c4-63e240b2c7c4"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("9e5657cb-13bf-488e-8d94-06a2e0d92582"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("c91929aa-fea6-44be-bebf-91de2b394205"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("ca975f17-522a-42ea-887f-ce69a53110ee"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("d827bf83-8b54-4ed9-b72f-6b487a3c5d28"));

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CompraItens");

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 823, DateTimeKind.Utc).AddTicks(2529));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 823, DateTimeKind.Utc).AddTicks(4881));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 823, DateTimeKind.Utc).AddTicks(4885));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 827, DateTimeKind.Utc).AddTicks(4484));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 827, DateTimeKind.Utc).AddTicks(5442));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 827, DateTimeKind.Utc).AddTicks(5445));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 827, DateTimeKind.Utc).AddTicks(5458));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 827, DateTimeKind.Utc).AddTicks(5460));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 827, DateTimeKind.Utc).AddTicks(5462));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 829, DateTimeKind.Utc).AddTicks(2412));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 829, DateTimeKind.Utc).AddTicks(3712));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 829, DateTimeKind.Utc).AddTicks(3715));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 821, DateTimeKind.Utc).AddTicks(15));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 821, DateTimeKind.Utc).AddTicks(8805));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 821, DateTimeKind.Utc).AddTicks(8860));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 821, DateTimeKind.Utc).AddTicks(8866));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 821, DateTimeKind.Utc).AddTicks(8871));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 821, DateTimeKind.Utc).AddTicks(8897));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 821, DateTimeKind.Utc).AddTicks(8914));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 821, DateTimeKind.Utc).AddTicks(8919));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 821, DateTimeKind.Utc).AddTicks(8935));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 821, DateTimeKind.Utc).AddTicks(8941));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 821, DateTimeKind.Utc).AddTicks(8946));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("0b35ec26-0832-4bb0-b823-eb1ef0f5c6e4"), 3, null, new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(5020), new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(5019), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("3cb8df4b-2861-4e4a-ac55-111cb9979d8e"), 1, null, new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(4995), new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(4990), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("4d39ca7e-6194-4998-a27f-b5fac8755e1a"), 3, null, new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(5002), new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(5001), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("81ef9fd1-fb1e-4052-a749-e3583d10ee1a"), 3, null, new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(4999), new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(4998), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("c2a1fd1e-fa08-4b70-bea3-56345171dfd5"), 3, null, new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(5005), new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(5004), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("cbde7633-6947-4fa1-849a-6c05c1ed88f2"), 3, null, new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(5023), new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(5022), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("cc646e89-02a4-46ce-a59d-032ee9c36588"), 1, null, new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(3281), new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(3268), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("f55ab7fc-e5cd-4ab4-800e-438e1d9e5da9"), 3, null, new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(5016), new DateTime(2026, 11, 4, 3, 28, 18, 825, DateTimeKind.Utc).AddTicks(5015), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 824, DateTimeKind.Utc).AddTicks(3656));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 824, DateTimeKind.Utc).AddTicks(6323));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 824, DateTimeKind.Utc).AddTicks(6343));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 824, DateTimeKind.Utc).AddTicks(6346));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 824, DateTimeKind.Utc).AddTicks(6350));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 824, DateTimeKind.Utc).AddTicks(6362));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 824, DateTimeKind.Utc).AddTicks(6366));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 824, DateTimeKind.Utc).AddTicks(6369));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 4, 3, 28, 18, 824, DateTimeKind.Utc).AddTicks(6372));
        }
    }
}
