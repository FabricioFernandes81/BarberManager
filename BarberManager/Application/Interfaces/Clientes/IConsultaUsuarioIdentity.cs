namespace Application.Interfaces.Clientes
{
    public interface IConsultaUsuarioIdentity
    {
        Task<DadosContatoUsuarioDto?> ObterContatoAsync(
            Guid usuarioId,
            CancellationToken cancellationToken = default);
    }

    public sealed record DadosContatoUsuarioDto(
        string? Email,
        string? Telefone);
}