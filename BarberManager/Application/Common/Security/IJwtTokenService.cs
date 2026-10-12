using Domain.Entities.AuthUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Security
{

    public sealed record ContextoAutenticacao(
    string TipoUsuario,
    Guid? EmpresaId,
    IReadOnlyList<string> Permissoes);

    public interface IJwtTokenService
    {
        Task<(string AccessToken, DateTime ExpiraEm)> GerarAsync(
         Usuario usuario,
         ContextoAutenticacao contexto,
         CancellationToken cancellationToken = default);
    }
}
