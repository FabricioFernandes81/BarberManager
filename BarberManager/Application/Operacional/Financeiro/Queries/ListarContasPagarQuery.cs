using Domain.Entities.Common.Enuns;

namespace Application.Operacional.Financeiro.Queries
{
    public sealed record ListarContasPagarQuery(
        int PageNumber = 1,
        int PageSize = 20,
        string? SearchTerm = null,
        DateTime? DataInicio = null,
        DateTime? DataFim = null,
        Guid? FornecedorId = null,
        Guid? CompraId = null,
        StatusContas? Status = null
    );
}