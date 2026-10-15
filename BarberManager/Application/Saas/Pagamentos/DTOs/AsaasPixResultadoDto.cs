using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public sealed record AsaasPixResultadoDto(
     string EncodedImage,
     string Payload,
     DateTime? ExpirationDate);
}
