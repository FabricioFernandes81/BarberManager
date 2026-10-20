using Application.Common.Exceptions;
using Application.Interfaces.Empresas;
using Application.Interfaces.Empresas.Pagamentos;
using Application.Saas.Pagamentos.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Saas;

public sealed class PagamentoService : IPagamentoService
{
    private const string ProvedorAsaas = "Asaas";

    private readonly IRepositorioPagamento _repositorio;
    private readonly IAsaasGateway _asaasGateway;

    public PagamentoService(
        IRepositorioPagamento repositorio,
        IAsaasGateway asaasGateway)
    {
        _repositorio = repositorio;
        _asaasGateway = asaasGateway;
    }

    public async Task<IniciarPagamentoResultadoDto> IniciarAsync(
        IniciarPagamentoRequest request,
        string chaveIdempotencia,
        string remoteIp,
        CancellationToken cancellationToken = default)
    {
        ValidarRequisicao(
            request,
            chaveIdempotencia,
            remoteIp);

        var chave = chaveIdempotencia.Trim();

        // 1. Verificar se a chave já foi utilizada.
        var existente =
            await _repositorio.ObterPorChaveIdempotenciaAsync(
                ProvedorAsaas,
                chave,
                cancellationToken);

        if (existente is not null)
        {
            ValidarCompatibilidadeDaChave(
                existente,
                request);

            return await MontarResultadoExistenteAsync(
                existente,
                cancellationToken);
        }

        // 2. Carregar a fatura, assinatura, empresa e tentativas.
        var fatura =
            await _repositorio.ObterFaturaParaCobrancaAsync(
                request.FaturaId,
                cancellationToken);

        if (fatura is null)
        {
            throw ErroValidacao("Fatura não encontrada.");
        }

        // Esta validação antecipada melhora a resposta ao cliente.
        // A validação definitiva também ocorre no repositório,
        // dentro da transação que bloqueia a fatura.
        if (fatura.Status is StatusFatura.Paga
            or StatusFatura.Cancelada)
        {
            throw ErroValidacao(
                "Não é permitido cobrar uma fatura paga ou cancelada.");
        }

        var empresa = fatura.Assinatura?.Empresa;

        if (empresa is null)
        {
            throw ErroValidacao(
                "Não foi possível localizar a empresa da assinatura.");
        }

        // 3. Preparar a tentativa de pagamento.
        var transacao = new TransacaoPagamento
        {
            FaturaId = fatura.Id,
            Provedor = ProvedorAsaas,
            ChaveIdempotencia = chave,
            MetodoPagamento = request.MetodoPagamento,
            Valor = fatura.Valor,
            Status = StatusTransacaoPagamento.Criada
        };

        // 4. Registrar a tentativa com exclusividade por fatura.
        //
        // O repositório deve:
        // - bloquear a linha da fatura no SQL Server;
        // - revalidar o status da fatura;
        // - verificar a chave de idempotência;
        // - verificar outras tentativas ativas;
        // - persistir a tentativa e liberar a transação.
        //
        // A transação do banco deve terminar antes das chamadas
        // HTTP ao Asaas.
        var resultadoRegistro =
            await _repositorio.RegistrarTentativaExclusivaAsync(
                transacao,
                cancellationToken);

        if (resultadoRegistro.StatusFatura is
            StatusFatura.Paga or StatusFatura.Cancelada)
        {
            throw ErroValidacao(
                "Não é permitido cobrar uma fatura paga ou cancelada.");
        }

        if (!resultadoRegistro.Inserida)
        {
            var transacaoExistente =
                resultadoRegistro.TransacaoExistente;

            if (transacaoExistente is null)
            {
                throw ErroValidacao(
                    "Já existe uma tentativa de pagamento em andamento " +
                    "ou não foi possível registrar uma nova tentativa.");
            }

            // A mesma chave deve representar a mesma operação.
            ValidarCompatibilidadeDaChave(
                transacaoExistente,
                request);

            if (transacaoExistente.ChaveIdempotencia != chave)
            {
                throw ErroValidacao(
                    "Já existe uma tentativa de pagamento em andamento " +
                    "para esta fatura.");
            }

            // Outra requisição já registrou a mesma operação.
            // Não chamar novamente a criação de cobrança no gateway.
            return await MontarResultadoExistenteAsync(
                transacaoExistente,
                cancellationToken);
        }

        // A partir daqui, somente a requisição que inseriu a
        // tentativa deve iniciar a chamada de criação da cobrança.
        try
        {
            // 5. Criar o cliente no Asaas quando necessário.
            if (string.IsNullOrWhiteSpace(
                empresa.ReferenciaClienteAsaas))
            {
                var cliente =
                    await _asaasGateway.CriarClienteAsync(
                        new AsaasCriarClienteDto(
                            empresa.NomeFantasia,
                            empresa.Cnpj,
                            empresa.Email,
                            empresa.Telefone,
                            empresa.Id.ToString()),
                        cancellationToken);

                empresa.ReferenciaClienteAsaas = cliente.Id;
                transacao.ReferenciaClienteAsaas = cliente.Id;

                // Persistir a referência do cliente antes de
                // continuar com a criação da cobrança.
                await _repositorio.SalvarAlteracoesAsync(
                    cancellationToken);
            }
            else
            {
                transacao.ReferenciaClienteAsaas =
                    empresa.ReferenciaClienteAsaas;
            }

            // 6. Criar a cobrança no Asaas.
            var vencimento = fatura.Vencimento.Date < DateTime.Today
                ? DateTime.Today
                : fatura.Vencimento.Date;

            var cobranca =
                await _asaasGateway.CriarCobrancaAsync(
                    new AsaasCriarCobrancaDto(
                        empresa.ReferenciaClienteAsaas!,
                        fatura.Id,
                        transacao.Id,
                        fatura.Valor,
                        vencimento,
                        request.MetodoPagamento,
                        $"Fatura BarberManager {fatura.Id}",
                        request.MetodoPagamento ==
                            MetodoPagamento.CartaoCredito
                                ? remoteIp
                                : null,
                        request.MetodoPagamento ==
                            MetodoPagamento.CartaoCredito
                                ? request.CartaoCredito
                                : null),
                    cancellationToken);

            // 7. Persistir o resultado retornado pelo gateway.
            transacao.ReferenciaGateway = cobranca.Id;
            transacao.Status = MapearStatus(cobranca.Status);

            fatura.ReferenciaGateway = cobranca.Id;

            await _repositorio.SalvarAlteracoesAsync(
                cancellationToken);

            // 8. Obter o QR Code após a criação da cobrança Pix.
            AsaasPixResultadoDto? pix = null;

            if (request.MetodoPagamento == MetodoPagamento.Pix)
            {
                pix = await _asaasGateway.ObterQrCodePixAsync(
                    cobranca.Id,
                    cancellationToken);
            }

            return new IniciarPagamentoResultadoDto
            {
                TransacaoId = transacao.Id,
                FaturaId = fatura.Id,
                Provedor = ProvedorAsaas,
                MetodoPagamento =
                    request.MetodoPagamento.ToString(),
                Status = transacao.Status.ToString(),
                Valor = transacao.Valor,
                ReferenciaGateway = cobranca.Id,
                UrlFatura = cobranca.InvoiceUrl,
                PixImagemBase64 = pix?.EncodedImage,
                PixCopiaECola = pix?.Payload,
                PixExpiracao = pix?.ExpirationDate,
                CartaoBandeira = cobranca.CreditCardBrand,
                CartaoFinal = cobranca.CreditCardNumber
            };
        }
        catch (Exception ex) when (
    ex is TaskCanceledException ||
    ex is HttpRequestException httpEx &&
        (httpEx.StatusCode is null ||
         (int)httpEx.StatusCode == 408 ||
         (int)httpEx.StatusCode == 429 ||
         (int)httpEx.StatusCode >= 500))
        {
            // O Asaas pode ter criado a cobrança mesmo que a resposta
            // tenha sido perdida ou que a requisição tenha sofrido timeout.
            try
            {
                var referenciaExterna = transacao.Id.ToString();

                var cobrancaRecuperada =
                    await _asaasGateway.ObterCobrancaPorReferenciaExternaAsync(
                        referenciaExterna,
                        CancellationToken.None);

                if (cobrancaRecuperada is not null)
                {
                    // Não aceitar uma cobrança associada a outra transação.
                    if (!string.Equals(
                            cobrancaRecuperada.ExternalReference,
                            referenciaExterna,
                            StringComparison.Ordinal))
                    {
                        throw new InvalidOperationException(
                            "A cobrança recuperada não corresponde à transação.");
                    }

                    // Recuperar os dados confirmados pelo Asaas.
                    transacao.ReferenciaGateway = cobrancaRecuperada.Id;
                    transacao.Status = MapearStatus(cobrancaRecuperada.Status);
                    transacao.CodigoErro = null;
                    transacao.MensagemErro = null;

                    fatura.ReferenciaGateway = cobrancaRecuperada.Id;

                    await _repositorio.SalvarAlteracoesAsync(
                        CancellationToken.None);

                    // A cobrança está recuperada. Não criar outra cobrança.
                    // O QR Code poderá ser obtido em uma nova chamada,
                    // usando a mesma chave de idempotência.
                    return new IniciarPagamentoResultadoDto
                    {
                        TransacaoId = transacao.Id,
                        FaturaId = fatura.Id,
                        Provedor = ProvedorAsaas,
                        MetodoPagamento =
                            transacao.MetodoPagamento.ToString(),
                        Status = transacao.Status.ToString(),
                        Valor = transacao.Valor,
                        ReferenciaGateway = cobrancaRecuperada.Id,
                        UrlFatura = cobrancaRecuperada.InvoiceUrl,
                        CartaoBandeira = cobrancaRecuperada.CreditCardBrand,
                        CartaoFinal = cobrancaRecuperada.CreditCardNumber
                    };
                }
            }
            catch (Exception)
            {
                // A consulta ou a persistência da recuperação também falhou.
                // Não presumir que a cobrança inexiste.
                // Preservar o estado indeterminado para reconciliação posterior.
            }

            transacao.Status =
                StatusTransacaoPagamento.EmProcessamento;

            transacao.CodigoErro =
                "RESULTADO_GATEWAY_INDETERMINADO";

            transacao.MensagemErro =
                "O resultado da solicitação ao gateway precisa ser reconciliado.";

            await _repositorio.SalvarAlteracoesAsync(
                CancellationToken.None);

            throw new InvalidOperationException(
                "Não foi possível confirmar o resultado da solicitação ao Asaas. " +
                "A transação foi mantida para reconciliação. " +
                "Consulte seu status antes de tentar novamente.",
                ex);
        }
    }

