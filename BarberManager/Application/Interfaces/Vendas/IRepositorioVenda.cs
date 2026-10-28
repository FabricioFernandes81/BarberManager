using Domain.Entities.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Vendas
{
    public interface IRepositorioVenda
    {
        Task<Venda?> ObterPorAgendamentoAsync(
            Guid empresaId,
            Guid agendamentoId,
            CancellationToken cancellationToken);

        Task<Venda?> ObterPorIdAsync(
            Guid empresaId,
            Guid vendaId,
            CancellationToken cancellationToken);

        Task<Venda?> ObterParaAtualizacaoAsync(
            Guid empresaId,
            Guid vendaId,
            CancellationToken cancellationToken);

        Task AdicionarAsync(
            Venda venda,
            CancellationToken cancellationToken);
    }
}
