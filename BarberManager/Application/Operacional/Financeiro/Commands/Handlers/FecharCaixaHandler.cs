using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.DTOs;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Financeiro.Commands.Handlers
{
    public class FecharCaixaHandler
      : ICommandHandler<FecharCaixaCommand, ResumoCaixaDto>
    {
        private readonly IRepositorioCaixa _repositorio;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public FecharCaixaHandler(
            IRepositorioCaixa repositorio,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorio = repositorio;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<ResumoCaixaDto> Handle(
            FecharCaixaCommand command,
            CancellationToken cancellationToken)
        {
            if (!_usuarioLogado.IsAuthenticated ||
                _usuarioLogado.UsuarioId == Guid.Empty)
            {
                throw Validacao("O usuário precisa estar autenticado.");
            }

            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw Validacao("Não foi possível identificar a empresa do usuário.");
            }

            if (command.SaldoContado < 0)
                throw Validacao("O saldo contado não pode ser negativo.");

            if (command.SaldoContado != decimal.Round(command.SaldoContado, 2))
                throw Validacao("O saldo contado deve possuir no máximo duas casas decimais.");

            var observacao = string.IsNullOrWhiteSpace(command.ObservacaoFechamento)
                ? null
                : command.ObservacaoFechamento.Trim();

            if (observacao?.Length > 1000)
                throw Validacao("A observação de fechamento deve possuir no máximo 1000 caracteres.");

            await _repositorio.BloquearOperacaoCaixaAsync(
                                      _tenantContext.EmpresaId,
                                           _usuarioLogado.UsuarioId,
                                                         cancellationToken);



            var caixa = await _repositorio
                .ObterCaixaAbertoComMovimentosParaAtualizacaoAsync(
                    _tenantContext.EmpresaId,
                    _usuarioLogado.UsuarioId,
                    cancellationToken);

            if (caixa is null)
                throw Validacao("Não existe um caixa aberto para este usuário.");

            var entradas = caixa.Movimentos
                .Where(m => m.TipoMovimento == TipoMovimentoCaixa.Entrada)
                .Sum(m => m.Valor);


            var suprimentos = caixa.Movimentos
                .Where(m => m.TipoMovimento == TipoMovimentoCaixa.Suprimento)
                .Sum(m => m.Valor);

            var saidas = caixa.Movimentos
                .Where(m => m.TipoMovimento == TipoMovimentoCaixa.Saida)
                .Sum(m => m.Valor);

            var sangrias = caixa.Movimentos
                .Where(m => m.TipoMovimento == TipoMovimentoCaixa.Sangria)
                .Sum(m => m.Valor);

            var entradasEmDinheiro = caixa.Movimentos
             .Where(m =>
                m.TipoMovimento == TipoMovimentoCaixa.Entrada &&
                m.FormaPagamento.Tipo == TipoFormaPagamento.Dinheiro)
                .Sum(m => m.Valor);

            var saldoEsperado = decimal.Round(
                caixa.SaldoInicial
                + entradasEmDinheiro
                + suprimentos
                - saidas
                - sangrias,
                2);
            /*
            var saldoEsperado = decimal.Round(
                caixa.SaldoInicial + entradas + suprimentos - saidas - sangrias,
                2);*/

            var diferenca = decimal.Round(
                command.SaldoContado - saldoEsperado,
                2);

            var dataFechamento = DateTime.UtcNow;

            caixa.SaldoEsperadoFechamento = saldoEsperado;
            caixa.SaldoFinal = command.SaldoContado;
            caixa.DiferencaFechamento = diferenca;
            caixa.ObservacaoFechamento = observacao;
            caixa.DataFechamento = dataFechamento;
            caixa.Status = StatusCaixa.Fechado;
            caixa.DataAtualizacao = dataFechamento;

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
                     command.SaldoContado,
                     diferenca);
        }

        private static CustomValidationException Validacao(string mensagem) =>
            new(new[] { mensagem });
    }
}
