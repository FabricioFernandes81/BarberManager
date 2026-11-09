using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Financeiro;

namespace Application.Operacional.Financeiro.Commands.Handlers;

public sealed class IniciarProcessamentoCobrancaPixHandler
    : ICommandHandler<IniciarProcessamentoCobrancaPixCommand, bool>
{
    private readonly IRepositorioCobrancaOperacional _repositorio;
    private readonly ITenantContext _tenantContext;

    public IniciarProcessamentoCobrancaPixHandler(
        IRepositorioCobrancaOperacional repositorio,
        ITenantContext tenantContext)
    {
        _repositorio = repositorio;
        _tenantContext = tenantContext;
    }

    public async Task<bool> Handle(
        IniciarProcessamentoCobrancaPixCommand command,
        CancellationToken cancellationToken)
    {
        if (!_tenantContext.IsAvailable ||
            _tenantContext.EmpresaId == Guid.Empty)
        {
            throw new CustomValidationException(
                new[] { "Não foi possível identificar a empresa." });
        }

        if (command.CobrancaId == Guid.Empty)
        {
            throw new CustomValidationException(
                new[] { "O identificador da cobrança é obrigatório." });
        }

        return await _repositorio.TentarIniciarProcessamentoAsync(
            _tenantContext.EmpresaId,
            command.CobrancaId,
            cancellationToken);
    }
}