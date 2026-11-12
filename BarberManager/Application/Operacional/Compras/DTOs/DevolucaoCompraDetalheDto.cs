
namespace Application.Operacional.Compras.DTOs
{
    public sealed record DevolucaoCompraDetalheDto(
        Guid Id,
        Guid CompraId,
        Guid FornecedorId,
        string NomeFornecedor,
        string? NumeroDocumentoCompra,
        Guid UsuarioResponsavelId,
        DateTimeOffset DataDevolucao,
        string Motivo,
        string? Observacao,
        IReadOnlyList<DevolucaoCompraItemDetalheDto> Itens
    );

    public sealed record DevolucaoCompraItemDetalheDto(
        Guid Id,
        Guid RecebimentoCompraItemId,
        Guid CompraItemId,
        Guid ProdutoId,
        string DescricaoProduto,
        decimal QuantidadeRecebida,
        decimal QuantidadeDevolvida
    );
}
