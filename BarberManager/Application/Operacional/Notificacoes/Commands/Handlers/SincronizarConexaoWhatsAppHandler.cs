using Application.Common.Middlewares.Handler;
using Application.Interfaces.Notificacoes;
using Application.Operacional.Notificacoes.Commands;
using Application.Operacional.Notificacoes.DTOs;
using Domain.Entities.Operacional.Enums;

namespace Application.Operacional.Notificacoes.Commands.Handlers;

public sealed class SincronizarConexaoWhatsAppHandler
    : ICommandHandler<
        SincronizarConexaoWhatsAppCommand,
        SincronizarConexaoWhatsAppResultadoDto>
{
    private readonly IRepositorioConfiguracaoWhatsApp _repositorio;

    public SincronizarConexaoWhatsAppHandler(
        IRepositorioConfiguracaoWhatsApp repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<SincronizarConexaoWhatsAppResultadoDto> Handle(
        SincronizarConexaoWhatsAppCommand command,
        CancellationToken cancellationToken)
    {
        ValidarRequest(command);

        var instanceName = command.Instance.Trim();
        var state = command.State.Trim().ToLowerInvariant();

        var configuracao =
            await _repositorio.ObterPorInstanceNameAsync(
                instanceName,
                cancellationToken);

        if (configuracao is null)
        {
            return Resultado(
                false,
                "InstanciaNaoEncontrada",
                "A instância WhatsApp não foi encontrada.");
        }

        var status = state switch
        {
            "open" => StatusConexaoWhatsApp.Conectado,
            "connecting" => StatusConexaoWhatsApp.AguardandoConexao,
            "close" => StatusConexaoWhatsApp.Desconectado,
            _ => (StatusConexaoWhatsApp?)null
        };

        if (status is null)
        {
            return Resultado(
                false,
                "EstadoNaoReconhecido",
                $"Estado de conexão '{command.State}' não reconhecido.");
        }

        configuracao.StatusConexao = status.Value;

        if (state == "open")
            configuracao.DataConexao = DateTime.UtcNow;

        await _repositorio.AtualizarAsync(
            configuracao,
            cancellationToken);

        return Resultado(
            true,
            status.Value.ToString(),
            "Estado da conexão WhatsApp sincronizado com sucesso.");
    }

    private static void ValidarRequest(
        SincronizarConexaoWhatsAppCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (string.IsNullOrWhiteSpace(command.Instance) ||
            command.Instance.Length > 150)
        {
            throw new ArgumentException(
                "Instância WhatsApp inválida.");
        }

        if (string.IsNullOrWhiteSpace(command.State))
        {
            throw new ArgumentException(
                "Estado da conexão é obrigatório.");
        }
    }

    private static SincronizarConexaoWhatsAppResultadoDto Resultado(
        bool processado,
        string status,
        string mensagem)
    {
        return new SincronizarConexaoWhatsAppResultadoDto(
            processado,
            status,
            mensagem);
    }
}