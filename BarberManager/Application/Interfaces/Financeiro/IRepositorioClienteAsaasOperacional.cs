
using Domain.Entities.Financeiro;

namespace Application.Interfaces.Financeiro
{
    public interface IRepositorioClienteAsaasOperacional
    {
        Task<ClienteAsaasOperacional?> ObterPorClienteAsync(
            Guid empresaId,
            Guid clienteId,
            CancellationToken cancellationToken);

        Task<ClienteAsaasOperacional?> ObterPorClienteAsaasIdAsync(
            Guid empresaId,
            string clienteAsaasId,
            CancellationToken cancellationToken);

        Task AdicionarAsync(
            ClienteAsaasOperacional clienteAsaas,
            CancellationToken cancellationToken);
    }
}
