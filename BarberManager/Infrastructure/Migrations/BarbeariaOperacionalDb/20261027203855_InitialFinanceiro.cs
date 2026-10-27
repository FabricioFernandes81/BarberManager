using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class InitialFinanceiro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("1e4e02bd-29d8-4351-9157-1dd79f55c82d"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("2761763f-2415-41c4-ab79-802a1ab4ede4"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("3aa05ab0-d48e-4dc1-abd3-24baa63d64c9"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("5b2ffbbe-761b-469c-b0b4-4a1094089193"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("7e567cca-34f2-483b-83a8-c077f729323f"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("80ffc4b0-6124-4c0d-86a9-97dc6f4f58b6"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("cc09a445-ee9c-4706-880c-d45cdab5e8fd"));

            migrationBuilder.DeleteData(
                table: "ProfissionalServicos",
                keyColumn: "Id",
                keyValue: new Guid("daa1bce0-349d-4cea-b1d9-77ddbc486c94"));

            migrationBuilder.CreateTable(
                name: "Caixa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataAbertura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFechamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SaldoInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurarcaoAssasCliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApiToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebhooksToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurarcaoAssasCliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContaPagar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaPagar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContaReceber",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataEmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorRecebido = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaReceber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContaReceber_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormaPagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PermiteParcelamento = table.Column<bool>(type: "bit", nullable: false),
                    GeraTroco = table.Column<bool>(type: "bit", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimentoCaixa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaixaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoMovimento = table.Column<int>(type: "int", nullable: false),
                    FormaPagamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataMovimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Historico = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentoCaixa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentoCaixa_Caixa_CaixaId",
                        column: x => x.CaixaId,
                        principalTable: "Caixa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentoCaixa_FormaPagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContaPagarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormaPagamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamento_ContaPagar_ContaPagarId",
                        column: x => x.ContaPagarId,
                        principalTable: "ContaPagar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pagamento_FormaPagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recebimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContaReceberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormaPagamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataRecebimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recebimento", x => x.Id);
                    table.CheckConstraint("CK_Recebimento_Valor_Positivo", "[Valor] > 0");
                    table.ForeignKey(
                        name: "FK_Recebimento_ContaReceber_ContaReceberId",
                        column: x => x.ContaReceberId,
                        principalTable: "ContaReceber",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recebimento_FormaPagamento_FormaPagamentoId",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormaPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_DataAbertura",
                table: "Caixa",
                column: "DataAbertura");

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_EmpresaId",
                table: "Caixa",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_Status",
                table: "Caixa",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_UsuarioId",
                table: "Caixa",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurarcaoAssasCliente_EmpresaId",
                table: "ConfigurarcaoAssasCliente",
                column: "EmpresaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContaPagar_DataVencimento",
                table: "ContaPagar",
                column: "DataVencimento");

            migrationBuilder.CreateIndex(
                name: "IX_ContaPagar_EmpresaId",
                table: "ContaPagar",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContaPagar_Status",
                table: "ContaPagar",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ContaReceber_ClienteId",
                table: "ContaReceber",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ContaReceber_EmpresaId",
                table: "ContaReceber",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContaReceber_EmpresaId_ClienteId",
                table: "ContaReceber",
                columns: new[] { "EmpresaId", "ClienteId" });

            migrationBuilder.CreateIndex(
                name: "IX_ContaReceber_EmpresaId_DataVencimento",
                table: "ContaReceber",
                columns: new[] { "EmpresaId", "DataVencimento" });

            migrationBuilder.CreateIndex(
                name: "IX_ContaReceber_EmpresaId_Status",
                table: "ContaReceber",
                columns: new[] { "EmpresaId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamento_EmpresaId",
                table: "FormaPagamento",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamento_Nome",
                table: "FormaPagamento",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoCaixa_CaixaId",
                table: "MovimentoCaixa",
                column: "CaixaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoCaixa_DataMovimento",
                table: "MovimentoCaixa",
                column: "DataMovimento");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoCaixa_FormaPagamentoId",
                table: "MovimentoCaixa",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoCaixa_TipoMovimento",
                table: "MovimentoCaixa",
                column: "TipoMovimento");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_ContaPagarId",
                table: "Pagamento",
                column: "ContaPagarId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_DataPagamento",
                table: "Pagamento",
                column: "DataPagamento");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_FormaPagamentoId",
                table: "Pagamento",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Recebimento_ContaReceberId",
                table: "Recebimento",
                column: "ContaReceberId");

            migrationBuilder.CreateIndex(
                name: "IX_Recebimento_DataRecebimento",
                table: "Recebimento",
                column: "DataRecebimento");

            migrationBuilder.CreateIndex(
                name: "IX_Recebimento_FormaPagamentoId",
                table: "Recebimento",
                column: "FormaPagamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurarcaoAssasCliente");

            migrationBuilder.DropTable(
                name: "MovimentoCaixa");

            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "Recebimento");

            migrationBuilder.DropTable(
                name: "Caixa");

            migrationBuilder.DropTable(
                name: "ContaPagar");

            migrationBuilder.DropTable(
                name: "ContaReceber");

            migrationBuilder.DropTable(
                name: "FormaPagamento");

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

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 672, DateTimeKind.Utc).AddTicks(7680));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 672, DateTimeKind.Utc).AddTicks(9754));

            migrationBuilder.UpdateData(
                table: "CategoriasServicos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 672, DateTimeKind.Utc).AddTicks(9757));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 676, DateTimeKind.Utc).AddTicks(342));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 676, DateTimeKind.Utc).AddTicks(1106));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 676, DateTimeKind.Utc).AddTicks(1109));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 676, DateTimeKind.Utc).AddTicks(1115));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 676, DateTimeKind.Utc).AddTicks(1117));

            migrationBuilder.UpdateData(
                table: "HorariosFuncionamento",
                keyColumn: "Id",
                keyValue: new Guid("81000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 676, DateTimeKind.Utc).AddTicks(1118));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000001"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 670, DateTimeKind.Utc).AddTicks(9743));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000002"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 671, DateTimeKind.Utc).AddTicks(6870));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000003"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 671, DateTimeKind.Utc).AddTicks(6884));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000004"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 671, DateTimeKind.Utc).AddTicks(6889));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000005"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 671, DateTimeKind.Utc).AddTicks(6894));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000006"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 671, DateTimeKind.Utc).AddTicks(6939));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000007"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 671, DateTimeKind.Utc).AddTicks(6944));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000008"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 671, DateTimeKind.Utc).AddTicks(6950));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000009"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 671, DateTimeKind.Utc).AddTicks(6970));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000010"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 671, DateTimeKind.Utc).AddTicks(6975));

            migrationBuilder.UpdateData(
                table: "Profissionais",
                keyColumn: "Id",
                keyValue: new Guid("91000000-0000-0000-0000-000000000011"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 671, DateTimeKind.Utc).AddTicks(6979));

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("1e4e02bd-29d8-4351-9157-1dd79f55c82d"), 3, null, new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(5322), new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(5321), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m },
                    { new Guid("2761763f-2415-41c4-ab79-802a1ab4ede4"), 3, null, new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(5325), new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(5324), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("3aa05ab0-d48e-4dc1-abd3-24baa63d64c9"), 1, null, new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(4029), new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(4020), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("5b2ffbbe-761b-469c-b0b4-4a1094089193"), 3, null, new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(5328), new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(5327), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("7e567cca-34f2-483b-83a8-c077f729323f"), 3, null, new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(5337), new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(5336), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("80ffc4b0-6124-4c0d-86a9-97dc6f4f58b6"), 3, null, new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(5340), new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(5339), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("cc09a445-ee9c-4706-880c-d45cdab5e8fd"), 1, null, new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(5318), new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(5313), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("daa1bce0-349d-4cea-b1d9-77ddbc486c94"), 3, null, new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(5343), new DateTime(2026, 10, 25, 3, 18, 59, 674, DateTimeKind.Utc).AddTicks(5342), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m }
                });

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 673, DateTimeKind.Utc).AddTicks(6381));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 673, DateTimeKind.Utc).AddTicks(8735));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 673, DateTimeKind.Utc).AddTicks(8747));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 673, DateTimeKind.Utc).AddTicks(8749));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 673, DateTimeKind.Utc).AddTicks(8752));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 673, DateTimeKind.Utc).AddTicks(8763));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 673, DateTimeKind.Utc).AddTicks(8766));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 673, DateTimeKind.Utc).AddTicks(8768));

            migrationBuilder.UpdateData(
                table: "Servicos",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                column: "DataCadastro",
                value: new DateTime(2026, 10, 25, 3, 18, 59, 673, DateTimeKind.Utc).AddTicks(8771));
        }
    }
}
