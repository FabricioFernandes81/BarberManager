using Application.Common.Exceptions;
using Application.Interfaces.Financeiro;
using Domain.Entities.Financeiro;

namespace Application.Operacional.Financeiro.Services
{
    public sealed class ValidadorCredencialAsaas
        : IValidadorCredencialAsaas
    {
        private readonly IAsaasOperacionalClient _asaasClient;

        public ValidadorCredencialAsaas(
            IAsaasOperacionalClient asaasClient)
        {
            _asaasClient = asaasClient;
        }

        public async Task ValidarAsync(
            string apiToken,
            AmbienteAsaas ambiente,
            CancellationToken cancellationToken)
        {
            var mensagens = new List<string>();

            if (string.IsNullOrWhiteSpace(apiToken))
            {
                mensagens.Add(
                    "O token da API Asaas deve ser informado.");
            }

            if (!Enum.IsDefined(typeof(AmbienteAsaas), ambiente))
            {
                mensagens.Add(
                    "O ambiente Asaas informado é inválido.");
            }

            if (mensagens.Any())
            {
                throw new CustomValidationException(mensagens);
            }

            cancellationToken.ThrowIfCancellationRequested();

            var credencialValida =
                await _asaasClient.ValidarCredencialAsync(
                    apiToken.Trim(),
                    ambiente,
                    cancellationToken);

            if (!credencialValida)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "A credencial Asaas não foi aceita no ambiente informado."
                    });
            }
        }
    }
}