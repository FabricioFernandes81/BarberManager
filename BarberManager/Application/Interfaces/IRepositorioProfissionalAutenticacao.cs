using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public sealed record ProfissionalAutenticacaoInfo(
    Guid ProfissionalId,
    Guid EmpresaId);
    public interface IRepositorioProfissionalAutenticacao
    {
        Task<ProfissionalAutenticacaoInfo?> ObterPorUsuarioIdAsync(
            Guid usuarioId, CancellationToken cancellationToken = default);
    }
}
