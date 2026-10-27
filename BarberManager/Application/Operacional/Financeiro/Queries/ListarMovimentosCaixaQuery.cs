using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Financeiro.Queries
{
    public sealed record ListarMovimentosCaixaQuery(
    DateTime? DataInicio = null,
    DateTime? DataFim = null,
    TipoMovimentoCaixa? TipoMovimento = null,
    int PageNumber = 1,
    int PageSize = 20
);
}
