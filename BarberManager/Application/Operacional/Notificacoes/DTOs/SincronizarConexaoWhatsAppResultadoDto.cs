namespace Application.Operacional.Notificacoes.DTOs;

public sealed record SincronizarConexaoWhatsAppResultadoDto(
    bool Processado,
    string Status,
    string Mensagem);