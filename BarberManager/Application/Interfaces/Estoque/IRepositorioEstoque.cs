using Domain.Entities.Common.Enuns;
using Domain.Entities.Estoque;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Estoque
{
    public interface IRepositorioEstoque
    {
        Task<bool> ProdutoExisteAsync(
           Guid empresaId,
           Guid produtoId,
           CancellationToken cancellationToken);

        Task<(
            IReadOnlyList<MovimentacaoEstoque> Itens,
            int TotalRegistros
        )> ListarMovimentacoesAsync(
            Guid empresaId,
            Guid produtoId,
            DateTimeOffset? dataInicio,
            DateTimeOffset? dataFim,
            TipoMovimentacaoEstoque? tipo,
            OrigemMovimentacaoEstoque? origem,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken);
    }
}

