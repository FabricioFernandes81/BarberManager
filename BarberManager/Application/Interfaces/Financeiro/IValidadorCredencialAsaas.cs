using Domain.Entities.Financeiro;

namespace Application.Interfaces.Financeiro
{
    public interface IValidadorCredencialAsaas
    {
        Task ValidarAsync(
            string apiToken,
            AmbienteAsaas ambiente,
            CancellationToken cancellationToken);
    }
}