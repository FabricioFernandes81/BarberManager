using Domain.Entities.Operacional.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Notificacoes.DTOs
{
    public sealed record ConfiguracaoWhatsAppDto(
        Guid Id,
        string InstanceName,
        string? NumeroWhatsApp,
        StatusConexaoWhatsApp StatusConexao,
        bool Ativo,
        DateTime? DataConexao
    );
}
