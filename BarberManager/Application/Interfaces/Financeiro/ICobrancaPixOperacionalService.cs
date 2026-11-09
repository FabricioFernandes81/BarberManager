using Application.Operacional.Financeiro.DTOs.Asaas;

namespace Application.Operacional.Financeiro.Interfaces
{
    public interface ICobrancaPixOperacionalService
    {
        Task<CriarCobrancaPixResponse> IniciarAsync(
            Guid cobrancaId,
            CancellationToken cancellationToken);
    }
}