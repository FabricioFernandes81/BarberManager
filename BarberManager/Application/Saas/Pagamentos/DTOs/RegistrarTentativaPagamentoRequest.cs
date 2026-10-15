using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public sealed record RegistrarTentativaPagamentoRequest(
     Guid FaturaId,
     string Provedor,
       MetodoPagamento MetodoPagamento
 );
}
