
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
    public sealed class RegistrarAjusteEstoqueHandler
        : ICommandHandler<
            RegistrarAjusteEstoqueCommand,
            MovimentacaoEstoqueDto>
    {
        private readonly IRepositorioEstoque _repositorio;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public RegistrarAjusteEstoqueHandler(
            IRepositorioEstoque repositorio,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorio = repositorio;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<MovimentacaoEstoqueDto> Handle(
            RegistrarAjusteEstoqueCommand command,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            if (!_usuarioLogado.IsAuthenticated ||
                _usuarioLogado.UsuarioId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar o usuário autenticado.");
            }

            var empresaId = _tenantContext.EmpresaId;
            var usuarioId = _usuarioLogado.UsuarioId;

            var erros = new List<string>();

            if (command.ProdutoId == Guid.Empty)
                erros.Add("O identificador do produto é inválido.");

            if (command.QuantidadeFisica < 0)
                erros.Add("A quantidade física não pode ser negativa.");

            if (command.QuantidadeFisica !=
                decimal.Round(command.QuantidadeFisica, 3))
            {
                erros.Add(
                    "A quantidade física deve possuir no máximo três casas decimais.");
            }

            var observacao = string.IsNullOrWhiteSpace(command.Observacao)
                ? null
                : command.Observacao.Trim();

            if (observacao?.Length > 500)
                erros.Add("A observação deve possuir no máximo 500 caracteres.");

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            var produto = await _repositorio.ObterProdutoParaMovimentacaoAsync(
                empresaId,
                command.ProdutoId,
                cancellationToken);

            if (produto is null)
            {
                throw new CustomValidationException(
                    new[] { "Produto não encontrado para esta empresa." });
            }

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

            var estoque = produto.Estoque;
            var saldoAnterior = estoque.QuantidadeAtual;
            var saldoPosterior = command.QuantidadeFisica;

            if (saldoAnterior == saldoPosterior)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "A quantidade física informada é igual ao saldo atual. Não há necessidade de ajuste."
                    });
            }

            var quantidadeMovimentada =
                Math.Abs(saldoPosterior - saldoAnterior);

            try
            {
                saldoAnterior = estoque.AjustarSaldo(
                    command.QuantidadeFisica);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new CustomValidationException(
                    new[] { ex.Message });
            }

            var movimentacao = new MovimentacaoEstoque(
                empresaId: empresaId,
                produtoId: produto.Id,
                tipo: TipoMovimentacaoEstoque.Ajuste,
                origem: OrigemMovimentacaoEstoque.AjusteManual,
                quantidade: quantidadeMovimentada,
                saldoAnterior: saldoAnterior,
                saldoPosterior: estoque.QuantidadeAtual,
                usuarioResponsavelId: usuarioId,
                observacao: observacao);

            await _repositorio.AdicionarMovimentacaoAsync(
                movimentacao,
                cancellationToken);

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
