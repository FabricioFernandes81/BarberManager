using Domain.Entities.Saas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Empresas
{
    public interface IRepositorioFatura
    {
        Task<Assinatura?> ObterAssinaturaAsync(
        Guid assinaturaId,
        CancellationToken cancellationToken = default);

        Task<Fatura?> ObterPorPeriodoAsync(
            Guid assinaturaId,
            DateTime periodoInicio,
            DateTime periodoFim,
            CancellationToken cancellationToken = default);

        Task AdicionarAsync(
            Fatura fatura,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<Assinatura>>
    ObterAssinaturasParaFaturamentoAsync(
        DateTime limiteVencimento,
        CancellationToken cancellationToken = default);
    }
}
