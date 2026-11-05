using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.DTOs
{
    public sealed record CompraCanceladaDto(
      Guid CompraId,
      StatusCompra Status,
      DateTime? DataAtualizacao,
      int ContasPagarCanceladas
  );
}
