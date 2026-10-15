using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public sealed record AsaasCriarClienteDto(
     string Nome,
     string CpfCnpj,
     string? Email,
     string? Telefone,
     string ExternalReference);
}
