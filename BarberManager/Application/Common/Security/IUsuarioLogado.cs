
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Security;

public interface IUsuarioLogado
{
    Guid UsuarioId { get; }

    bool IsAuthenticated { get; }

    string? TipoUsuario { get; }

    IReadOnlyCollection<string> Roles { get; }

    bool TemRole(string role);

}
