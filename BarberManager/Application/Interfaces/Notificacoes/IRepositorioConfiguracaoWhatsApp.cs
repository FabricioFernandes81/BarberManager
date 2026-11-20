using Domain.Entities.Operacional;

namespace Application.Interfaces.Notificacoes;

public interface IRepositorioConfiguracaoWhatsApp
{
    Task<ConfiguracaoWhatsApp?> ObterAtivaPorEmpresaAsync(
        Guid empresaId,
        CancellationToken cancellationToken);

    Task AdicionarAsync(
        ConfiguracaoWhatsApp configuracao,
        CancellationToken cancellationToken);

    Task AtualizarAsync(
        ConfiguracaoWhatsApp configuracao,
        CancellationToken cancellationToken);

    Task<ConfiguracaoWhatsApp?> ObterPorEmpresaAsync(
    Guid empresaId,
    CancellationToken cancellationToken);
}