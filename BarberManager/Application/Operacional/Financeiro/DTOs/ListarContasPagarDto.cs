namespace Application.Operacional.Financeiro.DTOs
{
    public sealed record ListarContasPagarDto(
        IReadOnlyList<ContaPagarListaDto> Itens,
        int PageNumber,
        int PageSize,
        int TotalRegistros,
        int TotalPaginas
    );

    public sealed record ContaPagarListaDto(
        Guid Id,
        Guid FornecedorId,
        string NomeFornecedor,
        Guid? CompraId,
        string Descricao,
        DateTime DataEmissao,
        DateTime DataVencimento,
        decimal Valor,
        decimal ValorPago,
        decimal ValorPendente,
        Domain.Entities.Common.Enuns.StatusContas Status,
        int NumeroParcela,
        int TotalParcelas
    );
}