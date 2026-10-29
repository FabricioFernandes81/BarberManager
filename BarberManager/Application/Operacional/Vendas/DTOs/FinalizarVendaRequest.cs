using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Vendas.DTOs
{
    public sealed record FinalizarVendaRequest(
        [Required]
        Guid FormaPagamentoId,
        string? Observacao
    );
}
