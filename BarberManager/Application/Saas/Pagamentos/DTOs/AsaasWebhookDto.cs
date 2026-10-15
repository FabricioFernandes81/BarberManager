using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public sealed record AsaasWebhookDto(
       [property: JsonPropertyName("id")]
    string Id,

       [property: JsonPropertyName("event")]
    string Evento,

       [property: JsonPropertyName("payment")]
    AsaasWebhookPagamentoDto? Pagamento
   );

    public sealed record AsaasWebhookPagamentoDto(
        [property: JsonPropertyName("id")]
    string Id,

        [property: JsonPropertyName("status")]
    string Status,

        [property: JsonPropertyName("billingType")]
    string TipoCobranca,

        [property: JsonPropertyName("value")]
    decimal Valor
    );
}
