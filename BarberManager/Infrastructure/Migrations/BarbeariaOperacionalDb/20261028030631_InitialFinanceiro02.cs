using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class InitialFinanceiro02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgendamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProfissionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UsuarioResponsavelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAbertura = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DataFinalizacao = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendas_Agendamentos_AgendamentoId",
                        column: x => x.AgendamentoId,
                        principalTable: "Agendamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VendaItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoItem = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendaItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendaItens_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Vendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 657, DateTimeKind.Utc).AddTicks(5172));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 657, DateTimeKind.Utc).AddTicks(6875));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 657, DateTimeKind.Utc).AddTicks(6878));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 660, DateTimeKind.Utc).AddTicks(4283));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 660, DateTimeKind.Utc).AddTicks(5394));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 660, DateTimeKind.Utc).AddTicks(5405));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 660, DateTimeKind.Utc).AddTicks(5406));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 660, DateTimeKind.Utc).AddTicks(5407));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 660, DateTimeKind.Utc).AddTicks(5408));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 656, DateTimeKind.Utc).AddTicks(1198));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 656, DateTimeKind.Utc).AddTicks(6938));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 656, DateTimeKind.Utc).AddTicks(6947));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 656, DateTimeKind.Utc).AddTicks(6971));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 656, DateTimeKind.Utc).AddTicks(6977));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 656, DateTimeKind.Utc).AddTicks(6985));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 656, DateTimeKind.Utc).AddTicks(6989));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 656, DateTimeKind.Utc).AddTicks(7008));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 656, DateTimeKind.Utc).AddTicks(7012));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 656, DateTimeKind.Utc).AddTicks(7018));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 656, DateTimeKind.Utc).AddTicks(7022));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("272045a1-5dfa-4a45-8e37-2869e89cd8fa"), 3, null, new DateTime(2026, 10, 28, 3, 6, 30, 659, DateTimeKind.Utc).AddTicks(574), new DateTime(2026, 10, 28, 3, 6, 30, 659, DateTimeKind.Utc).AddTicks(573), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("43ed5c96-b542-4939-9f28-efeb1281377e"), 1, null, new DateTime(2026, 10, 28, 3, 6, 30, 659, DateTimeKind.Utc).AddTicks(550), new DateTime(2026, 10, 28, 3, 6, 30, 659, DateTimeKind.Utc).AddTicks(546), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("49f0a656-7f9a-4f60-b7b3-491e99db66cc"), 1, null, new DateTime(2026, 10, 28, 3, 6, 30, 658, DateTimeKind.Utc).AddTicks(9393), new DateTime(2026, 10, 28, 3, 6, 30, 658, DateTimeKind.Utc).AddTicks(9384), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("7d731587-bcd7-4966-985a-d1bbaaa4b7f8"), 3, null, new DateTime(2026, 10, 28, 3, 6, 30, 659, DateTimeKind.Utc).AddTicks(556), new DateTime(2026, 10, 28, 3, 6, 30, 659, DateTimeKind.Utc).AddTicks(556), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("aa9b3336-2368-48db-b6b9-b220e3451dd9"), 3, null, new DateTime(2026, 10, 28, 3, 6, 30, 659, DateTimeKind.Utc).AddTicks(571), new DateTime(2026, 10, 28, 3, 6, 30, 659, DateTimeKind.Utc).AddTicks(570), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("cfb1dd1c-c540-4010-a47e-15a1e10e5652"), 3, null, new DateTime(2026, 10, 28, 3, 6, 30, 659, DateTimeKind.Utc).AddTicks(568), new DateTime(2026, 10, 28, 3, 6, 30, 659, DateTimeKind.Utc).AddTicks(567), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("f8a9ea6d-58dd-4849-ad6f-4ba9c86706ae"), 3, null, new DateTime(2026, 10, 28, 3, 6, 30, 659, DateTimeKind.Utc).AddTicks(554), new DateTime(2026, 10, 28, 3, 6, 30, 659, DateTimeKind.Utc).AddTicks(553), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("fabe6308-9fc3-4194-b4da-3fe9d392ab47"), 3, null, new DateTime(2026, 10, 28, 3, 6, 30, 659, DateTimeKind.Utc).AddTicks(563), new DateTime(2026, 10, 28, 3, 6, 30, 659, DateTimeKind.Utc).AddTicks(559), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 658, DateTimeKind.Utc).AddTicks(2928));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 658, DateTimeKind.Utc).AddTicks(4778));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 658, DateTimeKind.Utc).AddTicks(4782));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 658, DateTimeKind.Utc).AddTicks(4785));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 658, DateTimeKind.Utc).AddTicks(4787));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 658, DateTimeKind.Utc).AddTicks(4809));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 658, DateTimeKind.Utc).AddTicks(4812));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 658, DateTimeKind.Utc).AddTicks(4814));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 28, 3, 6, 30, 658, DateTimeKind.Utc).AddTicks(4817));

            migrationBuilder.CreateIndex(
                name: "IX_VendaItens_TipoItem_ItemId",
                table: "VendaItens",
                columns: new[] { "TipoItem", "ItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_VendaItens_VendaId",
                table: "VendaItens",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_AgendamentoId",
                table: "Vendas",
                column: "AgendamentoId",
                unique: true,
                filter: "[AgendamentoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_EmpresaId",
                table: "Vendas",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_EmpresaId_DataAbertura",
                table: "Vendas",
                columns: new[] { "EmpresaId", "DataAbertura" });

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_EmpresaId_Status",
                table: "Vendas",
                columns: new[] { "EmpresaId", "Status" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendaItens");

            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("272045a1-5dfa-4a45-8e37-2869e89cd8fa"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("43ed5c96-b542-4939-9f28-efeb1281377e"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("49f0a656-7f9a-4f60-b7b3-491e99db66cc"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("7d731587-bcd7-4966-985a-d1bbaaa4b7f8"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("aa9b3336-2368-48db-b6b9-b220e3451dd9"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("cfb1dd1c-c540-4010-a47e-15a1e10e5652"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("f8a9ea6d-58dd-4849-ad6f-4ba9c86706ae"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("fabe6308-9fc3-4194-b4da-3fe9d392ab47"));

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
        }
    }
}
