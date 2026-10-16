using Application.Saas.Pagamentos.DTOs;
using Domain.Entities.Saas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Empresas
{
    public interface IRepositorioPagamento
    {
        Task<List<PagamentoTentativaDto>> ObterTentativasPorFaturaAsync(
        Guid faturaId,
        CancellationToken cancellationToken = default);

        Task<Fatura?> ObterFaturaComTentativasAsync(
        Guid faturaId,
        CancellationToken cancellationToken = default);

        Task<TransacaoPagamento?> ObterPorChaveIdempotenciaAsync(
            string provedor,
            string chaveIdempotencia,
            CancellationToken cancellationToken = default);

        Task AdicionarTentativaAsync(
            TransacaoPagamento transacao,
            CancellationToken cancellationToken = default);


        Task<Fatura?> ObterFaturaParaCobrancaAsync(
             Guid faturaId,
             CancellationToken cancellationToken = default);

        Task<TransacaoPagamento?> ObterPorReferenciaGatewayAsync(
    string provedor,
    string referenciaGateway,
    CancellationToken cancellationToken = default);

        /*
        Task<Fatura?> ObterFaturaParaConsultaStatusAsync(
        Guid faturaId,
        CancellationToken cancellationToken = default);*/


        Task SalvarAlteracoesAsync(
    CancellationToken cancellationToken = default);
    }
}
