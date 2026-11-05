using Domain.Entities.Common.Enuns;
using Domain.Entities.Financeiro;

namespace Application.Interfaces.Financeiro
{
    public interface IRepositorioContaPagar
    {
        Task<(
            IReadOnlyList<ContaPagar> Itens,
            int TotalRegistros
        )> ListarAsync(
            Guid empresaId,
            int pageNumber,
            int pageSize,
            string? searchTerm,
            DateTime? dataInicio,
            DateTime? dataFim,
            Guid? fornecedorId,
            Guid? compraId,
            StatusContas? status,
            DateTime dataReferencia,
            CancellationToken cancellationToken);

        Task<ContaPagar?> ObterDetalheAsync(
            Guid empresaId,
            Guid contaPagarId,
            CancellationToken cancellationToken);

        Task<ContaPagar?> ObterParaPagamentoAsync(
         Guid empresaId,
         Guid contaPagarId,
         CancellationToken cancellationToken);

        Task<FormaPagamento?> ObterFormaPagamentoAtivaAsync(
            Guid empresaId,
            Guid formaPagamentoId,
            CancellationToken cancellationToken);

        Task AdicionarPagamentoAsync(
            Pagamento pagamento,
            CancellationToken cancellationToken);
    }
}