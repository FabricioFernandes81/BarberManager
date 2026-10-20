using Application.Common.Middlewares.Handler;
using Application.Interfaces.Empresas;
using Application.Saas.Planos.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Planos.Queries.Handlers
{
    public class ListarPlanosPublicosHandler : IQueryHandler<ListarPlanosPublicosQuery, List<PlanoPublicoDto>>
    {
        private readonly IRepositorioEmpresa _repositorioEmpresa;
        private readonly ILogger<ListarPlanosPublicosHandler> _logger;

        public ListarPlanosPublicosHandler(IRepositorioEmpresa repositorioEmpresa, ILogger<ListarPlanosPublicosHandler>logger)
        {
            _repositorioEmpresa = repositorioEmpresa;
            _logger = logger;


        }

        public async Task<List<PlanoPublicoDto>> Handle(ListarPlanosPublicosQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consultando Cátalogo de Planos");

            return await _repositorioEmpresa.ListarPlanosPublicosAsync(cancellationToken);
        }
    }
}
