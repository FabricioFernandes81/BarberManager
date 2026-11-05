using Domain.Entities.Common.Enuns;

namespace Application.Operacional.Compras.DTOs
{
    public sealed record CompraDetalheDto(
        Guid Id,
        Guid FornecedorId,
        string NomeFornecedor,
        string? DocumentoFornecedor,
        string? NumeroDocumento,
        string? SerieDocumento,
        DateTime DataEmissao,
        DateTime DataCompra,
        decimal Subtotal,
        decimal Desconto,
        decimal Frete,
        decimal OutrasDespesas,
        decimal ValorTotal,
        string? Observacao,
        StatusCompra Status,
        DateTime DataCadastro,
        DateTime? DataAtualizacao,
        IReadOnlyList<CompraItemDetalheDto> Itens,
        IReadOnlyList<RecebimentoCompraDetalheDto> Recebimentos,
        IReadOnlyList<ContaPagarCompraDto> ContasPagar
    );

    public sealed record CompraItemDetalheDto(
        Guid Id,
        Guid ProdutoId,
        string DescricaoProduto,
        decimal Quantidade,
        decimal QuantidadeRecebida,
        decimal QuantidadePendente,
        decimal ValorUnitario,
        decimal Desconto,
        decimal ValorTotal
    );

    public sealed record RecebimentoCompraDetalheDto(
        Guid Id,
        Guid UsuarioResponsavelId,
        DateTimeOffset DataRecebimento,
        string ChaveIdempotencia,
        string? Observacao,
        IReadOnlyList<RecebimentoCompraItemDetalheDto> Itens
    );

    public sealed record RecebimentoCompraItemDetalheDto(
        Guid Id,
        Guid CompraItemId,
        Guid ProdutoId,
        string DescricaoProduto,
        decimal QuantidadeRecebida
    );

    public sealed record ContaPagarCompraDto(
        Guid Id,
        string Descricao,
        DateTime DataEmissao,
        DateTime DataVencimento,
        decimal Valor,
        decimal ValorPago,
        decimal ValorPendente,
        StatusContas Status,
        int NumeroParcela,
        int TotalParcelas,
        IReadOnlyList<PagamentoCompraDto> Pagamentos
    );

    public sealed record PagamentoCompraDto(
        Guid Id,
        Guid FormaPagamentoId,
        string NomeFormaPagamento,
        DateTime DataPagamento,
        decimal Valor,
        string? Observacao
    );
}