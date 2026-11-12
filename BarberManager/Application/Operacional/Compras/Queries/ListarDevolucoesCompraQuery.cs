namespace Application.Operacional.Compras.Queries
{
    public sealed record ListarDevolucoesCompraQuery(
        int PageNumber = 1,
        int PageSize = 10,
        Guid? CompraId = null,
        Guid? FornecedorId = null,
        DateTime? Inicio = null,
        DateTime? Fim = null,
        string? SearchTerm = null
    );
}