
using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.Commands;
using Domain.Entities.Financeiro;

namespace Application.Operacional.Financeiro.Commands.Handlers;

public sealed class RegistrarClienteAsaasOperacionalHandler
    : ICommandHandler<RegistrarClienteAsaasOperacionalCommand, bool>
{
    private readonly IRepositorioClienteAsaasOperacional _repositorio;
    private readonly ITenantContext _tenantContext;

    public RegistrarClienteAsaasOperacionalHandler(
        IRepositorioClienteAsaasOperacional repositorio,
        ITenantContext tenantContext)
    {
        _repositorio = repositorio;
        _tenantContext = tenantContext;
    }

    public async Task<bool> Handle(
        RegistrarClienteAsaasOperacionalCommand command,
        CancellationToken cancellationToken)
    {
        var erros = new List<string>();

        if (!_tenantContext.IsAvailable ||
            _tenantContext.EmpresaId == Guid.Empty)
        {
            erros.Add("Não foi possível identificar a empresa.");
        }

        if (command.EmpresaId == Guid.Empty)
            erros.Add("O identificador da empresa é obrigatório.");

        if (command.ClienteId == Guid.Empty)
            erros.Add("O identificador do cliente é obrigatório.");

        if (string.IsNullOrWhiteSpace(command.ClienteAsaasId))
            erros.Add("O identificador do cliente no Asaas é obrigatório.");

        if (erros.Count > 0)
            throw new CustomValidationException(erros);

        if (command.EmpresaId != _tenantContext.EmpresaId)
        {
            throw new CustomValidationException(
                new[]
                {
                    "A empresa informada não corresponde à empresa autenticada."
                });
        }

        var clienteAsaasId = command.ClienteAsaasId.Trim();

        // Verifica se já existe associação para o cliente.
        var associacaoExistente = await _repositorio.ObterPorClienteAsync(
            command.EmpresaId,
            command.ClienteId,
            cancellationToken);

        if (associacaoExistente is not null)
        {
            if (string.Equals(
                associacaoExistente.ClienteAsaasId,
                clienteAsaasId,
                StringComparison.Ordinal))
            {
                // Operação idempotente: associação já registrada.
                return true;
            }

            throw new CustomValidationException(
                new[]
                {
                    "O cliente já possui uma associação diferente com o Asaas."
                });
        }

        // Impede vincular o mesmo Customer Asaas a outro cliente.
        var clienteGatewayExistente =
            await _repositorio.ObterPorClienteAsaasIdAsync(
                command.EmpresaId,
                clienteAsaasId,
                cancellationToken);

        if (clienteGatewayExistente is not null)
        {
            throw new CustomValidationException(
                new[]
                {
                    "Este identificador do Asaas já está associado a outro cliente."
                });
        }

        var associacao = new ClienteAsaasOperacional(
            command.EmpresaId,
            command.ClienteId,
            clienteAsaasId);

        await _repositorio.AdicionarAsync(
            associacao,
            cancellationToken);

        // A UnitOfWork realiza a persistência.
        // Não chamar SaveChangesAsync neste handler.

        return true;
    }
}
