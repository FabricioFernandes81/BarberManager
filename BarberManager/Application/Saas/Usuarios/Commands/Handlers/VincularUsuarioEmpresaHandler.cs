using Application.Common.Middlewares.Handler;
using Application.Interfaces;
using Application.Interfaces.Empresas;
using Application.Saas.Usuarios.Enums;
using Domain.Entities.AuthUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Usuarios.Commands.Handlers
{
    public class VincularUsuarioEmpresaHandler : ICommandHandler<VincularUsuarioEmpresaCommand, ResultadoVinculoUsuarioEmpresa>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IRepositorioEmpresa _repositorioEmpresa;
        private readonly IRepositorioUsuarioEmpresa _repositorioUsuarioEmpresa;
        private readonly ILogger<VincularUsuarioEmpresaHandler> _logger;


        public VincularUsuarioEmpresaHandler(UserManager<Usuario> userManager, IRepositorioEmpresa repositorioEmpresa, IRepositorioUsuarioEmpresa repositorioUsuarioEmpresa,
            ILogger<VincularUsuarioEmpresaHandler> logger)
        {
            _logger = logger;
            _repositorioEmpresa = repositorioEmpresa;
            _repositorioUsuarioEmpresa = repositorioUsuarioEmpresa;
            _userManager = userManager;
        }

        public async Task<ResultadoVinculoUsuarioEmpresa> Handle(VincularUsuarioEmpresaCommand command, CancellationToken cancellationToken)
        {
            if(command.UsuarioId == Guid.Empty || command.EmpresaId == Guid.Empty)
            {
                return ResultadoVinculoUsuarioEmpresa.DadosInvalidos;
            }

            var usuario = await _userManager.FindByIdAsync(command.UsuarioId.ToString());

            if (usuario is null) return ResultadoVinculoUsuarioEmpresa.UsuarioNaoEncontrado;

            var empvresaExiste = await _repositorioEmpresa.ExisteAsync(command.EmpresaId, cancellationToken);


            if (!empvresaExiste)
            {
                return ResultadoVinculoUsuarioEmpresa.EmpresaNaoEncontrada;
            }

            var resultado = await _repositorioUsuarioEmpresa
                .VincularOuReativarAsync(command.UsuarioId,
                command.EmpresaId, cancellationToken);

            _logger.LogInformation("Resultado do vinculo de Usuario {UsuarioId} com a empresa {EmpresaIs} : {Resultado}",
                command.UsuarioId, command.EmpresaId, resultado);

            return resultado;
        }
    }

}

