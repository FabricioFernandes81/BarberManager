
using Domain.Entities.Compras;

namespace Application.Interfaces.Compras
{
    public interface IRepositorioDevolucaoCompra
    {
        Task<DevolucaoCompra?> ObterPorChaveIdempotenciaAsync(
            Guid empresaId,
            string chaveIdempotencia,
            CancellationToken cancellationToken);

        Task<Compra?> ObterParaDevolucaoAsync(
            Guid empresaId,
            Guid compraId,
            CancellationToken cancellationToken);

        Task AdicionarAsync(
            DevolucaoCompra devolucao,
            CancellationToken cancellationToken);


        Task<DevolucaoCompra?> ObterDetalheAsync(
            Guid empresaId,
            Guid devolucaoId,
            CancellationToken cancellationToken);

        Task<(
    IReadOnlyList<DevolucaoCompra> Itens,
    int TotalRegistros
)> ListarAsync(
    Guid empresaId,
    int pageNumber,
    int pageSize,
    Guid? compraId,
    Guid? fornecedorId,
    DateTime? inicio,
    DateTime? fim,
    string? searchTerm,
    CancellationToken cancellationToken);
    }
}
