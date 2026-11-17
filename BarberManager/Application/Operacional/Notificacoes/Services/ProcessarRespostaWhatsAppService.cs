
using Application.Common;
using Application.Common.Transactions;
using Application.Interfaces.Notificacoes;
using Application.Operacional.Notificacoes.DTOs;
using Domain.Entities.Notificacoes;
using Domain.Entities.Common.Enuns;
using System.Globalization;
using System.Text;

namespace Application.Operacional.Notificacoes.Services;

public sealed class ProcessarRespostaWhatsAppService
    : IProcessarRespostaWhatsAppService
{
    private readonly IRepositorioRespostaWhatsApp _repositorio;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public ProcessarRespostaWhatsAppService(
        IRepositorioRespostaWhatsApp repositorio,
        IUnitOfWorkFactory unitOfWorkFactory)
    {
        _repositorio = repositorio;
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<ProcessarRespostaWhatsAppResultadoDto> ProcessarAsync(
        ProcessarRespostaWhatsAppRequest request,
        CancellationToken cancellationToken)
    {
        ValidarRequest(request);

        var comando = NormalizarComando(request.Mensagem);

        if (comando is not ("SIM" or "NAO"))
        {
            return Resultado(
                false,
                "MensagemNaoReconhecida",
                "Responda SIM para confirmar ou NÃO para cancelar.");
        }

        var telefone = NormalizarTelefone(request.Telefone);

        var empresaId =
            await _repositorio.ObterEmpresaPorInstanciaAsync(
                request.Instance,
                cancellationToken);

        if (!empresaId.HasValue)
        {
            return Resultado(
                false,
                "InstanciaNaoConfigurada",
                "A instância WhatsApp não está configurada ou ativa.");
        }

        // Verificação antecipada para evitar consultas desnecessárias.
        if (await _repositorio.MensagemJaProcessadaAsync(
            empresaId.Value,
            request.MessageId,
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

        // SIM só pode confirmar pendentes.
        // NÃO pode cancelar pendentes ou confirmados.
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
            return Resultado(
                false,
                "MultiplosAgendamentos",
                "Existe mais de um agendamento elegível. " +
                "É necessário identificar qual deles deseja alterar.");
        }

        var agendamentoId = elegiveis[0].Id;

        await using var unitOfWork =
            _unitOfWorkFactory.Create(TransactionContext.Operacional);

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            // Revalidação dentro da transação.
            if (await _repositorio.MensagemJaProcessadaAsync(
                empresaId.Value,
                request.MessageId,
                cancellationToken))
            {
                await unitOfWork.RollbackTransactionAsync(
                    CancellationToken.None);

                return Resultado(
                    false,
                    "MensagemDuplicada",
                    "Esta mensagem já foi processada.");
            }

            var agendamento =
                await _repositorio.ObterAgendamentoParaAtualizacaoAsync(
                    empresaId.Value,
                    agendamentoId,
                    cancellationToken);

            if (agendamento is null)
            {
                await unitOfWork.RollbackTransactionAsync(
                    CancellationToken.None);

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
                    await unitOfWork.RollbackTransactionAsync(
                        CancellationToken.None);

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
                    await unitOfWork.RollbackTransactionAsync(
                        CancellationToken.None);

                    return Resultado(
                        false,
                        "TransicaoInvalida",
                        "Este agendamento não pode mais ser cancelado.");
                }

                agendamento.Status = StatusAgendamento.Cancelado;
                agendamento.DataCancelamento = DateTimeOffset.UtcNow;
                agendamento.MotivoCancelamento =
                    "Cancelado pelo cliente via WhatsApp.";

                // Cancelamento originado pelo próprio cliente,
                // sem usuário administrativo autenticado.
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
                InstanceName = request.Instance.Trim(),
                ProviderMessageId = request.MessageId.Trim(),
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

            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return Resultado(
                true,
                respostaProcessada.Resultado,
                comando == "SIM"
                    ? "Agendamento confirmado com sucesso."
                    : "Agendamento cancelado com sucesso.",
                agendamento.Id);
        }
        catch
        {
            try
            {
                await unitOfWork.RollbackTransactionAsync(
                    CancellationToken.None);
            }
            catch
            {
                // Preserva a exceção original.
            }

            throw;
        }
    }

    private static void ValidarRequest(
        ProcessarRespostaWhatsAppRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Instance) ||
            request.Instance.Length > 150)
        {
            throw new ArgumentException(
                "Instância WhatsApp inválida.");
        }

        if (string.IsNullOrWhiteSpace(request.Telefone))
        {
            throw new ArgumentException(
                "O telefone é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(request.Mensagem))
        {
            throw new ArgumentException(
                "A mensagem recebida é obrigatória.");
        }

        if (string.IsNullOrWhiteSpace(request.MessageId) ||
            request.MessageId.Length > 200)
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
        var texto = mensagem.Trim().ToUpperInvariant()
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
        Guid? agendamentoId = null)
    {
        return new ProcessarRespostaWhatsAppResultadoDto(
            processado,
            status,
            mensagem,
            agendamentoId);
    }
}
