using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public sealed record AsaasCobrancaDto(
     string Id,
     string Status,
     string BillingType,
     decimal Value,
     string? InvoiceUrl,
     string? ExternalReference,
     string? CreditCardBrand,
     string? CreditCardNumber);
}
