using System.Text.RegularExpressions;
using Application.Common.Exceptions;
using Application.Interfaces.Notificacoes;
using Application.Operacional.Notificacoes.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Operacional.Enums;
using System.Security.Cryptography;
using System.Text;
namespace Application.Operacional.Notificacoes.Services;

public sealed class EnvioWhatsAppService : IEnvioWhatsAppService
{
    private readonly IRepositorioConfiguracaoWhatsApp _repositorioConfiguracao;
    private readonly IRepositorioNotificacaoWhatsAppEnvio _repositorioEnvio;
    private readonly IProtecaoCredenciaisWhatsApp _protecao;
    private readonly IEvolutionApiClient _evolutionApiClient;

    public EnvioWhatsAppService(
        IRepositorioConfiguracaoWhatsApp repositorioConfiguracao,
        IRepositorioNotificacaoWhatsAppEnvio repositorioEnvio,
        IProtecaoCredenciaisWhatsApp protecao,
        IEvolutionApiClient evolutionApiClient)
    {
        _repositorioConfiguracao = repositorioConfiguracao;
        _repositorioEnvio = repositorioEnvio;
        _protecao = protecao;
        _evolutionApiClient = evolutionApiClient;
    }

    public async Task<EnviarMensagemWhatsAppResultadoDto> EnviarAsync(
        EnviarMensagemWhatsAppRequest request,
        CancellationToken cancellationToken)
    {
        ValidarRequisicao(request);

        // A empresa é determinada pelo evento persistido na Outbox.
        var empresaId = await _repositorioEnvio
            .ObterEmpresaIdDoEventoAsync(
                request.OutboxEventId,
                cancellationToken);

        if (!empresaId.HasValue || empresaId.Value == Guid.Empty)
        {
            throw ErroValidacao(
                "Evento não encontrado ou não pertence à empresa.");
        }

        // Só permite o envio se o evento estiver reservado.
        var envio = await _repositorioEnvio.ObterPorEventoAsync(
            empresaId.Value,
            request.OutboxEventId,
            cancellationToken);

        if (envio is null ||
            envio.Status != StatusEnvioWhatsApp.Processando)
        {
            throw ErroValidacao(
                "O envio não possui uma reserva válida para processamento.");
        }


        // Confirma que telefone e conteúdo são exatamente os reservados.
        var telefoneNormalizado = request.Telefone.Trim();

        var dadosHash = Encoding.UTF8.GetBytes(
            $"{telefoneNormalizado}\n{request.Conteudo}");

        var hashAtual = Convert.ToHexString(
            SHA256.HashData(dadosHash));

        if (!string.Equals(
                envio.HashConteudo,
                hashAtual,
                StringComparison.OrdinalIgnoreCase))
        {
            throw ErroValidacao(
                "O conteúdo da notificação foi alterado após a reserva.");
        }
        var agoraUtc = DateTime.UtcNow;

        if (!envio.LeaseExpiraEmUtc.HasValue ||
            envio.LeaseExpiraEmUtc.Value <= agoraUtc)
        {
            throw ErroValidacao(
                "A reserva do envio expirou. É necessário verificar o estado da notificação.");
        }
        // Recupera a configuração específica da empresa.
        var configuracao = await _repositorioConfiguracao
            .ObterPorEmpresaAsync(
                empresaId.Value,
                cancellationToken);

        if (configuracao is null)
        {
            throw ErroValidacao(
                "A empresa não possui uma configuração de WhatsApp.");
        }

        if (!configuracao.Ativo)
        {
            throw ErroValidacao(
                "A integração WhatsApp está desativada para esta empresa.");
        }

        if (configuracao.StatusConexao != StatusConexaoWhatsApp.Conectado)
        {
            throw ErroValidacao(
                "A instância WhatsApp da empresa não está conectada.");
        }

        if (string.IsNullOrWhiteSpace(configuracao.InstanceName) ||
            string.IsNullOrWhiteSpace(configuracao.BaseUrl) ||
            string.IsNullOrWhiteSpace(configuracao.ApiKeyProtegida))
        {
            throw ErroValidacao(
                "A configuração da instância WhatsApp está incompleta.");
        }

        // A credencial é descriptografada somente em memória.
        var apiKey = _protecao.Desproteger(
            configuracao.ApiKeyProtegida);

        var resultado = await _evolutionApiClient.EnviarTextoAsync(
            configuracao.BaseUrl,
            configuracao.InstanceName,
            apiKey,
            NormalizarTelefone(request.Telefone),
            request.Conteudo,
            cancellationToken);

        return new EnviarMensagemWhatsAppResultadoDto(
            resultado.MessageId,
            resultado.Status);
    }

    private static void ValidarRequisicao(
        EnviarMensagemWhatsAppRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.OutboxEventId == Guid.Empty)
        {
            throw ErroValidacao(
                "O identificador do evento é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(request.Telefone))
        {
            throw ErroValidacao(
                "O telefone do destinatário é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(request.Conteudo))
        {
            throw ErroValidacao(
                "O conteúdo da mensagem é obrigatório.");
        }

        if (request.Conteudo.Length > 4096)
        {
            throw ErroValidacao(
                "O conteúdo da mensagem excede o limite permitido.");
        }
    }

    private static string NormalizarTelefone(string telefone)
    {
        var normalizado = telefone.Trim();

        if (!Regex.IsMatch(normalizado, @"^\+[1-9]\d{7,14}$"))
        {
            throw ErroValidacao(
                "O telefone deve estar no formato internacional E.164.");
        }

        // A Evolution recebe o número sem o sinal de "+".
        return normalizado[1..];
    }

    private static CustomValidationException ErroValidacao(
        string mensagem)
    {
        return new CustomValidationException(
            new[] { mensagem });
    }
}