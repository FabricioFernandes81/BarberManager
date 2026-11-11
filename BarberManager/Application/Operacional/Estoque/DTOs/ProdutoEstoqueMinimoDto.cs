using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Estoque.DTOs
{
    public sealed record ProdutoEstoqueMinimoDto(
       Guid ProdutoId,
       string Nome,
       string? CodigoInterno,
       decimal QuantidadeAtual,
       decimal EstoqueMinimo,
       decimal QuantidadeNecessaria
   );
}
