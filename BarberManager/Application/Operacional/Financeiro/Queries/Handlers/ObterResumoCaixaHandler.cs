using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.DTOs;
using Domain.Entities.Common.Enuns;

namespace Application.Operacional.Financeiro.Queries.Handlers
{
    public class ObterResumoCaixaHandler : IQueryHandler<ObterResumoCaixaQuery, ResumoCaixaDto>
    {
        private readonly IRepositorioCaixa _repositorio;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public ObterResumoCaixaHandler(
            IRepositorioCaixa repositorio,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorio = repositorio;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<ResumoCaixaDto> Handle(
            ObterResumoCaixaQuery query,
            CancellationToken cancellationToken)
        {
            if (!_usuarioLogado.IsAuthenticated ||
                _usuarioLogado.UsuarioId == Guid.Empty)
            {
                throw Validacao(
                    "O usuário precisa estar autenticado.");
            }

            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw Validacao(
                    "Não foi possível identificar a empresa do usuário.");
            }

            var caixa = await _repositorio.ObterCaixaAbertoComMovimentosAsync(
                _tenantContext.EmpresaId,
                _usuarioLogado.UsuarioId,
                cancellationToken);

            if (caixa is null)
            {
                throw Validacao(
                    "Não existe um caixa aberto para este usuário.");
            }

            // Total financeiro de todas as entradas.
            var entradas = caixa.Movimentos
                .Where(m =>
                    m.TipoMovimento == TipoMovimentoCaixa.Entrada)
                .Sum(m => m.Valor);

            // Entradas que efetivamente aumentam o dinheiro físico.
            var entradasEmDinheiro = caixa.Movimentos
                .Where(m =>
                    m.TipoMovimento == TipoMovimentoCaixa.Entrada &&
                    m.FormaPagamento.Tipo == TipoFormaPagamento.Dinheiro)
                .Sum(m => m.Valor);

            var suprimentos = caixa.Movimentos
                .Where(m =>
                    m.TipoMovimento == TipoMovimentoCaixa.Suprimento)
                .Sum(m => m.Valor);

            var saidas = caixa.Movimentos
                .Where(m =>
                    m.TipoMovimento == TipoMovimentoCaixa.Saida)
                .Sum(m => m.Valor);

            var sangrias = caixa.Movimentos
                .Where(m =>
                    m.TipoMovimento == TipoMovimentoCaixa.Sangria)
                .Sum(m => m.Valor);

            // O saldo esperado representa o dinheiro físico disponível.
            var saldoEsperado = decimal.Round(
                caixa.SaldoInicial
                + entradasEmDinheiro
                + suprimentos
                - saidas
                - sangrias,
                2);

            return new ResumoCaixaDto(
                caixa.Id,
                caixa.Status,
                caixa.DataAbertura,
                caixa.DataFechamento,
                caixa.SaldoInicial,
                entradas,
                entradasEmDinheiro,
                suprimentos,
                saidas,
                sangrias,
                saldoEsperado,
                null,
                null);
        }

        private static CustomValidationException Validacao(
            string mensagem) =>
            new(new[] { mensagem });
    }
}