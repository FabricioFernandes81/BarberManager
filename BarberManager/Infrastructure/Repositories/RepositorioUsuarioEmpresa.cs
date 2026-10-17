using Infrastructure.Context;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Saas.Usuarios.DTOs;
using Application.Saas.Usuarios.Enums;
using Domain.Entities.AuthUsers;

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

        public async Task<GetUsuarioDetalheDto?> ObterDetalheAsync(Guid usuarioId, CancellationToken cancellationToken = default)
        {
            var usuario = await _dbContext.Users
                .AsNoTracking()
                .Where(u => u.Id == usuarioId)
                .Select(u => new
                {
                    u.Id,
                    NomeUsuario = u.UserName,
                    u.Email,
                    Telefone = u.PhoneNumber,
                    EmailConfirmado = u.EmailConfirmed
                }).SingleOrDefaultAsync(cancellationToken);

            if (usuario is null)
                return null;

            var empresas = await _dbContext.UsuarioEmpresa
                .AsNoTracking()
                .Where(v => v.UsuarioId == usuarioId)
                .OrderBy(v => v.EmpresaId)
                .Select(v => new UsuarioEmpresaDto
                {
                    EmpresaId = v.EmpresaId,
                    Ativo = v.Ativo,
                    DataVinculo = v.DataVinculo,
                    DataDesvinculo = v.DataDesvinculo
                }).ToListAsync(cancellationToken);

            return new GetUsuarioDetalheDto
            {
                Id = usuario.Id,
                NomeUsuario = usuario.NomeUsuario,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                EmailConfirmado = usuario.EmailConfirmado,
                Empresas = empresas
            };
                

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

        public async Task<ResultadoVinculoUsuarioEmpresa> VincularOuReativarAsync(Guid usuarioId, Guid empresaId, CancellationToken cancellationToken = default)
        {

            var vinculo = await _dbContext.UsuarioEmpresa
                .SingleOrDefaultAsync(
                x => x.UsuarioId == usuarioId && x.EmpresaId == empresaId, cancellationToken);

            if(vinculo is null)
            {
                var novoVinculo = new UsuarioEmpresa
                {
                    UsuarioId = usuarioId,
                    EmpresaId = empresaId,
                    Ativo = true,
                    DataVinculo = DateTime.UtcNow,
                    DataDesvinculo = null
                };
                _dbContext.UsuarioEmpresa.Add(novoVinculo);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return ResultadoVinculoUsuarioEmpresa.Criado;
            }
            if(vinculo.Ativo && vinculo.DataDesvinculo is null)
            {
                return ResultadoVinculoUsuarioEmpresa.JaAtivo;
            }

            vinculo.Ativo = true;
            vinculo.DataAtualizacao = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return ResultadoVinculoUsuarioEmpresa.Reativado;

        }
    }
    
}
