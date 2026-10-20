using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Notificacoes
{
    public sealed record CobrancaCriadaEvent(
     Guid EmpresaId,
     Guid AssinaturaId,
     Guid FaturaId,
     decimal Valor,
     DateTime Vencimento,
     string Status);
}
