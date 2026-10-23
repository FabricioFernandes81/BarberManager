using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Profissionais.DTOs
{
    public sealed record ListarProfissionaisDto(
      IReadOnlyCollection<ProfissionalListaDto> Itens,
      int Pagina,
      int TamanhoPagina,
      int TotalRegistros,
      int TotalPaginas
  );
}