    private static void ValidarCompatibilidadeDaChave(
        TransacaoPagamento existente,
        IniciarPagamentoRequest request)
    {
        if (existente.FaturaId != request.FaturaId ||
            existente.MetodoPagamento != request.MetodoPagamento)
        {
            throw ErroValidacao(
                "A chave de idempotência já foi utilizada em outra operação.");
        }
    }

    private async Task<IniciarPagamentoResultadoDto>
        MontarResultadoExistenteAsync(
            TransacaoPagamento transacao,
            CancellationToken cancellationToken)
    {
        AsaasPixResultadoDto? pix = null;

        if (transacao.MetodoPagamento == MetodoPagamento.Pix &&
            !string.IsNullOrWhiteSpace(transacao.ReferenciaGateway))
        {
            pix = await _asaasGateway.ObterQrCodePixAsync(
                transacao.ReferenciaGateway,
                cancellationToken);
        }

        return new IniciarPagamentoResultadoDto
        {
            TransacaoId = transacao.Id,
            FaturaId = transacao.FaturaId,
            Provedor = transacao.Provedor,
            MetodoPagamento =
                transacao.MetodoPagamento.ToString(),
            Status = transacao.Status.ToString(),
            Valor = transacao.Valor,
            ReferenciaGateway = transacao.ReferenciaGateway,
            PixImagemBase64 = pix?.EncodedImage,
            PixCopiaECola = pix?.Payload,
            PixExpiracao = pix?.ExpirationDate
        };
    }

