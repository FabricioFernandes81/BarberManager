using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Profissionais.Queries
{
    public sealed record ListarProfissionaisQuery(
     int PageNumber = 1,
     int PageSize = 10,
     string? SearchTerm = null,
     StatusProfissional? Status = null
 );
}
