using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Interfaces.Empresas;
using Application.Saas.Pagamentos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.Queries.Handlers
{
    public class ObterStatusFaturaHandler : IQueryHandler<ObterStatusFaturaQuery, ObterStatusFaturaDto?>
    {
        private readonly IRepositorioPagamento _repositorioPagamento;


        public ObterStatusFaturaHandler(IRepositorioPagamento repositorioPagamento)
        {
            _repositorioPagamento = repositorioPagamento;
        }


        public async Task<ObterStatusFaturaDto?> Handle(ObterStatusFaturaQuery query, CancellationToken cancellationToken)
        {
            if(query.FaturaId == Guid.Empty)
            {
                throw new CustomValidationException(new[] { $"O identificador da fatura é obrigatório." });

            }
            var fatura = await _repositorioPagamento
                .ObterFaturaComTentativasAsync(query.FaturaId, cancellationToken);

            if (fatura is null) return null;

            var ultimatransacao = fatura.TransacoesPagamento
                .OrderByDescending(t => t.DataCadastro)
                .FirstOrDefault();

            return new ObterStatusFaturaDto
            {
                FaturaId = fatura.Id,
                Valor = fatura.Valor,
                Status = fatura.Status.ToString(),
                Vencimento = fatura.Vencimento,
                DataPagamento = fatura.DataPagamento,
                PeriodoInicio = fatura.PeriodoInicio,
                PeriodoFim = fatura.PeriodoFim,

                ReferenciaGateway = fatura.ReferenciaGateway ?? ultimatransacao?.ReferenciaGateway,
                TransacaoId = ultimatransacao?.Id,

                StatusTransacao = ultimatransacao?.Status.ToString(),

                MetodoPagamento = ultimatransacao?.MetodoPagamento.ToString(),
                 DataProcessamento = ultimatransacao?.DataProcessamento
            
            };
        }
    }
}
