using Application.Interfaces;
using Domain.Entities.Common.Enuns;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositorioProfissionalAutenticacao : IRepositorioProfissionalAutenticacao
    {

        private readonly BarbeariaOperacionalDbContext _dbContext;

        public RepositorioProfissionalAutenticacao(BarbeariaOperacionalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProfissionalAutenticacaoInfo?> ObterPorUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Profissionais
                 .AsNoTracking()
                 .Where(p => p.UsuarioId == usuarioId && 
                 p.Status == StatusProfissional.Ativo &&
                 p.DataDemissao == null)
                 .Select(p => new ProfissionalAutenticacaoInfo(
                     p.Id, p.EmpresaId)
                 ).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
