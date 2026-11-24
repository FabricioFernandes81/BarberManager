
using Application.Common.Middlewares.Handler;
using Application.Interfaces.Notificacoes;
using Application.Operacional.Notificacoes.DTOs;
using Application.Operacional.Notificacoes.Commands;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Notificacoes;
using System.Globalization;
using System.Text;

namespace Application.Operacional.Notificacoes.Commands.Handlers;

public sealed class ProcessarRespostaWhatsAppHandler
    : ICommandHandler<
        ProcessarRespostaWhatsAppCommand,
        ProcessarRespostaWhatsAppResultadoDto>
{
    private readonly IRepositorioRespostaWhatsApp _repositorio;

    public ProcessarRespostaWhatsAppHandler(
        IRepositorioRespostaWhatsApp repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<ProcessarRespostaWhatsAppResultadoDto> Handle(
        ProcessarRespostaWhatsAppCommand command,
        CancellationToken cancellationToken)
    {
        ValidarRequest(command);

        var comando = NormalizarComando(command.Mensagem);

        if (comando is not ("SIM" or "NAO"))
        {
            return Resultado(
                false,
                "MensagemNaoReconhecida",
                "Responda SIM para confirmar ou NÃO para cancelar.");
        }

        var telefone = NormalizarTelefone(command.Telefone);

        var empresaId =
            await _repositorio.ObterEmpresaPorInstanciaAsync(
                command.Instance,
                cancellationToken);

        if (!empresaId.HasValue)
        {
            return Resultado(
                false,
                "InstanciaNaoConfigurada",
                "A instância WhatsApp não está configurada ou ativa.");
        }

        // Idempotência: evita processar a mesma mensagem novamente.
        if (await _repositorio.MensagemJaProcessadaAsync(
            empresaId.Value,
            command.MessageId,
            cancellationToken))
        {
            return Resultado(
                false,
                "MensagemDuplicada",
                "Esta mensagem já foi processada.");
        }

        var clientesIds =
            await _repositorio.ObterClientesPorTelefoneAsync(
                empresaId.Value,
                telefone,
                cancellationToken);

        if (clientesIds.Count == 0)
        {
            return Resultado(
                false,
                "AgendamentoNaoEncontrado",
                "Não encontramos um cliente vinculado a este telefone.");
        }

        if (clientesIds.Count > 1)
        {
            return Resultado(
                false,
                "ClienteAmbiguo",
                "Este telefone está associado a mais de um cadastro. " +
                "É necessária uma identificação adicional.");
        }

        var agendamentos =
            await _repositorio.ObterAgendamentosElegiveisAsync(
                empresaId.Value,
                clientesIds,
                cancellationToken);

        // SIM: somente agendamentos pendentes.
        // NÃO: pendentes ou confirmados.
        var elegiveis = comando == "SIM"
            ? agendamentos
                .Where(a => a.Status == StatusAgendamento.Pendente)
                .ToList()
            : agendamentos
                .Where(a =>
                    a.Status == StatusAgendamento.Pendente ||
                    a.Status == StatusAgendamento.Confirmado)
                .ToList();

        if (elegiveis.Count == 0)
        {
            return Resultado(
                false,
                "AgendamentoNaoEncontrado",
                "Não encontramos agendamento elegível para esta resposta.");
        }

        if (elegiveis.Count > 1)
        {
            /*  return Resultado(
                  false,
                  "MultiplosAgendamentos",
                  "Existe mais de um agendamento elegível. " +
                  "É necessário identificar qual deles deseja alterar.");*/
            var opcoes = elegiveis
        .Select(a => new AgendamentoElegivelRespostaWhatsAppDto(
            a.Id,
            a.DataHoraInicio,
            string.Join(", ",
                a.Servicos
                    .OrderBy(s => s.Ordem)
                    .Select(s => s.NomeServico)),
            $"{a.Profissional.Nome} {a.Profissional.Sobrenome}".Trim()))
        .ToList();

            return Resultado(
                false,
                "MultiplosAgendamentos",
                "Existe mais de um agendamento elegível. Escolha um deles.",
                agendamentos: opcoes);
        }

        var agendamentoId = elegiveis[0].Id;

        // Busca a entidade rastreada para atualização.
        var agendamento =
            await _repositorio.ObterAgendamentoParaAtualizacaoAsync(
                empresaId.Value,
                agendamentoId,
                cancellationToken);

        if (agendamento is null)
        {
            return Resultado(
                false,
                "AgendamentoNaoEncontrado",
                "O agendamento não está mais disponível.");
        }

        var agora = DateTime.UtcNow;

        if (comando == "SIM")
        {
            if (agendamento.Status != StatusAgendamento.Pendente)
            {
                return Resultado(
                    false,
                    "TransicaoInvalida",
                    "Este agendamento não pode mais ser confirmado.");
            }

            agendamento.Status = StatusAgendamento.Confirmado;
            agendamento.DataAtualizacao = agora;
        }
        else
        {
            if (agendamento.Status is not (
                StatusAgendamento.Pendente or
                StatusAgendamento.Confirmado))
            {
                return Resultado(
                    false,
                    "TransicaoInvalida",
                    "Este agendamento não pode mais ser cancelado.");
            }

            agendamento.Status = StatusAgendamento.Cancelado;
            agendamento.DataCancelamento = DateTimeOffset.UtcNow;

            agendamento.MotivoCancelamento =
                "Cancelado pelo cliente via WhatsApp.";

            agendamento.CanceladoPorUsuarioId = null;
            agendamento.DataAtualizacao = agora;

            await _repositorio.InvalidarLembretesPendentesAsync(
                empresaId.Value,
                agendamento.Id,
                "Agendamento cancelado pelo cliente via WhatsApp.",
                cancellationToken);
        }

        var respostaProcessada = new RespostaWhatsAppProcessada
        {
            EmpresaId = empresaId.Value,
            InstanceName = command.Instance.Trim(),
            ProviderMessageId = command.MessageId.Trim(),
            Telefone = telefone,
            Comando = comando,
            AgendamentoId = agendamento.Id,
            Resultado = comando == "SIM"
                ? "Confirmado"
                : "Cancelado",
            DataProcessamentoUtc = agora
        };

        await _repositorio.AdicionarRespostaProcessadaAsync(
            respostaProcessada,
            cancellationToken);

        // Não chamar SaveChangesAsync nem CommitTransactionAsync.
        // O TransactionMiddleware controla a unidade de trabalho.

        return Resultado(
            true,
            respostaProcessada.Resultado,
            comando == "SIM"
                ? "Agendamento confirmado com sucesso."
                : "Agendamento cancelado com sucesso.",
            agendamento.Id);
    }

    private static void ValidarRequest(
        ProcessarRespostaWhatsAppCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (string.IsNullOrWhiteSpace(command.Instance) ||
            command.Instance.Length > 150)
        {
            throw new ArgumentException(
                "Instância WhatsApp inválida.");
        }

        if (string.IsNullOrWhiteSpace(command.Telefone))
        {
            throw new ArgumentException(
                "O telefone é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(command.Mensagem))
        {
            throw new ArgumentException(
                "A mensagem recebida é obrigatória.");
        }

        if (string.IsNullOrWhiteSpace(command.MessageId) ||
            command.MessageId.Length > 200)
        {
            throw new ArgumentException(
                "O identificador da mensagem é inválido.");
        }
    }

    private static string NormalizarTelefone(string telefone)
    {
        var normalizado = new string(
            telefone.Where(char.IsDigit).ToArray());

        if (!normalizado.StartsWith("55") ||
            normalizado.Length is not (12 or 13))
        {
            throw new ArgumentException(
                "O telefone deve estar no formato brasileiro com DDI 55.");
        }

        return normalizado;
    }

    private static string NormalizarComando(string mensagem)
    {
        var texto = mensagem.Trim()
            .ToUpperInvariant()
            .Normalize(NormalizationForm.FormD);

        var semAcentos = new string(
            texto.Where(c =>
                CharUnicodeInfo.GetUnicodeCategory(c) !=
                UnicodeCategory.NonSpacingMark).ToArray());

        return semAcentos.Normalize(NormalizationForm.FormC);
    }

    private static ProcessarRespostaWhatsAppResultadoDto Resultado(
        bool processado,
        string status,
        string mensagem,
        Guid? agendamentoId = null,
        IReadOnlyCollection<AgendamentoElegivelRespostaWhatsAppDto>? agendamentos = null)
    {
        return new ProcessarRespostaWhatsAppResultadoDto(
            processado,
            status,
            mensagem,
            agendamentoId,
            agendamentos);
    }
}
