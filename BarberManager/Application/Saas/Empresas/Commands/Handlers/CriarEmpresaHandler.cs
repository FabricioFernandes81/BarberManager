using Application.Common.Middlewares.Handler;
using Application.Interfaces;
using Application.Interfaces.Empresas;
using Application.Saas.Empresas.DTOs;
using Application.Saas.Usuarios.Enums;
using Domain.Entities.AuthUsers;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Saas;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Empresas.Commands.Handlers
{
    public class CriarEmpresaHandler : ICommandHandler<CriarEmpresaCommand, CriarEmpresaResultadoDto>
    {
        private const string RoleAdministradorBarber = "AdministradorBarber";

        private readonly UserManager<Usuario> _userManager;
        private readonly IRepositorioEmpresa _repositorioEmpresa;
        private readonly IRepositorioUsuarioEmpresa _repositorioUsuarioEmpresa;
        private readonly ILogger<CriarEmpresaHandler> _logger;


        public CriarEmpresaHandler(UserManager<Usuario> userManager, IRepositorioEmpresa repositorioEmpresa, IRepositorioUsuarioEmpresa repositorioUsuarioEmpresa, ILogger<CriarEmpresaHandler> logger)
        {
            _userManager = userManager;
            _repositorioEmpresa = repositorioEmpresa;
            _repositorioUsuarioEmpresa = repositorioUsuarioEmpresa;
            _logger = logger;
        }

        public async Task<CriarEmpresaResultadoDto> Handle(
    CriarEmpresaCommand command,
    CancellationToken cancellationToken)
        {
            if (!DadosValidos(command))
            {
                return Falha(
                    "DadosInvalidos",
                    "Verifique os dados informados.");
            }

            if (!Enum.IsDefined(command.PeriodicidadeCobranca))
            {
                return Falha(
                    "PeriodicidadeInvalida",
                    "A periodicidade informada é inválida.");
            }

            var plano = await _repositorioEmpresa.ObterPlanoAtivoAsync(
                command.PlanoId,
                cancellationToken);

            if (plano is null)
            {
                return Falha(
                    "PlanoIndisponivel",
                    "O plano selecionado não está disponível.");
            }

            var valor = command.PeriodicidadeCobranca switch
            {
                PeriodicidadeCobranca.Mensal => plano.ValorMensal,
                PeriodicidadeCobranca.Anual => plano.ValorAnual,
                _ => 0m
            };

            if (valor <= 0)
            {
                return Falha(
                    "PlanoIndisponivel",
                    "O plano não possui um valor válido para a periodicidade selecionada.");
            }

            var email = command.Email.Trim();
            var agora = DateTime.UtcNow;

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                UserName = email,
                Email = email,
                PhoneNumber = command.Telefone.Trim(),
                EmailConfirmed = false,
                PhoneNumberConfirmed = false
            };

            var usuarioCriado = false;
            var vinculoTentado = false;
            Guid? empresaIdCriada = null;

            try
            {
                var criacaoUsuario = await _userManager.CreateAsync(
                    usuario,
                    command.Senha);

                if (!criacaoUsuario.Succeeded)
                {
                    // Não expõe detalhes que possam facilitar enumeração
                    // ou revelar regras internas do Identity.
                    _logger.LogWarning(
                        "O Identity recusou a criação do usuário para cadastro de empresa.");

                    return Falha(
                        "CadastroNaoConcluido",
                        "Não foi possível concluir o cadastro. Verifique os dados informados.");
                }

                usuarioCriado = true;

                var atribuicaoRole = await _userManager.AddToRoleAsync(
                    usuario,
                    RoleAdministradorBarber);

                if (!atribuicaoRole.Succeeded)
                {
                    await ExcluirUsuarioComSegurancaAsync(usuario);

                    usuarioCriado = false;

                    _logger.LogError(
                        "Não foi possível atribuir a role {Role} ao usuário {UsuarioId}.",
                        RoleAdministradorBarber,
                        usuario.Id);

                    return Falha(
                        "CadastroNaoConcluido",
                        "Não foi possível concluir o cadastro.");
                }

                var responsavel = new Responsavel
                {
                    Nome = command.NomeResponsavel.Trim(),
                    Email = email,
                    Telefone = command.Telefone.Trim(),
                    UsuarioId = usuario.Id
                };

                var empresa = new Empresa
                {
                    NomeFantasia = command.NomeBarbearia.Trim(),
                    RazaoSocial = command.NomeBarbearia.Trim(),
                    Cnpj = null!,
                    Email = email,
                    Telefone = command.Telefone.Trim(),
                    Ativa = StatusGeral.Ativo,
                    ResponsavelId = responsavel.Id,

                };

                empresaIdCriada = empresa.Id;

                var vencimento = command.PeriodicidadeCobranca switch
                {
                    PeriodicidadeCobranca.Mensal => agora.AddMonths(1),
                    PeriodicidadeCobranca.Anual => agora.AddYears(1),
                    _ => throw new InvalidOperationException(
                        "Periodicidade de cobrança não suportada.")
                };

                var assinatura = new Assinatura
                {
                    EmpresaId = empresa.Id,
                    PlanoId = plano.Id,
                    DataInicio = agora,
                    DataVencimento = vencimento,
                    DiaVencimento = agora.Day,
                    Valor = valor,
                    RenovacaoAutomatica = false,
                    Trial = false,
                    DataFimTrial = null,
                    Ativa = StatusAssinaturaCliente.Pendente,
                    PeriodicidadeCobranca = command.PeriodicidadeCobranca
                };

                // Adiciona ao DbContext SaaS. O middleware fará SaveChanges
                // e commit ao final do comando.
                await _repositorioEmpresa.CriarCadastroAsync(
                    responsavel,
                    empresa,
                    assinatura,
                    cancellationToken);

                // Marcamos antes da chamada para compensar também
                // se ocorrer uma exceção depois de o vínculo ser persistido.
                vinculoTentado = true;

                var resultadoVinculo =
                    await _repositorioUsuarioEmpresa.VincularOuReativarAsync(
                        usuario.Id,
                        empresa.Id,
                        cancellationToken);

                if (resultadoVinculo is not ResultadoVinculoUsuarioEmpresa.Criado)
                {
                    throw new InvalidOperationException(
                        $"Não foi possível criar o vínculo do usuário. Resultado: {resultadoVinculo}.");
                }

                return new CriarEmpresaResultadoDto(
                    true,
                    "Criado",
                    "Cadastro recebido. A assinatura está pendente de ativação.",
                    empresa.Id,
                    responsavel.Id,
                    assinatura.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Falha durante o cadastro da empresa. UsuarioId: {UsuarioId}.",
                    usuario.Id);

                // A exceção precisa propagar para o TransactionMiddleware
                // executar rollback da transação SaaS.
                if (vinculoTentado)
                {
                    try
                    {
                        await _repositorioUsuarioEmpresa.RemoverVinculoCriadoAsync(
                            usuario.Id,
                            empresaIdCriada.Value,
                            CancellationToken.None);
                    }
                    catch (Exception compensacaoEx)
                    {
                        _logger.LogCritical(
                            compensacaoEx,
                            "Falha ao compensar vínculo de usuário. UsuarioId: {UsuarioId}.",
                            usuario.Id);
                    }
                }

                if (usuarioCriado)
                {
                    try
                    {
                        await ExcluirUsuarioComSegurancaAsync(usuario);
                    }
                    catch (Exception compensacaoEx)
                    {
                        _logger.LogCritical(
                            compensacaoEx,
                            "Falha ao compensar usuário do Identity. UsuarioId: {UsuarioId}.",
                            usuario.Id);
                    }
                }

                throw;
            }
        }

        private static bool DadosValidos(CriarEmpresaCommand command)
        {
            return !string.IsNullOrWhiteSpace(command.NomeResponsavel)
                && !string.IsNullOrWhiteSpace(command.Telefone)
                && !string.IsNullOrWhiteSpace(command.NomeBarbearia)
                && !string.IsNullOrWhiteSpace(command.Email)
                && !string.IsNullOrWhiteSpace(command.Senha)
                && command.PlanoId != Guid.Empty;
        }

        private async Task ExcluirUsuarioComSegurancaAsync(Usuario usuario)
        {
            var resultado = await _userManager.DeleteAsync(usuario);

            if (!resultado.Succeeded)
            {
                throw new InvalidOperationException(
                    "Não foi possível remover o usuário durante a compensação.");
            }
        }

        private static CriarEmpresaResultadoDto Falha(
            string codigo,
            string mensagem)
        {
            return new CriarEmpresaResultadoDto(
                false,
                codigo,
                mensagem);
        }
    }
}