    private static StatusTransacaoPagamento MapearStatus(
        string status)
    {
        return status.Trim().ToUpperInvariant() switch
        {
            "PENDING" =>
                StatusTransacaoPagamento.Pendente,

            "PROCESSING" =>
                StatusTransacaoPagamento.EmProcessamento,

            "AWAITING_RISK_ANALYSIS" =>
                StatusTransacaoPagamento.EmProcessamento,

            "CONFIRMED" =>
                StatusTransacaoPagamento.Aprovada,

            "RECEIVED" =>
                StatusTransacaoPagamento.Aprovada,

            "REPROVED" =>
                StatusTransacaoPagamento.Recusada,

            "CANCELED" =>
                StatusTransacaoPagamento.Cancelada,

            "REFUNDED" =>
                StatusTransacaoPagamento.Estornada,

            "OVERDUE" =>
                StatusTransacaoPagamento.Expirada,

            _ =>
                StatusTransacaoPagamento.Criada
        };
    }

    private static void ValidarRequisicao(
        IniciarPagamentoRequest request,
        string chaveIdempotencia,
        string remoteIp)
    {
        if (request.FaturaId == Guid.Empty)
        {
            throw ErroValidacao(
                "A identificação da fatura é obrigatória.");
        }

        if (!Enum.IsDefined(request.MetodoPagamento) ||
            request.MetodoPagamento is not (
                MetodoPagamento.Pix
                or MetodoPagamento.CartaoCredito))
        {
            throw ErroValidacao(
                "Método de pagamento não suportado.");
        }

        if (string.IsNullOrWhiteSpace(chaveIdempotencia) ||
            chaveIdempotencia.Trim().Length > 100)
        {
            throw ErroValidacao(
                "Informe uma chave de idempotência válida, " +
                "com até 100 caracteres.");
        }

        if (request.MetodoPagamento == MetodoPagamento.Pix &&
            request.CartaoCredito is not null)
        {
            throw ErroValidacao(
                "Não envie dados de cartão para pagamento Pix.");
        }

        if (request.MetodoPagamento ==
            MetodoPagamento.CartaoCredito)
        {
            var cartao = request.CartaoCredito;

            if (cartao is null ||
                string.IsNullOrWhiteSpace(cartao.Numero) ||
                string.IsNullOrWhiteSpace(cartao.NomeTitular) ||
                string.IsNullOrWhiteSpace(cartao.MesValidade) ||
                string.IsNullOrWhiteSpace(cartao.AnoValidade) ||
                string.IsNullOrWhiteSpace(cartao.Cvv) ||
                string.IsNullOrWhiteSpace(cartao.EmailTitular) ||
                string.IsNullOrWhiteSpace(cartao.CpfCnpjTitular) ||
                string.IsNullOrWhiteSpace(cartao.CepTitular) ||
                string.IsNullOrWhiteSpace(cartao.NumeroEnderecoTitular) ||
                string.IsNullOrWhiteSpace(cartao.TelefoneTitular))
            {
                throw ErroValidacao(
                    "Preencha todos os dados necessários do cartão de crédito.");
            }

            if (string.IsNullOrWhiteSpace(remoteIp))
            {
                throw ErroValidacao(
                    "O IP remoto é obrigatório para pagamento com cartão.");
            }
        }
    }

    private static CustomValidationException ErroValidacao(
        string mensagem)
    {
        return new CustomValidationException(
            new[] { mensagem });
    }
}