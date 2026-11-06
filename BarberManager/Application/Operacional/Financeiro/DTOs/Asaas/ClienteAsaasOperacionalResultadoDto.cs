namespace Application.Operacional.Financeiro.DTOs.Asaas;

public sealed record ClienteAsaasOperacionalResultadoDto(
    string Id,
    string? Name,
    string? CpfCnpj,
    string? Email);