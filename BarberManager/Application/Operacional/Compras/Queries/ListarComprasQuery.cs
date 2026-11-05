using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.Queries
{
    public sealed record ListarComprasQuery(
       int PageNumber = 1,
       int PageSize = 10,
       string? SearchTerm = null,
       DateTime? Inicio = null,
       DateTime? Fim = null,
       Guid? FornecedorId = null,
       StatusCompra? Status = null
   );
}
