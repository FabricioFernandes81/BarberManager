using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.Webhooks.Commands;
using Domain.Entities.Financeiro;

namespace Application.Operacional.Financeiro.Webhooks.Handlers
{
    public sealed class RegistrarEventoWebhookAsaasHandler
        : ICommandHandler<RegistrarEventoWebhookAsaasCommand, bool>
    {
        private readonly IRepositorioEventoWebhookAsaas _repositorio;

        public RegistrarEventoWebhookAsaasHandler(
            IRepositorioEventoWebhookAsaas repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<bool> Handle(
            RegistrarEventoWebhookAsaasCommand command,
            CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            if (command.EmpresaId == Guid.Empty)
                erros.Add("Empresa não identificada.");

            if (string.IsNullOrWhiteSpace(command.EventoId))
                erros.Add("Identificador do evento não informado.");

            if (string.IsNullOrWhiteSpace(command.PagamentoGatewayId))
                erros.Add("Identificador do pagamento não informado.");

            if (string.IsNullOrWhiteSpace(command.TipoEvento))
                erros.Add("Tipo do evento não informado.");

            if (string.IsNullOrWhiteSpace(command.Payload))
                erros.Add("Payload do webhook não informado.");

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            var eventoExistente =
                await _repositorio.ObterPorEventoAsync(
                    command.EmpresaId,
                    command.EventoId,
                    cancellationToken);

            if (eventoExistente is not null)
                return false;

            var evento = new EventoWebhookAsaas
            {
                EmpresaId = command.EmpresaId,
                EventoId = command.EventoId,
                PagamentoGatewayId = command.PagamentoGatewayId,
                TipoEvento = command.TipoEvento,
                Payload = command.Payload,
                Status = StatusEventoWebhookAsaas.Pendente,
                Tentativas = 0,
                RecebidoEmUtc = DateTime.UtcNow,
                ProximaTentativaEmUtc = DateTime.UtcNow
            };

            await _repositorio.AdicionarAsync(
                evento,
                cancellationToken);

            return true;
        }
    }
}