using Application.Common.Middlewares.Handler;
using Application.Common.Pagination;
using Application.Interfaces;
using Application.Interfaces.Empresas;
using Application.Saas.Assinaturas.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Assinaturas.Queries.Handlers
{
    public class GetAssinaturaHandler : IQueryHandler<GetAssinaturaQuery,PagedResponse<List<GetAssinaturaDto>>>
    {
        private readonly ILogger<GetAssinaturaHandler> _logger;
        private readonly IRepositorioAssinatura _repositorioAssinatura;
        private readonly IUrlService _urlService;

        public GetAssinaturaHandler(ILogger<GetAssinaturaHandler> logger, IRepositorioAssinatura repositorioAssinatura, IUrlService urlService)
        {
            _logger = logger;
            _repositorioAssinatura = repositorioAssinatura;
            _urlService = urlService;
        }

        public async Task<PagedResponse<List<GetAssinaturaDto>>> Handle(GetAssinaturaQuery query, CancellationToken cancellationToken)
        {
            var (assinatura, total) = await _repositorioAssinatura.ListAsync(query.pageNumber, query.pageSize,
                query.SearchTerm, query.SortBy, query.Status, cancellationToken);

            var hoje = DateTime.Today;
            List<GetAssinaturaDto> assinaturaDtos = new List<GetAssinaturaDto>();
            foreach (var assin in assinatura)
            {
                assin.DiasParaVencer = (assin.DataVencimento - hoje).Days;
                assinaturaDtos.Add(assin);
            }

            var validFilter = new PaginationFilter(query.pageNumber, query.pageSize);
            var router = query.Route ?? string.Empty;

            return PaginationHelper.CreatePagedReponse(assinaturaDtos, validFilter, total, _urlService, router);


        }
    }
}
