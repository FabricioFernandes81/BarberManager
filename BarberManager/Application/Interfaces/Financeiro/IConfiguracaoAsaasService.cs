using Application.Operacional.Financeiro.DTOs;

namespace Application.Interfaces.Financeiro
{
    public interface IConfiguracaoAsaasService
    {
        Task<ConfiguracaoAsaasDto> ConfigurarAsync(
            ConfigurarCredenciaisAsaasRequest request,
            CancellationToken cancellationToken);
    }
}