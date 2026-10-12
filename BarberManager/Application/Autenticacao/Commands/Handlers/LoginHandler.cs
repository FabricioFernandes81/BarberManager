using Application.Common.Middlewares.Handler;
using Application.Common.Security;
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

        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IRepositorioUsuarioEmpresa _repositorioUsuarioEmpresa;
      //  private readonly IRepositorioPermissoesUsuario _repositorioPermissoes;
        private readonly IValidadorEmpresaAutenticacao _validadorEmpresa;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginHandler(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            RoleManager<Roles> roleManager,
            IRepositorioUsuarioEmpresa repositorioUsuarioEmpresa,
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

            var senhaValida =
                await _signInManager.CheckPasswordSignInAsync(
                    usuario,
                    command.Senha,
                    lockoutOnFailure: true);

            if (!senhaValida.Succeeded)
                return FalhaLogin();

            // Resolve somente os papéis que realmente existem,
            // estão ativos e foram atribuídos ao usuário.
            var nomesRoles = await _userManager.GetRolesAsync(usuario);

            var rolesAtivas = new List<string>();

            foreach (var nomeRole in nomesRoles)
            {
                var role = await _roleManager.FindByNameAsync(nomeRole);

                if (role is { Ativo: true, Name: not null })
                    rolesAtivas.Add(role.Name);
            }

            string tipoUsuario;
            Guid? empresaId = null;

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
                        if (!rolesAtivas.Contains(
                                RoleOperacional,
                                StringComparer.OrdinalIgnoreCase))
                        {
                            return FalhaLogin();
                        }

                        var empresas =
                            await _repositorioUsuarioEmpresa
                                .ObterEmpresasAtivasAsync(
                                    usuario.Id,
                                    cancellationToken);

                        if (command.EmpresaId.HasValue)
                        {
                            // O ID selecionado só é aceito se pertencer
                            // à lista de vínculos ativos do usuário.
                            if (!empresas.Contains(command.EmpresaId.Value))
                                return FalhaLogin();

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
                                    Mensagem: "Selecione a barbearia para continuar.",
                                    RequerSelecaoEmpresa: true,
                                    EmpresasDisponiveis: empresas);
                            }

                            empresaId = empresas[0];
                        }

                        // O vínculo pode estar ativo e a empresa suspensa.
                        // As duas condições precisam ser verificadas.
                        var empresaAtiva =
                            await _validadorEmpresa.EmpresaEstaAtivaAsync(
                                empresaId.Value,
                                cancellationToken);

                        if (!empresaAtiva)
                            return FalhaLogin();

                        tipoUsuario = "Operacional";
                        break;
                    }

                case TipoAcesso.Cliente:
                    {
                        // Não emitir token Cliente até implementar e validar
                        // o relacionamento real entre Usuario e Cliente.
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
                await _repositorioUsuarioEmpresa.ObterCodigosAsync(
                    usuario.Id,
                    cancellationToken);

            var contexto = new ContextoAutenticacao(
                TipoUsuario: tipoUsuario,
                EmpresaId: empresaId,
                Permissoes: permissoes);

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
                Mensagem: "Email, senha, perfil ou vínculo inválido.");
    }
}
