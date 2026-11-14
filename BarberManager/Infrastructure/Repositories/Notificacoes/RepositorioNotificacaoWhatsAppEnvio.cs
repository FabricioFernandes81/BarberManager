
using System.Data;
using Application.Interfaces.Notificacoes;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Notificacoes;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Notificacoes;

public sealed class RepositorioNotificacaoWhatsAppEnvio
    : IRepositorioNotificacaoWhatsAppEnvio
{
    private const string WorkflowNotificacao = "NotificacaoWhatsApp";
    private const int TamanhoMaximoErro = 2000;

    private readonly BarbeariaOperacionalDbContext _context;

    public RepositorioNotificacaoWhatsAppEnvio(
        BarbeariaOperacionalDbContext context)
    {
        _context = context;
    }

    public async Task<NotificacaoWhatsAppEnvio?> ObterPorEventoAsync(
        Guid empresaId,
        Guid outboxEventId,
        CancellationToken cancellationToken)
    {
        ValidarIdentificadores(empresaId, outboxEventId);

        return await _context.NotificacoesWhatsAppEnvios
            .AsNoTracking()
            .SingleOrDefaultAsync(
                x => x.EmpresaId == empresaId &&
                     x.OutboxEventId == outboxEventId,
                cancellationToken);
    }

    public async Task<NotificacaoWhatsAppEnvio?> TentarReservarEnvioAsync(
        Guid empresaId,
        Guid outboxEventId,
        string hashConteudo,
        DateTime agoraUtc,
        DateTime leaseExpiraEmUtc,
        CancellationToken cancellationToken)
    {
        ValidarIdentificadores(empresaId, outboxEventId);

        if (string.IsNullOrWhiteSpace(hashConteudo) ||
            hashConteudo.Length != 64 ||
            !hashConteudo.All(Uri.IsHexDigit))
        {
            throw new ArgumentException(
                "O hash do conteúdo deve ser um SHA-256 hexadecimal válido.",
                nameof(hashConteudo));
        }

        if (leaseExpiraEmUtc <= agoraUtc)
        {
            throw new ArgumentException(
                "A expiração do lease deve ser posterior ao início.",
                nameof(leaseExpiraEmUtc));
        }

        await using var transaction =
            await _context.Database.BeginTransactionAsync(
                IsolationLevel.Serializable,
                cancellationToken);

        var eventoValido = await _context.OutboxMessages
            .AnyAsync(
                x => x.Id == outboxEventId &&
                     x.EmpresaId == empresaId &&
                     x.TargetWorkflow == WorkflowNotificacao,
                cancellationToken);

        if (!eventoValido)
        {
            await transaction.RollbackAsync(cancellationToken);
            return null;
        }

        var envio = await _context.NotificacoesWhatsAppEnvios
            .SingleOrDefaultAsync(
                x => x.EmpresaId == empresaId &&
                     x.OutboxEventId == outboxEventId,
                cancellationToken);

        if (envio is null)
        {
            envio = new NotificacaoWhatsAppEnvio
            {
                EmpresaId = empresaId,
                OutboxEventId = outboxEventId,
                HashConteudo = hashConteudo,
                Status = StatusEnvioWhatsApp.Processando,
                Tentativas = 1,
                DataInicioProcessamentoUtc = agoraUtc,
                DataUltimaTentativaUtc = agoraUtc,
                LeaseExpiraEmUtc = leaseExpiraEmUtc
            };

            await _context.NotificacoesWhatsAppEnvios.AddAsync(
                envio,
                cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return envio;
        }

        if (!string.Equals(
            envio.HashConteudo,
            hashConteudo,
            StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                "O conteúdo do evento diverge do hash já registrado.");
        }

        if (envio.Status is StatusEnvioWhatsApp.Aceito
            or StatusEnvioWhatsApp.ResultadoIndeterminado)
        {
            await transaction.CommitAsync(cancellationToken);
            return null;
        }

        if (envio.Status == StatusEnvioWhatsApp.Processando)
        {
            if (envio.LeaseExpiraEmUtc.HasValue &&
                envio.LeaseExpiraEmUtc.Value > agoraUtc)
            {
                await transaction.CommitAsync(cancellationToken);
                return null;
            }

            // O processamento anterior pode ter enviado a mensagem.
            // Não repetir automaticamente após perder o lease.
            envio.Status = StatusEnvioWhatsApp.ResultadoIndeterminado;
            envio.LeaseExpiraEmUtc = null;
            envio.UltimoErro =
                "Lease expirado sem confirmação do resultado do envio.";

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return null;
        }

        if (envio.Status is not (
            StatusEnvioWhatsApp.Pendente or
            StatusEnvioWhatsApp.Falha))
        {
            await transaction.CommitAsync(cancellationToken);
            return null;
        }

        envio.Status = StatusEnvioWhatsApp.Processando;
        envio.Tentativas++;
        envio.DataInicioProcessamentoUtc = agoraUtc;
        envio.DataUltimaTentativaUtc = agoraUtc;
        envio.LeaseExpiraEmUtc = leaseExpiraEmUtc;
        envio.UltimoErro = null;

        await _context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return envio;
    }

    public async Task<bool> RegistrarAceiteAsync(
        Guid empresaId,
        Guid outboxEventId,
        string? providerMessageId,
        DateTime dataAceiteUtc,
        CancellationToken cancellationToken)
    {
        var envio = await ObterEnvioValidoAsync(
            empresaId,
            outboxEventId,
            cancellationToken);

        if (envio is null)
            return false;

        if (envio.Status == StatusEnvioWhatsApp.Aceito)
        {
            return envio.ProviderMessageId == providerMessageId;
        }

        if (envio.Status != StatusEnvioWhatsApp.Processando)
            return false;

        if (providerMessageId?.Length > 200)
        {
            throw new ArgumentException(
                "O identificador do provedor excede 200 caracteres.",
                nameof(providerMessageId));
        }

        envio.Status = StatusEnvioWhatsApp.Aceito;
        envio.ProviderMessageId = providerMessageId;
        envio.DataAceiteUtc = dataAceiteUtc;
        envio.LeaseExpiraEmUtc = null;
        envio.UltimoErro = null;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> RegistrarFalhaAsync(
        Guid empresaId,
        Guid outboxEventId,
        string erro,
        DateTime dataTentativaUtc,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(erro))
            throw new ArgumentException(
                "O motivo da falha deve ser informado.",
                nameof(erro));

        var envio = await ObterEnvioValidoAsync(
            empresaId,
            outboxEventId,
            cancellationToken);

        if (envio is null ||
            envio.Status != StatusEnvioWhatsApp.Processando)
            return false;

        envio.Status = StatusEnvioWhatsApp.Falha;
        envio.DataUltimaTentativaUtc = dataTentativaUtc;
        envio.LeaseExpiraEmUtc = null;
        envio.UltimoErro = LimitarErro(erro);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> RegistrarResultadoIndeterminadoAsync(
        Guid empresaId,
        Guid outboxEventId,
        string motivo,
        DateTime dataTentativaUtc,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(motivo))
            throw new ArgumentException(
                "O motivo do resultado indeterminado deve ser informado.",
                nameof(motivo));

        var envio = await ObterEnvioValidoAsync(
            empresaId,
            outboxEventId,
            cancellationToken);

        if (envio is null ||
            envio.Status != StatusEnvioWhatsApp.Processando)
            return false;

        envio.Status = StatusEnvioWhatsApp.ResultadoIndeterminado;
        envio.DataUltimaTentativaUtc = dataTentativaUtc;
        envio.LeaseExpiraEmUtc = null;
        envio.UltimoErro = LimitarErro(motivo);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    private async Task<NotificacaoWhatsAppEnvio?> ObterEnvioValidoAsync(
        Guid empresaId,
        Guid outboxEventId,
        CancellationToken cancellationToken)
    {
        ValidarIdentificadores(empresaId, outboxEventId);

        var eventoValido = await _context.OutboxMessages
            .AnyAsync(
                x => x.Id == outboxEventId &&
                     x.EmpresaId == empresaId &&
                     x.TargetWorkflow == WorkflowNotificacao,
                cancellationToken);

        if (!eventoValido)
            return null;

        return await _context.NotificacoesWhatsAppEnvios
            .SingleOrDefaultAsync(
                x => x.EmpresaId == empresaId &&
                     x.OutboxEventId == outboxEventId,
                cancellationToken);
    }

    private static void ValidarIdentificadores(
        Guid empresaId,
        Guid outboxEventId)
    {
        if (empresaId == Guid.Empty)
            throw new ArgumentException(
                "EmpresaId inválido.",
                nameof(empresaId));

        if (outboxEventId == Guid.Empty)
            throw new ArgumentException(
                "OutboxEventId inválido.",
                nameof(outboxEventId));
    }

    private static string LimitarErro(string erro)
    {
        var texto = erro.Trim();

        return texto.Length <= TamanhoMaximoErro
            ? texto
            : texto[..TamanhoMaximoErro];
    }
}
