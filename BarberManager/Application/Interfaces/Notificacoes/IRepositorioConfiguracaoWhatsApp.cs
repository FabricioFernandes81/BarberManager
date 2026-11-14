using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Notificacoes
{
    public interface IRepositorioConfiguracaoWhatsApp
    {
        Task<ConfiguracaoWhatsApp?> ObterAtivaPorEmpresaAsync(
            Guid empresaId,
            CancellationToken cancellationToken);
    }
}
