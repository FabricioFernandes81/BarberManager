using Domain.Entities.Common.Enuns;

namespace Application.Operacional.Financeiro.DTOs
{
    public sealed record ContaPagarDetalheDto(
        Guid Id,
        Guid FornecedorId,
        string NomeFornecedor,
        string? DocumentoFornecedor,
        Guid? CompraId,
        string Descricao,
        DateTime DataEmissao,
        DateTime DataVencimento,
        decimal Valor,
        decimal ValorPago,
        decimal ValorPendente,
        StatusContas Status,
        int NumeroParcela,
        int TotalParcelas,
        DateTime DataCadastro,
        DateTime? DataAtualizacao,
        IReadOnlyList<PagamentoContaPagarDto> Pagamentos
    );

    public sealed record PagamentoContaPagarDto(
        Guid Id,
        Guid FormaPagamentoId,
        string NomeFormaPagamento,
        DateTime DataPagamento,
        decimal Valor,
        string? Observacao
    );
}