using Domain.Entities.Common.Enuns;

namespace Application.Operacional.Compras.DTOs
{
    public sealed record RegistrarRecebimentoCompraDto(
        Guid Id,
        Guid CompraId,
        DateTimeOffset DataRecebimento,
        StatusCompra StatusCompra,
        IReadOnlyCollection<RegistrarRecebimentoCompraItemResultadoDto> Itens
    );

    public sealed record RegistrarRecebimentoCompraItemResultadoDto(
        Guid CompraItemId,
        Guid ProdutoId,
        decimal QuantidadeRecebida,
        decimal QuantidadeAcumulada,
        decimal QuantidadePendente
    );
}