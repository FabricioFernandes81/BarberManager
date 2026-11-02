using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations.BarbeariaOperacionalDb
{
    /// <inheritdoc />
    public partial class InitialOperacional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    SaldoEsperadoFechamento = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiferencaFechamento = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ObservacaoFechamento = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriasProdutos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasProdutos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriasServicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasServicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracoesAgendaEmpresa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToleranciaEncerramentoMinutos = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracoesAgendaEmpresa", x => x.Id);
                    table.CheckConstraint("CK_ConfiguracoesAgendaEmpresa_Tolerancia", "[ToleranciaEncerramentoMinutos] >= 0");
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
                name: "Feriados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feriados", x => x.Id);
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
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NomeFantasia = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Documento = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HorariosFuncionamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFim = table.Column<TimeOnly>(type: "time", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorariosFuncionamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanosAssinatura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Periodicidade = table.Column<int>(type: "int", nullable: false),
                    QuantidadeCiclos = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<int>(type: "int", nullable: false),
                    DisponivelOnline = table.Column<bool>(type: "bit", nullable: false),
                    DataInicioVigencia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFimVigencia = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanosAssinatura", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profissionais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataAdmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDemissao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TipoComissao = table.Column<int>(type: "int", nullable: false),
                    ValorTipoPagamento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IntervaloAtendimento = table.Column<int>(type: "int", nullable: false),
                    PermiteEncaixe = table.Column<bool>(type: "bit", nullable: false),
                    AgendaOnline = table.Column<bool>(type: "bit", nullable: false),
                    Biografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToleranciaEncerramentoMinutos = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ExibirAgendamentoOnline = table.Column<bool>(type: "bit", nullable: false),
                    RecebePagamentoDireto = table.Column<bool>(type: "bit", nullable: false),
                    OrdemAgenda = table.Column<int>(type: "int", nullable: false),
                    AvaliacaoMedia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAvaliacoes = table.Column<int>(type: "int", nullable: false),
                    TotalComissaoMes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Especialidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destaque = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissionais", x => x.Id);
                    table.CheckConstraint("CK_Profissionais_ToleranciaEncerramento", "[ToleranciaEncerramentoMinutos] IS NULL OR [ToleranciaEncerramentoMinutos] >= 0");
                });

            migrationBuilder.CreateTable(
                name: "Servicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Custo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DuracaoMinutos = table.Column<int>(type: "int", nullable: false),
                    IntervaloMinutos = table.Column<int>(type: "int", nullable: false),
                    TipoComissao = table.Column<int>(type: "int", nullable: false),
                    PercentualComissao = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PermiteAgendamentoOnline = table.Column<bool>(type: "bit", nullable: false),
                    ExibirAplicativo = table.Column<bool>(type: "bit", nullable: false),
                    Ativo = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicos_CategoriasServicos_CategoriaServicoId",
                        column: x => x.CategoriaServicoId,
                        principalTable: "CategoriasServicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientesEmpresas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataVinculo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPrimeiroAtendimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataUltimoAtendimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ObservacoesInternas = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesEmpresas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientesEmpresas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SerieDocumento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DataEmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Frete = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OutrasDespesas = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MarcaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CodigoInterno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodigoBarras = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ValorVenda = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Custo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ControlaEstoque = table.Column<bool>(type: "bit", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_CategoriasProdutos_CategoriaProdutoId",
                        column: x => x.CategoriaProdutoId,
                        principalTable: "CategoriasProdutos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produtos_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssinaturasClientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanoAssinaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorContratado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Periodicidade = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFimCicloAtual = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataProximaCobranca = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCancelamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RenovacaoAutomatica = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ReferenciaGateway = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssinaturasClientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssinaturasClientes_PlanosAssinatura_PlanoAssinaturaId",
                        column: x => x.PlanoAssinaturaId,
                        principalTable: "PlanosAssinatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BeneficiosPlano",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanoAssinaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiosPlano", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeneficiosPlano_PlanosAssinatura_PlanoAssinaturaId",
                        column: x => x.PlanoAssinaturaId,
                        principalTable: "PlanosAssinatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanoAssinaturaServicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanoAssinaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantidadePorCiclo = table.Column<int>(type: "int", nullable: true),
                    LimitePorDia = table.Column<int>(type: "int", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoAssinaturaServicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanoAssinaturaServicos_PlanosAssinatura_PlanoAssinaturaId",
                        column: x => x.PlanoAssinaturaId,
                        principalTable: "PlanosAssinatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agendamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHoraInicio = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DataHoraFim = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DataCancelamento = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    MotivoCancelamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanceladoPorUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agendamentos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamentos_Profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloqueiosAgenda",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataHoraInicio = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DataHoraFim = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloqueiosAgenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloqueiosAgenda_Profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissionais",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HorariosProfissionais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFim = table.Column<TimeOnly>(type: "time", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorariosProfissionais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HorariosProfissionais_Profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfissionalServicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfissionalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorPersonalizado = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DuracaoPersonalizada = table.Column<int>(type: "int", nullable: true),
                    DataVinculo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfissionalServicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfissionalServicos_Profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfissionalServicos_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContaPagar",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorPago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CompraId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NumeroParcela = table.Column<int>(type: "int", nullable: false),
                    TotalParcelas = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaPagar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContaPagar_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContaPagar_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompraItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DescricaoProduto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    QuantidadeRecebida = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompraItens_Compras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompraItens_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EstoquesProdutos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantidadeAtual = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    EstoqueMinimo = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    DataAtualizacaoEstoque = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstoquesProdutos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstoquesProdutos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovimentacoesEstoque",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Origem = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    SaldoAnterior = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    SaldoPosterior = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    DocumentoOrigemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UsuarioResponsavelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataMovimento = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacoesEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoque_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BeneficiariosAssinatura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssinaturaClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titular = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DataVinculo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDesvinculo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiariosAssinatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BeneficiariosAssinatura_AssinaturasClientes_AssinaturaClienteId",
                        column: x => x.AssinaturaClienteId,
                        principalTable: "AssinaturasClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgendamentoServicos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgendamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeServico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DuracaoMinutos = table.Column<int>(type: "int", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendamentoServicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendamentoServicos_Agendamentos_AgendamentoId",
                        column: x => x.AgendamentoId,
                        principalTable: "Agendamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgendamentoServicos_Servicos_ServicoId",
                        column: x => x.ServicoId,
                        principalTable: "Servicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "UtilizacoesAssinatura",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssinaturaClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeneficiarioAssinaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgendamentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataUtilizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Observacao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilizacoesAssinatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UtilizacoesAssinatura_AssinaturasClientes_AssinaturaClienteId",
                        column: x => x.AssinaturaClienteId,
                        principalTable: "AssinaturasClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UtilizacoesAssinatura_BeneficiariosAssinatura_BeneficiarioAssinaturaId",
                        column: x => x.BeneficiarioAssinaturaId,
                        principalTable: "BeneficiariosAssinatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    VendaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_ContaReceber_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Vendas",
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

            migrationBuilder.InsertData(
                table: "CategoriasProdutos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "Descricao", "EmpresaId", "Nome" },
                values: new object[,]
                {
                    { new Guid("a1000000-0000-0000-0000-000000000001"), true, null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Produtos para finalização e modelagem capilar.", new Guid("50000000-0000-0000-0000-000000000001"), "Pomadas e Finalizadores" },
                    { new Guid("a1000000-0000-0000-0000-000000000002"), true, null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Produtos para limpeza e cuidados capilares.", new Guid("50000000-0000-0000-0000-000000000001"), "Shampoos e Higiene" },
                    { new Guid("a1000000-0000-0000-0000-000000000003"), true, null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Acessórios utilizados em barbearias.", new Guid("50000000-0000-0000-0000-000000000001"), "Acessórios" }
                });

            migrationBuilder.InsertData(
                table: "CategoriasServicos",
                columns: new[] { "Id", "Ativo", "Cor", "DataAtualizacao", "DataCadastro", "Descricao", "EmpresaId", "Icone", "Nome", "Ordem" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222222"), 1, "#FF5733", null, new DateTime(2026, 11, 2, 16, 57, 6, 733, DateTimeKind.Utc).AddTicks(3962), "Serviço de corte de cabelo", new Guid("50000000-0000-0000-0000-000000000001"), "content_cut", "Cabelo", 1 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 1, "#33FF57", null, new DateTime(2026, 11, 2, 16, 57, 6, 733, DateTimeKind.Utc).AddTicks(6087), "Serviço de categoria 2", new Guid("50000000-0000-0000-0000-000000000001"), "icone-categoria2", "Mão e Pés", 2 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 1, "#3357FF", null, new DateTime(2026, 11, 2, 16, 57, 6, 733, DateTimeKind.Utc).AddTicks(6096), "Serviço de categoria 3", new Guid("50000000-0000-0000-0000-000000000001"), "icone-categoria3", "Barba e Bigode", 3 }
                });

            migrationBuilder.InsertData(
                table: "HorariosFuncionamento",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DiaSemana", "EmpresaId", "HoraFim", "HoraInicio" },
                values: new object[,]
                {
                    { new Guid("81000000-0000-0000-0000-000000000001"), true, null, new DateTime(2026, 11, 2, 16, 57, 6, 736, DateTimeKind.Utc).AddTicks(4393), 1, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(8, 0, 0) },
                    { new Guid("81000000-0000-0000-0000-000000000002"), true, null, new DateTime(2026, 11, 2, 16, 57, 6, 736, DateTimeKind.Utc).AddTicks(5150), 2, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(8, 0, 0) },
                    { new Guid("81000000-0000-0000-0000-000000000003"), true, null, new DateTime(2026, 11, 2, 16, 57, 6, 736, DateTimeKind.Utc).AddTicks(5152), 3, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(8, 0, 0) },
                    { new Guid("81000000-0000-0000-0000-000000000004"), true, null, new DateTime(2026, 11, 2, 16, 57, 6, 736, DateTimeKind.Utc).AddTicks(5153), 4, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(8, 0, 0) },
                    { new Guid("81000000-0000-0000-0000-000000000005"), true, null, new DateTime(2026, 11, 2, 16, 57, 6, 736, DateTimeKind.Utc).AddTicks(5155), 5, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(8, 0, 0) },
                    { new Guid("81000000-0000-0000-0000-000000000006"), true, null, new DateTime(2026, 11, 2, 16, 57, 6, 736, DateTimeKind.Utc).AddTicks(5156), 6, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(13, 0, 0), new TimeOnly(8, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "Descricao", "EmpresaId", "Nome" },
                values: new object[,]
                {
                    { new Guid("a2000000-0000-0000-0000-000000000001"), true, null, new DateTime(2026, 11, 2, 16, 57, 6, 737, DateTimeKind.Utc).AddTicks(6224), "Linha profissional para barbearias.", new Guid("50000000-0000-0000-0000-000000000001"), "Barber Pro" },
                    { new Guid("a2000000-0000-0000-0000-000000000002"), true, null, new DateTime(2026, 11, 2, 16, 57, 6, 737, DateTimeKind.Utc).AddTicks(7158), "Produtos clássicos para cuidados pessoais.", new Guid("50000000-0000-0000-0000-000000000001"), "Classic Grooming" },
                    { new Guid("a2000000-0000-0000-0000-000000000003"), true, null, new DateTime(2026, 11, 2, 16, 57, 6, 737, DateTimeKind.Utc).AddTicks(7161), "Linha premium de cuidados capilares.", new Guid("50000000-0000-0000-0000-000000000001"), "Premium Care" }
                });

            migrationBuilder.InsertData(
                table: "PlanosAssinatura",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataFimVigencia", "DataInicioVigencia", "Descricao", "DisponivelOnline", "EmpresaId", "Nome", "Periodicidade", "QuantidadeCiclos", "Valor" },
                values: new object[,]
                {
                    { new Guid("a1000000-0000-0000-0000-000000000001"), 1, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Plano de teste com até quatro cortes por ciclo.", true, new Guid("50000000-0000-0000-0000-000000000001"), "Corte Mensal", 1, 1, 99.90m },
                    { new Guid("a1000000-0000-0000-0000-000000000002"), 1, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Plano de teste para corte e barba recorrentes.", true, new Guid("50000000-0000-0000-0000-000000000001"), "Corte e Barba", 1, 1, 179.90m },
                    { new Guid("a1000000-0000-0000-0000-000000000003"), 1, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Plano demonstrativo para contratação familiar.", true, new Guid("50000000-0000-0000-0000-000000000001"), "Plano Família", 1, 1, 249.90m },
                    { new Guid("a1000000-0000-0000-0000-000000000004"), 1, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "Plano demonstrativo com benefícios exclusivos.", true, new Guid("50000000-0000-0000-0000-000000000001"), "Clube VIP", 1, 1, 299.90m }
                });

            migrationBuilder.InsertData(
                table: "Profissionais",
                columns: new[] { "Id", "AgendaOnline", "AvaliacaoMedia", "Biografia", "Cargo", "Celular", "Cpf", "DataAdmissao", "DataAtualizacao", "DataCadastro", "DataDemissao", "DataNascimento", "Destaque", "Email", "EmpresaId", "Especialidade", "ExibirAgendamentoOnline", "Facebook", "Foto", "Instagram", "IntervaloAtendimento", "Matricula", "Nome", "OrdemAgenda", "PermiteEncaixe", "RecebePagamentoDireto", "Rg", "Sexo", "Sobrenome", "Status", "Telefone", "TipoComissao", "ToleranciaEncerramentoMinutos", "TotalAvaliacoes", "TotalComissaoMes", "UsuarioId", "ValorTipoPagamento" },
                values: new object[,]
                {
                    { new Guid("91000000-0000-0000-0000-000000000001"), true, 4.5m, null, "Barbeiro", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 11, 2, 16, 57, 6, 731, DateTimeKind.Utc).AddTicks(3956), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "carlos.henrique@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Carlos Henrique", 1, true, true, "22222222222", 1, "ffffffffffff", 1, "51515151515", 4, null, 0, 0m, new Guid("90000000-0000-0000-0000-000000000008"), 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000002"), true, 4.5m, null, "Barbeiro", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 11, 2, 16, 57, 6, 732, DateTimeKind.Utc).AddTicks(2139), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Alexandre =", 1, true, true, "22222222222", 1, "Fernandes", 1, "51515151515", 4, null, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000003"), true, 4.5m, null, "Administrador", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 11, 2, 16, 57, 6, 732, DateTimeKind.Utc).AddTicks(2150), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "0000000", "Roberto Almeida", 1, true, true, "22222222222", 1, "Fernandes", 1, "51515151515", 0, null, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000004"), true, 4.5m, null, "Recepcionista", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 11, 2, 16, 57, 6, 732, DateTimeKind.Utc).AddTicks(2156), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Bete Carvalho", 1, true, true, "22222222222", 1, "Faaaaaaaaa", 1, "51515151515", 0, null, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000005"), true, 4.5m, null, "Caixa", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 11, 2, 16, 57, 6, 732, DateTimeKind.Utc).AddTicks(2161), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Alexandre11111111 = aaaaa", 1, true, true, "22222222222", 1, "Fernandes", 1, "51515151515", 4, null, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000006"), true, 4.5m, null, "Estoquita", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 11, 2, 16, 57, 6, 732, DateTimeKind.Utc).AddTicks(2222), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Agenor Rodrigues", 1, true, true, "22222222222", 1, "Fernandes", 1, "51515151515", 0, null, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000007"), true, 4.5m, null, "Financeiro", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 11, 2, 16, 57, 6, 732, DateTimeKind.Utc).AddTicks(2231), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Paulo Silva", 1, true, true, "22222222222", 1, "a", 1, "51515151515", 4, null, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000008"), true, 4.5m, null, "Barbeiro", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 11, 2, 16, 57, 6, 732, DateTimeKind.Utc).AddTicks(2238), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Paula Mota", 1, true, true, "22222222222", 1, "Fernandes", 1, "51515151515", 4, null, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000009"), true, 4.5m, null, "Barbeiro", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 11, 2, 16, 57, 6, 732, DateTimeKind.Utc).AddTicks(2246), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "fabricio.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000002"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Fabricio Machado", 1, true, true, "22222222222", 1, "Fernandes", 1, "51515151515", 4, null, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000010"), true, 4.5m, null, "Barbeiro", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 11, 2, 16, 57, 6, 732, DateTimeKind.Utc).AddTicks(2255), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "alexandre.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Paula Mota", 1, true, true, "22222222222", 2, "Fernandes", 1, "51515151515", 4, null, 0, 0m, null, 10.0m },
                    { new Guid("91000000-0000-0000-0000-000000000011"), true, 4.5m, null, "Barbeira", "11999999999", "123 ", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2026, 11, 2, 16, 57, 6, 732, DateTimeKind.Utc).AddTicks(2262), null, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "rebeca.fernandes@example.com", new Guid("50000000-0000-0000-0000-000000000001"), "Cortes de cabelo e barba", true, null, null, null, 30, "123456", "Rebeca Fernandes", 1, true, true, "22222222222", 1, "Fernandes", 1, "51515151515", 4, null, 0, 0m, null, 10.0m }
                });

            migrationBuilder.InsertData(
                table: "BeneficiosPlano",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "Descricao", "EmpresaId", "Nome", "PlanoAssinaturaId", "Tipo", "Valor" },
                values: new object[,]
                {
                    { new Guid("a2000000-0000-0000-0000-000000000001"), true, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Desconto demonstrativo de 10% em produtos elegíveis.", new Guid("50000000-0000-0000-0000-000000000001"), "Desconto em cosméticos", new Guid("a1000000-0000-0000-0000-000000000004"), 1, 10m },
                    { new Guid("a2000000-0000-0000-0000-000000000002"), true, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Uma cortesia conforme as regras da barbearia.", new Guid("50000000-0000-0000-0000-000000000001"), "Café de cortesia", new Guid("a1000000-0000-0000-0000-000000000004"), 4, null }
                });

            migrationBuilder.InsertData(
                table: "HorariosProfissionais",
                columns: new[] { "Id", "Ativo", "DiaSemana", "EmpresaId", "HoraFim", "HoraInicio", "ProfissionalId" },
                values: new object[,]
                {
                    { new Guid("82000000-0000-0000-0000-000000000001"), true, 1, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(12, 0, 0), new TimeOnly(9, 0, 0), new Guid("91000000-0000-0000-0000-000000000001") },
                    { new Guid("82000000-0000-0000-0000-000000000002"), true, 1, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(13, 0, 0), new Guid("91000000-0000-0000-0000-000000000001") },
                    { new Guid("82000000-0000-0000-0000-000000000003"), true, 2, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(12, 0, 0), new TimeOnly(9, 0, 0), new Guid("91000000-0000-0000-0000-000000000001") },
                    { new Guid("82000000-0000-0000-0000-000000000004"), true, 2, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(13, 0, 0), new Guid("91000000-0000-0000-0000-000000000001") },
                    { new Guid("82000000-0000-0000-0000-000000000005"), true, 3, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(12, 0, 0), new TimeOnly(9, 0, 0), new Guid("91000000-0000-0000-0000-000000000001") },
                    { new Guid("82000000-0000-0000-0000-000000000006"), true, 3, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(13, 0, 0), new Guid("91000000-0000-0000-0000-000000000001") },
                    { new Guid("82000000-0000-0000-0000-000000000007"), true, 4, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(12, 0, 0), new TimeOnly(9, 0, 0), new Guid("91000000-0000-0000-0000-000000000001") },
                    { new Guid("82000000-0000-0000-0000-000000000008"), true, 4, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(13, 0, 0), new Guid("91000000-0000-0000-0000-000000000001") },
                    { new Guid("82000000-0000-0000-0000-000000000009"), true, 5, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(12, 0, 0), new TimeOnly(9, 0, 0), new Guid("91000000-0000-0000-0000-000000000001") },
                    { new Guid("82000000-0000-0000-0000-000000000010"), true, 5, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(13, 0, 0), new Guid("91000000-0000-0000-0000-000000000001") },
                    { new Guid("82000000-0000-0000-0000-000000000011"), true, 1, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0), new Guid("91000000-0000-0000-0000-000000000002") },
                    { new Guid("82000000-0000-0000-0000-000000000012"), true, 1, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(13, 0, 0), new Guid("91000000-0000-0000-0000-000000000002") },
                    { new Guid("82000000-0000-0000-0000-000000000013"), true, 2, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0), new Guid("91000000-0000-0000-0000-000000000002") },
                    { new Guid("82000000-0000-0000-0000-000000000014"), true, 2, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(13, 0, 0), new Guid("91000000-0000-0000-0000-000000000002") },
                    { new Guid("82000000-0000-0000-0000-000000000015"), true, 3, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0), new Guid("91000000-0000-0000-0000-000000000002") },
                    { new Guid("82000000-0000-0000-0000-000000000016"), true, 3, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(13, 0, 0), new Guid("91000000-0000-0000-0000-000000000002") },
                    { new Guid("82000000-0000-0000-0000-000000000017"), true, 4, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0), new Guid("91000000-0000-0000-0000-000000000002") },
                    { new Guid("82000000-0000-0000-0000-000000000018"), true, 4, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(13, 0, 0), new Guid("91000000-0000-0000-0000-000000000002") },
                    { new Guid("82000000-0000-0000-0000-000000000019"), true, 5, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(12, 0, 0), new TimeOnly(8, 0, 0), new Guid("91000000-0000-0000-0000-000000000002") },
                    { new Guid("82000000-0000-0000-0000-000000000020"), true, 5, new Guid("50000000-0000-0000-0000-000000000001"), new TimeOnly(18, 0, 0), new TimeOnly(13, 0, 0), new Guid("91000000-0000-0000-0000-000000000002") }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Ativo", "CategoriaProdutoId", "CodigoBarras", "CodigoInterno", "ControlaEstoque", "Custo", "DataAtualizacao", "DataCadastro", "Descricao", "EmpresaId", "MarcaId", "Nome", "ValorVenda" },
                values: new object[,]
                {
                    { new Guid("a3000000-0000-0000-0000-000000000001"), true, new Guid("a1000000-0000-0000-0000-000000000001"), "7891000000001", "PROD-001", true, 20.00m, null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pomada modeladora com efeito matte.", new Guid("50000000-0000-0000-0000-000000000001"), new Guid("a2000000-0000-0000-0000-000000000001"), "Pomada Efeito Matte", 39.90m },
                    { new Guid("a3000000-0000-0000-0000-000000000002"), true, new Guid("a1000000-0000-0000-0000-000000000001"), "7891000000002", "PROD-002", true, 17.50m, null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pomada modeladora com efeito brilho.", new Guid("50000000-0000-0000-0000-000000000001"), new Guid("a2000000-0000-0000-0000-000000000002"), "Pomada Efeito Brilho", 34.90m },
                    { new Guid("a3000000-0000-0000-0000-000000000003"), true, new Guid("a1000000-0000-0000-0000-000000000002"), "7891000000003", "PROD-003", true, 25.00m, null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shampoo para uso profissional.", new Guid("50000000-0000-0000-0000-000000000001"), new Guid("a2000000-0000-0000-0000-000000000003"), "Shampoo Profissional", 49.90m },
                    { new Guid("a3000000-0000-0000-0000-000000000004"), true, new Guid("a1000000-0000-0000-0000-000000000003"), "7891000000004", "PROD-004", true, 5.00m, null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pente para uso profissional.", new Guid("50000000-0000-0000-0000-000000000001"), new Guid("a2000000-0000-0000-0000-000000000001"), "Pente Profissional", 12.90m },
                    { new Guid("a3000000-0000-0000-0000-000000000005"), true, new Guid("a1000000-0000-0000-0000-000000000003"), "7891000000005", "PROD-005", true, 10.00m, null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Escova para cuidados com a barba.", new Guid("50000000-0000-0000-0000-000000000001"), new Guid("a2000000-0000-0000-0000-000000000002"), "Escova para Barba", 24.90m }
                });

            migrationBuilder.InsertData(
                table: "Servicos",
                columns: new[] { "Id", "Ativo", "CategoriaServicoId", "Custo", "DataAtualizacao", "DataCadastro", "Descricao", "DuracaoMinutos", "EmpresaId", "ExibirAplicativo", "IntervaloMinutos", "Nome", "Observacao", "PercentualComissao", "PermiteAgendamentoOnline", "TipoComissao", "Valor" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 1, new Guid("22222222-2222-2222-2222-222222222222"), 1m, null, new DateTime(2026, 11, 2, 16, 57, 6, 734, DateTimeKind.Utc).AddTicks(2246), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Corte de Cabelo", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111112"), 1, new Guid("22222222-2222-2222-2222-222222222222"), 1m, null, new DateTime(2026, 11, 2, 16, 57, 6, 734, DateTimeKind.Utc).AddTicks(4030), "Serviço de corte de cabelo", 60, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Cabelo e Barba", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111113"), 1, new Guid("22222222-2222-2222-2222-222222222222"), 1m, null, new DateTime(2026, 11, 2, 16, 57, 6, 734, DateTimeKind.Utc).AddTicks(4034), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Botox Capilar", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111114"), 1, new Guid("22222222-2222-2222-2222-222222222222"), 1m, null, new DateTime(2026, 11, 2, 16, 57, 6, 734, DateTimeKind.Utc).AddTicks(4037), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Corte + Barba + Botox Capilar", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111115"), 1, new Guid("44444444-4444-4444-4444-444444444444"), 1m, null, new DateTime(2026, 11, 2, 16, 57, 6, 734, DateTimeKind.Utc).AddTicks(4039), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Esmaltação - Francesinha", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111116"), 1, new Guid("33333333-3333-3333-3333-333333333333"), 1m, null, new DateTime(2026, 11, 2, 16, 57, 6, 734, DateTimeKind.Utc).AddTicks(4046), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Esmaltação - Mãos", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111117"), 2, new Guid("33333333-3333-3333-3333-333333333333"), 1m, null, new DateTime(2026, 11, 2, 16, 57, 6, 734, DateTimeKind.Utc).AddTicks(4048), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Esmaltação - Mãos", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111118"), 1, new Guid("33333333-3333-3333-3333-333333333333"), 1m, null, new DateTime(2026, 11, 2, 16, 57, 6, 734, DateTimeKind.Utc).AddTicks(4056), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Esmaltação - Mãos", null, 10m, true, 1, 60m },
                    { new Guid("11111111-1111-1111-1111-111111111119"), 2, new Guid("33333333-3333-3333-3333-333333333333"), 1m, null, new DateTime(2026, 11, 2, 16, 57, 6, 734, DateTimeKind.Utc).AddTicks(4059), "Serviço de corte de cabelo", 30, new Guid("50000000-0000-0000-0000-000000000001"), true, 5, "Esmaltação - Mãos", null, 10m, true, 1, 60m }
                });

            migrationBuilder.InsertData(
                table: "EstoquesProdutos",
                columns: new[] { "Id", "DataAtualizacao", "DataAtualizacaoEstoque", "DataCadastro", "EmpresaId", "EstoqueMinimo", "ProdutoId", "QuantidadeAtual" },
                values: new object[,]
                {
                    { new Guid("a4000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("50000000-0000-0000-0000-000000000001"), 3m, new Guid("a3000000-0000-0000-0000-000000000001"), 15m },
                    { new Guid("a4000000-0000-0000-0000-000000000002"), null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("50000000-0000-0000-0000-000000000001"), 2m, new Guid("a3000000-0000-0000-0000-000000000002"), 10m },
                    { new Guid("a4000000-0000-0000-0000-000000000003"), null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("50000000-0000-0000-0000-000000000001"), 2m, new Guid("a3000000-0000-0000-0000-000000000003"), 8m },
                    { new Guid("a4000000-0000-0000-0000-000000000004"), null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("50000000-0000-0000-0000-000000000001"), 5m, new Guid("a3000000-0000-0000-0000-000000000004"), 20m },
                    { new Guid("a4000000-0000-0000-0000-000000000005"), null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("50000000-0000-0000-0000-000000000001"), 3m, new Guid("a3000000-0000-0000-0000-000000000005"), 12m }
                });

            migrationBuilder.InsertData(
                table: "MovimentacoesEstoque",
                columns: new[] { "Id", "DataAtualizacao", "DataCadastro", "DataMovimento", "DocumentoOrigemId", "EmpresaId", "Observacao", "Origem", "ProdutoId", "Quantidade", "SaldoAnterior", "SaldoPosterior", "Tipo", "UsuarioResponsavelId" },
                values: new object[,]
                {
                    { new Guid("a5000000-0000-0000-0000-000000000001"), null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTimeOffset(new DateTime(2026, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("50000000-0000-0000-0000-000000000001"), "Estoque inicial para testes.", 1, new Guid("a3000000-0000-0000-0000-000000000001"), 15m, 0m, 15m, 1, null },
                    { new Guid("a5000000-0000-0000-0000-000000000002"), null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTimeOffset(new DateTime(2026, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("50000000-0000-0000-0000-000000000001"), "Estoque inicial para testes.", 1, new Guid("a3000000-0000-0000-0000-000000000002"), 10m, 0m, 10m, 1, null },
                    { new Guid("a5000000-0000-0000-0000-000000000003"), null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTimeOffset(new DateTime(2026, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("50000000-0000-0000-0000-000000000001"), "Estoque inicial para testes.", 1, new Guid("a3000000-0000-0000-0000-000000000003"), 8m, 0m, 8m, 1, null },
                    { new Guid("a5000000-0000-0000-0000-000000000004"), null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTimeOffset(new DateTime(2026, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("50000000-0000-0000-0000-000000000001"), "Estoque inicial para testes.", 1, new Guid("a3000000-0000-0000-0000-000000000004"), 20m, 0m, 20m, 1, null },
                    { new Guid("a5000000-0000-0000-0000-000000000005"), null, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTimeOffset(new DateTime(2026, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new Guid("50000000-0000-0000-0000-000000000001"), "Estoque inicial para testes.", 1, new Guid("a3000000-0000-0000-0000-000000000005"), 12m, 0m, 12m, 1, null }
                });

            migrationBuilder.InsertData(
                table: "ProfissionalServicos",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCadastro", "DataVinculo", "DuracaoPersonalizada", "EmpresaId", "ProfissionalId", "ServicoId", "ValorPersonalizado" },
                values: new object[,]
                {
                    { new Guid("074d2ed5-9c26-421f-86b0-96a3244f19db"), 3, null, new DateTime(2026, 11, 2, 16, 57, 6, 735, DateTimeKind.Utc).AddTicks(136), new DateTime(2026, 11, 2, 16, 57, 6, 735, DateTimeKind.Utc).AddTicks(132), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111114"), 90m },
                    { new Guid("0aeb851d-6b2f-4d0a-adc6-ef3b19e52c07"), 1, null, new DateTime(2026, 11, 2, 16, 57, 6, 735, DateTimeKind.Utc).AddTicks(115), new DateTime(2026, 11, 2, 16, 57, 6, 735, DateTimeKind.Utc).AddTicks(111), 35, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111112"), 70m },
                    { new Guid("36f0bb89-e13e-47f6-b44e-2298904b98ae"), 3, null, new DateTime(2026, 11, 2, 16, 57, 6, 735, DateTimeKind.Utc).AddTicks(147), new DateTime(2026, 11, 2, 16, 57, 6, 735, DateTimeKind.Utc).AddTicks(147), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111116"), 90m },
                    { new Guid("4185e591-8951-4cba-ad01-8f795c28ed16"), 3, null, new DateTime(2026, 11, 2, 16, 57, 6, 735, DateTimeKind.Utc).AddTicks(145), new DateTime(2026, 11, 2, 16, 57, 6, 735, DateTimeKind.Utc).AddTicks(144), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000003"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("6bf29a6e-288b-4496-8d51-121c1fd82619"), 1, null, new DateTime(2026, 11, 2, 16, 57, 6, 734, DateTimeKind.Utc).AddTicks(8930), new DateTime(2026, 11, 2, 16, 57, 6, 734, DateTimeKind.Utc).AddTicks(8922), 25, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("8426bd87-459b-4961-9bfc-4de8fdd8f07c"), 3, null, new DateTime(2026, 11, 2, 16, 57, 6, 735, DateTimeKind.Utc).AddTicks(153), new DateTime(2026, 11, 2, 16, 57, 6, 735, DateTimeKind.Utc).AddTicks(149), 10, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000011"), new Guid("11111111-1111-1111-1111-111111111111"), 50m },
                    { new Guid("bbc121d9-c0af-4b3c-be25-53cb95650234"), 3, null, new DateTime(2026, 11, 2, 16, 57, 6, 735, DateTimeKind.Utc).AddTicks(139), new DateTime(2026, 11, 2, 16, 57, 6, 735, DateTimeKind.Utc).AddTicks(139), 45, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000002"), new Guid("11111111-1111-1111-1111-111111111115"), 90m },
                    { new Guid("d3f64917-6465-4c1e-a939-52929907eba3"), 3, null, new DateTime(2026, 11, 2, 16, 57, 6, 735, DateTimeKind.Utc).AddTicks(129), new DateTime(2026, 11, 2, 16, 57, 6, 735, DateTimeKind.Utc).AddTicks(128), 40, new Guid("50000000-0000-0000-0000-000000000001"), new Guid("91000000-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111113"), 80m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_ClienteId",
                table: "Agendamentos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_EmpresaId_DataHoraInicio",
                table: "Agendamentos",
                columns: new[] { "EmpresaId", "DataHoraInicio" });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_EmpresaId_ProfissionalId_DataHoraInicio",
                table: "Agendamentos",
                columns: new[] { "EmpresaId", "ProfissionalId", "DataHoraInicio" });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_ProfissionalId",
                table: "Agendamentos",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentoServicos_AgendamentoId",
                table: "AgendamentoServicos",
                column: "AgendamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentoServicos_ServicoId",
                table: "AgendamentoServicos",
                column: "ServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasClientes_EmpresaId_ClienteId",
                table: "AssinaturasClientes",
                columns: new[] { "EmpresaId", "ClienteId" });

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasClientes_EmpresaId_Status_DataProximaCobranca",
                table: "AssinaturasClientes",
                columns: new[] { "EmpresaId", "Status", "DataProximaCobranca" });

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasClientes_PlanoAssinaturaId",
                table: "AssinaturasClientes",
                column: "PlanoAssinaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_AssinaturasClientes_ReferenciaGateway",
                table: "AssinaturasClientes",
                column: "ReferenciaGateway",
                unique: true,
                filter: "[ReferenciaGateway] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiariosAssinatura_AssinaturaClienteId_ClienteId",
                table: "BeneficiariosAssinatura",
                columns: new[] { "AssinaturaClienteId", "ClienteId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiariosAssinatura_EmpresaId_ClienteId_Ativo",
                table: "BeneficiariosAssinatura",
                columns: new[] { "EmpresaId", "ClienteId", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiosPlano_EmpresaId_PlanoAssinaturaId",
                table: "BeneficiosPlano",
                columns: new[] { "EmpresaId", "PlanoAssinaturaId" });

            migrationBuilder.CreateIndex(
                name: "IX_BeneficiosPlano_PlanoAssinaturaId",
                table: "BeneficiosPlano",
                column: "PlanoAssinaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_BloqueiosAgenda_EmpresaId_DataHoraInicio",
                table: "BloqueiosAgenda",
                columns: new[] { "EmpresaId", "DataHoraInicio" });

            migrationBuilder.CreateIndex(
                name: "IX_BloqueiosAgenda_ProfissionalId",
                table: "BloqueiosAgenda",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_DataAbertura",
                table: "Caixa",
                column: "DataAbertura");

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_EmpresaId",
                table: "Caixa",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_EmpresaId_UsuarioId",
                table: "Caixa",
                columns: new[] { "EmpresaId", "UsuarioId" },
                unique: true,
                filter: "[Status] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_Status",
                table: "Caixa",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Caixa_UsuarioId",
                table: "Caixa",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasProdutos_EmpresaId_Nome",
                table: "CategoriasProdutos",
                columns: new[] { "EmpresaId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasServicos_EmpresaId_Nome",
                table: "CategoriasServicos",
                columns: new[] { "EmpresaId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Cpf",
                table: "Clientes",
                column: "Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_UsuarioId",
                table: "Clientes",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientesEmpresas_ClienteId_EmpresaId",
                table: "ClientesEmpresas",
                columns: new[] { "ClienteId", "EmpresaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientesEmpresas_EmpresaId_Ativo",
                table: "ClientesEmpresas",
                columns: new[] { "EmpresaId", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_CompraItens_CompraId",
                table: "CompraItens",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraItens_ProdutoId",
                table: "CompraItens",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_EmpresaId",
                table: "Compras",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_EmpresaId_FornecedorId_DataCompra",
                table: "Compras",
                columns: new[] { "EmpresaId", "FornecedorId", "DataCompra" });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_EmpresaId_Status",
                table: "Compras",
                columns: new[] { "EmpresaId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_FornecedorId",
                table: "Compras",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracoesAgendaEmpresa_EmpresaId",
                table: "ConfiguracoesAgendaEmpresa",
                column: "EmpresaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurarcaoAssasCliente_EmpresaId",
                table: "ConfigurarcaoAssasCliente",
                column: "EmpresaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContaPagar_CompraId",
                table: "ContaPagar",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_ContaPagar_DataVencimento",
                table: "ContaPagar",
                column: "DataVencimento");

            migrationBuilder.CreateIndex(
                name: "IX_ContaPagar_EmpresaId",
                table: "ContaPagar",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContaPagar_FornecedorId",
                table: "ContaPagar",
                column: "FornecedorId");

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
                name: "IX_ContaReceber_VendaId",
                table: "ContaReceber",
                column: "VendaId",
                unique: true,
                filter: "[VendaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EstoquesProdutos_EmpresaId_QuantidadeAtual",
                table: "EstoquesProdutos",
                columns: new[] { "EmpresaId", "QuantidadeAtual" });

            migrationBuilder.CreateIndex(
                name: "IX_EstoquesProdutos_ProdutoId",
                table: "EstoquesProdutos",
                column: "ProdutoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feriados_EmpresaId_Data",
                table: "Feriados",
                columns: new[] { "EmpresaId", "Data" });

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamento_EmpresaId",
                table: "FormaPagamento",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamento_Nome",
                table: "FormaPagamento",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_EmpresaId_Documento",
                table: "Fornecedores",
                columns: new[] { "EmpresaId", "Documento" },
                unique: true,
                filter: "[Documento] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_EmpresaId_Nome",
                table: "Fornecedores",
                columns: new[] { "EmpresaId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_HorariosFuncionamento_EmpresaId_DiaSemana",
                table: "HorariosFuncionamento",
                columns: new[] { "EmpresaId", "DiaSemana" });

            migrationBuilder.CreateIndex(
                name: "IX_HorariosProfissionais_ProfissionalId_DiaSemana",
                table: "HorariosProfissionais",
                columns: new[] { "ProfissionalId", "DiaSemana" });

            migrationBuilder.CreateIndex(
                name: "IX_Marcas_EmpresaId_Nome",
                table: "Marcas",
                columns: new[] { "EmpresaId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_Empresa_Produto_Data",
                table: "MovimentacoesEstoque",
                columns: new[] { "EmpresaId", "ProdutoId", "DataMovimento" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_Origem_Documento",
                table: "MovimentacoesEstoque",
                columns: new[] { "EmpresaId", "Origem", "DocumentoOrigemId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_ProdutoId",
                table: "MovimentacoesEstoque",
                column: "ProdutoId");

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
                name: "IX_PlanoAssinaturaServicos_EmpresaId_PlanoAssinaturaId_Ativo",
                table: "PlanoAssinaturaServicos",
                columns: new[] { "EmpresaId", "PlanoAssinaturaId", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_PlanoAssinaturaServicos_PlanoAssinaturaId_ServicoId",
                table: "PlanoAssinaturaServicos",
                columns: new[] { "PlanoAssinaturaId", "ServicoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanosAssinatura_EmpresaId_Nome",
                table: "PlanosAssinatura",
                columns: new[] { "EmpresaId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaProdutoId",
                table: "Produtos",
                column: "CategoriaProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_EmpresaId_CodigoBarras",
                table: "Produtos",
                columns: new[] { "EmpresaId", "CodigoBarras" },
                unique: true,
                filter: "[CodigoBarras] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_EmpresaId_CodigoInterno",
                table: "Produtos",
                columns: new[] { "EmpresaId", "CodigoInterno" },
                unique: true,
                filter: "[CodigoInterno] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_EmpresaId_Nome",
                table: "Produtos",
                columns: new[] { "EmpresaId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_MarcaId",
                table: "Produtos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_Cpf",
                table: "Profissionais",
                column: "Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_EmpresaId",
                table: "Profissionais",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_UsuarioId",
                table: "Profissionais",
                column: "UsuarioId",
                unique: true,
                filter: "[UsuarioId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProfissionalServicos_EmpresaId_ProfissionalId_Ativo",
                table: "ProfissionalServicos",
                columns: new[] { "EmpresaId", "ProfissionalId", "Ativo" });

            migrationBuilder.CreateIndex(
                name: "IX_ProfissionalServicos_ProfissionalId_ServicoId",
                table: "ProfissionalServicos",
                columns: new[] { "ProfissionalId", "ServicoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfissionalServicos_ServicoId",
                table: "ProfissionalServicos",
                column: "ServicoId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_CategoriaServicoId",
                table: "Servicos",
                column: "CategoriaServicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Servicos_EmpresaId_Nome",
                table: "Servicos",
                columns: new[] { "EmpresaId", "Nome" });

            migrationBuilder.CreateIndex(
                name: "IX_UtilizacoesAssinatura_AgendamentoId",
                table: "UtilizacoesAssinatura",
                column: "AgendamentoId",
                unique: true,
                filter: "[AgendamentoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UtilizacoesAssinatura_AssinaturaClienteId",
                table: "UtilizacoesAssinatura",
                column: "AssinaturaClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilizacoesAssinatura_BeneficiarioAssinaturaId",
                table: "UtilizacoesAssinatura",
                column: "BeneficiarioAssinaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_UtilizacoesAssinatura_EmpresaId_AssinaturaClienteId_DataUtilizacao",
                table: "UtilizacoesAssinatura",
                columns: new[] { "EmpresaId", "AssinaturaClienteId", "DataUtilizacao" });

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
                name: "AgendamentoServicos");

            migrationBuilder.DropTable(
                name: "BeneficiosPlano");

            migrationBuilder.DropTable(
                name: "BloqueiosAgenda");

            migrationBuilder.DropTable(
                name: "ClientesEmpresas");

            migrationBuilder.DropTable(
                name: "CompraItens");

            migrationBuilder.DropTable(
                name: "ConfiguracoesAgendaEmpresa");

            migrationBuilder.DropTable(
                name: "ConfigurarcaoAssasCliente");

            migrationBuilder.DropTable(
                name: "EstoquesProdutos");

            migrationBuilder.DropTable(
                name: "Feriados");

            migrationBuilder.DropTable(
                name: "HorariosFuncionamento");

            migrationBuilder.DropTable(
                name: "HorariosProfissionais");

            migrationBuilder.DropTable(
                name: "MovimentacoesEstoque");

            migrationBuilder.DropTable(
                name: "MovimentoCaixa");

            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "PlanoAssinaturaServicos");

            migrationBuilder.DropTable(
                name: "ProfissionalServicos");

            migrationBuilder.DropTable(
                name: "Recebimento");

            migrationBuilder.DropTable(
                name: "UtilizacoesAssinatura");

            migrationBuilder.DropTable(
                name: "VendaItens");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Caixa");

            migrationBuilder.DropTable(
                name: "ContaPagar");

            migrationBuilder.DropTable(
                name: "Servicos");

            migrationBuilder.DropTable(
                name: "ContaReceber");

            migrationBuilder.DropTable(
                name: "FormaPagamento");

            migrationBuilder.DropTable(
                name: "BeneficiariosAssinatura");

            migrationBuilder.DropTable(
                name: "CategoriasProdutos");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "CategoriasServicos");

            migrationBuilder.DropTable(
                name: "Vendas");

            migrationBuilder.DropTable(
                name: "AssinaturasClientes");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "Agendamentos");

            migrationBuilder.DropTable(
                name: "PlanosAssinatura");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Profissionais");
        }
    }
}
