using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.DTOs
{
    public sealed record CriarCompraDto(
        Guid Id,
        Guid FornecedorId,
        string NomeFornecedor,
        string? NumeroDocumento,
        DateTime DataEmissao,
        DateTime DataCompra,
        decimal Subtotal,
        decimal Desconto,
        decimal Frete,
        decimal OutrasDespesas,
        decimal ValorTotal,
        string Status,
        IReadOnlyList<CriarCompraItemDto> Itens,
        IReadOnlyList<CriarCompraContaPagarDto> ContasPagar
    );

    public sealed record CriarCompraItemDto(
        Guid Id,
        Guid ProdutoId,
        string DescricaoProduto,
        decimal Quantidade,
        decimal ValorUnitario,
        decimal Desconto,
        decimal ValorTotal
    );

    public sealed record CriarCompraContaPagarDto(
        Guid Id,
        int NumeroParcela,
        int TotalParcelas,
        DateTime DataVencimento,
        decimal Valor,
        string Status
    );
}
