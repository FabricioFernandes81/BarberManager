using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.DTOs
{
    public sealed record RegistrarRecebimentoCompraRequest(
       string? Observacao,
       IReadOnlyCollection<RegistrarRecebimentoCompraItemDto> Itens
   );
}
