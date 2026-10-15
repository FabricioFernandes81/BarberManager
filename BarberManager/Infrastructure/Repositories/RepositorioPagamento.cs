using Application.Interfaces.Empresas;
using Application.Saas.Pagamentos.DTOs;
using Domain.Entities.Saas;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RepositorioPagamento : IRepositorioPagamento
    {

        private readonly BarbeariaSaasDbContext _dbContext;

        public RepositorioPagamento(BarbeariaSaasDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       

        public async Task AdicionarTentativaAsync(TransacaoPagamento transacao, CancellationToken cancellationToken = default)
        {
            await _dbContext.TransacoesPagamento.AddAsync(transacao, 
                cancellationToken);
        }

        

        public async Task<Fatura?> ObterFaturaComTentativasAsync(Guid faturaId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Faturas
                .Include(f => f.TransacoesPagamento)
                .FirstOrDefaultAsync(f => f.Id == faturaId, cancellationToken);
        }

        public async Task<Fatura?> ObterFaturaParaCobrancaAsync(Guid faturaId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Faturas
                .Include(f => f.Assinatura)
                .ThenInclude(a => a.Empresa)
                .Include(f => f.TransacoesPagamento)
                .FirstOrDefaultAsync(f => f.Id == faturaId, cancellationToken);
        }

        public async Task<TransacaoPagamento?> ObterPorChaveIdempotenciaAsync(string provedor, string chaveIdempotencia, CancellationToken cancellationToken = default)
        {
            return await _dbContext.TransacoesPagamento
                            .AsNoTracking()
                            .FirstOrDefaultAsync(
                                t => t.Provedor == provedor
                                     && t.ChaveIdempotencia == chaveIdempotencia,
                                cancellationToken);
        }

        public async Task<TransacaoPagamento?> ObterPorReferenciaGatewayAsync(string provedor, string referenciaGateway, CancellationToken cancellationToken = default)
        {
            return await _dbContext.TransacoesPagamento
        .Include(t => t.Fatura)
        .FirstOrDefaultAsync(
            t => t.Provedor == provedor &&
                 t.ReferenciaGateway == referenciaGateway,
            cancellationToken);
        }

        public async Task<List<PagamentoTentativaDto>> ObterTentativasPorFaturaAsync(Guid faturaId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.TransacoesPagamento
                .AsNoTracking()
                .Where(x => x.FaturaId == faturaId)
                .OrderBy(x => x.DataCadastro)
                .Select(x => new PagamentoTentativaDto
                {
                    Id = x.Id,

                    FaturaId = x.FaturaId,
                    Provedor = x.Provedor,
                    ReferenciaGateway = x.ReferenciaGateway,
                    Valor = x.Valor,
                    Status = x.Status.ToString(),
                    DataCadastro = x.DataCadastro,
                    DataProcessamento = x.DataProcessamento,
                    CodigoErro = x.CodigoErro,
                    MensagemErro = x.MensagemErro

                }).ToListAsync(cancellationToken);
        }

        public async Task SalvarAlteracoesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
