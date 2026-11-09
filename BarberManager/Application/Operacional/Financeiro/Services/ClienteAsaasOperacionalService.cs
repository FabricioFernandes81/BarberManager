using Application.Common.Exceptions;
using Application.Dispatcher;
using Application.Interfaces.Clientes;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.Commands;
using Application.Operacional.Financeiro.DTOs.Asaas;
using Application.Operacional.Financeiro.Interfaces;

namespace Application.Operacional.Financeiro.Services
{
    public sealed class ClienteAsaasOperacionalService
        : IClienteAsaasOperacionalService
    {
        private readonly IRepositorioCliente _repositorioCliente;
        private readonly IRepositorioClienteAsaasOperacional
            _repositorioClienteAsaas;
        private readonly IRepositorioConfiguracaoAssasClient
            _repositorioConfiguracao;
        private readonly IConsultaUsuarioIdentity _consultaIdentity;
        private readonly IProtecaoCredenciaisAsaas _protecaoCredenciais;
        private readonly IAsaasOperacionalGateway _gateway;
        private readonly IDispatcher _dispatcher;

        public ClienteAsaasOperacionalService(
            IRepositorioCliente repositorioCliente,
            IRepositorioClienteAsaasOperacional repositorioClienteAsaas,
            IRepositorioConfiguracaoAssasClient repositorioConfiguracao,
            IConsultaUsuarioIdentity consultaIdentity,
            IProtecaoCredenciaisAsaas protecaoCredenciais,
            IAsaasOperacionalGateway gateway,
            IDispatcher dispatcher)
        {
            _repositorioCliente = repositorioCliente;
            _repositorioClienteAsaas = repositorioClienteAsaas;
            _repositorioConfiguracao = repositorioConfiguracao;
            _consultaIdentity = consultaIdentity;
            _protecaoCredenciais = protecaoCredenciais;
            _gateway = gateway;
            _dispatcher = dispatcher;
        }

        public async Task<string> ObterOuCriarAsync(
            Guid empresaId,
            Guid clienteId,
            CancellationToken cancellationToken = default)
        {
            var erros = new List<string>();

            if (empresaId == Guid.Empty)
                erros.Add("Empresa inválida.");

            if (clienteId == Guid.Empty)
                erros.Add("Cliente inválido.");

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            // 1. Validar cliente e vínculo com a empresa.
            var cliente = await _repositorioCliente
                .ObterPorIdEEmpresaAsync(
                    clienteId,
                    empresaId,
                    cancellationToken);

            if (cliente is null)
            {
                throw new CustomValidationException(
                    new List<string>
                    {
                        "Cliente não encontrado ou sem vínculo ativo com a empresa."
                    });
            }

            // 2. Verificar associação já existente.
            var associacao = await _repositorioClienteAsaas
                .ObterPorClienteAsync(
                    empresaId,
                    clienteId,
                    cancellationToken);

            if (associacao is not null)
                return associacao.ClienteAsaasId;

            // 3. Recuperar configuração Asaas da empresa.
            var configuracao = await _repositorioConfiguracao
                .ObterPorEmpresaAsync(
                    empresaId,
                    cancellationToken);

            if (configuracao is null || !configuracao.Ativo)
            {
                throw new CustomValidationException(
                    new List<string>
                    {
                        "A integração Asaas não está configurada ou está desativada para esta empresa."
                    });
            }

            if (string.IsNullOrWhiteSpace(
                configuracao.ApiTokenProtegido))
            {
                throw new CustomValidationException(
                    new List<string>
                    {
                        "O token da API Asaas não está configurado."
                    });
            }

            // 4. Consultar dados de contato no Identity.
            var contato = await _consultaIdentity.ObterContatoAsync(
                cliente.UsuarioId,
                cancellationToken);

            // 5. Descriptografar credencial.
            var apiToken = _protecaoCredenciais.Desproteger(
                configuracao.ApiTokenProtegido);

            if (string.IsNullOrWhiteSpace(apiToken))
            {
                throw new CustomValidationException(
                    new List<string>
                    {
                        "Não foi possível recuperar a credencial da integração Asaas."
                    });
            }

            // 6. Criar Customer no Asaas.
            var nome = string.Join(
                " ",
                new[] { cliente.Nome, cliente.Sobrenome }
                    .Where(x => !string.IsNullOrWhiteSpace(x)));

            var dadosCliente = new CriarClienteAsaasOperacionalDto(
                nome,
                cliente.Cpf,
                contato?.Email,
                contato?.Telefone,
                $"{empresaId}:{clienteId}");

            var resultado = await _gateway.CriarClienteAsync(
                apiToken,
                configuracao.Ambiente,
                dadosCliente,
                cancellationToken);

            if (string.IsNullOrWhiteSpace(resultado.Id))
            {
                throw new InvalidOperationException(
                    "O Asaas não retornou o identificador do cliente.");
            }

            // 7. Persistir associação local por comando transacional.
            await _dispatcher.Command<
                RegistrarClienteAsaasOperacionalCommand,
                bool>(
                    new RegistrarClienteAsaasOperacionalCommand(
                        empresaId,
                        clienteId,
                        resultado.Id),
                    cancellationToken);

            return resultado.Id;
        }
    }
}