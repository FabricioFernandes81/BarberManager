using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.Queries
{
    public record ListarFornecedoresQuery(
        int PageNumber = 1,
        int PageSize = 10,
        string? SearchTerm = null,
        bool? Ativo = null);
}
