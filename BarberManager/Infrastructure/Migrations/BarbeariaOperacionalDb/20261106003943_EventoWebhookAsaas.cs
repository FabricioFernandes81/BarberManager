using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class EventoWebhookAsaas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "EventosWebhookAsaas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventoId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PagamentoGatewayId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoEvento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Tentativas = table.Column<int>(type: "int", nullable: false),
                    RecebidoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProximaTentativaEmUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BloqueadoAteUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessadoEmUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimoErro = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventosWebhookAsaas", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 387, DateTimeKind.Utc).AddTicks(8085));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 387, DateTimeKind.Utc).AddTicks(9974));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 387, DateTimeKind.Utc).AddTicks(9987));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 391, DateTimeKind.Utc).AddTicks(2907));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 391, DateTimeKind.Utc).AddTicks(3693));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 391, DateTimeKind.Utc).AddTicks(3695));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 391, DateTimeKind.Utc).AddTicks(3697));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 391, DateTimeKind.Utc).AddTicks(3720));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 391, DateTimeKind.Utc).AddTicks(3722));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 393, DateTimeKind.Utc).AddTicks(4147));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 393, DateTimeKind.Utc).AddTicks(5200));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 393, DateTimeKind.Utc).AddTicks(5202));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 385, DateTimeKind.Utc).AddTicks(1619));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5370));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5382));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5386));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5391));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5420));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5453));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5457));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5461));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5466));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 386, DateTimeKind.Utc).AddTicks(5471));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("0e3ec0a0-7162-4ca0-b949-aba15b5ca8b8"), 3, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4352), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4352), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("560c3fff-53aa-4b9f-acbd-bbfbf0be0792"), 3, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4362), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4362), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("66a4ce67-fb2a-4c18-a71a-18124b6fd942"), 3, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4368), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4367), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("79c44013-2f22-48d3-9d79-85a47d42c978"), 3, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4370), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4370), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("892d4b57-0bf5-47c9-8305-073bf7a98290"), 3, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4359), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4354), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("b7a4f942-bd5b-4b4a-8e6b-ec13360a85ab"), 1, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4349), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4345), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("bf5aafd6-ba83-4d88-8b3a-bd0d0b9dad66"), 1, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(2971), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(2965), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("e70ae7cd-4e6a-45c4-ac90-d87632d8ef38"), 3, null, new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4375), new DateTime(2026, 11, 6, 0, 39, 42, 389, DateTimeKind.Utc).AddTicks(4372), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(6139));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8134));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8138));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8141));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8143));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8155));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8158));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8167));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 6, 0, 39, 42, 388, DateTimeKind.Utc).AddTicks(8170));

            migrationBuilder.CreateIndex(
                name: "IX_EventosWebhookAsaas_EmpresaId_PagamentoGatewayId",
                table: "EventosWebhookAsaas",
                columns: new[] { "EmpresaId", "PagamentoGatewayId" });

            migrationBuilder.CreateIndex(
                name: "IX_EventosWebhookAsaas_Processamento",
                table: "EventosWebhookAsaas",
                columns: new[] { "Status", "ProximaTentativaEmUtc", "BloqueadoAteUtc" });

            migrationBuilder.CreateIndex(
                name: "UX_EventosWebhookAsaas_Empresa_Evento",
                table: "EventosWebhookAsaas",
                columns: new[] { "EmpresaId", "EventoId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventosWebhookAsaas");

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("0e3ec0a0-7162-4ca0-b949-aba15b5ca8b8"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("560c3fff-53aa-4b9f-acbd-bbfbf0be0792"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("66a4ce67-fb2a-4c18-a71a-18124b6fd942"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("79c44013-2f22-48d3-9d79-85a47d42c978"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("892d4b57-0bf5-47c9-8305-073bf7a98290"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("b7a4f942-bd5b-4b4a-8e6b-ec13360a85ab"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("bf5aafd6-ba83-4d88-8b3a-bd0d0b9dad66"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("e70ae7cd-4e6a-45c4-ac90-d87632d8ef38"));

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
    }
}
