using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class trancacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("2047d1d1-328f-40ac-b8a4-ce6ca82b6aed"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("5c4072a1-f5e0-4f0e-86ce-3be125f8bc09"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("6107f57a-a85a-4034-a2e3-974f41c4f42b"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("b6192f5d-024e-4aed-b321-892010a869b5"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("c08ef045-27ad-489f-9837-1aa9efd7d235"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("c0e21a66-92fa-497c-bd9e-3da0703897ed"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("c7a3507c-c6f1-41e5-81ed-766f7d6cc888"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("f737a84e-3f05-44eb-9ced-afa17acc2664"));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 288, DateTimeKind.Utc).AddTicks(381));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 288, DateTimeKind.Utc).AddTicks(2241));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 288, DateTimeKind.Utc).AddTicks(2244));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 286, DateTimeKind.Utc).AddTicks(6478));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 287, DateTimeKind.Utc).AddTicks(2316));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 287, DateTimeKind.Utc).AddTicks(2345));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 287, DateTimeKind.Utc).AddTicks(2350));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 287, DateTimeKind.Utc).AddTicks(2354));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 287, DateTimeKind.Utc).AddTicks(2367));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 287, DateTimeKind.Utc).AddTicks(2378));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 287, DateTimeKind.Utc).AddTicks(2383));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 287, DateTimeKind.Utc).AddTicks(2387));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 287, DateTimeKind.Utc).AddTicks(2392));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 287, DateTimeKind.Utc).AddTicks(2398));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("47bc49be-29f3-426e-ace8-025f32afc03e"), 3, null, new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(6330), new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(6330), 10, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("5812c78e-da8c-4b0c-9a97-173ef9ba5224"), 3, null, new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(6313), new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(6313), 45, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("7daee910-e216-43ee-858b-1bdc554f5541"), 3, null, new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(6316), new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(6315), 45, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("bccff9ab-3428-4ae8-9f07-19bb23675e23"), 1, null, new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(5211), new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(5203), 25, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("c21c2ef5-cc07-4f6f-a6a1-83fec9acccec"), 1, null, new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(6303), new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(6299), 35, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("c9759e1f-0a51-4e5f-8ccc-2b3baf47bab8"), 3, null, new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(6328), new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(6327), 45, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("ebcd32de-fbe6-4330-ad7b-36f2747d81c0"), 3, null, new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(6322), new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(6322), 45, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("f17e9d24-1bd1-4267-bb7e-6735f8b05fe5"), 3, null, new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(6311), new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(6310), 40, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 288, DateTimeKind.Utc).AddTicks(8571));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(510));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(514));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(516));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(526));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(536));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(539));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(541));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 23, 34, 289, DateTimeKind.Utc).AddTicks(544));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("47bc49be-29f3-426e-ace8-025f32afc03e"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("5812c78e-da8c-4b0c-9a97-173ef9ba5224"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("7daee910-e216-43ee-858b-1bdc554f5541"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("bccff9ab-3428-4ae8-9f07-19bb23675e23"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("c21c2ef5-cc07-4f6f-a6a1-83fec9acccec"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("c9759e1f-0a51-4e5f-8ccc-2b3baf47bab8"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("ebcd32de-fbe6-4330-ad7b-36f2747d81c0"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("f17e9d24-1bd1-4267-bb7e-6735f8b05fe5"));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 777, DateTimeKind.Utc).AddTicks(6943));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 777, DateTimeKind.Utc).AddTicks(8874));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 777, DateTimeKind.Utc).AddTicks(8877));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 775, DateTimeKind.Utc).AddTicks(9853));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 776, DateTimeKind.Utc).AddTicks(5819));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 776, DateTimeKind.Utc).AddTicks(5840));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 776, DateTimeKind.Utc).AddTicks(5844));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 776, DateTimeKind.Utc).AddTicks(5858));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 776, DateTimeKind.Utc).AddTicks(5872));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 776, DateTimeKind.Utc).AddTicks(5885));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 776, DateTimeKind.Utc).AddTicks(5889));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 776, DateTimeKind.Utc).AddTicks(5893));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 776, DateTimeKind.Utc).AddTicks(5898));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 776, DateTimeKind.Utc).AddTicks(5902));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("2047d1d1-328f-40ac-b8a4-ce6ca82b6aed"), 1, null, new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(2116), new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(2107), 25, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("5c4072a1-f5e0-4f0e-86ce-3be125f8bc09"), 1, null, new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(3197), new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(3193), 35, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("6107f57a-a85a-4034-a2e3-974f41c4f42b"), 3, null, new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(3205), new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(3204), 40, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("b6192f5d-024e-4aed-b321-892010a869b5"), 3, null, new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(3222), new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(3221), 45, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("c08ef045-27ad-489f-9837-1aa9efd7d235"), 3, null, new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(3225), new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(3224), 10, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("c0e21a66-92fa-497c-bd9e-3da0703897ed"), 3, null, new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(3212), new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(3211), 45, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("c7a3507c-c6f1-41e5-81ed-766f7d6cc888"), 3, null, new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(3209), new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(3207), 45, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("f737a84e-3f05-44eb-9ced-afa17acc2664"), 3, null, new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(3217), new DateTime(2026, 10, 14, 5, 7, 50, 779, DateTimeKind.Utc).AddTicks(3216), 45, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 778, DateTimeKind.Utc).AddTicks(5568));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 778, DateTimeKind.Utc).AddTicks(7373));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 778, DateTimeKind.Utc).AddTicks(7377));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 778, DateTimeKind.Utc).AddTicks(7380));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 778, DateTimeKind.Utc).AddTicks(7394));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 778, DateTimeKind.Utc).AddTicks(7407));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 778, DateTimeKind.Utc).AddTicks(7410));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 778, DateTimeKind.Utc).AddTicks(7413));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 14, 5, 7, 50, 778, DateTimeKind.Utc).AddTicks(7415));
        }
    }
}
