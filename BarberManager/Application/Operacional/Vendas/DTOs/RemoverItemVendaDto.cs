using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Vendas.DTOs
{
    public sealed record RemoverItemVendaDto(
        Guid VendaId,
        Guid ItemVendaId,
        string Descricao,
        decimal SubtotalVenda,
        decimal DescontoVenda,
        decimal ValorTotalVenda
    );
}
