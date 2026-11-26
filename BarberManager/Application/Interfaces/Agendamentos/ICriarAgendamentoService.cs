using Application.Operacional.Agendamentos.Commands;
using Application.Operacional.Agendamentos.Queries;

namespace Application.Interfaces.Agendamentos;

public interface ICriarAgendamentoService
{
    Task<CriarAgendamentoDto> CriarAsync(
        Guid empresaId,
        CriarAgendamentoCommand command,
        CancellationToken cancellationToken);
}