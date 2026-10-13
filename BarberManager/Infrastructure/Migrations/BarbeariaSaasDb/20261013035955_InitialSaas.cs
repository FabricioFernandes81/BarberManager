using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaSaasDb
{
    /// <inheritdoc />
    public partial class InitialSaas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RazaoSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeFantasia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativa = table.Column<int>(type: "int", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataBloqueio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MotivoBloqueio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorMensal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorAnual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LimiteProfissionais = table.Column<int>(type: "int", nullable: false),
                    LimiteEmpresas = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assinaturas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RenovacaoAutomatica = table.Column<bool>(type: "bit", nullable: false),
                    Trial = table.Column<bool>(type: "bit", nullable: false),
                    Ativa = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assinaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assinaturas_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assinaturas_Planos_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Planos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "Id", "Ativa", "Cnpj", "DataAtualizacao", "DataBloqueio", "DataCadastro", "Email", "LogoUrl", "MotivoBloqueio", "NomeFantasia", "RazaoSocial", "Telefone" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), 1, "11.111.111/0001-11", null, null, new DateTime(2026, 10, 13, 3, 59, 55, 460, DateTimeKind.Utc).AddTicks(4512), "contato@barbeariavintage.com.br", null, null, "Barbearia Vintage", "Barbearia Vintage LTDA", "(51) 3333-1111" },
                    { new Guid("50000000-0000-0000-0000-000000000002"), 1, "22.222.222/0001-22", null, null, new DateTime(2026, 10, 13, 3, 59, 55, 460, DateTimeKind.Utc).AddTicks(7531), "contato@oldschoolbarber.com.br", null, null, "Old School Barber", "Old School Barber LTDA", "(51) 3333-2222" },
                    { new Guid("50000000-0000-0000-0000-000000000003"), 1, "33.333.333/0001-33", null, null, new DateTime(2026, 10, 13, 3, 59, 55, 460, DateTimeKind.Utc).AddTicks(7536), "contato@blackbeard.com.br", null, null, "Black Beard Barber", "Black Beard Barber LTDA", "(51) 3333-3333" },
                    { new Guid("50000000-0000-0000-0000-000000000004"), 1, "44.444.444/0001-44", null, null, new DateTime(2026, 10, 13, 3, 59, 55, 460, DateTimeKind.Utc).AddTicks(7537), "contato@imperiobarber.com.br", null, null, "Império Barber", "Império Barber LTDA", "(51) 3333-4444" },
                    { new Guid("50000000-0000-0000-0000-000000000005"), 1, "55.555.555/0001-55", null, null, new DateTime(2026, 10, 13, 3, 59, 55, 460, DateTimeKind.Utc).AddTicks(7539), "contato@domcorte.com.br", null, null, "Dom Corte", "Dom Corte LTDA", "(51) 3333-5555" },
                    { new Guid("50000000-0000-0000-0000-000000000006"), 1, "66.666.666/0001-66", null, null, new DateTime(2026, 10, 13, 3, 59, 55, 460, DateTimeKind.Utc).AddTicks(7541), "contato@primebarber.com.br", null, null, "Prime Barber Club", "Prime Barber Club LTDA", "(51) 3333-6666" }
                });

            migrationBuilder.InsertData(
                table: "Planos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "LimiteEmpresas", "LimiteProfissionais", "Nome", "ValorAnual", "ValorMensal" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), 1, null, new DateTime(2026, 10, 13, 3, 59, 55, 462, DateTimeKind.Utc).AddTicks(4264), 1, 2, "Trial", 0m, 0m },
                    { new Guid("20000000-0000-0000-0000-000000000002"), 1, null, new DateTime(2026, 10, 13, 3, 59, 55, 462, DateTimeKind.Utc).AddTicks(7458), 1, 3, "Starter", 0m, 49.90m },
                    { new Guid("20000000-0000-0000-0000-000000000003"), 1, null, new DateTime(2026, 10, 13, 3, 59, 55, 462, DateTimeKind.Utc).AddTicks(7632), 1, 5, "Basic", 49.90m, 89.90m },
                    { new Guid("20000000-0000-0000-0000-000000000004"), 1, null, new DateTime(2026, 10, 13, 3, 59, 55, 462, DateTimeKind.Utc).AddTicks(7634), 1, 10, "Professional", 130m, 149.90m },
                    { new Guid("20000000-0000-0000-0000-000000000005"), 1, null, new DateTime(2026, 10, 13, 3, 59, 55, 462, DateTimeKind.Utc).AddTicks(7641), 1, 25, "Premium", 211.50m, 249.90m },
                    { new Guid("20000000-0000-0000-0000-000000000006"), 1, null, new DateTime(2026, 10, 13, 3, 59, 55, 462, DateTimeKind.Utc).AddTicks(7653), 999, 999, "Enterprise", 480.65m, 499.90m }
                });

            migrationBuilder.InsertData(
                table: "Assinaturas",
                columns: new[] { "Id", "Ativa", "DataAtualizacao", "DataCadastro", "DataInicio", "DataVencimento", "EmpresaId", "PlanoId", "RenovacaoAutomatica", "Trial" },
                values: new object[,]
                {
                    { new Guid("60000000-0000-0000-0000-000000000001"), 1, null, new DateTime(2026, 10, 13, 3, 59, 55, 463, DateTimeKind.Utc).AddTicks(3460), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("50000000-0000-0000-0000-000000000001"), new Guid("20000000-0000-0000-0000-000000000001"), false, true },
                    { new Guid("60000000-0000-0000-0000-000000000002"), 1, null, new DateTime(2026, 10, 13, 3, 59, 55, 463, DateTimeKind.Utc).AddTicks(5338), new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("50000000-0000-0000-0000-000000000002"), new Guid("20000000-0000-0000-0000-000000000002"), true, false },
                    { new Guid("60000000-0000-0000-0000-000000000003"), 1, null, new DateTime(2026, 10, 13, 3, 59, 55, 463, DateTimeKind.Utc).AddTicks(5358), new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("50000000-0000-0000-0000-000000000003"), new Guid("20000000-0000-0000-0000-000000000003"), true, false },
                    { new Guid("60000000-0000-0000-0000-000000000004"), 1, null, new DateTime(2026, 10, 13, 3, 59, 55, 463, DateTimeKind.Utc).AddTicks(5360), new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("50000000-0000-0000-0000-000000000004"), new Guid("20000000-0000-0000-0000-000000000004"), true, false },
                    { new Guid("60000000-0000-0000-0000-000000000005"), 1, null, new DateTime(2026, 10, 13, 3, 59, 55, 463, DateTimeKind.Utc).AddTicks(5362), new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("50000000-0000-0000-0000-000000000005"), new Guid("20000000-0000-0000-0000-000000000005"), true, false },
                    { new Guid("60000000-0000-0000-0000-000000000006"), 1, null, new DateTime(2026, 10, 13, 3, 59, 55, 463, DateTimeKind.Utc).AddTicks(5374), new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2027, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("50000000-0000-0000-0000-000000000006"), new Guid("20000000-0000-0000-0000-000000000006"), true, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assinaturas_EmpresaId",
                table: "Assinaturas",
                column: "EmpresaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assinaturas_PlanoId",
                table: "Assinaturas",
                column: "PlanoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assinaturas");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Planos");
        }
    }
}
