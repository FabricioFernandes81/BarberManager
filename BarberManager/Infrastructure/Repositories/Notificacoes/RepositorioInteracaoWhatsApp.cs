using Application.Interfaces.Notificacoes;
using Domain.Common.Enums;
using Domain.Entities.Notificacoes;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Notificacoes;

public sealed class RepositorioInteracaoWhatsApp
    : IRepositorioInteracaoWhatsApp
{
    private readonly BarbeariaOperacionalDbContext _context;

    public RepositorioInteracaoWhatsApp(
        BarbeariaOperacionalDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(
        InteracaoWhatsAppPendente interacao,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(interacao);

        await _context.InteracoesWhatsAppPendentes.AddAsync(
            interacao,
            cancellationToken);
    }

    public async Task<InteracaoWhatsAppPendente?>
        ObterInteracaoAtivaAsync(
            Guid empresaId,
            string instanceName,
            string telefone,
            CancellationToken cancellationToken)
    {
        if (empresaId == Guid.Empty ||
            string.IsNullOrWhiteSpace(instanceName) ||
            string.IsNullOrWhiteSpace(telefone))
        {
            return null;
        }

        return await _context.InteracoesWhatsAppPendentes
            .Include(x => x.Opcoes)
            .Where(x =>
                x.EmpresaId == empresaId &&
                x.InstanceName == instanceName.Trim() &&
                x.Telefone == telefone.Trim() &&
                (x.Status == StatusInteracaoWhatsApp.AguardandoSelecao ||
                 x.Status == StatusInteracaoWhatsApp.AguardandoConfirmacao))
            .OrderByDescending(x => x.CriadaEmUtc)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<InteracaoWhatsAppPendente?>
        ObterInteracaoParaAtualizacaoAsync(
            Guid empresaId,
            string instanceName,
            string telefone,
            Guid interacaoId,
            CancellationToken cancellationToken)
    {
        if (empresaId == Guid.Empty ||
            interacaoId == Guid.Empty ||
            string.IsNullOrWhiteSpace(instanceName) ||
            string.IsNullOrWhiteSpace(telefone))
        {
            return null;
        }

        return await _context.InteracoesWhatsAppPendentes
            .Include(x => x.Opcoes)
            .SingleOrDefaultAsync(
                x =>
                    x.Id == interacaoId &&
                    x.EmpresaId == empresaId &&
                    x.InstanceName == instanceName.Trim() &&
                    x.Telefone == telefone.Trim() &&
                    (x.Status == StatusInteracaoWhatsApp.AguardandoSelecao ||
                     x.Status == StatusInteracaoWhatsApp.AguardandoConfirmacao),
                cancellationToken);
    }

    public async Task<bool> MensagemJaProcessadaAsync(
    Guid empresaId,
    string providerMessageId,
    CancellationToken cancellationToken)
    {
        if (empresaId == Guid.Empty ||
            string.IsNullOrWhiteSpace(providerMessageId))
        {
            return false;
        }

        return await _context.RespostasWhatsAppProcessadas
            .AnyAsync(
                x => x.EmpresaId == empresaId &&
                     x.ProviderMessageId == providerMessageId.Trim(),
                cancellationToken);
    }

    public async Task AdicionarRespostaProcessadaAsync(
        RespostaWhatsAppProcessada resposta,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(resposta);

        await _context.RespostasWhatsAppProcessadas.AddAsync(
            resposta,
            cancellationToken);
    }
}