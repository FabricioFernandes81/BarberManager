using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Saas
{
    public class TransacaoPagamento : EntityBase
    {
        public Guid FaturaId { get; set; }

        public Fatura Fatura { get; set; } = null!;

        // Identificação do gateway: MercadoPago, Stripe, Asaas etc.
        public string Provedor { get; set; } = string.Empty;

        // Identificador da cobrança/transação no gateway.
        public string? ReferenciaGateway { get; set; }

        public string? ReferenciaClienteAsaas { get; set; }
        // Identificador único da operação para evitar cobranças duplicadas.
        public string ChaveIdempotencia { get; set; } = string.Empty;

        public decimal Valor { get; set; }

        public StatusTransacaoPagamento Status { get; set; }
            = StatusTransacaoPagamento.Criada;

        // Momento em que o resultado foi confirmado.
        public DateTime? DataProcessamento { get; set; }

        // Informações técnicas retornadas pelo gateway.
        public string? CodigoErro { get; set; }

        public string? MensagemErro { get; set; }

        public MetodoPagamento MetodoPagamento { get; set; }
    }
}
