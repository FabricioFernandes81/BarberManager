using Domain.Entities.Financeiro;

namespace Application.Interfaces.Financeiro
{
    public interface IAsaasOperacionalClient
    {
        Task<bool> ValidarCredencialAsync(
            string apiToken,
            AmbienteAsaas ambiente,
            CancellationToken cancellationToken);
    }
}