using Domain.Entities.Financeiro;

namespace Application.Interfaces.Financeiro
{
    public interface IRepositorioConfiguracaoAssasClient
    {
        Task<ConfiguracaoAssasClient?> ObterPorEmpresaAsync(
            Guid empresaId,
            CancellationToken cancellationToken);

        Task AdicionarAsync(
            ConfiguracaoAssasClient configuracao,
            CancellationToken cancellationToken);
    }
}