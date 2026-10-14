using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Interfaces.Empresas;
using Application.Saas.Pagamentos.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Saas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.Commands.Handlers
{
    public class RegistrarTentativaPagamentoHandler : ICommandHandler<RegistrarTentativaPagamentoCommand, RegistrarTentativaPagamentoDto>
    {
        
        private readonly IRepositorioPagamento _repositorioPagamento;



        public RegistrarTentativaPagamentoHandler(
      IRepositorioPagamento repositorioPagamento)
        {
            _repositorioPagamento = repositorioPagamento;
        }


        public async Task<RegistrarTentativaPagamentoDto> Handle(RegistrarTentativaPagamentoCommand command, CancellationToken cancellationToken)
        {
            // 1. Validar os dados recebidos.
            ValidarCommand(command);

            var provedor = command.Provedor.Trim();
            var chaveIdempotencia = command.ChaveIdempotencia.Trim();

            // 2. Verificar se a chave já foi utilizada.
            var tentativaExistente =
                await _repositorioPagamento.ObterPorChaveIdempotenciaAsync(
                    provedor,
                    chaveIdempotencia,
                    cancellationToken);

            if (tentativaExistente is not null)
            {
                if (tentativaExistente.FaturaId != command.FaturaId)
                {
                    throw ErroValidacao(
                        "A chave de idempotência já foi utilizada em outra fatura.");
                }

                // A mesma requisição lógica não deve gerar outra tentativa.
                return MapearDto(
                    tentativaExistente,
                    jaExistia: true);
            }

            // 3. Buscar a fatura e suas tentativas anteriores.
            var fatura =
                await _repositorioPagamento.ObterFaturaComTentativasAsync(
                    command.FaturaId,
                    cancellationToken);

            if (fatura is null)
            {
                throw ErroValidacao("Fatura não encontrada.");
            }

            // 4. Impedir tentativas em faturas encerradas.
            if (fatura.Status is StatusFatura.Paga
                or StatusFatura.Cancelada)
            {
                throw ErroValidacao(
                    "Não é permitido registrar uma tentativa para uma fatura paga ou cancelada.");
            }

            // 5. Impedir mais de uma tentativa ativa na mesma fatura.
            var existeTentativaAtiva =
                fatura.TransacoesPagamento.Any(t =>
                    t.Status is StatusTransacaoPagamento.Criada
                        or StatusTransacaoPagamento.Pendente
                        or StatusTransacaoPagamento.EmProcessamento);

            if (existeTentativaAtiva)
            {
                throw ErroValidacao(
                    "Já existe uma tentativa de pagamento em andamento para esta fatura.");
            }

            // 6. Criar a tentativa usando o valor persistido da fatura.
            var tentativa = new TransacaoPagamento
            {
                FaturaId = fatura.Id,
                Provedor = provedor,
                ChaveIdempotencia = chaveIdempotencia,
                Valor = fatura.Valor,
                Status = StatusTransacaoPagamento.Criada
            };

            // 7. Persistir. O repositório utiliza o mesmo DbContext
            // que a UnitOfWork selecionada pelo TransactionMiddleware.
            await _repositorioPagamento.AdicionarTentativaAsync(
                tentativa,
                cancellationToken);

            // 8. O middleware confirma a transação após o retorno
            // bem-sucedido deste handler.

            return MapearDto(
                tentativa,
                jaExistia: false);
        }

        private static void ValidarCommand(
            RegistrarTentativaPagamentoCommand command)
        {
            if (command.FaturaId == Guid.Empty)
            {
                throw ErroValidacao(
                    "A identificação da fatura é obrigatória.");
            }

            if (string.IsNullOrWhiteSpace(command.Provedor))
            {
                throw ErroValidacao(
                    "O provedor de pagamento é obrigatório.");
            }

            if (command.Provedor.Trim().Length > 50)
            {
                throw ErroValidacao(
                    "O provedor deve ter no máximo 50 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(command.ChaveIdempotencia))
            {
                throw ErroValidacao(
                    "A chave de idempotência é obrigatória.");
            }

            if (command.ChaveIdempotencia.Trim().Length > 100)
            {
                throw ErroValidacao(
                    "A chave de idempotência deve ter no máximo 100 caracteres.");
            }
        }

        private static CustomValidationException ErroValidacao(
            string mensagem)
        {
            return new CustomValidationException(
                new[] { mensagem });
        }

        private static RegistrarTentativaPagamentoDto MapearDto(
            TransacaoPagamento tentativa,
            bool jaExistia)
        {
            return new RegistrarTentativaPagamentoDto
            {
                TransacaoId = tentativa.Id,
                FaturaId = tentativa.FaturaId,
                Valor = tentativa.Valor,
                Provedor = tentativa.Provedor,
                Status = tentativa.Status.ToString(),
                DataCadastro = tentativa.DataCadastro,
                JaExistia = jaExistia
            };
        }
    }
}
