using Domain.Entities.Common.Enuns;
using Domain.Entities.Compras;
using Domain.Entities.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Compras
{
    public interface IRepositorioCompra
    {
        Task AdicionarAsync(
            Compra compra,
            IReadOnlyCollection<ContaPagar> contasPagar,
            CancellationToken cancellationToken);

        Task<Compra?> ObterParaRecebimentoAsync(
          Guid empresaId,
          Guid compraId,
          CancellationToken cancellationToken);

        Task<RecebimentoCompra?> ObterPorChaveIdempotenciaAsync(
            Guid empresaId,
            string chaveIdempotencia,
            CancellationToken cancellationToken);

        Task AdicionarRecebimentoAsync(
            RecebimentoCompra recebimento,
            CancellationToken cancellationToken);

        Task<Compra?> ObterParaCancelamentoAsync(
    Guid empresaId,
    Guid compraId,
    CancellationToken cancellationToken);

        Task<IReadOnlyCollection<ContaPagar>>
            ObterContasPagarParaCancelamentoAsync(
                Guid empresaId,
                Guid compraId,
                CancellationToken cancellationToken);


        Task<(
    IReadOnlyList<Compra> Itens,
    int TotalRegistros
)> ListarAsync(
    Guid empresaId,
    int pageNumber,
    int pageSize,
    string? searchTerm,
    DateTime? inicio,
    DateTime? fim,
    Guid? fornecedorId,
    StatusCompra? status,
    CancellationToken cancellationToken);


        Task<(
    Compra? Compra,
    IReadOnlyList<ContaPagar> ContasPagar
)> ObterDetalheAsync(
    Guid empresaId,
    Guid compraId,
    CancellationToken cancellationToken);
    }
}
