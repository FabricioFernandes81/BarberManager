
using Application.Common;
using Application.Common.Transactions;
using Application.Interfaces.Notificacoes;
using Application.Operacional.Notificacoes.DTOs;
using Domain.Common.Enums;
using Domain.Entities.Notificacoes;

namespace Application.Operacional.Notificacoes.Services;

public sealed class InteracaoWhatsAppService
    : IInteracaoWhatsAppService
{
    private readonly IRepositorioInteracaoWhatsApp _repositorio;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    private static readonly TimeSpan DuracaoInteracao =
        TimeSpan.FromMinutes(10);

    public InteracaoWhatsAppService(
        IRepositorioInteracaoWhatsApp repositorio,
        IUnitOfWorkFactory unitOfWorkFactory)
    {
        _repositorio = repositorio;
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<Guid> CriarInteracaoAsync(
        Guid empresaId,
        Guid clienteId,
        string instanceName,
        string telefone,
        TipoInteracaoWhatsApp tipoInteracao,
        string messageIdOrigem,
        IReadOnlyCollection<AgendamentoElegivelRespostaWhatsAppDto> agendamentos,
        CancellationToken cancellationToken)
    {
        ValidarDadosCriacao(
            empresaId,
            clienteId,
            instanceName,
            telefone,
            messageIdOrigem,
            agendamentos);

        var agora = DateTimeOffset.UtcNow;

        await using var unitOfWork =
            _unitOfWorkFactory.Create(TransactionContext.Operacional);

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var interacaoAtiva =
                await _repositorio.ObterInteracaoAtivaAsync(
                    empresaId,
                    instanceName,
                    telefone,
                    cancellationToken);

            if (interacaoAtiva is not null)
            {
                if (interacaoAtiva.ExpiraEmUtc > agora)
                {
                    throw new InvalidOperationException(
                        "Já existe uma interação ativa para este telefone.");
                }

                interacaoAtiva.Status =
                    StatusInteracaoWhatsApp.Expirada;

                interacaoAtiva.DataAtualizacao = agora.UtcDateTime;
            }

            var interacao = new InteracaoWhatsAppPendente
            {
                EmpresaId = empresaId,
                ClienteId = clienteId,
                InstanceName = instanceName.Trim(),
                Telefone = telefone.Trim(),
                TipoInteracao = tipoInteracao,
                Status = StatusInteracaoWhatsApp.AguardandoSelecao,
                MessageIdOrigem = messageIdOrigem.Trim(),
                CriadaEmUtc = agora,
                ExpiraEmUtc = agora.Add(DuracaoInteracao)
            };

            var ordem = 1;

            foreach (var agendamento in agendamentos
                .OrderBy(x => x.DataHoraInicio))
            {
                interacao.Opcoes.Add(
                    new OpcaoInteracaoWhatsApp
                    {
                        AgendamentoId = agendamento.AgendamentoId,
                        Ordem = ordem++,
                        DataHoraInicioSnapshot =
                            agendamento.DataHoraInicio,
                        ServicosSnapshot = agendamento.Servicos,
                        ProfissionalSnapshot = agendamento.Profissional
                    });
            }

            await _repositorio.AdicionarAsync(
                interacao,
                cancellationToken);

            await unitOfWork.CommitTransactionAsync(
                cancellationToken);

            return interacao.Id;
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

    public async Task<ResultadoInteracaoWhatsAppDto> SelecionarOpcaoAsync(
        Guid empresaId,
        string instanceName,
        string telefone,
        int ordem,
        string providerMessageId,
        CancellationToken cancellationToken)
    {
        ValidarDadosInteracao(
            empresaId,
            instanceName,
            telefone,
            providerMessageId);

        if (ordem <= 0)
        {
            throw new ArgumentException(
                "Número da opção inválido.");
        }

        await using var unitOfWork =
            _unitOfWorkFactory.Create(TransactionContext.Operacional);

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            if (await _repositorio.MensagemJaProcessadaAsync(
                empresaId,
                providerMessageId,
                cancellationToken))
            {
                await unitOfWork.RollbackTransactionAsync(
                    CancellationToken.None);

                return new ResultadoInteracaoWhatsAppDto(
                    false,
                    "MensagemDuplicada",
                    "Esta mensagem já foi processada.");
            }

            var interacao =
                await _repositorio.ObterInteracaoAtivaAsync(
                    empresaId,
                    instanceName,
                    telefone,
                    cancellationToken);

            if (interacao is null)
            {
                await unitOfWork.RollbackTransactionAsync(
                    CancellationToken.None);

                return new ResultadoInteracaoWhatsAppDto(
                    false,
                    "InteracaoNaoEncontrada",
                    "Não encontramos uma seleção pendente.");
            }

            var agora = DateTimeOffset.UtcNow;

            if (interacao.ExpiraEmUtc <= agora)
            {
                interacao.Status =
                    StatusInteracaoWhatsApp.Expirada;

                interacao.DataAtualizacao = agora.UtcDateTime;

                await unitOfWork.CommitTransactionAsync(
                    cancellationToken);

                return new ResultadoInteracaoWhatsAppDto(
                    false,
                    "InteracaoExpirada",
                    "O prazo para selecionar o agendamento expirou.");
            }

            if (interacao.Status !=
                StatusInteracaoWhatsApp.AguardandoSelecao)
            {
                await unitOfWork.RollbackTransactionAsync(
                    CancellationToken.None);

                return new ResultadoInteracaoWhatsAppDto(
                    false,
                    "SelecaoNaoPermitida",
                    "A interação não está aguardando uma seleção.");
            }

            var opcao = interacao.Opcoes
                .SingleOrDefault(x => x.Ordem == ordem);

            if (opcao is null)
            {
                await unitOfWork.RollbackTransactionAsync(
                    CancellationToken.None);

                return new ResultadoInteracaoWhatsAppDto(
                    false,
                    "OpcaoInvalida",
                    "Número inválido. Escolha uma das opções apresentadas.");
            }

            interacao.AgendamentoSelecionadoId =
                opcao.AgendamentoId;

            interacao.Status =
                StatusInteracaoWhatsApp.AguardandoConfirmacao;

            interacao.DataAtualizacao = agora.UtcDateTime;

            await _repositorio.AdicionarRespostaProcessadaAsync(
                new RespostaWhatsAppProcessada
                {
                    EmpresaId = empresaId,
                    InstanceName = instanceName.Trim(),
                    ProviderMessageId = providerMessageId.Trim(),
                    Telefone = telefone.Trim(),
                    Comando = ordem.ToString(),
                    AgendamentoId = opcao.AgendamentoId,
                    Resultado = "OpcaoSelecionada",
                    DataProcessamentoUtc = agora.UtcDateTime
                },
                cancellationToken);

            await unitOfWork.CommitTransactionAsync(
                cancellationToken);

            return new ResultadoInteracaoWhatsAppDto(
                true,
                "AguardandoConfirmacao",
                "Confirme se deseja prosseguir com este agendamento.",
                interacao.Id,
                opcao.AgendamentoId,
                opcao.Ordem,
                opcao.DataHoraInicioSnapshot,
                opcao.ServicosSnapshot,
                opcao.ProfissionalSnapshot);
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

    public async Task<ResultadoInteracaoWhatsAppDto> CancelarInteracaoAsync(
        Guid empresaId,
        string instanceName,
        string telefone,
        string providerMessageId,
        CancellationToken cancellationToken)
    {
        ValidarDadosInteracao(
            empresaId,
            instanceName,
            telefone,
            providerMessageId);

        await using var unitOfWork =
            _unitOfWorkFactory.Create(TransactionContext.Operacional);

        await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            if (await _repositorio.MensagemJaProcessadaAsync(
                empresaId,
                providerMessageId,
                cancellationToken))
            {
                await unitOfWork.RollbackTransactionAsync(
                    CancellationToken.None);

                return new ResultadoInteracaoWhatsAppDto(
                    false,
                    "MensagemDuplicada",
                    "Esta mensagem já foi processada.");
            }

            var interacao =
                await _repositorio.ObterInteracaoAtivaAsync(
                    empresaId,
                    instanceName,
                    telefone,
                    cancellationToken);

            if (interacao is null)
            {
                await unitOfWork.RollbackTransactionAsync(
                    CancellationToken.None);

                return new ResultadoInteracaoWhatsAppDto(
                    false,
                    "InteracaoNaoEncontrada",
                    "Não encontramos uma interação ativa para encerrar.");
            }

            var agora = DateTimeOffset.UtcNow;

            if (interacao.ExpiraEmUtc <= agora)
            {
                interacao.Status =
                    StatusInteracaoWhatsApp.Expirada;

                interacao.DataAtualizacao = agora.UtcDateTime;

                await unitOfWork.CommitTransactionAsync(
                    cancellationToken);

                return new ResultadoInteracaoWhatsAppDto(
                    false,
                    "InteracaoExpirada",
                    "O prazo desta interação expirou.");
            }

            interacao.Status =
                StatusInteracaoWhatsApp.Cancelada;

            interacao.ConcluidaEmUtc = agora;
            interacao.DataAtualizacao = agora.UtcDateTime;

            await _repositorio.AdicionarRespostaProcessadaAsync(
                new RespostaWhatsAppProcessada
                {
                    EmpresaId = empresaId,
                    InstanceName = instanceName.Trim(),
                    ProviderMessageId = providerMessageId.Trim(),
                    Telefone = telefone.Trim(),
                    Comando = "NAO",
                    AgendamentoId = interacao.AgendamentoSelecionadoId,
                    Resultado = "InteracaoCancelada",
                    DataProcessamentoUtc = agora.UtcDateTime
                },
                cancellationToken);

            await unitOfWork.CommitTransactionAsync(
                cancellationToken);

            return new ResultadoInteracaoWhatsAppDto(
                true,
                "InteracaoCancelada",
                "Operação cancelada. Nenhum agendamento foi alterado.",
                interacao.Id,
                interacao.AgendamentoSelecionadoId);
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

    private static void ValidarDadosCriacao(
        Guid empresaId,
        Guid clienteId,
        string instanceName,
        string telefone,
        string messageIdOrigem,
        IReadOnlyCollection<AgendamentoElegivelRespostaWhatsAppDto> agendamentos)
    {
        if (empresaId == Guid.Empty ||
            clienteId == Guid.Empty)
        {
            throw new ArgumentException(
                "Empresa e cliente são obrigatórios.");
        }

        if (string.IsNullOrWhiteSpace(instanceName) ||
            instanceName.Length > 150)
        {
            throw new ArgumentException(
                "Instância WhatsApp inválida.");
        }

        if (string.IsNullOrWhiteSpace(telefone) ||
            telefone.Length > 20)
        {
            throw new ArgumentException(
                "Telefone inválido.");
        }

        if (string.IsNullOrWhiteSpace(messageIdOrigem) ||
            messageIdOrigem.Length > 200)
        {
            throw new ArgumentException(
                "Identificador da mensagem inválido.");
        }

        if (agendamentos is null || agendamentos.Count < 2)
        {
            throw new ArgumentException(
                "A interação deve possuir pelo menos dois agendamentos.");
        }

        if (agendamentos.Any(x =>
            x.AgendamentoId == Guid.Empty ||
            string.IsNullOrWhiteSpace(x.Servicos) ||
            string.IsNullOrWhiteSpace(x.Profissional)))
        {
            throw new ArgumentException(
                "Existem opções de agendamento inválidas.");
        }

        if (agendamentos.Select(x => x.AgendamentoId).Distinct().Count()
            != agendamentos.Count)
        {
            throw new ArgumentException(
                "Existem agendamentos duplicados nas opções.");
        }
    }

    private static void ValidarDadosInteracao(
        Guid empresaId,
        string instanceName,
        string telefone,
        string providerMessageId)
    {
        if (empresaId == Guid.Empty)
        {
            throw new ArgumentException(
                "Empresa inválida.");
        }

        if (string.IsNullOrWhiteSpace(instanceName) ||
            instanceName.Length > 150)
        {
            throw new ArgumentException(
                "Instância WhatsApp inválida.");
        }

        if (string.IsNullOrWhiteSpace(telefone) ||
            telefone.Length > 20)
        {
            throw new ArgumentException(
                "Telefone inválido.");
        }

        if (string.IsNullOrWhiteSpace(providerMessageId) ||
            providerMessageId.Length > 200)
        {
            throw new ArgumentException(
                "Identificador da mensagem inválido.");
        }
    }
}
