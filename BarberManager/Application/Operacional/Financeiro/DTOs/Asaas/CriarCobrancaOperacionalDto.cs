using Domain.Entities.Common.Enuns;

namespace Application.Operacional.Financeiro.DTOs.Asaas;

public sealed record CriarCobrancaOperacionalDto(
    string ClienteAsaasId,
    string ExternalReference,
    decimal Valor,
    DateTime Vencimento,
    MetodoPagamento MetodoPagamento,
    string? Descricao,
    string? RemoteIp,
    DadosCartaoOperacionalDto? CartaoCredito);