
using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Estoque;
using Application.Operacional.Estoque.Commands;
using Application.Operacional.Estoque.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Estoque;

namespace Application.Operacional.Estoque.Commands.Handlers
{
    public sealed class RegistrarEntradaEstoqueHandler
        : ICommandHandler<
            RegistrarEntradaEstoqueCommand,
            MovimentacaoEstoqueDto>
    {
        private readonly IRepositorioEstoque _repositorio;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public RegistrarEntradaEstoqueHandler(
            IRepositorioEstoque repositorio,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorio = repositorio;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<MovimentacaoEstoqueDto> Handle(
            RegistrarEntradaEstoqueCommand command,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // 1. Validar empresa.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            // 2. Validar usuário responsável.
            if (!_usuarioLogado.IsAuthenticated ||
                _usuarioLogado.UsuarioId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar o usuário autenticado.");
            }

            var empresaId = _tenantContext.EmpresaId;
            var usuarioId = _usuarioLogado.UsuarioId;

            // 3. Validar os dados recebidos.
            var erros = new List<string>();

            if (command.ProdutoId == Guid.Empty)
                erros.Add("O identificador do produto é inválido.");

            if (command.Quantidade <= 0)
                erros.Add("A quantidade deve ser maior que zero.");

            if (command.Quantidade !=
                decimal.Round(command.Quantidade, 3))
            {
                erros.Add(
                    "A quantidade deve possuir no máximo três casas decimais.");
            }

            var observacao = string.IsNullOrWhiteSpace(command.Observacao)
                ? null
                : command.Observacao.Trim();

            if (observacao?.Length > 500)
                erros.Add("A observação deve possuir no máximo 500 caracteres.");

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 4. Buscar produto e estoque rastreados.
            var produto = await _repositorio
                .ObterProdutoParaMovimentacaoAsync(
                    empresaId,
                    command.ProdutoId,
                    cancellationToken);

            if (produto is null)
            {
                throw new CustomValidationException(
                    new[] { "Produto não encontrado para esta empresa." });
            }

            // 5. Validar controle de estoque.
            if (!produto.ControlaEstoque)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Este produto não possui controle de estoque habilitado."
                    });
            }

            if (produto.Estoque is null)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "O produto não possui um registro de estoque cadastrado."
                    });
            }

            // 6. Registrar entrada utilizando a regra de domínio.
            var estoque = produto.Estoque;

            decimal saldoAnterior;

            try
            {
                saldoAnterior = estoque.RegistrarEntrada(
                    command.Quantidade);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new CustomValidationException(
                    new[] { ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                throw new CustomValidationException(
                    new[] { ex.Message });
            }

            var saldoPosterior = estoque.QuantidadeAtual;

            // 7. Criar histórico da movimentação.
            var movimentacao = new MovimentacaoEstoque(
                empresaId: empresaId,
                produtoId: produto.Id,
                tipo: TipoMovimentacaoEstoque.Entrada,
                origem: OrigemMovimentacaoEstoque.AjusteManual,
                quantidade: command.Quantidade,
                saldoAnterior: saldoAnterior,
                saldoPosterior: saldoPosterior,
                usuarioResponsavelId: usuarioId,
                observacao: observacao);

            // 8. Adicionar movimentação ao contexto.
            await _repositorio.AdicionarMovimentacaoAsync(
                movimentacao,
                cancellationToken);

            // O TransactionMiddleware persiste as alterações.
            // Não chamar SaveChangesAsync neste Handler.

            return new MovimentacaoEstoqueDto(
                movimentacao.Id,
                movimentacao.ProdutoId,
                movimentacao.Tipo.ToString(),
                movimentacao.Origem.ToString(),
                movimentacao.Quantidade,
                movimentacao.SaldoAnterior,
                movimentacao.SaldoPosterior,
                movimentacao.DocumentoOrigemId,
                movimentacao.UsuarioResponsavelId,
                movimentacao.DataMovimento,
                movimentacao.Observacao);
        }
    }
}
