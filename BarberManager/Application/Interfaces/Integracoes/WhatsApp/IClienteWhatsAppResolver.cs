using Application.Operacional.Agendamentos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Integracoes.WhatsApp
{
    public interface IClienteWhatsAppResolver
    {
        Task<ClienteWhatsAppResolucaoDto?> ResolverAsync(
            string instanceName,
            string telefone,
            CancellationToken cancellationToken = default);
    }
}
