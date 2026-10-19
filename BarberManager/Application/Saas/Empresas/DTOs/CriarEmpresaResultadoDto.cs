using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Empresas.DTOs
{
    public sealed record CriarEmpresaResultadoDto(
     bool Sucesso,
     string Codigo,
     string Mensagem,
     Guid? EmpresaId = null,
     Guid? ResponsavelId = null,
     Guid? AssinaturaId = null);
}
