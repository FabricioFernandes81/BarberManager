using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Interfaces;
using Application.Saas.Usuarios.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Usuarios.Queries.Handlers
{
    public class GetUsuarioDetalheHandler : IQueryHandler<GetUsuarioDetalheQuery, GetUsuarioDetalheDto?>
    {
        private readonly IRepositorioUsuarioEmpresa _repositorio;
        private readonly ILogger<GetUsuarioDetalheHandler> _logger;

        public GetUsuarioDetalheHandler(IRepositorioUsuarioEmpresa repositorio,ILogger<GetUsuarioDetalheHandler> logger)
        {
            _logger = logger;
            _repositorio = repositorio;

        }

        public async Task<GetUsuarioDetalheDto?> Handle(GetUsuarioDetalheQuery query, CancellationToken cancellationToken)
        {
            if (query.UsuarioId == Guid.Empty)
                throw new CustomValidationException(new[] { "O identificador do usuário é invalido." });

            _logger.LogInformation("Consultando detalhes do Usuário {Usuario}", query.UsuarioId);

            return await _repositorio.ObterDetalheAsync(query.UsuarioId, cancellationToken);
                
        }
    }
}
