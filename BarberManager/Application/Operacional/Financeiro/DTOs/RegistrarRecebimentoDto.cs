using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Financeiro.DTOs
{
    public sealed record RegistrarRecebimentoDto(
    Guid RecebimentoId,
    Guid ContaReceberId,
    decimal ValorRecebimento,
    decimal TotalRecebido,
    decimal SaldoRestante,
    StatusContas Status,
    DateTime DataRecebimento
);
}
