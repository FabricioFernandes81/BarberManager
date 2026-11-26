using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Agendamentos;
using Application.Operacional.Agendamentos.Commands;
using Application.Operacional.Agendamentos.Queries;

namespace Application.Operacional.Agendamentos.Commands.Handlers;

public sealed class CriarAgendamentoHandler
    : ICommandHandler<CriarAgendamentoCommand, CriarAgendamentoDto>
{
    private readonly ITenantContext _tenantContext;
    private readonly ICriarAgendamentoService _criarAgendamentoService;

    public CriarAgendamentoHandler(
        ITenantContext tenantContext,
        ICriarAgendamentoService criarAgendamentoService)
    {
        _tenantContext = tenantContext;
        _criarAgendamentoService = criarAgendamentoService;
    }

    public async Task<CriarAgendamentoDto> Handle(
        CriarAgendamentoCommand command,
        CancellationToken cancellationToken)
    {
        if (!_tenantContext.IsAvailable ||
            _tenantContext.EmpresaId == Guid.Empty)
        {
            throw new CustomValidationException(
                new[]
                {
                    "Não foi possível identificar a empresa do usuário."
                });
        }

        return await _criarAgendamentoService.CriarAsync(
            _tenantContext.EmpresaId,
            command,
            cancellationToken);
    }
}