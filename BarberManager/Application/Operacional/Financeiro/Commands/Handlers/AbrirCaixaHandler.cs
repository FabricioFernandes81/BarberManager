using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Common.Transactions;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Financeiro.Commands.Handlers
{
    public class AbrirCaixaHandler
    : ICommandHandler<AbrirCaixaCommand, ResumoCaixaDto>
    {
        private readonly IRepositorioCaixa _repositorio;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public AbrirCaixaHandler(
            IRepositorioCaixa repositorio,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorio = repositorio;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<ResumoCaixaDto> Handle(
            AbrirCaixaCommand command,
            CancellationToken cancellationToken)
        {
            ValidarContexto();

            if (command.SaldoInicial < 0)
                throw Validacao("O saldo inicial não pode ser negativo.");

            if (command.SaldoInicial != decimal.Round(command.SaldoInicial, 2))
                throw Validacao("O saldo inicial deve possuir no máximo duas casas decimais.");

            var empresaId = _tenantContext.EmpresaId;
            var usuarioId = _usuarioLogado.UsuarioId;

            /*  if (await _repositorio.ExisteCaixaAbertoAsync(
                  empresaId, usuarioId, cancellationToken))
              {
                  throw Validacao(
                      "Já existe um caixa aberto para este usuário nesta empresa.");
              }*/

            await _repositorio.BloquearOperacaoCaixaAsync(
    empresaId,
    usuarioId,
    cancellationToken);

            if (await _repositorio.ExisteCaixaAbertoAsync(
                empresaId,
                usuarioId,
                cancellationToken))
            {
                throw Validacao(
                    "Já existe um caixa aberto para este usuário nesta empresa.");
            }



            var observacao = string.IsNullOrWhiteSpace(command.Observacao)
                ? null
                : command.Observacao.Trim();

            if (observacao?.Length > 1000)
                throw Validacao("A observação deve possuir no máximo 1000 caracteres.");

            var caixa = new Caixa
            {
                EmpresaId = empresaId,
                UsuarioId = usuarioId,
                DataAbertura = DateTime.UtcNow,
                SaldoInicial = command.SaldoInicial,
                SaldoFinal = 0,
                Status = StatusCaixa.Aberto,
                Observacao = observacao
            };

            await _repositorio.AdicionarAsync(caixa, cancellationToken);

            return new ResumoCaixaDto(
                        caixa.Id,
                        caixa.Status,
                        caixa.DataAbertura,
                        caixa.DataFechamento,
                        caixa.SaldoInicial,
                        0m,                  // TotalEntradas
                        0m,                  // TotalEntradasEmDinheiro
                        0m,                  // TotalSuprimentos
                        0m,                  // TotalSaidas
                        0m,                  // TotalSangrias
                        caixa.SaldoInicial,  // SaldoEsperado
                        null,                // SaldoContado
                        null                 // DiferencaFechamento
                    );
        }

        private void ValidarContexto()
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
        }

        private static CustomValidationException Validacao(string mensagem) =>
            new(new[] { mensagem });
    }
}
