using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Empresas.Enums
{
    public enum ResultadoAlteracaoAcessoEmpresa
    {
        Habilitado = 1,
        Suspenso = 2,
        JaHabilitado = 3,
        JaSuspenso = 4,
        EmpresaNaoEncontrada = 5,
        DadosInvalidos = 6,
        EmpresaInativa = 7
    }
}
