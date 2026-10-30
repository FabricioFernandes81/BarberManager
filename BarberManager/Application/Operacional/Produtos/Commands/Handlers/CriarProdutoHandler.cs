using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Produtos;
using Application.Operacional.Produtos.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Estoque;
using Domain.Entities.Produtos;

namespace Application.Operacional.Produtos.Commands.Handlers
{
    public class CriarProdutoHandler
        : ICommandHandler<CriarProdutoCommand, CriarProdutoDto>
    {
        private readonly IRepositorioProdutos _repositorioProdutos;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public CriarProdutoHandler(
            IRepositorioProdutos repositorioProdutos,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorioProdutos = repositorioProdutos;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<CriarProdutoDto> Handle(
            CriarProdutoCommand command,
            CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            // 1. Validar a empresa autenticada.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário autenticado.");
            }

            // 2. Validar o usuário responsável.
            if (!_usuarioLogado.IsAuthenticated ||
                _usuarioLogado.UsuarioId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar o usuário autenticado.");
            }

            var empresaId = _tenantContext.EmpresaId;
            var usuarioId = _usuarioLogado.UsuarioId;

            // 3. Normalizar os dados.
            var nome = command.Nome?.Trim() ?? string.Empty;
            var descricao = command.Descricao?.Trim();
            var codigoInterno = string.IsNullOrWhiteSpace(
                command.CodigoInterno)
                    ? null
                    : command.CodigoInterno.Trim();

            var codigoBarras = string.IsNullOrWhiteSpace(
                command.CodigoBarras)
                    ? null
                    : command.CodigoBarras.Trim();

            // 4. Validar campos obrigatórios e limites.
            if (string.IsNullOrWhiteSpace(nome))
                erros.Add("O nome do produto é obrigatório.");
            else if (nome.Length > 150)
                erros.Add("O nome do produto deve ter no máximo 150 caracteres.");

            if (descricao?.Length > 100)
                erros.Add("A descrição deve ter no máximo 100 caracteres.");

            if (codigoInterno?.Length > 50)
                erros.Add("O código interno deve ter no máximo 50 caracteres.");

            if (codigoBarras?.Length > 50)
                erros.Add("O código de barras deve ter no máximo 50 caracteres.");

            if (command.ValorVenda < 0)
                erros.Add("O valor de venda não pode ser negativo.");

            if (command.Custo < 0)
                erros.Add("O custo do produto não pode ser negativo.");

            if (command.QuantidadeEstoqueInicial < 0)
                erros.Add("A quantidade inicial do estoque não pode ser negativa.");

            if (command.EstoqueMinimo < 0)
                erros.Add("O estoque mínimo não pode ser negativo.");

            if (command.CategoriaProdutoId == Guid.Empty)
                erros.Add("A categoria informada é inválida.");

            if (command.MarcaId == Guid.Empty)
                erros.Add("A marca informada é inválida.");

            if (!command.ControlaEstoque &&
                (command.QuantidadeEstoqueInicial > 0 ||
                 command.EstoqueMinimo > 0))
            {
                erros.Add(
                    "A quantidade inicial e o estoque mínimo só podem ser " +
                    "informados para produtos que controlam estoque.");
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 5. Validar categoria e marca dentro da empresa.
            if (command.CategoriaProdutoId.HasValue)
            {
                var categoriaValida =
                    await _repositorioProdutos.CategoriaPertenceEmpresaAsync(
                        empresaId,
                        command.CategoriaProdutoId.Value,
                        cancellationToken);

                if (!categoriaValida)
                {
                    erros.Add(
                        "A categoria informada não existe, está inativa " +
                        "ou não pertence à empresa.");
                }
            }

            if (command.MarcaId.HasValue)
            {
                var marcaValida =
                    await _repositorioProdutos.MarcaPertenceEmpresaAsync(
                        empresaId,
                        command.MarcaId.Value,
                        cancellationToken);

                if (!marcaValida)
                {
                    erros.Add(
                        "A marca informada não existe, está inativa " +
                        "ou não pertence à empresa.");
                }
            }

            // 6. Validar unicidade dos códigos dentro da empresa.
            if (codigoInterno is not null &&
                await _repositorioProdutos.CodigoInternoExisteAsync(
                    empresaId,
                    codigoInterno,
                    cancellationToken))
            {
                erros.Add(
                    "Já existe um produto cadastrado com esse código interno.");
            }

            if (codigoBarras is not null &&
                await _repositorioProdutos.CodigoBarrasExisteAsync(
                    empresaId,
                    codigoBarras,
                    cancellationToken))
            {
                erros.Add(
                    "Já existe um produto cadastrado com esse código de barras.");
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            // 7. Criar a entidade de domínio.
            var produto = new Produto(
                empresaId: empresaId,
                nome: nome,
                valorVenda: command.ValorVenda,
                custo: command.Custo,
                controlaEstoque: command.ControlaEstoque,
                categoriaProdutoId: command.CategoriaProdutoId,
                marcaId: command.MarcaId,
                descricao: descricao,
                codigoInterno: codigoInterno,
                codigoBarras: codigoBarras);

            EstoqueProduto? estoque = null;
            MovimentacaoEstoque? movimentacao = null;

            // 8. Criar estoque e movimentação quando aplicável.
            if (command.ControlaEstoque)
            {
                estoque = new EstoqueProduto(
                    empresaId: empresaId,
                    produtoId: produto.Id,
                    estoqueMinimo: command.EstoqueMinimo);

                if (command.QuantidadeEstoqueInicial > 0)
                {
                    var saldoAnterior = estoque.RegistrarEntrada(
                        command.QuantidadeEstoqueInicial);

                    var saldoPosterior = estoque.QuantidadeAtual;

                    movimentacao = new MovimentacaoEstoque(
                        empresaId: empresaId,
                        produtoId: produto.Id,
                        tipo: TipoMovimentacaoEstoque.Entrada,
                        origem: OrigemMovimentacaoEstoque.CadastroInicial,
                        quantidade: command.QuantidadeEstoqueInicial,
                        saldoAnterior: saldoAnterior,
                        saldoPosterior: saldoPosterior,
                        usuarioResponsavelId: usuarioId,
                        observacao:
                            "Estoque inicial registrado no cadastro do produto.");
                }
            }

            // 9. Registrar as entidades no contexto da mesma unidade de trabalho.
            await _repositorioProdutos.AdicionarComEstoqueInicialAsync(
                produto,
                estoque,
                movimentacao,
                cancellationToken);

            // 10. Retornar os dados do produto criado.
            return new CriarProdutoDto(
                Id: produto.Id,
                Nome: produto.Nome,
                CodigoInterno: produto.CodigoInterno,
                CodigoBarras: produto.CodigoBarras,
                CategoriaProdutoId: produto.CategoriaProdutoId,
                MarcaId: produto.MarcaId,
                ValorVenda: produto.ValorVenda,
                Custo: produto.Custo,
                ControlaEstoque: produto.ControlaEstoque,
                QuantidadeEstoqueInicial:
                    estoque?.QuantidadeAtual ?? 0,
                EstoqueMinimo:
                    estoque?.EstoqueMinimo ?? 0,
                Ativo: produto.Ativo);
        }
    }
}