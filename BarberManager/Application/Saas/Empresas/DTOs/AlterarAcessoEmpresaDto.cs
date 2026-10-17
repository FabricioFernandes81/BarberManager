using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Empresas.DTOs
{
    public class AlterarAcessoEmpresaDto
    {
        public bool HabilitarAcesso { get; init; }

        public string? Motivo { get; init; }

    }
}
