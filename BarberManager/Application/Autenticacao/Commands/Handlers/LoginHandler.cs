using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Interfaces;
using Domain.Entities.AuthUsers;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Autenticacao.Commands.Handlers
{
    public class LoginHandler : ICommandHandler<LoginCommand, LoginResult>
    {
        private const string RoleSaas = "Administrador";
        private const string RoleOperacional = "AdministradorBarber";
        private const string RoleBarbeiro = "Barbeiro";


        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IRepositorioUsuarioEmpresa _repositorioUsuarioEmpresa;
      //  private readonly IRepositorioPermissoesUsuario _repositorioPermissoes;
        private readonly IValidadorEmpresaAutenticacao _validadorEmpresa;
        private readonly IJwtTokenService _jwtTokenService;

        private readonly IRepositorioProfissionalAutenticacao _repositorioProfissionalAutenticacao;

        public LoginHandler(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            RoleManager<Roles> roleManager,
            IRepositorioUsuarioEmpresa repositorioUsuarioEmpresa,
            IRepositorioProfissionalAutenticacao repositorioProfissionalAutenticacao,
            // IRepositorioPermissoesUsuario repositorioPermissoes,
            IValidadorEmpresaAutenticacao validadorEmpresa,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _repositorioUsuarioEmpresa = repositorioUsuarioEmpresa;
          //  _repositorioPermissoes = repositorioPermissoes;
            _validadorEmpresa = validadorEmpresa;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResult> Handle(
           LoginCommand command,
           CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Email) ||
                string.IsNullOrWhiteSpace(command.Senha) ||
                !Enum.IsDefined(command.TipoAcesso))
            {
                return FalhaLogin();
            }

            var usuario = await _userManager.FindByEmailAsync(
                command.Email.Trim());

            if (usuario is null)
                return FalhaLogin();

            var resultadoSenha =
                await _signInManager.CheckPasswordSignInAsync(
                    usuario,
                    command.Senha,
                    lockoutOnFailure: true);

            if (!resultadoSenha.Succeeded)
                return FalhaLogin();

            var nomesRoles =
                await _userManager.GetRolesAsync(usuario);

            var rolesAtivas = new List<string>();

            foreach (var nomeRole in nomesRoles)
            {
                var role =
                    await _roleManager.FindByNameAsync(nomeRole);

                if (role is { Ativo: true, Name: not null })
                    rolesAtivas.Add(role.Name);
            }

            string tipoUsuario;
            Guid? empresaId = null;
            Guid? profissionalId = null;

            switch (command.TipoAcesso)
            {
                case TipoAcesso.Saas:
                    {
                        if (!rolesAtivas.Contains(
                                RoleSaas,
                                StringComparer.OrdinalIgnoreCase))
                        {
                            return FalhaLogin();
                        }

                        tipoUsuario = "Saas";
                        break;
                    }

                case TipoAcesso.Operacional:
                    {
                        var ehAdministrador =
                            rolesAtivas.Contains(
                                RoleOperacional,
                                StringComparer.OrdinalIgnoreCase);

                        var ehBarbeiro =
                            rolesAtivas.Contains(
                                RoleBarbeiro,
                                StringComparer.OrdinalIgnoreCase);

                        if (!ehAdministrador && !ehBarbeiro)
                            return FalhaLogin();

                        // O barbeiro entra pela empresa de seu próprio
                        // cadastro. Não pode escolher outra empresa.
                        if (ehBarbeiro && !ehAdministrador)
                        {
                            var profissional =
                                await _repositorioProfissionalAutenticacao
                                    .ObterPorUsuarioIdAsync(
                                        usuario.Id,
                                        cancellationToken);

                            if (profissional is null)
                                return FalhaLogin();

                            if (command.EmpresaId.HasValue &&
                                command.EmpresaId.Value !=
                                    profissional.EmpresaId)
                            {
                                return FalhaLogin();
                            }

                            var possuiVinculo =
                                await _repositorioUsuarioEmpresa
                                    .PossuiVinculoAtivoAsync(
                                        usuario.Id,
                                        profissional.EmpresaId,
                                        cancellationToken);

                            if (!possuiVinculo)
                                return FalhaLogin();

                            empresaId = profissional.EmpresaId;
                            profissionalId =
                                profissional.ProfissionalId;
                        }
                        else
                        {
                            // Fluxo existente para administrador
                            // operacional da barbearia.
                            var empresas =
                                await _repositorioUsuarioEmpresa
                                    .ObterEmpresasAtivasAsync(
                                        usuario.Id,
                                        cancellationToken);

                            if (command.EmpresaId.HasValue)
                            {
                                if (!empresas.Contains(
                                        command.EmpresaId.Value))
                                {
                                    return FalhaLogin();
                                }

                                empresaId = command.EmpresaId.Value;
                            }
                            else
                            {
                                if (empresas.Count == 0)
                                    return FalhaLogin();

                                if (empresas.Count > 1)
                                {
                                    return new LoginResult(
                                        Sucesso: false,
                                        AccessToken: null,
                                        ExpiraEm: null,
                                        Mensagem:
                                            "Selecione a barbearia para continuar.",
                                        RequerSelecaoEmpresa: true,
                                        EmpresasDisponiveis: empresas);
                                }

                                empresaId = empresas[0];
                            }
                        }

                        // Valida também se a empresa está ativa no SaaS.
                        var empresaAtiva =
                            await _validadorEmpresa
                                .EmpresaEstaAtivaAsync(
                                    empresaId!.Value,
                                    cancellationToken);

                        if (!empresaAtiva)
                            return FalhaLogin();

                        tipoUsuario = "Operacional";
                        break;
                    }

                case TipoAcesso.Cliente:
                    {
                        // Mantém o comportamento atual: não emitir
                        // token Cliente sem vínculo implementado.
                        return new LoginResult(
                            Sucesso: false,
                            AccessToken: null,
                            ExpiraEm: null,
                            Mensagem:
                                "O acesso de cliente ainda não está configurado.");
                    }

                default:
                    return FalhaLogin();
            }

            var permissoes =
                await _repositorioUsuarioEmpresa
                    .ObterCodigosAsync(
                        usuario.Id,
                        cancellationToken);

            var contexto = new ContextoAutenticacao(
                TipoUsuario: tipoUsuario,
                EmpresaId: empresaId,
                Permissoes: permissoes,
                ProfissionalId: profissionalId);

            var (accessToken, expiraEm) =
                await _jwtTokenService.GerarAsync(
                    usuario,
                    contexto,
                    cancellationToken);

            return new LoginResult(
                Sucesso: true,
                AccessToken: accessToken,
                ExpiraEm: expiraEm,
                Mensagem: null);
        }

        private static LoginResult FalhaLogin() =>
            new(
                Sucesso: false,
                AccessToken: null,
                ExpiraEm: null,
                Mensagem:
                    "Email, senha, perfil ou vínculo inválido.");
    }
}
