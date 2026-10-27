using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Financeiro.DTOs
{
    public sealed record MovimentoCaixaListaDto(
     Guid Id,
     DateTime DataMovimento,
     TipoMovimentoCaixa TipoMovimento,
     Guid FormaPagamentoId,
     string FormaPagamento,
     decimal Valor,
     string Historico,
     string? Documento
 );
}
