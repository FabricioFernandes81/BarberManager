using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class AddAssas01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 948, DateTimeKind.Utc).AddTicks(3622));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 948, DateTimeKind.Utc).AddTicks(5456));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 948, DateTimeKind.Utc).AddTicks(5459));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 951, DateTimeKind.Utc).AddTicks(6564));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 951, DateTimeKind.Utc).AddTicks(7829));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 951, DateTimeKind.Utc).AddTicks(7831));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 951, DateTimeKind.Utc).AddTicks(7839));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 951, DateTimeKind.Utc).AddTicks(7841));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 951, DateTimeKind.Utc).AddTicks(7842));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 953, DateTimeKind.Utc).AddTicks(3894));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 953, DateTimeKind.Utc).AddTicks(4922));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 953, DateTimeKind.Utc).AddTicks(4935));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 946, DateTimeKind.Utc).AddTicks(492));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 947, DateTimeKind.Utc).AddTicks(3642));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 947, DateTimeKind.Utc).AddTicks(3653));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 947, DateTimeKind.Utc).AddTicks(3657));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 947, DateTimeKind.Utc).AddTicks(3661));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 947, DateTimeKind.Utc).AddTicks(3689));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 947, DateTimeKind.Utc).AddTicks(3693));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 947, DateTimeKind.Utc).AddTicks(3697));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 947, DateTimeKind.Utc).AddTicks(3709));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 947, DateTimeKind.Utc).AddTicks(3717));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 947, DateTimeKind.Utc).AddTicks(3721));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("06126da8-6383-4104-82ef-197fe37e4261"), 1, null, new DateTime(2026, 11, 5, 18, 46, 16, 949, DateTimeKind.Utc).AddTicks(8725), new DateTime(2026, 11, 5, 18, 46, 16, 949, DateTimeKind.Utc).AddTicks(8719), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("22b88197-68a3-4f8e-a019-fabff35341e7"), 3, null, new DateTime(2026, 11, 5, 18, 46, 16, 950, DateTimeKind.Utc).AddTicks(110), new DateTime(2026, 11, 5, 18, 46, 16, 950, DateTimeKind.Utc).AddTicks(109), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("3211570b-26b9-4006-96de-34b8dc290337"), 3, null, new DateTime(2026, 11, 5, 18, 46, 16, 950, DateTimeKind.Utc).AddTicks(107), new DateTime(2026, 11, 5, 18, 46, 16, 950, DateTimeKind.Utc).AddTicks(106), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("7df38fc6-b823-464f-ae5f-5e7ceff168b8"), 3, null, new DateTime(2026, 11, 5, 18, 46, 16, 950, DateTimeKind.Utc).AddTicks(88), new DateTime(2026, 11, 5, 18, 46, 16, 950, DateTimeKind.Utc).AddTicks(88), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("b7554450-88ff-44a3-97ba-d42748eaa23d"), 3, null, new DateTime(2026, 11, 5, 18, 46, 16, 950, DateTimeKind.Utc).AddTicks(91), new DateTime(2026, 11, 5, 18, 46, 16, 950, DateTimeKind.Utc).AddTicks(90), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("bba19085-c43b-4105-af4d-e4434836f12c"), 3, null, new DateTime(2026, 11, 5, 18, 46, 16, 950, DateTimeKind.Utc).AddTicks(104), new DateTime(2026, 11, 5, 18, 46, 16, 950, DateTimeKind.Utc).AddTicks(103), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("d7480115-1e5a-4a60-826d-dfbd107322e3"), 3, null, new DateTime(2026, 11, 5, 18, 46, 16, 950, DateTimeKind.Utc).AddTicks(94), new DateTime(2026, 11, 5, 18, 46, 16, 950, DateTimeKind.Utc).AddTicks(93), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("dd1b18cb-d126-4531-9dac-d525be12a866"), 1, null, new DateTime(2026, 11, 5, 18, 46, 16, 950, DateTimeKind.Utc).AddTicks(84), new DateTime(2026, 11, 5, 18, 46, 16, 950, DateTimeKind.Utc).AddTicks(80), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 949, DateTimeKind.Utc).AddTicks(1983));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 949, DateTimeKind.Utc).AddTicks(3886));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 949, DateTimeKind.Utc).AddTicks(3917));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 949, DateTimeKind.Utc).AddTicks(3920));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 949, DateTimeKind.Utc).AddTicks(3923));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 949, DateTimeKind.Utc).AddTicks(3932));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 949, DateTimeKind.Utc).AddTicks(3935));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 949, DateTimeKind.Utc).AddTicks(3937));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 5, 18, 46, 16, 949, DateTimeKind.Utc).AddTicks(3940));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("06126da8-6383-4104-82ef-197fe37e4261"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("22b88197-68a3-4f8e-a019-fabff35341e7"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("3211570b-26b9-4006-96de-34b8dc290337"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("7df38fc6-b823-464f-ae5f-5e7ceff168b8"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("b7554450-88ff-44a3-97ba-d42748eaa23d"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("bba19085-c43b-4105-af4d-e4434836f12c"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("d7480115-1e5a-4a60-826d-dfbd107322e3"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("dd1b18cb-d126-4531-9dac-d525be12a866"));

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
    }
}
