using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Financeiro.DTOs
{
    public sealed record RegistrarRecebimentoRequest(
    Guid FormaPagamentoId,
    decimal Valor,
    string? Observacao);
}
