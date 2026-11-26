using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Agendamentos;
using Application.Operacional.Agendamentos.DTOs;

namespace Application.Operacional.Agendamentos.Queries.Handlers;

public sealed class ObterDisponibilidadeAgendaHandler
    : IQueryHandler<
        ObterDisponibilidadeAgendaQuery,
        DisponibilidadeAgendaDto>
{
    private readonly ITenantContext _tenantContext;
    private readonly IDisponibilidadeAgendaService _service;

    public ObterDisponibilidadeAgendaHandler(
        ITenantContext tenantContext,
        IDisponibilidadeAgendaService service)
    {
        _tenantContext = tenantContext;
        _service = service;
    }

    public async Task<DisponibilidadeAgendaDto> Handle(
        ObterDisponibilidadeAgendaQuery query,
        CancellationToken cancellationToken)
    {
        if (!_tenantContext.IsAvailable ||
            _tenantContext.EmpresaId == Guid.Empty)
        {
            throw new UnauthorizedAccessException(
                "Não foi possível identificar a empresa do usuário.");
        }

        return await _service.ObterAsync(
            _tenantContext.EmpresaId,
            query.ProfissionalId,
            query.ServicoId,
            query.Data,
            cancellationToken);
    }
}