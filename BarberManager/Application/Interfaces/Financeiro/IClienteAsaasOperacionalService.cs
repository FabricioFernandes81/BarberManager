namespace Application.Operacional.Financeiro.Interfaces
{
    public interface IClienteAsaasOperacionalService
    {
        Task<string> ObterOuCriarAsync(
            Guid empresaId,
            Guid clienteId,
            CancellationToken cancellationToken = default);

        Task<string?> ObterClienteAsaasIdAsync(
    Guid empresaId,
    Guid clienteId,
    CancellationToken cancellationToken = default);
    }
}