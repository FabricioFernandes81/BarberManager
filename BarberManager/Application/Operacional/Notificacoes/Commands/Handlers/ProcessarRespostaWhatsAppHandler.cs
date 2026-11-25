
using Application.Common.Middlewares.Handler;
using Application.Interfaces.Financeiro;
using Application.Interfaces.Notificacoes;
using Application.Operacional.Notificacoes.Commands;
using Application.Operacional.Notificacoes.DTOs;
using Application.Operacional.Notificacoes.Events;
using Domain.Common.Enums;
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
    private readonly IRepositorioInteracaoWhatsApp _repositorioInteracao;
    private readonly IInteracaoWhatsAppService _interacaoWhatsAppService;
    private readonly IOutboxOperacionalWriter _outboxWriter;

    public ProcessarRespostaWhatsAppHandler(
     IRepositorioRespostaWhatsApp repositorio,
     IRepositorioInteracaoWhatsApp repositorioInteracao,
     IInteracaoWhatsAppService interacaoWhatsAppService,
     IOutboxOperacionalWriter outboxWriter)
    {
        _repositorio = repositorio;
        _repositorioInteracao = repositorioInteracao;
        _interacaoWhatsAppService = interacaoWhatsAppService;
        _outboxWriter = outboxWriter;
    }

    public async Task<ProcessarRespostaWhatsAppResultadoDto> Handle(
        ProcessarRespostaWhatsAppCommand command,
        CancellationToken cancellationToken)
    {
        ValidarRequest(command);

        var mensagem = NormalizarComando(command.Mensagem);
        var telefone = NormalizarTelefone(command.Telefone);
        var instance = command.Instance.Trim();
        var messageId = command.MessageId.Trim();

        var empresaId =
            await _repositorio.ObterEmpresaPorInstanciaAsync(
                instance,
                cancellationToken);

        if (!empresaId.HasValue)
        {
            return Resultado(
                false,
                "InstanciaNaoConfigurada",
                "A instância WhatsApp não está configurada ou ativa.");
        }

        // Impede o reprocessamento de mensagens já recebidas.
        if (await _repositorio.MensagemJaProcessadaAsync(
            empresaId.Value,
            messageId,
            cancellationToken))
        {
            return Resultado(
                false,
                "MensagemDuplicada",
                "Esta mensagem já foi processada.");
        }

        // Primeiro verifica se existe uma interação aguardando resposta.
        var interacao =
            await _repositorioInteracao.ObterInteracaoAtivaAsync(
                empresaId.Value,
                instance,
                telefone,
                cancellationToken);

        if (interacao is not null)
        {
            return await ProcessarInteracaoAtivaAsync(
                interacao,
                empresaId.Value,
                instance,
                telefone,
                mensagem,
                messageId,
                cancellationToken);
        }

        // Sem interação ativa, somente SIM ou NAO iniciam o fluxo.
        if (mensagem is not ("SIM" or "NAO"))
        {
            return Resultado(
                false,
                "MensagemNaoReconhecida",
                "Não existe uma seleção pendente. Responda SIM para confirmar ou NÃO para cancelar.");
        }

        return await ProcessarNovaRespostaAsync(
            empresaId.Value,
            instance,
            telefone,
            mensagem,
            messageId,
            cancellationToken);
    }

    private async Task<ProcessarRespostaWhatsAppResultadoDto>
        ProcessarInteracaoAtivaAsync(
            InteracaoWhatsAppPendente interacao,
            Guid empresaId,
            string instance,
            string telefone,
            string mensagem,
            string messageId,
            CancellationToken cancellationToken)
    {
        if (interacao.Status == StatusInteracaoWhatsApp.AguardandoSelecao)
        {
            if (!int.TryParse(
                mensagem,
                NumberStyles.None,
                CultureInfo.InvariantCulture,
                out var ordem) ||
                ordem <= 0)
            {
                return Resultado(
                    false,
                    "AguardandoSelecao",
                    "Escolha uma das opções enviadas respondendo apenas com o número correspondente.");
            }

            var resultado =
     await _interacaoWhatsAppService.SelecionarOpcaoAsync(
         empresaId,
         instance,
         telefone,
         ordem,
         messageId,
         cancellationToken);

            if (resultado.Processado)
            {
                var conteudo = MontarMensagemSolicitarConfirmacao(resultado);

                var evento = new MensagemInteracaoWhatsAppEvent(
                    EventId: Guid.NewGuid(),
                    EmpresaId: empresaId,
                    InteracaoId: resultado.InteracaoId,
                    AgendamentoId: resultado.AgendamentoId,
                    Telefone: telefone,
                    Conteudo: conteudo,
                    TipoMensagem: "SolicitarConfirmacao",
                    ProviderMessageIdOrigem: messageId);

                await _outboxWriter.AdicionarAsync(
                    empresaId,
                    evento.EventId,
                    "MensagemInteracaoWhatsApp",
                    "NotificacaoWhatsApp",
                    evento,
                    cancellationToken);
            }

            return Resultado(
                resultado.Processado,
                resultado.Status,
                resultado.Mensagem,
                resultado.AgendamentoId);
        }

        if (interacao.Status == StatusInteracaoWhatsApp.AguardandoConfirmacao)
        {
            if (mensagem == "SIM")
            {
                var resultado =
                    await _interacaoWhatsAppService.ConcluirInteracaoAsync(
                        empresaId,
                        instance,
                        telefone,
                        messageId,
                        cancellationToken);

                if (resultado.Processado)
                {
                    await RegistrarMensagemSaidaAsync(
                        empresaId,
                        resultado.InteracaoId,
                        resultado.AgendamentoId,
                        telefone,
                        resultado.Mensagem,
                        "AgendamentoConfirmado",
                        messageId,
                        cancellationToken);
                }

                return Resultado(
                    resultado.Processado,
                    resultado.Status,
                    resultado.Mensagem,
                    resultado.AgendamentoId);
            }

            if (mensagem == "NAO")
            {
                var resultado =
                    await _interacaoWhatsAppService.CancelarInteracaoAsync(
                        empresaId,
                        instance,
                        telefone,
                        messageId,
                        cancellationToken);

                if (resultado.Processado)
                {
                    await RegistrarMensagemSaidaAsync(
                        empresaId,
                        resultado.InteracaoId,
                        resultado.AgendamentoId,
                        telefone,
                        resultado.Mensagem,
                        "InteracaoCancelada",
                        messageId,
                        cancellationToken);
                }

                return Resultado(
                    resultado.Processado,
                    resultado.Status,
                    resultado.Mensagem,
                    resultado.AgendamentoId);
            }

            return Resultado(
                false,
                "AguardandoConfirmacao",
                "Responda SIM para confirmar a opção selecionada ou NÃO para cancelar a operação.");
        }

        return Resultado(
            false,
            "InteracaoIndisponivel",
            "Não foi possível continuar esta interação. Inicie uma nova solicitação.");
    }

    private static string MontarMensagemSolicitarConfirmacao(
    ResultadoInteracaoWhatsAppDto resultado)
    {
        var mensagem = new StringBuilder();

        mensagem.AppendLine("Confira o agendamento selecionado:");
        mensagem.AppendLine();

        if (resultado.DataHoraInicio.HasValue)
        {
            mensagem.AppendLine(
                $"Data: {resultado.DataHoraInicio.Value.ToString(
                    "dd/MM/yyyy 'às' HH:mm",
                    CultureInfo.GetCultureInfo("pt-BR"))}");
        }

        if (!string.IsNullOrWhiteSpace(resultado.Servicos))
            mensagem.AppendLine($"Serviços: {resultado.Servicos}");

        if (!string.IsNullOrWhiteSpace(resultado.Profissional))
            mensagem.AppendLine($"Profissional: {resultado.Profissional}");

        mensagem.AppendLine();
        mensagem.AppendLine("Deseja prosseguir com esta operação?");
        mensagem.AppendLine();
        mensagem.AppendLine("Responda:");
        mensagem.AppendLine("SIM - para confirmar.");
        mensagem.AppendLine("NÃO - para cancelar a operação.");

        return mensagem.ToString();
    }
    private async Task<ProcessarRespostaWhatsAppResultadoDto>
        ProcessarNovaRespostaAsync(
            Guid empresaId,
            string instance,
            string telefone,
            string comando,
            string messageId,
            CancellationToken cancellationToken)
    {
        var clientesIds =
            await _repositorio.ObterClientesPorTelefoneAsync(
                empresaId,
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
                "Este telefone está associado a mais de um cadastro. É necessária uma identificação adicional.");
        }

        var agendamentos =
            await _repositorio.ObterAgendamentosElegiveisAsync(
                empresaId,
                clientesIds,
                cancellationToken);

        // SIM: somente agendamentos pendentes.
        // NAO: agendamentos pendentes ou confirmados.
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

            var tipoInteracao = comando == "SIM"
                ? TipoInteracaoWhatsApp.ConfirmarAgendamento
                : TipoInteracaoWhatsApp.CancelarAgendamento;

            var interacaoId =
     await _interacaoWhatsAppService.CriarInteracaoAsync(
         empresaId,
         clientesIds[0],
         instance,
         telefone,
         tipoInteracao,
         messageId,
         opcoes,
         cancellationToken);

            // Monta a mensagem com as opções disponíveis.
            var conteudo = MontarMensagemListaOpcoes(opcoes);

            // Cada mensagem possui seu próprio identificador.
            var evento = new MensagemInteracaoWhatsAppEvent(
                EventId: Guid.NewGuid(),
                EmpresaId: empresaId,
                InteracaoId: interacaoId,
                AgendamentoId: null,
                Telefone: telefone,
                Conteudo: conteudo,
                TipoMensagem: "ListaOpcoes",
                ProviderMessageIdOrigem: messageId);

            // Registra a mensagem na Outbox.
            await _outboxWriter.AdicionarAsync(
                empresaId,
                evento.EventId,
                "MensagemInteracaoWhatsApp",
                "NotificacaoWhatsApp",
                evento,
                cancellationToken);

            // Registra a mensagem que originou a interação.
            // Isso impede que uma repetição do webhook crie outra interação.
            await _repositorio.AdicionarRespostaProcessadaAsync(
                new RespostaWhatsAppProcessada
                {
                    EmpresaId = empresaId,
                    InstanceName = instance,
                    ProviderMessageId = messageId,
                    Telefone = telefone,
                    Comando = comando,
                    AgendamentoId = null,
                    Resultado = "InteracaoCriada",
                    DataProcessamentoUtc = DateTime.UtcNow
                },
                cancellationToken);

            return Resultado(
                false,
                "MultiplosAgendamentos",
                "Existe mais de um agendamento elegível. Escolha uma das opções enviadas.",
                agendamentos: opcoes);
        }

        // Mantém o fluxo anterior quando existe apenas um agendamento.
        var agendamentoId = elegiveis[0].Id;

        var agendamento =
            await _repositorio.ObterAgendamentoParaAtualizacaoAsync(
                empresaId,
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
                empresaId,
                agendamento.Id,
                "Agendamento cancelado pelo cliente via WhatsApp.",
                cancellationToken);
        }

        await _repositorio.AdicionarRespostaProcessadaAsync(
            new RespostaWhatsAppProcessada
            {
                EmpresaId = empresaId,
                InstanceName = instance,
                ProviderMessageId = messageId,
                Telefone = telefone,
                Comando = comando,
                AgendamentoId = agendamento.Id,
                Resultado = comando == "SIM"
                    ? "Confirmado"
                    : "Cancelado",
                DataProcessamentoUtc = agora
            },
            cancellationToken);

        await RegistrarMensagemSaidaAsync(
                empresaId,
                interacaoId: null,
                agendamentoId: agendamento.Id,
                telefone: telefone,
                conteudo: comando == "SIM"
                    ? "Agendamento confirmado com sucesso."
                    : "Agendamento cancelado com sucesso.",
                tipoMensagem: comando == "SIM"
                    ? "AgendamentoConfirmado"
                    : "AgendamentoCancelado",
                providerMessageIdOrigem: messageId,
                cancellationToken: cancellationToken);


        // O TransactionMiddleware controla SaveChanges e Commit.
        return Resultado(
            true,
            comando == "SIM" ? "Confirmado" : "Cancelado",
            comando == "SIM"
                ? "Agendamento confirmado com sucesso."
                : "Agendamento cancelado com sucesso.",
            agendamento.Id);
    }
    private static string MontarMensagemListaOpcoes(
     IReadOnlyCollection<AgendamentoElegivelRespostaWhatsAppDto> opcoes)
    {
        var mensagem = new StringBuilder();

        mensagem.AppendLine(
            "Identificamos mais de um agendamento associado ao seu telefone.");

        mensagem.AppendLine();
        mensagem.AppendLine("Escolha uma das opções abaixo:");

        var ordem = 1;

        foreach (var opcao in opcoes.OrderBy(x => x.DataHoraInicio))
        {
            mensagem.AppendLine();

            mensagem.AppendLine(
                $"{ordem++}. {opcao.DataHoraInicio.ToString(
                    "dd/MM/yyyy 'às' HH:mm",
                    CultureInfo.GetCultureInfo("pt-BR"))}");

            mensagem.AppendLine($"Serviços: {opcao.Servicos}");
            mensagem.AppendLine($"Profissional: {opcao.Profissional}");
        }

        mensagem.AppendLine();
        mensagem.AppendLine(
            "Responda com o número correspondente ao agendamento desejado.");

        mensagem.AppendLine();
        mensagem.AppendLine(
            "Após selecionar, você deverá confirmar a operação.");

        return mensagem.ToString();
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

    private async Task RegistrarMensagemSaidaAsync(
    Guid empresaId,
    Guid? interacaoId,
    Guid? agendamentoId,
    string telefone,
    string conteudo,
    string tipoMensagem,
    string providerMessageIdOrigem,
    CancellationToken cancellationToken)
    {
        var evento = new MensagemInteracaoWhatsAppEvent(
            EventId: Guid.NewGuid(),
            EmpresaId: empresaId,
            InteracaoId: interacaoId,
            AgendamentoId: agendamentoId,
            Telefone: telefone,
            Conteudo: conteudo,
            TipoMensagem: tipoMensagem,
            ProviderMessageIdOrigem: providerMessageIdOrigem);

        await _outboxWriter.AdicionarAsync(
            empresaId,
            evento.EventId,
            "MensagemInteracaoWhatsApp",
            "NotificacaoWhatsApp",
            evento,
            cancellationToken);
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
        IReadOnlyCollection<AgendamentoElegivelRespostaWhatsAppDto>?
            agendamentos = null)
    {
        return new ProcessarRespostaWhatsAppResultadoDto(
            processado,
            status,
            mensagem,
            agendamentoId,
            agendamentos);
    }
}
