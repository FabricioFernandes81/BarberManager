using Application.Common.Middlewares.Handler;
using Application.Common.Pagination;
using Application.Interfaces;
using Application.Interfaces.Empresas;
using Application.Saas.Planos.DTOs;
using Application.Saas.Planos.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Planos.Queries.Handlers
{
    public class GetPlanosHandler : IQueryHandler<GetPlanosQuery, PagedResponse<List<GetPlanoDto>>>
    {
        private readonly ILogger<GetPlanosHandler> _logger;
        private readonly IUrlService _urlService;
        private readonly IRepositórioPlano _repositórioPlano;

        public GetPlanosHandler(IRepositórioPlano repositórioPlano, IUrlService urlService, ILogger<GetPlanosHandler> logger)
        {
            _logger = logger;
            _urlService = urlService;
            _repositórioPlano = repositórioPlano;
        }

        public async Task<PagedResponse<List<GetPlanoDto>>> Handle(GetPlanosQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando a requisição GetPlanosHandler");

            var (plano, total) = await _repositórioPlano.ListarAsync(
                query.pageNumber, query.pageSize, query.SearchTerm, query.SortBy, query.Status, cancellationToken);


            var validFilter = new PaginationFilter(query.pageNumber, query.pageSize);
            var router = query.Route = string.Empty;


            return PaginationHelper.CreatePagedReponse(plano, validFilter, total, _urlService, router);

        
        }
    }
}
