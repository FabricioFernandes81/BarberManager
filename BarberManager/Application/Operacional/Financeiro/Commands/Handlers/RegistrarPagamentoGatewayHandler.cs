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
    private readonly IOutboxOperacionalWriter _outboxWriter;



    public RegistrarPagamentoGatewayHandler(
    IRepositorioCobrancaOperacional repositorio,
    ITenantContext tenantContext,
    IOutboxOperacionalWriter outboxWriter)
    {
        _repositorio = repositorio;
        _tenantContext = tenantContext;
        _outboxWriter = outboxWriter;
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

        if (command.Evento is null)
            erros.Add("Os dados do evento de cobrança são obrigatórios.");

        if (command.Evento is not null &&
            (command.Evento.CobrancaId != command.CobrancaId ||
             command.Evento.EmpresaId != _tenantContext.EmpresaId))
        {
            erros.Add("Os dados do evento não correspondem à cobrança ou empresa.");
        }


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
     command.GatewayPagamentoId,
     command.InvoiceUrl);


            await _outboxWriter.AdicionarAsync(
      empresaId: _tenantContext.EmpresaId,
      aggregateId: command.CobrancaId,
      eventType: "CobrancaPixCriada",
      targetWorkflow: "NotificacaoWhatsApp",
      payload: command.Evento,
      cancellationToken: cancellationToken);


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