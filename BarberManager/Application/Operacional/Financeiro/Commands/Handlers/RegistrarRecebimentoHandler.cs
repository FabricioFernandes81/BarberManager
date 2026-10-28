using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
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
    public class RegistrarRecebimentoHandler
    : ICommandHandler<
        RegistrarRecebimentoCommand,
        RegistrarRecebimentoDto>
    {
        private readonly IRepositorioRecebimento _repositorio;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;
        private readonly IRepositorioCaixa _repositorioCaixa;

        public RegistrarRecebimentoHandler(
            IRepositorioRecebimento repositorio,
            IRepositorioCaixa repositorioCaixa,
        ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorio = repositorio;
            _tenantContext = tenantContext;
            _repositorioCaixa = repositorioCaixa;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<RegistrarRecebimentoDto> Handle(
            RegistrarRecebimentoCommand command,
            CancellationToken cancellationToken)
        {
            // 1. Validar o contexto da empresa.
            if (!_tenantContext.IsAvailable)
            {
                throw Erro(
                    "Não foi possível identificar a empresa do usuário.");
            }

            var empresaId = _tenantContext.EmpresaId;

            // 2. Validar a autenticação.
            if (!_usuarioLogado.IsAuthenticated)
            {
                throw Erro(
                    "É necessário estar autenticado para registrar um recebimento.");
            }

            if (_usuarioLogado.UsuarioId == Guid.Empty)
            {
                throw Erro("O identificador do usuário é inválido.");
            }

            await _repositorioCaixa.BloquearOperacaoCaixaAsync(
                empresaId,
                _usuarioLogado.UsuarioId,
                cancellationToken);

            var caixa = await _repositorio.ObterCaixaAbertoAsync(
                empresaId,
                _usuarioLogado.UsuarioId,
                cancellationToken);

            if (caixa is null)
            {
                throw Erro(
                    "Não existe um caixa aberto para este usuário nesta empresa. " +
                    "Abra o caixa antes de registrar o recebimento.");
            }

            // 3. Validar os identificadores.
            if (command.ContaReceberId == Guid.Empty)
            {
                throw Erro(
                    "O identificador da conta a receber é obrigatório.");
            }

            if (command.FormaPagamentoId == Guid.Empty)
            {
                throw Erro(
                    "A forma de pagamento deve ser informada.");
            }

            // 4. Validar o valor monetário.
            if (command.Valor <= 0)
            {
                throw Erro(
                    "O valor do recebimento deve ser maior que zero.");
            }

            if (command.Valor != decimal.Round(command.Valor, 2))
            {
                throw Erro(
                    "O valor do recebimento deve possuir no máximo duas casas decimais.");
            }

            // 5. Validar a observação.
            var observacao = command.Observacao?.Trim();

            if (observacao is { Length: > 500 })
            {
                throw Erro(
                    "A observação não pode ultrapassar 500 caracteres.");
            }

            // 6. Buscar a conta dentro da empresa autenticada.
            var conta = await _repositorio.ObterContaParaRecebimentoAsync(
                empresaId,
                command.ContaReceberId,
                cancellationToken);

            if (conta is null)
            {
                throw Erro(
                    "Conta a receber não encontrada para a empresa.");
            }

            // 7. Validar a forma de pagamento e seu tenant.
            var formaPagamento =
                await _repositorio.ObterFormaPagamentoAtivaAsync(
                    empresaId,
                    command.FormaPagamentoId,
                    cancellationToken);

            if (formaPagamento is null)
            {
                throw Erro(
                    "A forma de pagamento não existe, está inativa ou não pertence à empresa.");
            }

            // 8. Definir a data do recebimento no servidor.
            var dataRecebimento = DateTime.UtcNow;

            // 9. Aplicar as regras financeiras no domínio.
            try
            {
                conta.RegistrarRecebimento(
                    command.Valor,
                    dataRecebimento);
            }
            catch (InvalidOperationException ex)
            {
                throw Erro(ex.Message);
            }

            // 10. Criar o registro individual do recebimento.
            /*var recebimento = new Recebimento
            {
                ContaReceberId = conta.Id,
                ContaReceber = conta,
                FormaPagamentoId = formaPagamento.Id,
                FormaPagamento = formaPagamento,
                DataRecebimento = dataRecebimento,
                Valor = command.Valor,
                Observacao = observacao
            };*/
            var recebimento = new Recebimento
            {
                ContaReceberId = conta.Id,
                ContaReceber = conta,
                FormaPagamentoId = formaPagamento.Id,
                DataRecebimento = dataRecebimento,
                Valor = command.Valor,
                Observacao = observacao
            };

            await _repositorio.AdicionarAsync(
                recebimento,
                cancellationToken);


            var movimento = new MovimentoCaixa
            {
                CaixaId = caixa.Id,
                Caixa = caixa,

                TipoMovimento = TipoMovimentoCaixa.Entrada,

                FormaPagamentoId = formaPagamento.Id,

                DataMovimento = dataRecebimento,
                Valor = command.Valor,

                Historico = $"Recebimento da conta a receber {conta.Id}",

                Documento = recebimento.Id.ToString()
            };

            await _repositorio.AdicionarMovimentoCaixaAsync(
                movimento,
                cancellationToken);



            // 11. O TransactionMiddleware persiste a conta
            // atualizada e o recebimento na mesma transação.
            return new RegistrarRecebimentoDto(
                recebimento.Id,
                conta.Id,
                recebimento.Valor,
                conta.ValorRecebido,
                conta.ObterSaldoRestante(),
                conta.Status,
                recebimento.DataRecebimento);
        }

        private static CustomValidationException Erro(
            string mensagem)
        {
            return new CustomValidationException(
                new[] { mensagem });
        }
    }
}
