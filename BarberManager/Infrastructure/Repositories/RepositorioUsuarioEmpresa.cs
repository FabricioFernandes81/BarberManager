using Infrastructure.Context;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositorioUsuarioEmpresa : IRepositorioUsuarioEmpresa
    {
        private readonly AuthDbContext _dbContext;

        public RepositorioUsuarioEmpresa(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<string>> ObterCodigosAsync(Guid usuarioId, CancellationToken cancellationToken = default)
        {
            var codigos = await(
              from usuarioRole in _dbContext.UserRoles.AsNoTracking()
              join role in _dbContext.Roles.AsNoTracking()
                  on usuarioRole.RoleId equals role.Id
              join roleGrupo in _dbContext.RolePermissaos.AsNoTracking()
                  on role.Id equals roleGrupo.RoleId
              join grupoPermissao in _dbContext.GrupoPermissaoPermissaos
                  .AsNoTracking()
                  on roleGrupo.PermissaoGupoId
                  equals grupoPermissao.PermissaoGrupoId
              join permissao in _dbContext.Permissoes.AsNoTracking()
                  on grupoPermissao.PermissaoId equals permissao.Id
              where usuarioRole.UserId == usuarioId
                    && role.Ativo
              select permissao.Codigo
          )
          .Distinct()
          .ToListAsync(cancellationToken);

            return codigos;
        }

        public async Task<IReadOnlyList<Guid>> ObterEmpresasAtivasAsync(Guid usuarioId, CancellationToken cancellationToken = default)
        {
           return await  _dbContext.UsuarioEmpresa
                .AsNoTracking()
                .Where(x=>x.UsuarioId == usuarioId && x.Ativo && x.DataDesvinculo== null)
                .Select(x=>x.EmpresaId)
                .Distinct()
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> PossuiVinculoAtivoAsync(Guid usuarioId, Guid empresaId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.UsuarioEmpresa
        .AsNoTracking()
        .AnyAsync(
            vinculo =>
                vinculo.UsuarioId == usuarioId &&
                vinculo.EmpresaId == empresaId &&
                vinculo.Ativo &&
                vinculo.DataDesvinculo == null,
            cancellationToken);
        }
    }
    
}
