using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Vendas.DTOs
{
    public sealed record FinalizarVendaDto(
       Guid VendaId,
       StatusVenda Status,
       DateTimeOffset DataAbertura,
       DateTimeOffset DataFinalizacao,
       decimal Subtotal,
       decimal Desconto,
       decimal ValorTotal
   );
}
