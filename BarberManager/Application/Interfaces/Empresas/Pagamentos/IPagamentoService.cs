using Application.Saas.Pagamentos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Empresas.Pagamentos
{
    public interface IPagamentoService
    {
        Task<IniciarPagamentoResultadoDto> IniciarAsync(
            IniciarPagamentoRequest request,
            string chaveIdempotencia,
            string remoteIp,
            CancellationToken cancellationToken = default);
    }
}
