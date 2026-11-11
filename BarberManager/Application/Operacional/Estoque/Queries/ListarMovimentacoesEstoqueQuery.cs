using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Estoque.Queries
{
    public sealed record ListarMovimentacoesEstoqueQuery(
         Guid ProdutoId,
         DateTimeOffset? DataInicio = null,
         DateTimeOffset? DataFim = null,
         Domain.Entities.Common.Enuns.TipoMovimentacaoEstoque? Tipo = null,
         Domain.Entities.Common.Enuns.OrigemMovimentacaoEstoque? Origem = null,
         int PageNumber = 1,
         int PageSize = 10
     );
}
