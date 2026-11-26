using Application.Operacional.Agendamentos.DTOs;

namespace Application.Interfaces.Agendamentos;

public interface IDisponibilidadeAgendaService
{
    Task<DisponibilidadeAgendaDto> ObterAsync(
        Guid empresaId,
        Guid profissionalId,
        Guid servicoId,
        DateOnly data,
        CancellationToken cancellationToken);
}