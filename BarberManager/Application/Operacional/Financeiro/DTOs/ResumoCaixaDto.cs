using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Financeiro.DTOs
{
    public sealed record ResumoCaixaDto(
     Guid CaixaId,
     StatusCaixa Status,
     DateTime DataAbertura,
     DateTime? DataFechamento,
     decimal SaldoInicial,
     decimal TotalEntradas,
     decimal TotalSuprimentos,
     decimal TotalSaidas,
     decimal TotalSangrias,
     decimal SaldoEsperado,
     decimal? SaldoContado,
     decimal? DiferencaFechamento
 );
}
