using Application.Clientes.DTOs;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Interfaces.Clientes;
using Domain.Entities.AuthUsers;
using Domain.Entities.Clientes;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Clientes.Commands.Handlers
{
    public class CriarPerfilClienteHandler : ICommandHandler<CriarPerfilClienteCommand, CriarPerfilClienteResultadoDto>
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IUsuarioLogado _usuarioLogado;
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly ILogger<CriarPerfilClienteHandler> _logger;

        public CriarPerfilClienteHandler(UserManager<Usuario> userManager, IUsuarioLogado usuarioLogado, IRepositorioCliente repositorioCliente, ILogger<CriarPerfilClienteHandler> logger)
        {
            _userManager = userManager;
            _usuarioLogado = usuarioLogado;
            _repositorioCliente = repositorioCliente;
            _logger = logger;
        }



        public async Task<CriarPerfilClienteResultadoDto> Handle(CriarPerfilClienteCommand command, CancellationToken cancellationToken)
        {
            if(!_usuarioLogado.IsAuthenticated || _usuarioLogado.UsuarioId == Guid.Empty)
            {
                return Falha(
                "NaoAutenticado",
                "É necessário estar autenticado para criar o perfil.");
            }

            if (string.IsNullOrWhiteSpace(command.Nome))
            {
                return Falha(
              "NomeObrigatorio",
              "O nome do cliente é obrigatório.");
            }

            var nome = command.Nome.Trim();
            var sobrenome = string.IsNullOrWhiteSpace(command.Sobrenome) ? null
                : command.Sobrenome.Trim();

            var cpf = NormalizarCpf(command.Cpf);

            if (cpf is not null && cpf.Length != 11)
            {
                return Falha(
                    "CpfInvalido",
                    "O CPF deve conter 11 dígitos.");
            }

            if(command.DataNascimento.HasValue && 
                command.DataNascimento.Value.Date > DateTime.UtcNow.Date)
            {
                return Falha(
          "DataNascimentoInvalida",
          "A data de nascimento não pode ser futura.");
            }


            var usuario = await _userManager.FindByIdAsync(
            _usuarioLogado.UsuarioId.ToString());

            if (usuario is null)
            {
                return Falha(
                    "ContaIdentityNaoEncontrada",
                    "A conta de autenticação não foi encontrada.");
            }
            if (await _userManager.IsLockedOutAsync(usuario))
            {
                return Falha(
                    "ContaBloqueada",
                    "A conta não está autorizada a realizar esta operação.");
            }


            var perfilExistente =
           await _repositorioCliente.ObterPorUsuarioIdAsync(
               usuario.Id,
               cancellationToken);

            if (perfilExistente is not null)
            {
                return Falha(
                    "PerfilJaExiste",
                    "Esta conta já possui um perfil de cliente.");
            }

            var cliente = new Cliente
            {
                UsuarioId = usuario.Id,
                Nome = nome,
                Sobrenome = sobrenome,
                Cpf = cpf,
                DataNascimento = command.DataNascimento?.Date,
                Ativo = true
            };

            var criado = await _repositorioCliente.CriarAsync(
                cliente,
                cancellationToken);

            if (!criado)
            {
                return Falha(
                    "PerfilJaExiste",
                    "Esta conta já possui um perfil de cliente.");
            }

            _logger.LogInformation(
                "Perfil global de cliente criado. ClienteId: {ClienteId}.",
                cliente.Id);

            return new CriarPerfilClienteResultadoDto(
                true,
                "PerfilCriado",
                "Perfil de cliente criado com sucesso.",
                Mapear(cliente, usuario));
        }

        private string? NormalizarCpf(string? cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return null;

            return new string(cpf.Where(char.IsDigit).ToArray());
        }


        private static PerfilClienteDto Mapear(
     Cliente cliente,
     Usuario usuario)
        {
            return new PerfilClienteDto(
                cliente.Id,
                cliente.UsuarioId,
                cliente.Nome,
                cliente.Sobrenome,
                cliente.Cpf,
                cliente.DataNascimento,
                usuario.Email,
                usuario.PhoneNumber,
                cliente.Ativo,
                cliente.DataCadastro);
        }
        private static CriarPerfilClienteResultadoDto Falha(
        string codigo,
        string mensagem)
        {
            return new CriarPerfilClienteResultadoDto(
                false,
                codigo,
                mensagem);
        }
    }
}
