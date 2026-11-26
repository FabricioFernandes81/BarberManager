using Application.Common.Exceptions;
using Application.Interfaces.Clientes;
using Application.Interfaces.Notificacoes;
using Application.Operacional.Agendamentos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Integracoes.WhatsApp
{
    public sealed class ClienteWhatsAppResolver : IClienteWhatsAppResolver
    {
        private readonly IRepositorioConfiguracaoWhatsApp _repositorioConfiguracao;
        private readonly IConsultaUsuarioIdentity _consultaUsuario;
        private readonly IRepositorioCliente _repositorioCliente;

        public ClienteWhatsAppResolver(
            IRepositorioConfiguracaoWhatsApp repositorioConfiguracao,
            IConsultaUsuarioIdentity consultaUsuario,
            IRepositorioCliente repositorioCliente)
        {
            _repositorioConfiguracao = repositorioConfiguracao;
            _consultaUsuario = consultaUsuario;
            _repositorioCliente = repositorioCliente;
        }

        public async Task<ClienteWhatsAppResolucaoDto> ResolverAsync(
            string instanceName,
            string telefone,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(instanceName))
                throw new CustomValidationException(
    new[] { "Instância WhatsApp inválida ou inativa." });

            if (string.IsNullOrWhiteSpace(telefone))
                throw new CustomValidationException(
      new[] { "O telefone do cliente não foi informado." });

            var configuracao = await _repositorioConfiguracao
                .ObterPorInstanceNameAsync(
                    instanceName,
                    cancellationToken);

            if (configuracao is null || !configuracao.Ativo)
                throw new CustomValidationException(
          new[] { "A instância WhatsApp não foi informada." });

            var telefoneNormalizado = new string(
                telefone.Where(char.IsDigit).ToArray());

            var usuarioId = await _consultaUsuario
                .ObterUsuarioIdPorTelefoneAsync(
                    telefoneNormalizado,
                    cancellationToken);

            if (usuarioId is null)
                return null;

            var cliente = await _repositorioCliente
                .ObterPorUsuarioIdEEmpresaAsync(
                    usuarioId.Value,
                    configuracao.EmpresaId,
                    cancellationToken);

            return cliente is null
    ? null
    : new ClienteWhatsAppResolucaoDto(
        configuracao.EmpresaId,
        cliente.Id);
        }
    }
}
