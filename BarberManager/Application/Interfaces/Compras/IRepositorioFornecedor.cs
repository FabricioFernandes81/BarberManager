using Domain.Entities.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Compras
{
    public interface IRepositorioFornecedor
    {
        Task<(
            IReadOnlyList<Fornecedor> Itens,
            int TotalRegistros
        )> ListarAsync(
            Guid empresaId,
            int pageNumber,
            int pageSize,
            string? searchTerm,
            bool? ativo,
            CancellationToken cancellationToken);

        Task<Fornecedor?> ObterPorIdAsync(
            Guid empresaId,
            Guid fornecedorId,
            CancellationToken cancellationToken);

        Task<Fornecedor?> ObterPorIdParaAtualizacaoAsync(
            Guid empresaId,
            Guid fornecedorId,
            CancellationToken cancellationToken);

        Task<bool> DocumentoExisteAsync(
            Guid empresaId,
            string documento,
            CancellationToken cancellationToken,
            Guid? ignorarFornecedorId = null);

        Task AdicionarAsync(
            Fornecedor fornecedor,
            CancellationToken cancellationToken);

        Task AtualizarAsync(Fornecedor fornecedor);
    }
}
