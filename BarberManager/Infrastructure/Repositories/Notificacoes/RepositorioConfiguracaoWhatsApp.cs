using Application.Interfaces.Notificacoes;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Notificacoes;

public sealed class RepositorioConfiguracaoWhatsApp
    : IRepositorioConfiguracaoWhatsApp
{
    private readonly BarbeariaOperacionalDbContext _context;

    public RepositorioConfiguracaoWhatsApp(
        BarbeariaOperacionalDbContext context)
    {
        _context = context;
    }

    public async Task<ConfiguracaoWhatsApp?>
        ObterAtivaPorEmpresaAsync(
            Guid empresaId,
            CancellationToken cancellationToken)
    {
        return await _context.ConfiguracoesWhatsApp
            .AsNoTracking()
            .SingleOrDefaultAsync(
                x => x.EmpresaId == empresaId &&
                     x.Ativo,
                cancellationToken);
    }

    public async Task AdicionarAsync(
    ConfiguracaoWhatsApp configuracao,
    CancellationToken cancellationToken)
    {
        await _context.ConfiguracoesWhatsApp.AddAsync(
            configuracao,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(
     ConfiguracaoWhatsApp configuracao,
     CancellationToken cancellationToken)
    {
        var entry = _context.Entry(configuracao);

        if (entry.State == EntityState.Detached)
        {
            _context.ConfiguracoesWhatsApp.Update(configuracao);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ConfiguracaoWhatsApp?> ObterPorEmpresaAsync(
       Guid empresaId,
       CancellationToken cancellationToken)
    {
        if (empresaId == Guid.Empty)
            throw new ArgumentException(
                "EmpresaId inválido.",
                nameof(empresaId));

        return await _context.ConfiguracoesWhatsApp
            .AsNoTracking()
            .SingleOrDefaultAsync(
                x => x.EmpresaId == empresaId,
                cancellationToken);
    }
}