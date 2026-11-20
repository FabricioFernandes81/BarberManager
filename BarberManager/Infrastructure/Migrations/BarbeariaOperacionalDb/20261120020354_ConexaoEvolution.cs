using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class ConexaoEvolution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("16f55928-9530-45fe-9186-65de2dad4e99"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("5020b524-9f0a-4b93-8076-b3f554cc3a20"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("5bd94f0b-d846-4d20-a1cf-0e193b150e6f"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("75c5143d-9ebd-40f2-9a8c-ea3c36c87438"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("81b3bc17-30de-4f25-b751-e34e25de95a0"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("a53ffb34-b5ff-4b3f-be73-81b4263cf19b"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("c3851dae-49b3-4cdf-a38f-252c2e189abe"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("df5eab97-6441-45dc-bf44-c645a04a8b13"));

            migrationBuilder.AddColumn<int>(
                name: "StatusConexao",
                table: "ConfiguracoesWhatsApp",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 877, DateTimeKind.Utc).AddTicks(2139));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 877, DateTimeKind.Utc).AddTicks(3980));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 877, DateTimeKind.Utc).AddTicks(3983));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 880, DateTimeKind.Utc).AddTicks(5091));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 880, DateTimeKind.Utc).AddTicks(5881));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 880, DateTimeKind.Utc).AddTicks(5888));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 880, DateTimeKind.Utc).AddTicks(5889));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 880, DateTimeKind.Utc).AddTicks(5891));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 880, DateTimeKind.Utc).AddTicks(5892));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 882, DateTimeKind.Utc).AddTicks(2091));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 882, DateTimeKind.Utc).AddTicks(3099));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 882, DateTimeKind.Utc).AddTicks(3102));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 874, DateTimeKind.Utc).AddTicks(8554));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 876, DateTimeKind.Utc).AddTicks(2865));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 876, DateTimeKind.Utc).AddTicks(2881));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 876, DateTimeKind.Utc).AddTicks(2885));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 876, DateTimeKind.Utc).AddTicks(2910));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 876, DateTimeKind.Utc).AddTicks(2925));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 876, DateTimeKind.Utc).AddTicks(2930));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 876, DateTimeKind.Utc).AddTicks(2941));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 876, DateTimeKind.Utc).AddTicks(2946));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 876, DateTimeKind.Utc).AddTicks(2951));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 876, DateTimeKind.Utc).AddTicks(2955));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("0652ad16-2e70-4d59-9225-956c5ea92b89"), 3, null, new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(9663), new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(9663), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("2d1e73a0-b395-42e9-b9e6-fe4f15b9c516"), 1, null, new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(9643), new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(9639), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("5ac004c9-82e5-4df8-a487-071f191430d4"), 3, null, new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(9650), new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(9649), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("640b6aeb-90ee-4158-b8db-395ec1969564"), 3, null, new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(9657), new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(9652), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("8a913a57-3e24-4fc3-89ac-1b0ed78d42bd"), 3, null, new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(9666), new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(9665), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("a9e551ea-512c-4802-93ca-b43fbc550f9e"), 3, null, new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(9647), new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(9646), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("c6a97b2f-f922-4267-908f-23cc96235d87"), 1, null, new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(8288), new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(8276), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("d22ec679-8553-4907-9047-17d99b7c84aa"), 3, null, new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(9669), new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(9668), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(637));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(2728));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(2733));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(2735));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(2738));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(2748));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(2751));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(2753));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 20, 2, 3, 52, 878, DateTimeKind.Utc).AddTicks(2756));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("0652ad16-2e70-4d59-9225-956c5ea92b89"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("2d1e73a0-b395-42e9-b9e6-fe4f15b9c516"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("5ac004c9-82e5-4df8-a487-071f191430d4"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("640b6aeb-90ee-4158-b8db-395ec1969564"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("8a913a57-3e24-4fc3-89ac-1b0ed78d42bd"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("a9e551ea-512c-4802-93ca-b43fbc550f9e"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("c6a97b2f-f922-4267-908f-23cc96235d87"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("d22ec679-8553-4907-9047-17d99b7c84aa"));

            migrationBuilder.DropColumn(
                name: "StatusConexao",
                table: "ConfiguracoesWhatsApp");

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 433, DateTimeKind.Utc).AddTicks(2624));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 433, DateTimeKind.Utc).AddTicks(4678));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 433, DateTimeKind.Utc).AddTicks(4681));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 436, DateTimeKind.Utc).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 436, DateTimeKind.Utc).AddTicks(4840));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 436, DateTimeKind.Utc).AddTicks(4842));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 436, DateTimeKind.Utc).AddTicks(4849));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 436, DateTimeKind.Utc).AddTicks(4850));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 436, DateTimeKind.Utc).AddTicks(4851));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 437, DateTimeKind.Utc).AddTicks(6919));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 437, DateTimeKind.Utc).AddTicks(7895));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 437, DateTimeKind.Utc).AddTicks(7905));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 431, DateTimeKind.Utc).AddTicks(7172));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 432, DateTimeKind.Utc).AddTicks(3533));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 432, DateTimeKind.Utc).AddTicks(3543));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 432, DateTimeKind.Utc).AddTicks(3548));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 432, DateTimeKind.Utc).AddTicks(3573));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 432, DateTimeKind.Utc).AddTicks(3586));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 432, DateTimeKind.Utc).AddTicks(3590));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 432, DateTimeKind.Utc).AddTicks(3619));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 432, DateTimeKind.Utc).AddTicks(3628));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 432, DateTimeKind.Utc).AddTicks(3633));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 432, DateTimeKind.Utc).AddTicks(3638));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("16f55928-9530-45fe-9186-65de2dad4e99"), 3, null, new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(9313), new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(9312), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("5020b524-9f0a-4b93-8076-b3f554cc3a20"), 1, null, new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(8010), new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(8003), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("5bd94f0b-d846-4d20-a1cf-0e193b150e6f"), 3, null, new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(9330), new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(9329), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("75c5143d-9ebd-40f2-9a8c-ea3c36c87438"), 3, null, new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(9336), new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(9335), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("81b3bc17-30de-4f25-b751-e34e25de95a0"), 3, null, new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(9316), new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(9315), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("a53ffb34-b5ff-4b3f-be73-81b4263cf19b"), 3, null, new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(9333), new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(9332), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("c3851dae-49b3-4cdf-a38f-252c2e189abe"), 3, null, new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(9318), new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(9318), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("df5eab97-6441-45dc-bf44-c645a04a8b13"), 1, null, new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(9309), new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(9305), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(683));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(2806));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(2822));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(2825));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(2828));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(2836));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(2838));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(2841));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 17, 18, 55, 29, 434, DateTimeKind.Utc).AddTicks(2844));
        }
    }
}
