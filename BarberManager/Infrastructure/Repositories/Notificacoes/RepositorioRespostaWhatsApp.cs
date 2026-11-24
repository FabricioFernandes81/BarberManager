
using Application.Interfaces.Agendamentos;
using Application.Interfaces.Notificacoes;
using Domain.Entities.Agendamentos;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Notificacoes;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Notificacoes;

public sealed class RepositorioRespostaWhatsApp
    : IRepositorioRespostaWhatsApp
{
    private readonly BarbeariaOperacionalDbContext _operacionalContext;
    private readonly AuthDbContext _authContext;
    private readonly IRepositorioGerenciarAgendamento _repositorioAgendamento;

    public RepositorioRespostaWhatsApp(
        BarbeariaOperacionalDbContext operacionalContext,
        AuthDbContext authContext,
        IRepositorioGerenciarAgendamento repositorioAgendamento)
    {
        _operacionalContext = operacionalContext;
        _authContext = authContext;
        _repositorioAgendamento = repositorioAgendamento;
    }

    public async Task<Guid?> ObterEmpresaPorInstanciaAsync(
        string instance,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(instance))
            return null;

        return await _operacionalContext.ConfiguracoesWhatsApp
            .AsNoTracking()
            .Where(x =>
                x.InstanceName == instance.Trim() &&
                x.Ativo)
            .Select(x => (Guid?)x.EmpresaId)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Guid>> ObterClientesPorTelefoneAsync(
        Guid empresaId,
        string telefoneNormalizado,
        CancellationToken cancellationToken)
    {
        if (empresaId == Guid.Empty ||
            string.IsNullOrWhiteSpace(telefoneNormalizado))
        {
            return new List<Guid>();
        }

        // Busca somente clientes ativos e vinculados à empresa.
        var clientesVinculados = await _operacionalContext.ClientesEmpresas
            .AsNoTracking()
            .Where(v =>
                v.EmpresaId == empresaId &&
                v.Ativo &&
                v.Cliente.Ativo)
            .Select(v => new
            {
                v.ClienteId,
                v.Cliente.UsuarioId
            })
            .ToListAsync(cancellationToken);

        if (clientesVinculados.Count == 0)
            return new List<Guid>();

        var usuariosIds = clientesVinculados
            .Select(x => x.UsuarioId)
            .Distinct()
            .ToList();

        // PhoneNumber pertence ao contexto Identity.
        var contatos = await _authContext.Users
            .AsNoTracking()
            .Where(u =>
                usuariosIds.Contains(u.Id) &&
                u.PhoneNumber != null)
            .Select(u => new
            {
                u.Id,
                u.PhoneNumber
            })
            .ToListAsync(cancellationToken);

        var usuariosCorrespondentes = contatos
            .Where(u =>
                NormalizarTelefone(u.PhoneNumber!) ==
                telefoneNormalizado)
            .Select(u => u.Id)
            .ToHashSet();

        return clientesVinculados
            .Where(c => usuariosCorrespondentes.Contains(c.UsuarioId))
            .Select(c => c.ClienteId)
            .Distinct()
            .ToList();
    }

    public async Task<List<Agendamento>> ObterAgendamentosElegiveisAsync(
        Guid empresaId,
        IReadOnlyCollection<Guid> clientesIds,
        CancellationToken cancellationToken)
    {
        if (empresaId == Guid.Empty || clientesIds.Count == 0)
            return new List<Agendamento>();

        var clientes = clientesIds.Distinct().ToList();

        var agoraUtc = DateTimeOffset.UtcNow;

        return await _operacionalContext.Agendamentos
            .AsNoTracking()
            .Include(a => a.Profissional)
            .Include(a => a.Servicos)
            .Where(a =>
                a.EmpresaId == empresaId &&
                clientes.Contains(a.ClienteId) &&
                a.DataHoraInicio >= agoraUtc &&
                (a.Status == StatusAgendamento.Pendente ||
                 a.Status == StatusAgendamento.Confirmado))
            .OrderBy(a => a.DataHoraInicio)
            .ToListAsync(cancellationToken);
    }

    public async Task<Agendamento?> ObterAgendamentoParaAtualizacaoAsync(
        Guid empresaId,
        Guid agendamentoId,
        CancellationToken cancellationToken)
    {
        return await _operacionalContext.Agendamentos
            .SingleOrDefaultAsync(
                a =>
                    a.EmpresaId == empresaId &&
                    a.Id == agendamentoId,
                cancellationToken);
    }

    public Task InvalidarLembretesPendentesAsync(
        Guid empresaId,
        Guid agendamentoId,
        string motivo,
        CancellationToken cancellationToken)
    {
        return _repositorioAgendamento.InvalidarLembretesPendentesAsync(
            empresaId,
            agendamentoId,
            motivo,
            cancellationToken);
    }

    private static string NormalizarTelefone(string telefone)
    {
        return new string(
            telefone.Where(char.IsDigit).ToArray());
    }

    public async Task<bool> MensagemJaProcessadaAsync(
    Guid empresaId,
    string providerMessageId,
    CancellationToken cancellationToken)
    {
        return await _operacionalContext.RespostasWhatsAppProcessadas
            .AsNoTracking()
            .AnyAsync(
                x => x.EmpresaId == empresaId &&
                     x.ProviderMessageId == providerMessageId,
                cancellationToken);
    }

    public async Task AdicionarRespostaProcessadaAsync(
        RespostaWhatsAppProcessada resposta,
        CancellationToken cancellationToken)
    {
        await _operacionalContext.RespostasWhatsAppProcessadas
            .AddAsync(resposta, cancellationToken);
    }
}
