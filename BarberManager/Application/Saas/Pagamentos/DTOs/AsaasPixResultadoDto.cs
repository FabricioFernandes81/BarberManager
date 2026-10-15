namespace Application.Saas.Pagamentos.DTOs
{
    public sealed record AsaasPixResultadoDto(
   string EncodedImage,
   string Payload,
   DateTime? ExpirationDate);





}
