using Domain.Entities.Financeiro;

namespace Application.Interfaces.Financeiro
{
    public interface IRepositorioCobrancaOperacional
    {
        Task<CobrancaOperacional?> ObterPorIdempotenciaAsync(
            Guid empresaId,
            string chaveIdempotencia,
            CancellationToken cancellationToken);

        Task<CobrancaOperacional?> ObterPorPagamentoGatewayAsync(
            Guid empresaId,
            string gateway,
            string pagamentoGatewayId,
            CancellationToken cancellationToken);

        Task AdicionarAsync(
            CobrancaOperacional cobranca,
            CancellationToken cancellationToken);
    }
}