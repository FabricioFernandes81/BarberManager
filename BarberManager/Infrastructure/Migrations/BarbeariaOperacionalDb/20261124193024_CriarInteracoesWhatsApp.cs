using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class CriarInteracoesWhatsApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("11dd874c-7f78-41c0-a8a1-188a6a481d19"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("3916cd63-7f13-4a42-a675-88ebb56638d7"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("3c05f8c1-ba0a-4af8-9aa3-f58b789ddfde"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("6ab8594d-bedb-455b-b4c3-bfd17d26e9d8"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("7fcd68a3-3894-4da2-aeb9-3a271246593d"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("7fe9b733-1c99-49f1-b3b2-f87731b884e3"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("8361bc32-c98e-4135-8295-4c12e49d4f7d"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("e76922a2-51ec-468e-bd19-1aed9f5d25d2"));

            migrationBuilder.CreateTable(
                name: "InteracoesWhatsAppPendentes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstanceName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TipoInteracao = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    AgendamentoSelecionadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MessageIdOrigem = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CriadaEmUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpiraEmUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ConcluidaEmUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteracoesWhatsAppPendentes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpcoesInteracaoWhatsApp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InteracaoWhatsAppPendenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgendamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    DataHoraInicioSnapshot = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ServicosSnapshot = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ProfissionalSnapshot = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcoesInteracaoWhatsApp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcoesInteracaoWhatsApp_InteracoesWhatsAppPendentes_InteracaoWhatsAppPendenteId",
                        column: x => x.InteracaoWhatsAppPendenteId,
                        principalTable: "InteracoesWhatsAppPendentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 28, DateTimeKind.Utc).AddTicks(8914));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 29, DateTimeKind.Utc).AddTicks(1612));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 29, DateTimeKind.Utc).AddTicks(1616));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 32, DateTimeKind.Utc).AddTicks(2200));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 32, DateTimeKind.Utc).AddTicks(2923));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 32, DateTimeKind.Utc).AddTicks(2925));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 32, DateTimeKind.Utc).AddTicks(2926));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 32, DateTimeKind.Utc).AddTicks(2928));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 32, DateTimeKind.Utc).AddTicks(2929));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 33, DateTimeKind.Utc).AddTicks(7711));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 33, DateTimeKind.Utc).AddTicks(8658));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 33, DateTimeKind.Utc).AddTicks(8661));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 26, DateTimeKind.Utc).AddTicks(7058));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 27, DateTimeKind.Utc).AddTicks(6296));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 27, DateTimeKind.Utc).AddTicks(6311));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 27, DateTimeKind.Utc).AddTicks(6347));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 27, DateTimeKind.Utc).AddTicks(6357));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 27, DateTimeKind.Utc).AddTicks(6409));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 27, DateTimeKind.Utc).AddTicks(6415));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 27, DateTimeKind.Utc).AddTicks(6421));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 27, DateTimeKind.Utc).AddTicks(6427));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 27, DateTimeKind.Utc).AddTicks(6435));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 27, DateTimeKind.Utc).AddTicks(6441));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("1bb1cfc5-207c-48d3-966f-4c0d77f4473e"), 3, null, new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(6431), new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(6430), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("2c88b260-122c-4726-a705-145c5f29da94"), 3, null, new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(6395), new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(6390), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("4985168d-f4f6-408a-84e8-19b7064b6bf9"), 3, null, new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(6398), new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(6398), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("63f9018b-3233-4f19-993f-0dccf0e2241d"), 3, null, new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(6401), new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(6400), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("a7847630-1220-49fa-9faf-ca08dc042b2e"), 3, null, new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(6428), new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(6424), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("aaf83b34-9d5b-41e5-9448-ae2df3a307d8"), 1, null, new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(6387), new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(6383), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("c0c1f445-61d6-463c-81de-8fb1e53c7621"), 1, null, new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(5127), new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(5121), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("d6ed859a-36ce-4cfd-979c-e2e84fbf9412"), 3, null, new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(6408), new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(6407), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 29, DateTimeKind.Utc).AddTicks(8505));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(412));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(417));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(419));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(422));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(435));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(440));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 24, 19, 30, 23, 30, DateTimeKind.Utc).AddTicks(443));

            migrationBuilder.CreateIndex(
                name: "IX_InteracoesWhatsAppPendentes_EmpresaId_ClienteId_CriadaEmUtc",
                table: "InteracoesWhatsAppPendentes",
                columns: new[] { "EmpresaId", "ClienteId", "CriadaEmUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_InteracoesWhatsAppPendentes_EmpresaId_InstanceName_Telefone",
                table: "InteracoesWhatsAppPendentes",
                columns: new[] { "EmpresaId", "InstanceName", "Telefone" },
                unique: true,
                filter: "[Status] IN ('AguardandoSelecao', 'AguardandoConfirmacao')");

            migrationBuilder.CreateIndex(
                name: "IX_InteracoesWhatsAppPendentes_EmpresaId_MessageIdOrigem",
                table: "InteracoesWhatsAppPendentes",
                columns: new[] { "EmpresaId", "MessageIdOrigem" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InteracoesWhatsAppPendentes_Status_ExpiraEmUtc",
                table: "InteracoesWhatsAppPendentes",
                columns: new[] { "Status", "ExpiraEmUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_OpcoesInteracaoWhatsApp_AgendamentoId",
                table: "OpcoesInteracaoWhatsApp",
                column: "AgendamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_OpcoesInteracaoWhatsApp_InteracaoWhatsAppPendenteId_AgendamentoId",
                table: "OpcoesInteracaoWhatsApp",
                columns: new[] { "InteracaoWhatsAppPendenteId", "AgendamentoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpcoesInteracaoWhatsApp_InteracaoWhatsAppPendenteId_Ordem",
                table: "OpcoesInteracaoWhatsApp",
                columns: new[] { "InteracaoWhatsAppPendenteId", "Ordem" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpcoesInteracaoWhatsApp");

            migrationBuilder.DropTable(
                name: "InteracoesWhatsAppPendentes");

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("1bb1cfc5-207c-48d3-966f-4c0d77f4473e"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("2c88b260-122c-4726-a705-145c5f29da94"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("4985168d-f4f6-408a-84e8-19b7064b6bf9"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("63f9018b-3233-4f19-993f-0dccf0e2241d"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("a7847630-1220-49fa-9faf-ca08dc042b2e"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("aaf83b34-9d5b-41e5-9448-ae2df3a307d8"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("c0c1f445-61d6-463c-81de-8fb1e53c7621"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("d6ed859a-36ce-4cfd-979c-e2e84fbf9412"));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 564, DateTimeKind.Utc).AddTicks(6369));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 564, DateTimeKind.Utc).AddTicks(8185));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 564, DateTimeKind.Utc).AddTicks(8188));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 567, DateTimeKind.Utc).AddTicks(9448));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 568, DateTimeKind.Utc).AddTicks(215));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 568, DateTimeKind.Utc).AddTicks(218));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 568, DateTimeKind.Utc).AddTicks(219));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 568, DateTimeKind.Utc).AddTicks(220));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 568, DateTimeKind.Utc).AddTicks(221));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 569, DateTimeKind.Utc).AddTicks(2636));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 569, DateTimeKind.Utc).AddTicks(3613));

            migrationBuilder.UpdateData(
                table: "Marcas",
                keyColumn: "Id",
                keyValue: new Guid("a2000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 569, DateTimeKind.Utc).AddTicks(3616));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 563, DateTimeKind.Utc).AddTicks(405));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 563, DateTimeKind.Utc).AddTicks(7086));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 563, DateTimeKind.Utc).AddTicks(7095));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 563, DateTimeKind.Utc).AddTicks(7111));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 563, DateTimeKind.Utc).AddTicks(7136));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 563, DateTimeKind.Utc).AddTicks(7152));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 563, DateTimeKind.Utc).AddTicks(7172));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 563, DateTimeKind.Utc).AddTicks(7177));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 563, DateTimeKind.Utc).AddTicks(7181));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 563, DateTimeKind.Utc).AddTicks(7186));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 563, DateTimeKind.Utc).AddTicks(7190));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("11dd874c-7f78-41c0-a8a1-188a6a481d19"), 3, null, new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(3299), new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(3298), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("3916cd63-7f13-4a42-a675-88ebb56638d7"), 3, null, new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(3323), new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(3322), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("3c05f8c1-ba0a-4af8-9aa3-f58b789ddfde"), 1, null, new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(1974), new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(1960), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("6ab8594d-bedb-455b-b4c3-bfd17d26e9d8"), 3, null, new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(3314), new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(3313), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("7fcd68a3-3894-4da2-aeb9-3a271246593d"), 3, null, new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(3302), new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(3301), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("7fe9b733-1c99-49f1-b3b2-f87731b884e3"), 3, null, new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(3319), new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(3316), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("8361bc32-c98e-4135-8295-4c12e49d4f7d"), 1, null, new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(3285), new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(3281), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("e76922a2-51ec-468e-bd19-1aed9f5d25d2"), 3, null, new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(3296), new DateTime(2026, 11, 22, 2, 57, 23, 566, DateTimeKind.Utc).AddTicks(3289), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 565, DateTimeKind.Utc).AddTicks(4576));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 565, DateTimeKind.Utc).AddTicks(6569));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 565, DateTimeKind.Utc).AddTicks(6572));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 565, DateTimeKind.Utc).AddTicks(6575));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 565, DateTimeKind.Utc).AddTicks(6577));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 565, DateTimeKind.Utc).AddTicks(6597));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 565, DateTimeKind.Utc).AddTicks(6600));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 565, DateTimeKind.Utc).AddTicks(6602));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 11, 22, 2, 57, 23, 565, DateTimeKind.Utc).AddTicks(6605));
        }
    }
}
