using Application.Common.Middlewares.Handler;
using Application.Common.Pagination;
using Application.Empresas.DTOs;
using Application.Interfaces.Empresas;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empresas.Queries.Handlers
{
    public class GetEmpresaHandler : IQueryHandler<GetEmpresaQuery, PagedResponse<List<GetListEmpresaDto>>>
    {
        private readonly ILogger<GetEmpresaHandler> _logger;
        private readonly IRepositorioEmpresa _repositorioEmpresa;

        public GetEmpresaHandler(ILogger<GetEmpresaHandler> logger, IRepositorioEmpresa repositorioEmpresa)
        {
            _logger = logger;
            _repositorioEmpresa = repositorioEmpresa;

        }

        public async Task<PagedResponse<List<GetListEmpresaDto>>> Handle(GetEmpresaQuery query, CancellationToken cancellationToken)
        {
            var (empresa, totalRecords) = await _repositorioEmpresa.ListarAsync
                (query.pageNumber, query.pageSize, query.SearchTerm, query.SortBy);


            return null; 
        
        }
    }
}
