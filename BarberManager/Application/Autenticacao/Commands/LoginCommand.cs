using Application.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Autenticacao.Commands
{
    public sealed record LoginCommand(
        string Email,
        string Senha,
        TipoAcesso TipoAcesso/*,
        Guid? EmpresaId = null*/);

    public sealed record LoginResult(
     bool Sucesso,
     string? AccessToken,
     DateTime? ExpiraEm,
     string? Mensagem,
     bool RequerSelecaoEmpresa = false,
     IReadOnlyList<Guid>? EmpresasDisponiveis = null);

}
