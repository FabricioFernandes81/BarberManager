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
[MinLength(1)]
IReadOnlyCollection<PagamentoVendaRequest> Pagamentos,
 string? Observacao
 );

    public sealed record PagamentoVendaRequest(
        [Required]
    Guid FormaPagamentoId,

        [Required]
    [ValorPagamentoValido]
    decimal Valor
    );

    public sealed class ValorPagamentoValidoAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value is decimal valor && valor > 0;
        }

        public override string FormatErrorMessage(string name)
        {
            return "O valor do pagamento deve ser maior que zero.";
        }
    }
}
