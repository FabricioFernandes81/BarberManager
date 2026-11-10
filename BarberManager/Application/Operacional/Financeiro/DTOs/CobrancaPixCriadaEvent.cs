
namespace Application.Operacional.Financeiro.DTOs;

public sealed record CobrancaPixCriadaEvent(
    Guid EventId,
    Guid EmpresaId,
    Guid ClienteId,
    string ClienteNome,
    string? Telefone,
    Guid CobrancaId,
    decimal Valor,
    string? LinkPagamento,
    DateTime? DataVencimento,
    bool Notificavel,
    string? MotivoNaoEnvio
);
