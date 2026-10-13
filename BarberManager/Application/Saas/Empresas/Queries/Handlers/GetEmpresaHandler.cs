using Application.Common.Middlewares.Handler;
using Application.Common.Pagination;
using Application.Interfaces;
using Application.Interfaces.Empresas;
using Application.Saas.Empresas.DTOs;
using Application.Saas.Empresas.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Empresas.Queries.Handlers
{
    public class GetEmpresaHandler : IQueryHandler<GetEmpresaQuery, PagedResponse<List<GetListEmpresaDto>>>
    {
        private readonly ILogger<GetEmpresaHandler> _logger;
        private readonly IRepositorioEmpresa _repositorioEmpresa;
        private readonly IUrlService _urlService;
        public GetEmpresaHandler(ILogger<GetEmpresaHandler> logger, IRepositorioEmpresa repositorioEmpresa, IUrlService urlService)
        {
            _logger = logger;
            _repositorioEmpresa = repositorioEmpresa;
            _urlService = urlService;

        }

        public async Task<PagedResponse<List<GetListEmpresaDto>>> Handle(GetEmpresaQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando a requisição GetEmpresaHandler");

            var (empresa, totalRecords) = await _repositorioEmpresa.ListarAsync
                (query.pageNumber, query.pageSize, query.SearchTerm, query.SortBy,query.Status);

            var validFilter = new PaginationFilter(query.pageNumber, query.pageSize);
            var router = query.Route ?? string.Empty;

            return PaginationHelper.CreatePagedReponse(empresa, validFilter, totalRecords,_urlService,router); 
        
        }
    }
}
