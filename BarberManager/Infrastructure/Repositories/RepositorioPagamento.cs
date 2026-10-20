using Application.Interfaces.Empresas;
using Application.Saas.Pagamentos.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Saas;
using Infrastructure.Context;
using Microsoft.Data.SqlClient;
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
        .ThenInclude(a=>a.Assinatura)
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

        public async Task<(bool Inserida, TransacaoPagamento? Existente)> TentarAdicionarTentativaComIdempotenciaAsync(TransacaoPagamento transacao, CancellationToken cancellationToken = default)
        {
            await _dbContext.TransacoesPagamento.AddAsync(transacao, cancellationToken);

            try
            {

                await _dbContext.SaveChangesAsync(cancellationToken);

                return (true, null);

            }
            catch (DbUpdateException ex) when(
              EhViolacaoDeIndiceUnico(ex))
            {

                _dbContext.Entry(transacao).State = EntityState.Detached;

                var existe = await _dbContext.TransacoesPagamento
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                    t=>t.Provedor == transacao.Provedor &&
                    transacao.ChaveIdempotencia == transacao.ChaveIdempotencia,
                    cancellationToken
                    );

                if (existe is null)
                    throw;
                return (false, existe);
            }
        }
        private static bool EhViolacaoDeIndiceUnico(DbUpdateException exception)
        {
            return exception.GetBaseException() is SqlException sqlException
                             && sqlException.Number is 2601 or 2627;
        }

        public async Task<RegistroTentativaPagamentoResultado> RegistrarTentativaExclusivaAsync(TransacaoPagamento transacao, CancellationToken cancellationToken = default)
        {
            await using var transaction =
       await _dbContext.Database.BeginTransactionAsync(
           System.Data.IsolationLevel.ReadCommitted,
           cancellationToken);

            try
            {
                // Bloqueia a linha da fatura até o fim da transação.
                var fatura = await _dbContext.Faturas
                    .FromSqlInterpolated($@"
                SELECT *
                FROM [Faturas] WITH (UPDLOCK, HOLDLOCK)
                WHERE [Id] = {transacao.FaturaId}")
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);

                if (fatura is null)
                {
                    await transaction.RollbackAsync(cancellationToken);

                    throw new InvalidOperationException(
                        "Fatura não encontrada.");
                }

                if (fatura.Status is StatusFatura.Paga or StatusFatura.Cancelada)
                {
                    await transaction.CommitAsync(cancellationToken);

                    return new RegistroTentativaPagamentoResultado(
                        false,
                        null,
                        fatura.Status);
                }

                // A consulta acontece depois de adquirir o bloqueio.
                var existente = await _dbContext.TransacoesPagamento
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        t => t.Provedor == transacao.Provedor
                             && t.ChaveIdempotencia ==
                                transacao.ChaveIdempotencia,
                        cancellationToken);

                if (existente is not null)
                {
                    await transaction.CommitAsync(cancellationToken);

                    return new RegistroTentativaPagamentoResultado(
                        false,
                        existente,
                        fatura.Status);
                }

                // Verifica se outra chave já possui tentativa ativa.
                var tentativaAtiva = await _dbContext.TransacoesPagamento
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        t => t.FaturaId == transacao.FaturaId
                             && (
                                 t.Status == StatusTransacaoPagamento.Criada
                                 || t.Status == StatusTransacaoPagamento.Pendente
                                 || t.Status == StatusTransacaoPagamento.EmProcessamento
                             ),
                        cancellationToken);

                if (tentativaAtiva is not null)
                {
                    await transaction.CommitAsync(cancellationToken);

                    return new RegistroTentativaPagamentoResultado(
                        false,
                        tentativaAtiva,
                        fatura.Status);
                }

                // Somente uma requisição por fatura chega a este ponto
                // enquanto mantiver o bloqueio.
                await _dbContext.TransacoesPagamento.AddAsync(
                    transacao,
                    cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return new RegistroTentativaPagamentoResultado(
                    true,
                    null,
                    fatura.Status);
            }
            catch
            {
                await transaction.RollbackAsync(CancellationToken.None);
                throw;
            }
        }
    }
}
