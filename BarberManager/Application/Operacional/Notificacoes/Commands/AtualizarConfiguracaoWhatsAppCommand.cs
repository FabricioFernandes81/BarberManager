using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Notificacoes.Commands
{
    public sealed record AtualizarConfiguracaoWhatsAppCommand(
     string? NumeroWhatsApp,
     bool Ativo
 );
}
