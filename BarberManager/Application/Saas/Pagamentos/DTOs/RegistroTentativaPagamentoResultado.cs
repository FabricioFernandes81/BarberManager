using Domain.Entities.Common.Enuns;
using Domain.Entities.Saas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public sealed record RegistroTentativaPagamentoResultado(
    bool Inserida,
    TransacaoPagamento? TransacaoExistente,
    StatusFatura? StatusFatura);
}
