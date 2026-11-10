using Domain.Entities.Common;

namespace Domain.Entities.Financeiro
{
    public class CobrancaOperacional : EntityBase
    {
        public Guid EmpresaId { get; private set; }

        public Guid ContaReceberId { get; private set; }

        public ContaReceber ContaReceber { get; private set; } = null!;

        public string Gateway { get; private set; } = "Asaas";

        public string ExternalReference { get; private set; } = string.Empty;

        public string? GatewayPagamentoId { get; private set; }

        public string ChaveIdempotencia { get; private set; } = string.Empty;

        public decimal Valor { get; private set; }

        public StatusCobrancaOperacional Status { get; private set; }
            = StatusCobrancaOperacional.Reservada;

        public DateTime CriadaEmUtc { get; private set; }

        public DateTime? ConfirmadaEmUtc { get; private set; }
        public string? InvoiceUrl { get; private set; }
        protected CobrancaOperacional()
        {
        }

        public CobrancaOperacional(
            Guid empresaId,
            Guid contaReceberId,
            string externalReference,
            string chaveIdempotencia,
            decimal valor)
        {
            if (empresaId == Guid.Empty)
                throw new ArgumentException("Empresa inválida.");

            if (contaReceberId == Guid.Empty)
                throw new ArgumentException("Conta a receber inválida.");

            if (string.IsNullOrWhiteSpace(externalReference))
                throw new ArgumentException("A referência externa é obrigatória.");

            if (string.IsNullOrWhiteSpace(chaveIdempotencia))
                throw new ArgumentException("A chave de idempotência é obrigatória.");

            if (valor <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(valor),
                    "O valor deve ser maior que zero.");

            EmpresaId = empresaId;
            ContaReceberId = contaReceberId;
            ExternalReference = externalReference.Trim();
            ChaveIdempotencia = chaveIdempotencia.Trim();
            Valor = decimal.Round(valor, 2, MidpointRounding.AwayFromZero);
            CriadaEmUtc = DateTime.UtcNow;
            Status = StatusCobrancaOperacional.Reservada;
        }

        public void RegistrarPagamentoGateway(
    string pagamentoGatewayId, string? invoiceUrl)
        {
            if (string.IsNullOrWhiteSpace(pagamentoGatewayId))
                throw new ArgumentException(
                    "O identificador do pagamento é obrigatório.");

            if (Status != StatusCobrancaOperacional.EmProcessamento)
                throw new InvalidOperationException(
                    "A cobrança não está em processamento.");

            if (!string.IsNullOrWhiteSpace(GatewayPagamentoId))
                throw new InvalidOperationException(
                    "A cobrança já possui identificador no gateway.");

            GatewayPagamentoId = pagamentoGatewayId.Trim();

            InvoiceUrl = string.IsNullOrWhiteSpace(invoiceUrl)
                ? null : invoiceUrl.Trim();

            Status = StatusCobrancaOperacional.Pendente;

        }

        public void Confirmar(DateTime confirmadaEmUtc)
        {
            if (Status == StatusCobrancaOperacional.Confirmada)
                return;

            if (Status != StatusCobrancaOperacional.Pendente)
                throw new InvalidOperationException(
                    "Somente cobranças pendentes podem ser confirmadas.");

            Status = StatusCobrancaOperacional.Confirmada;
            ConfirmadaEmUtc = DateTime.SpecifyKind(
                confirmadaEmUtc,
                DateTimeKind.Utc);
        }
    }

    public enum StatusCobrancaOperacional
    {
        Pendente = 1,
        Confirmada = 2,
        Cancelada = 3,
        Expirada = 4,
        Falha = 5,
        EmProcessamento = 6,
        Reservada = 7
    }
}