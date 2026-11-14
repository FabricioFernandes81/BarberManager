using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Application.Common.Exceptions;
using Application.Interfaces.Notificacoes;
using Application.Operacional.Notificacoes.DTOs;

namespace Application.Operacional.Notificacoes.Services;

public sealed class NotificacaoWhatsAppOrquestracaoService
    : INotificacaoWhatsAppOrquestracaoService
{
    private static readonly TimeSpan LeaseDuration =
        TimeSpan.FromMinutes(5);

    private readonly IRepositorioNotificacaoWhatsAppEnvio _repositorio;

    public NotificacaoWhatsAppOrquestracaoService(
        IRepositorioNotificacaoWhatsAppEnvio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<ReservarEnvioWhatsAppResultadoDto> ReservarAsync(
        ReservarEnvioWhatsAppRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        ValidarRequisicao(request);

        var empresaId = await ObterEmpresaIdAsync(
            request.OutboxEventId,
            cancellationToken);

        var hashConteudo = CalcularHash(
            request.Telefone,
            request.Conteudo);

        var agoraUtc = DateTime.UtcNow;

        var envio = await _repositorio.TentarReservarEnvioAsync(
            empresaId,
            request.OutboxEventId,
            hashConteudo,
            agoraUtc,
            agoraUtc.Add(LeaseDuration),
            cancellationToken);

        return new ReservarEnvioWhatsAppResultadoDto(
            envio is not null,
            envio?.Id,
            envio?.Tentativas);
    }

    public async Task<bool> RegistrarAceiteAsync(
        Guid outboxEventId,
        string? providerMessageId,
        CancellationToken cancellationToken)
    {
        var empresaId = await ObterEmpresaIdAsync(
            outboxEventId,
            cancellationToken);

        return await _repositorio.RegistrarAceiteAsync(
            empresaId,
            outboxEventId,
            providerMessageId,
            DateTime.UtcNow,
            cancellationToken);
    }

    public async Task<bool> RegistrarFalhaAsync(
        Guid outboxEventId,
        string erro,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(erro))
            throw ErroValidacao("O motivo da falha deve ser informado.");

        var empresaId = await ObterEmpresaIdAsync(
            outboxEventId,
            cancellationToken);

        return await _repositorio.RegistrarFalhaAsync(
            empresaId,
            outboxEventId,
            erro,
            DateTime.UtcNow,
            cancellationToken);
    }

    public async Task<bool> RegistrarResultadoIndeterminadoAsync(
        Guid outboxEventId,
        string motivo,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(motivo))
            throw ErroValidacao(
                "O motivo do resultado indeterminado deve ser informado.");

        var empresaId = await ObterEmpresaIdAsync(
            outboxEventId,
            cancellationToken);

        return await _repositorio.RegistrarResultadoIndeterminadoAsync(
            empresaId,
            outboxEventId,
            motivo,
            DateTime.UtcNow,
            cancellationToken);
    }

    private async Task<Guid> ObterEmpresaIdAsync(
        Guid outboxEventId,
        CancellationToken cancellationToken)
    {
        if (outboxEventId == Guid.Empty)
            throw ErroValidacao("O identificador do evento é obrigatório.");

        var empresaId = await _repositorio.ObterEmpresaIdDoEventoAsync(
            outboxEventId,
            cancellationToken);

        if (!empresaId.HasValue || empresaId.Value == Guid.Empty)
        {
            throw ErroValidacao(
                "Evento não encontrado ou não pertence ao workflow de notificações WhatsApp.");
        }

        return empresaId.Value;
    }

    private static void ValidarRequisicao(
        ReservarEnvioWhatsAppRequest request)
    {
        if (request.OutboxEventId == Guid.Empty)
            throw ErroValidacao("O identificador do evento é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Telefone))
            throw ErroValidacao("O telefone é obrigatório.");

        var telefone = request.Telefone.Trim();

        // Formato internacional E.164.
        if (!Regex.IsMatch(telefone, @"^\+[1-9]\d{7,14}$"))
        {
            throw ErroValidacao(
                "O telefone deve estar normalizado no formato internacional E.164.");
        }

        if (string.IsNullOrWhiteSpace(request.Conteudo))
            throw ErroValidacao("O conteúdo da mensagem é obrigatório.");

        if (request.Conteudo.Length > 4096)
            throw ErroValidacao(
                "O conteúdo da mensagem excede o limite permitido.");
    }

    private static string CalcularHash(
        string telefone,
        string conteudo)
    {
        var telefoneNormalizado = telefone.Trim();

        var dados = Encoding.UTF8.GetBytes(
            $"{telefoneNormalizado}\n{conteudo}");

        var hash = SHA256.HashData(dados);

        return Convert.ToHexString(hash);
    }

    private static CustomValidationException ErroValidacao(
        string mensagem)
    {
        return new CustomValidationException(
            new[] { mensagem });
    }
}