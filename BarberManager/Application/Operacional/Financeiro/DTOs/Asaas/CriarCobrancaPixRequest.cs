using Domain.Entities.Common.Enuns;

namespace Application.Operacional.Financeiro.DTOs.Asaas;

public sealed record CriarCobrancaPixRequest(
    Guid ContaReceberId,
    string ChaveIdempotencia
);