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

            // Um plano Trial deve ter preço zero e duração positiva.
            // Um plano comercial deve ter duração Trial igual a zero.
            if (plano.EhTrial)
            {
                if (plano.ValorMensal != 0m ||
                    plano.ValorAnual != 0m ||
                    plano.DuracaoTrialDias <= 0)
                {
                    return Falha(
                        "ConfiguracaoPlanoInvalida",
                        "O plano Trial está com uma configuração inválida.");
                }
            }
            else
            {
                if (plano.DuracaoTrialDias != 0)
                {
                    return Falha(
                        "ConfiguracaoPlanoInvalida",
                        "Planos comerciais não devem possuir duração de Trial.");
                }

                var valorMensalEquivalente =
                    command.PeriodicidadeCobranca switch
                    {
                        PeriodicidadeCobranca.Mensal => plano.ValorMensal,
                        PeriodicidadeCobranca.Anual => plano.ValorAnual,
                        _ => 0m
                    };

                if (valorMensalEquivalente <= 0m)
                {
                    return Falha(
                        "PlanoIndisponivel",
                        "O plano não possui um valor válido para a periodicidade selecionada.");
                }
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
                    ResponsavelId = responsavel.Id
                };

                empresaIdCriada = empresa.Id;

                var ehTrial = plano.EhTrial;

                // Valor da assinatura representa o valor do ciclo faturável.
                // No Trial, o valor é zero e não haverá fatura.
                var valorCiclo = ehTrial
                    ? 0m
                    : command.PeriodicidadeCobranca switch
                    {
                        PeriodicidadeCobranca.Mensal => plano.ValorMensal,
                        PeriodicidadeCobranca.Anual => decimal.Round(
                            plano.ValorAnual * 12m,
                            2,
                            MidpointRounding.AwayFromZero),
                        _ => 0m
                    };

                var fimTrial = ehTrial
                    ? agora.AddDays(plano.DuracaoTrialDias)
                    : (DateTime?)null;

                var vencimento = ehTrial
                    ? fimTrial!.Value
                    : command.PeriodicidadeCobranca switch
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
                    DiaVencimento = vencimento.Day,
                    Valor = valorCiclo,
                    RenovacaoAutomatica = false,
                    Trial = ehTrial,
                    DataFimTrial = fimTrial,
                    Ativa = ehTrial
                        ? StatusAssinaturaCliente.Ativa
                        : StatusAssinaturaCliente.Pendente,
                    PeriodicidadeCobranca = command.PeriodicidadeCobranca
                };

                await _repositorioEmpresa.CriarCadastroAsync(
                    responsavel,
                    empresa,
                    assinatura,
                    cancellationToken);

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

                var mensagem = ehTrial
                    ? $"Cadastro concluído. Seu período de avaliação é de {plano.DuracaoTrialDias} dias."
                    : "Cadastro recebido. A assinatura está pendente de confirmação do pagamento.";

                return new CriarEmpresaResultadoDto(
                    true,
                    "Criado",
                    mensagem,
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

                // A exceção deve propagar para que o middleware execute
                // rollback da transação SaaS.
                if (vinculoTentado && empresaIdCriada.HasValue)
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
