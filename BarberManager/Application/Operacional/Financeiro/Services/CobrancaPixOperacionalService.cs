
using Application.Common.Exceptions;
using Application.Dispatcher;
using Application.Interfaces.Financeiro;
using Application.Operacional.Financeiro.Commands;
using Application.Operacional.Financeiro.DTOs.Asaas;
using Application.Operacional.Financeiro.Interfaces;
using Application.Common.Tenancy;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Financeiro;

namespace Application.Operacional.Financeiro.Services;

public sealed class CobrancaPixOperacionalService
    : ICobrancaPixOperacionalService
{
    private readonly IRepositorioCobrancaOperacional _repositorioCobranca;
    private readonly IRepositorioRecebimento _repositorioRecebimento;
    private readonly IRepositorioConfiguracaoAssasClient _repositorioConfiguracao;
    private readonly IProtecaoCredenciaisAsaas _protecaoCredenciais;
    private readonly IClienteAsaasOperacionalService _clienteAsaas;
    private readonly IAsaasOperacionalGateway _gateway;
    private readonly IDispatcher _dispatcher;
    private readonly ITenantContext _tenantContext;

    public CobrancaPixOperacionalService(
        IRepositorioCobrancaOperacional repositorioCobranca,
        IRepositorioRecebimento repositorioRecebimento,
        IRepositorioConfiguracaoAssasClient repositorioConfiguracao,
        IProtecaoCredenciaisAsaas protecaoCredenciais,
        IClienteAsaasOperacionalService clienteAsaas,
        IAsaasOperacionalGateway gateway,
        IDispatcher dispatcher,
        ITenantContext tenantContext)
    {
        _repositorioCobranca = repositorioCobranca;
        _repositorioRecebimento = repositorioRecebimento;
        _repositorioConfiguracao = repositorioConfiguracao;
        _protecaoCredenciais = protecaoCredenciais;
        _clienteAsaas = clienteAsaas;
        _gateway = gateway;
        _dispatcher = dispatcher;
        _tenantContext = tenantContext;
    }

    public async Task<CriarCobrancaPixResponse> IniciarAsync(
        Guid cobrancaId,
        CancellationToken cancellationToken)
    {
        // 1. Validar tenant e identificador.
        if (!_tenantContext.IsAvailable ||
            _tenantContext.EmpresaId == Guid.Empty)
        {
            throw Erro("Não foi possível identificar a empresa.");
        }

        if (cobrancaId == Guid.Empty)
            throw Erro("O identificador da cobrança é obrigatório.");

        var empresaId = _tenantContext.EmpresaId;

        // 2. Recuperar a reserva.
        var cobranca = await _repositorioCobranca.ObterPorIdAsync(
            empresaId,
            cobrancaId,
            cancellationToken);

        if (cobranca is null)
            throw Erro("Cobrança não encontrada para esta empresa.");

        /*  if (cobranca.Status != StatusCobrancaOperacional.Reservada)
              throw Erro(
                  $"A cobrança não pode ser iniciada. Status atual: {cobranca.Status}.");

          if (!string.IsNullOrWhiteSpace(cobranca.GatewayPagamentoId))
              throw Erro("A cobrança já possui identificador no gateway.");*/
        // Cenário 1: a cobrança já foi criada no Asaas.
        // Não criar outra. Apenas recuperar o QR Code.
        if (cobranca.Status == StatusCobrancaOperacional.Pendente &&
            !string.IsNullOrWhiteSpace(cobranca.GatewayPagamentoId))
        {
            return await RecuperarQrCodeAsync(
                cobranca,
                cancellationToken);
        }

        // 3. Recuperar a conta a receber.
        var conta = await _repositorioRecebimento
            .ObterContaParaRecebimentoAsync(
                empresaId,
                cobranca.ContaReceberId,
                cancellationToken);

        if (conta is null)
            throw Erro("Conta a receber não encontrada.");

        if (!conta.ClienteId.HasValue ||
            conta.ClienteId.Value == Guid.Empty)
        {
            throw Erro("A conta não possui cliente vinculado.");
        }

        switch (cobranca.Status)
        {
            case StatusCobrancaOperacional.Pendente:

                if (string.IsNullOrWhiteSpace(cobranca.GatewayPagamentoId))
                {
                    throw Erro(
                        "A cobrança está pendente, mas não possui identificador no gateway.");
                }

                return await RecuperarQrCodeAsync(
                    cobranca,
                    cancellationToken);

            case StatusCobrancaOperacional.EmProcessamento:

                return await ReconciliarCobrancaAsync(
                    cobranca,
                    cancellationToken);

            case StatusCobrancaOperacional.Reservada:

                break;

            default:

                throw Erro(
                    $"A cobrança não pode ser iniciada. Status atual: {cobranca.Status}.");
        }

        // 4. Revalidar o saldo antes de chamar o gateway.
        var saldoAtual = decimal.Round(
            conta.ObterSaldoRestante(),
            2,
            MidpointRounding.AwayFromZero);

        if (saldoAtual < cobranca.Valor)
        {
            throw Erro(
                "O saldo atual da conta é inferior ao valor reservado para cobrança.");
        }

        // 5. Recuperar configuração Asaas.
        var configuracao = await _repositorioConfiguracao
            .ObterPorEmpresaAsync(
                empresaId,
                cancellationToken);

        if (configuracao is null || !configuracao.Ativo)
            throw Erro("A integração Asaas não está configurada ou está desativada.");

        if (string.IsNullOrWhiteSpace(configuracao.ApiTokenProtegido))
            throw Erro("O token da API Asaas não está configurado.");

        var apiToken = _protecaoCredenciais.Desproteger(
            configuracao.ApiTokenProtegido);

        if (string.IsNullOrWhiteSpace(apiToken))
            throw Erro("Não foi possível recuperar a credencial Asaas.");

        // 6. Obter ou criar o cliente no Asaas.
        var clienteAsaasId = await _clienteAsaas.ObterOuCriarAsync(
            empresaId,
            conta.ClienteId.Value,
            cancellationToken);

        if (string.IsNullOrWhiteSpace(clienteAsaasId))
            throw Erro("Não foi possível identificar o cliente no Asaas.");

        // 7. Assumir atomicamente o processamento.
        // A transação termina antes da chamada HTTP de criação da cobrança.
        var iniciouProcessamento = await _dispatcher.Command<
            IniciarProcessamentoCobrancaPixCommand,
            bool>(
                new IniciarProcessamentoCobrancaPixCommand(cobrancaId),
                cancellationToken);

        if (!iniciouProcessamento)
        {
            throw Erro(
                "A cobrança já está sendo processada ou não está mais disponível.");
        }

        // 8. Criar cobrança Pix no Asaas.
        var dadosCobranca = new CriarCobrancaOperacionalDto(
            clienteAsaasId,
            cobranca.ExternalReference,
            cobranca.Valor,
            conta.DataVencimento,
            MetodoPagamento.Pix,
            conta.Descricao,
            null,
            null);

        var resultadoGateway = await _gateway.CriarCobrancaAsync(
            apiToken,
            configuracao.Ambiente,
            dadosCobranca,
            cancellationToken);

        if (string.IsNullOrWhiteSpace(resultadoGateway.Id))
        {
            throw new InvalidOperationException(
                "O Asaas não retornou o identificador da cobrança.");
        }

        // 9. Persistir o identificador do gateway.
        // O comando possui sua própria transação curta.
        await _dispatcher.Command<
            RegistrarPagamentoGatewayCommand,
            Guid>(
                new RegistrarPagamentoGatewayCommand(
                    cobrancaId,
                    resultadoGateway.Id),
                cancellationToken);

        // 10. Consultar QR Code Pix.
        var pix = await _gateway.ObterQrCodePixAsync(
            apiToken,
            configuracao.Ambiente,
            resultadoGateway.Id,
            cancellationToken);

        if (string.IsNullOrWhiteSpace(pix.Payload) ||
            string.IsNullOrWhiteSpace(pix.EncodedImage))
        {
            throw new InvalidOperationException(
                "O Asaas não retornou os dados do QR Code Pix.");
        }

        return new CriarCobrancaPixResponse(
            cobranca.Id,
            cobranca.ContaReceberId,
            resultadoGateway.Id,
            cobranca.Valor,
            resultadoGateway.Status,
            pix.Payload,
            pix.EncodedImage,
            pix.ExpirationDate);
    }

    private static CustomValidationException Erro(string mensagem)
    {
        return new CustomValidationException(
            new[] { mensagem });
    }

    private async Task<CriarCobrancaPixResponse> ReconciliarCobrancaAsync(
    CobrancaOperacional cobranca,
    CancellationToken cancellationToken)
    {
        var configuracao = await _repositorioConfiguracao
            .ObterPorEmpresaAsync(
                cobranca.EmpresaId,
                cancellationToken);

        if (configuracao is null || !configuracao.Ativo)
            throw Erro("A integração Asaas não está configurada ou está desativada.");

        if (string.IsNullOrWhiteSpace(configuracao.ApiTokenProtegido))
            throw Erro("O token da API Asaas não está configurado.");

        var apiToken = _protecaoCredenciais.Desproteger(
            configuracao.ApiTokenProtegido);

        if (string.IsNullOrWhiteSpace(apiToken))
            throw Erro("Não foi possível recuperar a credencial Asaas.");

        var cobrancasGateway = await _gateway.ListarCobrancasPorReferenciaAsync(
            apiToken,
            configuracao.Ambiente,
            cobranca.ExternalReference,
            cancellationToken);

        if (cobrancasGateway.Count == 0)
        {
            throw Erro(
                "Não foi encontrada cobrança no Asaas para esta referência. " +
                "A cobrança permanece em processamento e deverá ser reconciliada novamente.");
        }

        if (cobrancasGateway.Count > 1)
        {
            throw Erro(
                "Foram encontradas múltiplas cobranças no Asaas para a mesma referência. " +
                "É necessária análise antes de continuar.");
        }

        var pagamento = cobrancasGateway.Single();

        if (!string.Equals(
                pagamento.ExternalReference,
                cobranca.ExternalReference,
                StringComparison.Ordinal))
        {
            throw Erro("A referência externa retornada pelo Asaas não corresponde à cobrança.");
        }

        if (!string.Equals(
                pagamento.BillingType,
                "PIX",
                StringComparison.OrdinalIgnoreCase))
        {
            throw Erro("O pagamento localizado no Asaas não é do tipo Pix.");
        }

        if (pagamento.Valor != cobranca.Valor)
            throw Erro("O valor da cobrança localizada no Asaas não corresponde ao valor reservado.");

        if (string.IsNullOrWhiteSpace(pagamento.Customer))
            throw Erro("O Asaas não retornou o identificador do cliente da cobrança.");

        // Confirma que o cliente corresponde ao cliente da conta a receber.
        var conta = await _repositorioRecebimento
            .ObterContaParaRecebimentoAsync(
                cobranca.EmpresaId,
                cobranca.ContaReceberId,
                cancellationToken);

        if (conta is null || !conta.ClienteId.HasValue)
            throw Erro("Não foi possível validar o cliente da conta a receber.");

        var clienteAsaasId = await _clienteAsaas.ObterClienteAsaasIdAsync(
    cobranca.EmpresaId,
    conta.ClienteId.Value,
    cancellationToken);

        if (string.IsNullOrWhiteSpace(clienteAsaasId))
        {
            throw Erro(
                "O cliente não possui vínculo previamente registrado no Asaas. " +
                "A reconciliação não pode criar um novo cliente automaticamente.");
        }
        /*
        var clienteAsaasId = await _clienteAsaas.ObterOuCriarAsync(
            cobranca.EmpresaId,
            conta.ClienteId.Value,
            cancellationToken);*/

        if (!string.Equals(
                pagamento.Customer,
                clienteAsaasId,
                StringComparison.Ordinal))
        {
            throw Erro("O cliente da cobrança localizada no Asaas não corresponde ao cliente esperado.");
        }

        await _dispatcher.Command<RegistrarPagamentoGatewayCommand, Guid>(
            new RegistrarPagamentoGatewayCommand(
                cobranca.Id,
                pagamento.Id),
            cancellationToken);

        // Após registrar o ID, a cobrança passa para Pendente.
        var pix = await _gateway.ObterQrCodePixAsync(
            apiToken,
            configuracao.Ambiente,
            pagamento.Id,
            cancellationToken);

        if (string.IsNullOrWhiteSpace(pix.Payload) ||
            string.IsNullOrWhiteSpace(pix.EncodedImage))
        {
            throw new InvalidOperationException(
                "O Asaas não retornou os dados do QR Code Pix.");
        }

        return new CriarCobrancaPixResponse(
            cobranca.Id,
            cobranca.ContaReceberId,
            pagamento.Id,
            cobranca.Valor,
            pagamento.Status,
            pix.Payload,
            pix.EncodedImage,
            pix.ExpirationDate);
    }

    private async Task<CriarCobrancaPixResponse> RecuperarQrCodeAsync(
    Domain.Entities.Financeiro.CobrancaOperacional cobranca,
    CancellationToken cancellationToken)
    {
        var configuracao = await _repositorioConfiguracao
            .ObterPorEmpresaAsync(
                cobranca.EmpresaId,
                cancellationToken);

        if (configuracao is null || !configuracao.Ativo)
            throw Erro("A integração Asaas não está configurada ou está desativada.");

        if (string.IsNullOrWhiteSpace(configuracao.ApiTokenProtegido))
            throw Erro("O token da API Asaas não está configurado.");

        var apiToken = _protecaoCredenciais.Desproteger(
            configuracao.ApiTokenProtegido);

        if (string.IsNullOrWhiteSpace(apiToken))
            throw Erro("Não foi possível recuperar a credencial Asaas.");

        var pix = await _gateway.ObterQrCodePixAsync(
            apiToken,
            configuracao.Ambiente,
            cobranca.GatewayPagamentoId!,
            cancellationToken);

        if (string.IsNullOrWhiteSpace(pix.Payload) ||
            string.IsNullOrWhiteSpace(pix.EncodedImage))
        {
            throw new InvalidOperationException(
                "O Asaas não retornou os dados do QR Code Pix.");
        }

        return new CriarCobrancaPixResponse(
            cobranca.Id,
            cobranca.ContaReceberId,
            cobranca.GatewayPagamentoId!,
            cobranca.Valor,
            cobranca.Status.ToString(),
            pix.Payload,
            pix.EncodedImage,
            pix.ExpirationDate);
    }


}
