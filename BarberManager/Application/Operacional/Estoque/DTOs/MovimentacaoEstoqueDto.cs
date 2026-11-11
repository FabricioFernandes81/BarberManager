using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Estoque.DTOs
{
    public sealed record MovimentacaoEstoqueDto(
      Guid Id,
      Guid ProdutoId,
      string Tipo,
      string Origem,
      decimal Quantidade,
      decimal SaldoAnterior,
      decimal SaldoPosterior,
      Guid? DocumentoOrigemId,
      Guid? UsuarioResponsavelId,
      DateTimeOffset DataMovimento,
      string? Observacao
  );
}
