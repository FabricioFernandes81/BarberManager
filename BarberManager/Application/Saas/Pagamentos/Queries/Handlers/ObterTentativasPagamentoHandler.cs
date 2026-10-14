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
    public class ObterTentativasPagamentoHandler : IQueryHandler<ObterTentativasPagamentoQuery, List<PagamentoTentativaDto>>
    {
        private IRepositorioPagamento _repositorioPagamento;


        public ObterTentativasPagamentoHandler(IRepositorioPagamento repositorioPagamento)
        {
            _repositorioPagamento = repositorioPagamento;
        }

        public async Task<List<PagamentoTentativaDto>> Handle(ObterTentativasPagamentoQuery query, CancellationToken cancellationToken)
        {
            if(query.FaturaId == Guid.Empty)
            {
                throw new CustomValidationException(new[] { $"O identificador da fatura é obrigatório." });

            }

            return await _repositorioPagamento.ObterTentativasPorFaturaAsync(query.FaturaId, cancellationToken);
        }
    }
}
