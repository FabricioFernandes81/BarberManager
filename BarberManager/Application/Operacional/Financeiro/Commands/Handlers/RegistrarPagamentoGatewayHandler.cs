using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Financeiro;

namespace Application.Operacional.Financeiro.Commands.Handlers;

public sealed class RegistrarPagamentoGatewayHandler
    : ICommandHandler<RegistrarPagamentoGatewayCommand, Guid>
{
    private readonly IRepositorioCobrancaOperacional _repositorio;
    private readonly ITenantContext _tenantContext;

    public RegistrarPagamentoGatewayHandler(
        IRepositorioCobrancaOperacional repositorio,
        ITenantContext tenantContext)
    {
        _repositorio = repositorio;
        _tenantContext = tenantContext;
    }

    public async Task<Guid> Handle(
        RegistrarPagamentoGatewayCommand command,
        CancellationToken cancellationToken)
    {
        var erros = new List<string>();

        if (!_tenantContext.IsAvailable)
            erros.Add("Não foi possível identificar a empresa.");

        if (command.CobrancaId == Guid.Empty)
            erros.Add("O identificador da cobrança é obrigatório.");

        if (string.IsNullOrWhiteSpace(command.GatewayPagamentoId))
            erros.Add("O identificador do pagamento no gateway é obrigatório.");

        if (erros.Count > 0)
            throw new CustomValidationException(erros);

        var cobranca = await _repositorio.ObterPorIdAsync(
            _tenantContext.EmpresaId,
            command.CobrancaId,
            cancellationToken);

        if (cobranca is null)
        {
            throw new CustomValidationException(
                new[] { "Cobrança não encontrada para esta empresa." });
        }

        try
        {
            cobranca.RegistrarPagamentoGateway(
                command.GatewayPagamentoId);
        }
        catch (InvalidOperationException ex)
        {
            throw new CustomValidationException(
                new[] { ex.Message });
        }

        // A UnitOfWork persiste a alteração.
        // Não chamar SaveChangesAsync neste handler.

        return cobranca.Id;
    }
}