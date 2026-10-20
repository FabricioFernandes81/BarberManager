using Application.Saas.Pagamentos.DTOs;
using Domain.Entities.Saas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Empresas.Pagamentos
{
    public interface IAsaasGateway
    {
        Task<AsaasCobrancaResultadoDto> CriarCobrancaAsync(
            AsaasCriarCobrancaDto cobranca,
            CancellationToken cancellationToken = default);

        Task<AsaasPixResultadoDto> ObterQrCodePixAsync(
            string referenciaGateway,
            CancellationToken cancellationToken = default);

        Task<AsaasClienteResultadoDto> CriarClienteAsync(
            AsaasCriarClienteDto cliente,
            CancellationToken cancellationToken = default);

        Task<AsaasCobrancaDto?> ObterCobrancaPorReferenciaExternaAsync(
    string referenciaExterna,
    CancellationToken cancellationToken);
    }
}
