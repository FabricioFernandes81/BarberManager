using Application.Common.Exceptions;
using Application.Interfaces.Financeiro;
using Application.Dispatcher;
using Application.Operacional.Financeiro.Commands;
using Application.Operacional.Financeiro.DTOs;

namespace Application.Operacional.Financeiro.Services
{
    public sealed class ConfiguracaoAsaasService
        : IConfiguracaoAsaasService
    {
        private readonly IValidadorCredencialAsaas _validador;
        private readonly IProtecaoCredenciaisAsaas _protecao;
        private readonly IDispatcher _dispatcher;

        public ConfiguracaoAsaasService(
            IValidadorCredencialAsaas validador,
            IProtecaoCredenciaisAsaas protecao,
            IDispatcher dispatcher)
        {
            _validador = validador;
            _protecao = protecao;
            _dispatcher = dispatcher;
        }

        public async Task<ConfiguracaoAsaasDto> ConfigurarAsync(
            ConfigurarCredenciaisAsaasRequest request,
            CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            if (string.IsNullOrWhiteSpace(request.ApiToken))
                erros.Add("O token da API Asaas é obrigatório.");

            if (string.IsNullOrWhiteSpace(request.WebhooksToken))
                erros.Add("O token do webhook Asaas é obrigatório.");

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            // 1. Validar a credencial externa.
            // Nenhuma transação SQL está aberta neste momento.
            await _validador.ValidarAsync(
                request.ApiToken,
                request.Ambiente,
                cancellationToken);

            // 2. Proteger as credenciais.
            var apiTokenProtegido =
                _protecao.Proteger(request.ApiToken.Trim());

            var webhookTokenProtegido =
                _protecao.Proteger(request.WebhooksToken.Trim());

            // 3. Persistir por meio do comando transacional.
            var command = new SalvarConfiguracaoAsaasCommand(
                apiTokenProtegido,
                webhookTokenProtegido,
                request.Ambiente,
                DateTime.UtcNow);

            return await _dispatcher.Command<
                SalvarConfiguracaoAsaasCommand,
                ConfiguracaoAsaasDto>(
                    command,
                    cancellationToken);
        }
    }
}