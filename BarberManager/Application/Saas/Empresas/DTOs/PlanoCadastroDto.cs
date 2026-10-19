using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Empresas.DTOs
{
    public sealed record PlanoCadastroDto(
    Guid Id,
    string Nome,
    decimal ValorMensal,
    decimal ValorAnual);
}
