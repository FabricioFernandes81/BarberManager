using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.DTOs
{
    public sealed record CompraListaDto(
        Guid Id,
        Guid FornecedorId,
        string NomeFornecedor,
        string? NumeroDocumento,
        string? SerieDocumento,
        DateTime DataEmissao,
        DateTime DataCompra,
        decimal Subtotal,
        decimal Desconto,
        decimal Frete,
        decimal OutrasDespesas,
        decimal ValorTotal,
        StatusCompra Status,
        DateTime DataCadastro
    );
}
