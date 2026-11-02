using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Produtos;
using Application.Operacional.Produtos.DTOs;

namespace Application.Operacional.Produtos.Commands.Handlers
{
    public class AtualizarProdutoHandler : ICommandHandler<AtualizarProdutoCommand, ProdutoDetalheDto>
    {
        private readonly ITenantContext _tenantContext;
        private readonly IRepositorioProdutos _repositorio;

        public AtualizarProdutoHandler(ITenantContext tenantContext, IRepositorioProdutos repositorio)
        {
            _tenantContext = tenantContext;
            _repositorio = repositorio;
        }

        public async Task<ProdutoDetalheDto> Handle(AtualizarProdutoCommand command, CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            // 1. Validar o contexto da empresa.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            if (command.ProdutoId == Guid.Empty)
            {
                erros.Add("O identificador do produto é inválido.");
            }

            // 2. Normalizar os dados.
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

            // 3. Validar os campos.
            if (string.IsNullOrWhiteSpace(nome))
                erros.Add("O nome do produto é obrigatório.");
            else if (nome.Length > 150)
                erros.Add(
                    "O nome do produto deve ter no máximo 150 caracteres.");

            if (descricao?.Length > 1000)
                erros.Add(
                    "A descrição deve ter no máximo 1000 caracteres.");

            if (codigoInterno?.Length > 50)
                erros.Add(
                    "O código interno deve ter no máximo 50 caracteres.");

            if (codigoBarras?.Length > 50)
                erros.Add(
                    "O código de barras deve ter no máximo 50 caracteres.");

            if (command.ValorVenda < 0)
                erros.Add("O valor de venda não pode ser negativo.");

            if (command.Custo < 0)
                erros.Add("O custo do produto não pode ser negativo.");

            if (command.CategoriaProdutoId == Guid.Empty)
                erros.Add("A categoria informada é inválida.");

            if (command.MarcaId == Guid.Empty)
                erros.Add("A marca informada é inválida.");

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            var empresaId = _tenantContext.EmpresaId;

            // 4. Consultar o produto dentro da empresa.
            var produto = await _repositorio
                .ObterPorIdParaAtualizacaoAsync(
                    empresaId,
                    command.ProdutoId,
                    cancellationToken);

            if (produto is null)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Produto não encontrado nesta empresa."
                    });
            }

            // 5. Validar categoria.
            if (command.CategoriaProdutoId.HasValue)
            {
                var categoriaValida =
                    await _repositorio.CategoriaPertenceEmpresaAsync(
                        empresaId,
                        command.CategoriaProdutoId.Value,
                        cancellationToken);

                if (!categoriaValida)
                {
                    erros.Add(
                        "A categoria não existe, está inativa " +
                        "ou não pertence à empresa.");
                }
            }

            // 6. Validar marca.
            if (command.MarcaId.HasValue)
            {
                var marcaValida =
                    await _repositorio.MarcaPertenceEmpresaAsync(
                        empresaId,
                        command.MarcaId.Value,
                        cancellationToken);

                if (!marcaValida)
                {
                    erros.Add(
                        "A marca não existe, está inativa " +
                        "ou não pertence à empresa.");
                }
            }

            // 7. Validar duplicidade ignorando o produto atual.
            if (codigoInterno is not null &&
                await _repositorio.CodigoInternoExisteAsync(
                    empresaId,
                    codigoInterno,
                    cancellationToken,
                    produto.Id))
            {
                erros.Add(
                    "Já existe outro produto com esse código interno.");
            }

            if (codigoBarras is not null &&
                await _repositorio.CodigoBarrasExisteAsync(
                    empresaId,
                    codigoBarras,
                    cancellationToken,
                    produto.Id))
            {
                erros.Add(
                    "Já existe outro produto com esse código de barras.");
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 8. Atualizar os dados cadastrais.
            produto.AtualizarDados(
                nome: nome,
                descricao: descricao,
                categoriaProdutoId: command.CategoriaProdutoId,
                marcaId: command.MarcaId,
                codigoInterno: codigoInterno,
                codigoBarras: codigoBarras);

            // 9. Atualizar os valores comerciais.
            produto.AtualizarValores(
                valorVenda: command.ValorVenda,
                custo: command.Custo);

            // A entidade está rastreada pelo DbContext.
            // A persistência será confirmada pelo TransactionMiddleware.
            await _repositorio.AtualizarAsync(produto);

            // 10. Retornar os dados atualizados.
            return new ProdutoDetalheDto(
                produto.Id,
                produto.CategoriaProdutoId,
                produto.CategoriaProduto?.Nome,
                produto.MarcaId,
                produto.Marca?.Nome,
                produto.Nome,
                produto.Descricao,
                produto.CodigoInterno,
                produto.CodigoBarras,
                produto.ValorVenda,
                produto.Custo,
                produto.ControlaEstoque,
                produto.Estoque?.QuantidadeAtual,
                produto.Estoque?.EstoqueMinimo,
                produto.Ativo,
                produto.DataCadastro,
                produto.DataAtualizacao);
        }
    }
    
}
