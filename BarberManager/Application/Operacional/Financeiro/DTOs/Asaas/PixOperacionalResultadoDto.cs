namespace Application.Operacional.Financeiro.DTOs.Asaas;

public sealed record PixOperacionalResultadoDto(
    string EncodedImage,
    string Payload,
    DateTime? ExpirationDate);