namespace Application.Operacional.Financeiro.DTOs.Asaas;

public sealed record CriarClienteAsaasOperacionalDto(
    string Nome,
    string? CpfCnpj,
    string? Email,
    string? Telefone,
    string? ExternalReference);