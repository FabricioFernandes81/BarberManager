using Application.Operacional.Financeiro.DTOs;
using Domain.Entities.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Financeiro
{
    public interface IRepositorioCaixa
    {
        Task<bool> ExisteCaixaAbertoAsync(
            Guid empresaId,
            Guid usuarioId,
            CancellationToken cancellationToken);

        Task<Caixa?> ObterCaixaAbertoParaAtualizacaoAsync(
            Guid empresaId,
            Guid usuarioId,
            CancellationToken cancellationToken);

        Task<Caixa?> ObterCaixaAbertoComMovimentosAsync(
            Guid empresaId,
            Guid usuarioId,
            CancellationToken cancellationToken);

        Task AdicionarAsync(
            Caixa caixa,
            CancellationToken cancellationToken);

        Task<ListarMovimentosCaixaDto> ListarMovimentosAsync(
            Guid empresaId,
            Guid usuarioId,
            DateTime? dataInicio,
            DateTime? dataFim,
            Domain.Entities.Common.Enuns.TipoMovimentoCaixa? tipoMovimento,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken);

        Task<Caixa?> ObterCaixaAbertoComMovimentosParaAtualizacaoAsync(
    Guid empresaId,
    Guid usuarioId,
    CancellationToken cancellationToken);

        Task BloquearOperacaoCaixaAsync(
            Guid empresaId,
            Guid usuarioId,
            CancellationToken cancellationToken);
    }
}
