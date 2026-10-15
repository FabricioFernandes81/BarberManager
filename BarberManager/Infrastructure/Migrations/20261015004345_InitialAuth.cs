using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Auth");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissaoGrupo",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissaoGrupo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissoes",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "Auth",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "Auth",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "Auth",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosEmpresa",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DataVinculo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDesvinculo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosEmpresa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosEmpresa_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Auth",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissaos",
                schema: "Auth",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissaoGupoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissaos", x => new { x.RoleId, x.PermissaoGupoId });
                    table.ForeignKey(
                        name: "FK_RolePermissaos_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissaos_PermissaoGrupo_PermissaoGupoId",
                        column: x => x.PermissaoGupoId,
                        principalSchema: "Auth",
                        principalTable: "PermissaoGrupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioPermissaos",
                schema: "Auth",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioPermissaos", x => new { x.UsuarioId, x.PermissaoId });
                    table.ForeignKey(
                        name: "FK_UsuarioPermissaos_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Auth",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioPermissaos_PermissaoGrupo_PermissaoId",
                        column: x => x.PermissaoId,
                        principalSchema: "Auth",
                        principalTable: "PermissaoGrupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrupoPermissaoPermissaos",
                schema: "Auth",
                columns: table => new
                {
                    PermissaoGrupoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoPermissaoPermissaos", x => new { x.PermissaoGrupoId, x.PermissaoId });
                    table.ForeignKey(
                        name: "FK_GrupoPermissaoPermissaos_PermissaoGrupo_PermissaoGrupoId",
                        column: x => x.PermissaoGrupoId,
                        principalSchema: "Auth",
                        principalTable: "PermissaoGrupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrupoPermissaoPermissaos_Permissoes_PermissaoId",
                        column: x => x.PermissaoId,
                        principalSchema: "Auth",
                        principalTable: "Permissoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "AspNetRoles",
                columns: new[] { "Id", "Ativo", "ConcurrencyStamp", "Descricao", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("70000000-0000-0000-0000-000000000001"), true, null, "Administrador da Barbearia", "AdministradorBarber", "ADMINISTRADORBARBER" },
                    { new Guid("70000000-0000-0000-0000-000000000002"), true, "70000000-0000-0000-0000-000000000002", "Profissional com acesso à própria agenda.", "Barbeiro", "BARBEIRO" },
                    { new Guid("c0000000-0000-0000-0000-000000000001"), true, "C0000000-0000-0000-0000-000000000001", "Administrador da plataforma SaaS.", "Administrador", "ADMINISTRADOR" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "PermissaoGrupo",
                columns: new[] { "Id", "DataAtualizacao", "DataCadastro", "Descricao", "Nome" },
                values: new object[,]
                {
                    { new Guid("c0000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gerenciar configurações da plataforma.", "settings.manage" },
                    { new Guid("c0000000-0000-0000-0000-000000000007"), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gerenciar operações financeiras.", "financeiro.manage" },
                    { new Guid("c0000000-0000-0000-0000-000000000008"), null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Gerenciar clientes da barbearia.", "clientes.manage" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "Permissoes",
                columns: new[] { "Id", "Codigo", "DataAtualizacao", "DataCadastro", "Descricao", "Nome" },
                values: new object[,]
                {
                    { new Guid("80000000-0000-0000-0000-000000000002"), "cliente.visualizar", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(9670), "Permite visualizar os clientes cadastrados.", "Visualizar Clientes" },
                    { new Guid("80000000-0000-0000-0000-000000000003"), "cliente.criar", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(9671), "Permite cadastrar novos clientes.", "Cadastrar Clientes" },
                    { new Guid("80000000-0000-0000-0000-000000000004"), "cliente.editar", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(9673), "Permite editar clientes cadastrados.", "Editar Clientes" },
                    { new Guid("80000000-0000-0000-0000-000000000005"), "cliente.excluir", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(9674), "Permite excluir clientes cadastrados.", "Excluir Clientes" },
                    { new Guid("b0000000-0000-0000-0000-000000000013"), "empresa.visualizar", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(8190), "Permite listar empresas.", "Visualizar Empresas" },
                    { new Guid("b0000000-0000-0000-0000-000000000014"), "empresa.detalhes", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(9618), "Permite visualizar detalhes.", "Detalhes Empresa" },
                    { new Guid("b0000000-0000-0000-0000-000000000015"), "empresa.criar", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(9630), "Permite cadastrar empresas.", "Cadastrar Empresa" },
                    { new Guid("b0000000-0000-0000-0000-000000000016"), "empresa.editar", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(9632), "Permite editar empresas.", "Editar Empresa" },
                    { new Guid("b0000000-0000-0000-0000-000000000017"), "empresa.excluir", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(9634), "Permite excluir empresas.", "Excluir Empresa" },
                    { new Guid("b0000000-0000-0000-0000-000000000018"), "empresa.ativar", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(9640), "Permite ativar empresas.", "Ativar Empresa" },
                    { new Guid("b0000000-0000-0000-0000-000000000019"), "empresa.bloquear", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(9642), "Permite bloquear empresas.", "Bloquear Empresa" },
                    { new Guid("b0000000-0000-0000-0000-000000000020"), "empresa.desbloquear", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(9643), "Permite desbloquear empresas.", "Desbloquear Empresa" },
                    { new Guid("b0000000-0000-0000-0000-000000000021"), "empresa.renovar", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(9645), "Permite renovar assinatura.", "Renovar Assinatura" },
                    { new Guid("b0000000-0000-0000-0000-000000000022"), "empresa.estatisticas", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(9647), "Permite visualizar estatísticas.", "Estatísticas" },
                    { new Guid("b0000000-0000-0000-0000-000000000023"), "empresa.usuarios", null, new DateTime(2026, 10, 15, 0, 43, 44, 811, DateTimeKind.Utc).AddTicks(9651), "Permite visualizar usuários da empresa.", "Usuários" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "GrupoPermissaoPermissaos",
                columns: new[] { "PermissaoGrupoId", "PermissaoId" },
                values: new object[,]
                {
                    { new Guid("c0000000-0000-0000-0000-000000000001"), new Guid("b0000000-0000-0000-0000-000000000014") },
                    { new Guid("c0000000-0000-0000-0000-000000000001"), new Guid("b0000000-0000-0000-0000-000000000016") },
                    { new Guid("c0000000-0000-0000-0000-000000000008"), new Guid("80000000-0000-0000-0000-000000000002") },
                    { new Guid("c0000000-0000-0000-0000-000000000008"), new Guid("80000000-0000-0000-0000-000000000003") },
                    { new Guid("c0000000-0000-0000-0000-000000000008"), new Guid("80000000-0000-0000-0000-000000000004") },
                    { new Guid("c0000000-0000-0000-0000-000000000008"), new Guid("80000000-0000-0000-0000-000000000005") }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "RolePermissaos",
                columns: new[] { "PermissaoGupoId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("c0000000-0000-0000-0000-000000000008"), new Guid("70000000-0000-0000-0000-000000000001") },
                    { new Guid("c0000000-0000-0000-0000-000000000001"), new Guid("c0000000-0000-0000-0000-000000000001") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "Auth",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Auth",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "Auth",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "Auth",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "Auth",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Auth",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Auth",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoPermissaoPermissaos_PermissaoId",
                schema: "Auth",
                table: "GrupoPermissaoPermissaos",
                column: "PermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissaos_PermissaoGupoId",
                schema: "Auth",
                table: "RolePermissaos",
                column: "PermissaoGupoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioPermissaos_PermissaoId",
                schema: "Auth",
                table: "UsuarioPermissaos",
                column: "PermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosEmpresa_UsuarioId_EmpresaId",
                schema: "Auth",
                table: "UsuariosEmpresa",
                columns: new[] { "UsuarioId", "EmpresaId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "GrupoPermissaoPermissaos",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "RolePermissaos",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "UsuarioPermissaos",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "UsuariosEmpresa",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Permissoes",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "PermissaoGrupo",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "Auth");
        }
    }
}
