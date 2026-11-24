using Application.Operacional.Notificacoes.DTOs;

public interface ISincronizarConexaoWhatsAppService
{
    Task ExecutarAsync(
        SincronizarConexaoWhatsAppRequest request,
        CancellationToken cancellationToken);
}