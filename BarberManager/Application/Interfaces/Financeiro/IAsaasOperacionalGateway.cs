using Domain.Entities.Financeiro;
using Application.Operacional.Financeiro.DTOs.Asaas;

namespace Application.Operacional.Financeiro.Interfaces;

public interface IAsaasOperacionalGateway
{

    Task<IReadOnlyList<CobrancaOperacionalResultadoDto>>
    ListarCobrancasPorReferenciaAsync(
        string apiToken,
        AmbienteAsaas ambiente,
        string externalReference,
        CancellationToken cancellationToken);


    Task<CobrancaOperacionalResultadoDto> CriarCobrancaAsync(
        string apiToken,
        AmbienteAsaas ambiente,
        CriarCobrancaOperacionalDto cobranca,
        CancellationToken cancellationToken);

    Task<PixOperacionalResultadoDto> ObterQrCodePixAsync(
        string apiToken,
        AmbienteAsaas ambiente,
        string pagamentoGatewayId,
        CancellationToken cancellationToken);

    Task<ClienteAsaasOperacionalResultadoDto> CriarClienteAsync(
   string apiToken,
   AmbienteAsaas ambiente,
   CriarClienteAsaasOperacionalDto cliente,
   CancellationToken cancellationToken);



  
}