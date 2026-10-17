using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Usuarios.Commands
{
    public class VincularUsuarioEmpresaCommand
    {
        public Guid UsuarioId { get; init; }

        public Guid EmpresaId { get; init; }
    }
}
