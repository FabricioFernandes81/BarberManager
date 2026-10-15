using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public sealed record AsaasClienteResultadoDto(
     string Id,
     string Name,
     string? Email);
}
