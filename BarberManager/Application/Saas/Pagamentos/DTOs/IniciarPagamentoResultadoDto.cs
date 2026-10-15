using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public sealed class IniciarPagamentoResultadoDto
    {
        public Guid TransacaoId { get; init; }
        public Guid FaturaId { get; init; }

        public string Provedor { get; init; } = "Asaas";
        public string MetodoPagamento { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;

        public decimal Valor { get; init; }
        public string? ReferenciaGateway { get; init; }

        public string? UrlFatura { get; init; }

        // Preenchidos somente para Pix.
        public string? PixImagemBase64 { get; init; }
        public string? PixCopiaECola { get; init; }
        public DateTime? PixExpiracao { get; init; }

        // Representação mascarada, se retornada pelo gateway.
        public string? CartaoBandeira { get; init; }
        public string? CartaoFinal { get; init; }
    }
}
