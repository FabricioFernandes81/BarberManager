using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Profissionais.DTOs
{
    public sealed record AtualizarProfissionalDto(
     Guid Id,
     string Nome,
     string Cpf,
     string Matricula,
     string Status,
     int TotalHorariosAtivos);
}
