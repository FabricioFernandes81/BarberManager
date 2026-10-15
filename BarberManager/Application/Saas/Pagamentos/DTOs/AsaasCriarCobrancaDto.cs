using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.DTOs
{
    public sealed record AsaasCriarCobrancaDto(
    string ClienteAsaasId,
    Guid FaturaId,
    Guid TransacaoId,
    decimal Valor,
    DateTime Vencimento,
    MetodoPagamento MetodoPagamento,
    string? Descricao,
    string? RemoteIp,
    DadosCartaoCreditoDto? CartaoCredito);
}
