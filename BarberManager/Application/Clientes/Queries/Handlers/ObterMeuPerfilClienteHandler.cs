using Application.Clientes.DTOs;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Interfaces.Clientes;
using Domain.Entities.AuthUsers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Clientes.Queries.Handlers
{
    public class ObterMeuPerfilClienteHandler
    : IQueryHandler<
        ObterMeuPerfilClienteQuery,
        PerfilClienteDto?>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IUsuarioLogado _usuarioLogado;
        private readonly IRepositorioCliente _repositorioCliente;

        public ObterMeuPerfilClienteHandler(
            UserManager<Usuario> userManager,
            IUsuarioLogado usuarioLogado,
            IRepositorioCliente repositorioCliente)
        {
            _userManager = userManager;
            _usuarioLogado = usuarioLogado;
            _repositorioCliente = repositorioCliente;
        }

        public async Task<PerfilClienteDto?> Handle(ObterMeuPerfilClienteQuery query, CancellationToken cancellationToken)
        {
            if(!_usuarioLogado.IsAuthenticated || _usuarioLogado.UsuarioId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "É necessario estar autenticado.");
            }

            var usuario = await _userManager.FindByIdAsync(_usuarioLogado.UsuarioId.ToString());
       
            if(usuario is null)
            {
                throw new UnauthorizedAccessException("A conta de autenticação não foi encontrada.");
            }

            var cliente = await _repositorioCliente.ObterPorUsuarioIdAsync(
                usuario.Id, cancellationToken);

            if (cliente is null)
                return null;

            return new PerfilClienteDto(
                cliente.Id,cliente.UsuarioId,
                cliente.Nome, cliente.Sobrenome,
                cliente.Cpf, cliente.DataNascimento,
                usuario.Email,usuario.PhoneNumber,
                cliente.Ativo,cliente.DataCadastro);
        }
    }
}
