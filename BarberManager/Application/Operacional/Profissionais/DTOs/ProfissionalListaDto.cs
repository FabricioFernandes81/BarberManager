using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Profissionais.DTOs
{
    public sealed record ProfissionalListaDto(
      Guid Id,
      string Nome,
      string? Sobrenome,
      string Celular,
      string Email,
      string Cargo,
      string? Especialidade,
      string? Foto,
      string Status,
      DateTime DataAdmissao,
      bool AgendaOnline,
      bool ExibirAgendamentoOnline,
      int OrdemAgenda
  );
}
