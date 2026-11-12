
namespace Application.Operacional.Compras.DTOs
{
    public sealed record RegistrarDevolucaoCompraItemDto(
        Guid RecebimentoCompraItemId,
        decimal QuantidadeDevolvida
    );

    public sealed record RegistrarDevolucaoCompraDto(
        Guid Id,
        Guid CompraId,
        DateTimeOffset DataDevolucao,
        string Motivo,
        string? Observacao,
        IReadOnlyCollection<RegistrarDevolucaoCompraItemResultadoDto> Itens
    );

    public sealed record RegistrarDevolucaoCompraItemResultadoDto(
        Guid RecebimentoCompraItemId,
        Guid ProdutoId,
        decimal QuantidadeDevolvida
    );
}
