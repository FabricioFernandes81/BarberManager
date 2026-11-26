using Domain.Entities.Clientes;

namespace Application.Interfaces.Clientes
{
    public interface IConsultaUsuarioIdentity
    {
        Task<DadosContatoUsuarioDto?> ObterContatoAsync(
            Guid usuarioId,
            CancellationToken cancellationToken = default);

        Task<Guid?> ObterUsuarioIdPorTelefoneAsync(
    string telefone,
    CancellationToken cancellationToken = default);

 
    }

    public sealed record DadosContatoUsuarioDto(
        string? Email,
        string? Telefone);


}