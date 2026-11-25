namespace Application.Operacional.Notificacoes.Events;

public sealed record MensagemInteracaoWhatsAppEvent(
    Guid EventId,
    Guid EmpresaId,
    Guid? InteracaoId,
    Guid? AgendamentoId,
    string Telefone,
    string Conteudo,
    string TipoMensagem,
    string? ProviderMessageIdOrigem
);
