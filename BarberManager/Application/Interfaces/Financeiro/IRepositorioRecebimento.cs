using Domain.Entities.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Financeiro
{
    public interface IRepositorioRecebimento
    {
        Task<ContaReceber?> ObterContaParaRecebimentoAsync(
           Guid empresaId,
           Guid contaReceberId,
           CancellationToken cancellationToken);

        Task<FormaPagamento?> ObterFormaPagamentoAtivaAsync(
            Guid empresaId,
            Guid formaPagamentoId,
            CancellationToken cancellationToken);

        Task<Caixa?> ObterCaixaAbertoAsync(
            Guid empresaId,
            Guid usuarioId,
            CancellationToken cancellationToken);

        Task AdicionarAsync(
            Recebimento recebimento,
            CancellationToken cancellationToken);

        Task AdicionarMovimentoCaixaAsync(
            MovimentoCaixa movimento,
            CancellationToken cancellationToken);

        Task<ContaReceber?> ObterPorVendaAsync(
                 Guid empresaId, Guid vendaId, CancellationToken cancellationToken);


        Task AdicionarContaAsync(
    ContaReceber conta,
    CancellationToken cancellationToken);
    }
}
