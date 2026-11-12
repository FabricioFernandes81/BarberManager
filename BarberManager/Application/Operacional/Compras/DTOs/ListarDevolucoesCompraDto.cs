namespace Application.Operacional.Compras.DTOs
{
    public sealed record ListarDevolucoesCompraDto(
        IReadOnlyList<DevolucaoCompraListaDto> Itens,
        int PageNumber,
        int PageSize,
        int TotalRegistros,
        int TotalPaginas
    );

    public sealed record DevolucaoCompraListaDto(
        Guid Id,
        Guid CompraId,
        Guid FornecedorId,
        string NomeFornecedor,
        string? NumeroDocumentoCompra,
        DateTimeOffset DataDevolucao,
        string Motivo,
        int QuantidadeItens
    );
}