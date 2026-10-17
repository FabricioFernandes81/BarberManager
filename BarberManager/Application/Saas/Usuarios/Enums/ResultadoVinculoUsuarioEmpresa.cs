using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Usuarios.Enums
{
    public enum ResultadoVinculoUsuarioEmpresa
    {
        Criado = 1,
        Reativado = 2,
        JaAtivo = 3,
        UsuarioNaoEncontrado = 4,
        EmpresaNaoEncontrada = 5,
        DadosInvalidos = 6
    }
}
