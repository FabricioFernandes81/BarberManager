
using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.DTOs.Asaas;
using Domain.Entities.Financeiro;

namespace Application.Operacional.Financeiro.Commands.Handlers
{
    public sealed class RegistrarCobrancaPixHandler
        : ICommandHandler<
            RegistrarCobrancaPixCommand,
            ReservaCobrancaPixDto>
    {
        private readonly IRepositorioCobrancaOperacional _repositorioCobranca;
        private readonly IRepositorioRecebimento _repositorioRecebimento;
        private readonly ITenantContext _tenantContext;

        public RegistrarCobrancaPixHandler(
            IRepositorioCobrancaOperacional repositorioCobranca,
            IRepositorioRecebimento repositorioRecebimento,
            ITenantContext tenantContext)
        {
            _repositorioCobranca = repositorioCobranca;
            _repositorioRecebimento = repositorioRecebimento;
            _tenantContext = tenantContext;
        }

        public async Task<ReservaCobrancaPixDto> Handle(
            RegistrarCobrancaPixCommand command,
            CancellationToken cancellationToken)
        {
            // 1. Validar contexto do tenant.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw Erro(
                    "Não foi possível identificar a empresa do usuário.");
            }

            var empresaId = _tenantContext.EmpresaId;

            // 2. Validar identificadores e chave de idempotência.
            var erros = new List<string>();

            if (command.ContaReceberId == Guid.Empty)
                erros.Add("A conta a receber deve ser informada.");

            if (string.IsNullOrWhiteSpace(command.ChaveIdempotencia))
                erros.Add("A chave de idempotência é obrigatória.");

            var chaveIdempotencia =
                command.ChaveIdempotencia?.Trim();

            if (chaveIdempotencia is { Length: > 100 })
                erros.Add("A chave de idempotência não pode ultrapassar 100 caracteres.");

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            // 3. Consultar reserva existente pela chave de idempotência.
            var cobrancaExistente =
                await _repositorioCobranca.ObterPorIdempotenciaAsync(
                    empresaId,
                    chaveIdempotencia!,
                    cancellationToken);

            if (cobrancaExistente is not null)
            {
                // A mesma chave não pode ser reutilizada para outra conta.
                if (cobrancaExistente.ContaReceberId != command.ContaReceberId)
                {
                    throw Erro(
                        "A chave de idempotência já foi utilizada em outra conta a receber.");
                }

                return MapearReserva(
                    cobrancaExistente,
                    jaExistente: true);
            }

            // 4. Consultar conta a receber dentro da empresa autenticada.
            var conta = await _repositorioRecebimento
                .ObterContaParaRecebimentoAsync(
                    empresaId,
                    command.ContaReceberId,
                    cancellationToken);

            if (conta is null)
            {
                throw Erro(
                    "Conta a receber não encontrada para esta empresa.");
            }

            // 5. Validar situação financeira da conta.
            if (conta.Status == Domain.Entities.Common.Enuns.StatusContas.Cancelada)
                throw Erro("Não é possível gerar cobrança para uma conta cancelada.");

            if (conta.Status == Domain.Entities.Common.Enuns.StatusContas.Recebida)
                throw Erro("Esta conta a receber já foi totalmente liquidada.");

            if (!conta.ClienteId.HasValue ||
                conta.ClienteId.Value == Guid.Empty)
            {
                throw Erro(
                    "A conta a receber não possui cliente vinculado para gerar cobrança Pix.");
            }

            // 6. Calcular somente o saldo ainda não recebido.
            var saldoRestante = conta.ObterSaldoRestante();

            if (saldoRestante <= 0)
            {
                throw Erro(
                    "A conta a receber não possui saldo disponível para cobrança.");
            }

            saldoRestante = decimal.Round(
                saldoRestante,
                2,
                MidpointRounding.AwayFromZero);

            // 7. Criar reserva local.
            var externalReference = Guid.NewGuid().ToString("N");

            var cobranca = new CobrancaOperacional(
                empresaId,
                conta.Id,
                externalReference,
                chaveIdempotencia!,
                saldoRestante);

            await _repositorioCobranca.AdicionarAsync(
                cobranca,
                cancellationToken);

            // 8. O TransactionMiddleware persiste a reserva.
            return MapearReserva(
                cobranca,
                jaExistente: false);
        }

        private static ReservaCobrancaPixDto MapearReserva(
            CobrancaOperacional cobranca,
            bool jaExistente)
        {
            return new ReservaCobrancaPixDto(
                cobranca.Id,
                cobranca.ContaReceberId,
                cobranca.ExternalReference,
                cobranca.Valor,
                cobranca.Status.ToString(),
                cobranca.GatewayPagamentoId,
                jaExistente);
        }

        private static CustomValidationException Erro(
            string mensagem)
        {
            return new CustomValidationException(
                new[] { mensagem });
        }
    }
}
