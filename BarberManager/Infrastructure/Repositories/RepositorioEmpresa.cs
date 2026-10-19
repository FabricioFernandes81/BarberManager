using Application.Interfaces.Empresas;
using Application.Saas.Empresas.DTOs;
using Application.Saas.Empresas.Enums;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Saas;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositorioEmpresa : IRepositorioEmpresa
    {

        private readonly BarbeariaSaasDbContext _dbContext;

        public RepositorioEmpresa(BarbeariaSaasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultadoAlteracaoAcessoEmpresa> AlterarAcessoAsync(Guid empresaId, bool habilitarAcesso, string? motivo, CancellationToken cancellationToken = default)
        {
            var empresa = await _dbContext.Empresas
                 .FirstOrDefaultAsync(x => x.Id == empresaId, cancellationToken);

            if (empresa.Ativa == StatusGeral.Inativo)
            {
                return ResultadoAlteracaoAcessoEmpresa.EmpresaInativa;
            }
            if (empresa is null)
            {
                return ResultadoAlteracaoAcessoEmpresa.EmpresaNaoEncontrada;
            }
            if (habilitarAcesso)
            {
                if (empresa.Ativa == StatusGeral.Ativo && empresa.DataBloqueio is null)
                {
                    return ResultadoAlteracaoAcessoEmpresa.JaHabilitado;
                }
                empresa.Ativa = StatusGeral.Ativo;
                empresa.DataBloqueio = null;
                empresa.MotivoBloqueio = null;
            }
            else
            {
                if (empresa.Ativa == StatusGeral.Suspenso && empresa.DataBloqueio is not null)
            {
                return ResultadoAlteracaoAcessoEmpresa.JaSuspenso;
            }
            empresa.Ativa = StatusGeral.Suspenso;
            empresa.DataBloqueio = DateTime.UtcNow;
                empresa.MotivoBloqueio = motivo!.Trim();
        }
            empresa.DataAtualizacao = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return habilitarAcesso ? ResultadoAlteracaoAcessoEmpresa.Habilitado :
                ResultadoAlteracaoAcessoEmpresa.Suspenso;
        }

        public Task CriarCadastroAsync(Responsavel responsavel, Empresa empresa, Assinatura assinatura, CancellationToken cancellationToken = default)
        {
            empresa.Responsavel = responsavel;
            empresa.ResponsavelId = responsavel.Id;

            _dbContext.Responsaveis.Add(responsavel);
            _dbContext.Empresas.Add(empresa);
            _dbContext.Assinaturas.Add(assinatura);

            return Task.CompletedTask;
        }

     

        public async Task<bool> ExisteAsync(Guid empresaId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Empresas
                .AsNoTracking()
                .AnyAsync(empresa => empresa.Id == empresaId, cancellationToken);
        }

        public async Task<(List<GetListEmpresaDto> Empresas, int TotalRegistros)> ListarAsync(int pageNumber, int pageSize, string? searchTerm, string? sortBy, string? Status, CancellationToken cancellationToken = default)
        {
            var queryEmpresa = _dbContext.Empresas
                .Include(x => x.Assinaturas)
                .ThenInclude(p => p.Plano)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = $"%{searchTerm}%";
                queryEmpresa = queryEmpresa.Where(c => c.NomeFantasia.Contains(term));
            }
            if(!string.IsNullOrWhiteSpace(Status) && Status != "Todos")
            {
                if(Enum.TryParse<StatusGeral>(Status, out var statusEnum)){

                    queryEmpresa = queryEmpresa.Where(s => s.Ativa == statusEnum);

                }
            }

            var recordTotal = await queryEmpresa.CountAsync();

            var sortQuery = sortBy switch
            {


                _=> queryEmpresa.OrderBy(c=>c.Id)
            };

            var itemsQuery = await sortQuery
                .Select(e=> new GetListEmpresaDto
                {
                    Id = e.Id.ToString(),
                    CNPJ = e.Cnpj,
                    NameFantasia = e.NomeFantasia,
                    Status = e.Ativa.ToString(),
                    Plano = e.Assinaturas.OrderByDescending(a=>a.DataInicio)
                    .Select(a=>a.Plano.Nome).FirstOrDefault()?? "Sem assinatura",
                  //  e.Assinaturas.Plano.Nome,
                    StatusAssinatura = e.Assinaturas.OrderByDescending(a=>a.DataInicio)
                    .Select(a=> a.Ativa.ToString()).FirstOrDefault() ?? "Sem assinatura",
                    //e.Assinaturas.Ativa.ToString()
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
                
            return (itemsQuery, recordTotal);
        }

        public async Task<PlanoCadastroDto?> ObterPlanoAtivoAsync(Guid planoId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Planos
                .AsNoTracking()
                .Where(p => p.Id == planoId && p.Ativo == StatusGeral.Ativo)
                .Select(p => new PlanoCadastroDto(
                    p.Id,
                    p.Nome,
                    p.ValorMensal,
                    p.ValorAnual
                    )).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
