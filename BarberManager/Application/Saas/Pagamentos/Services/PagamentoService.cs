using Application.Common.Exceptions;
using Application.Interfaces.Empresas;
using Application.Interfaces.Empresas.Pagamentos;
using Application.Saas.Pagamentos.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Saas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Pagamentos.Services
{
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
            ValidarRequisicao(request, chaveIdempotencia, remoteIp);

            // 1. Idempotência: não iniciar uma segunda cobrança
            // para a mesma operação lógica.
            var existente =
                await _repositorio.ObterPorChaveIdempotenciaAsync(
                    ProvedorAsaas,
                    chaveIdempotencia.Trim(),
                    cancellationToken);

            if (existente is not null)
            {
                if (existente.FaturaId != request.FaturaId ||
                    existente.MetodoPagamento != request.MetodoPagamento)
                {
                    throw ErroValidacao(
                        "A chave de idempotência já foi utilizada em outra operação.");
                }

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
                throw ErroValidacao("Fatura não encontrada.");

            if (fatura.Status is StatusFatura.Paga or StatusFatura.Cancelada)
            {
                throw ErroValidacao(
                    "Não é permitido cobrar uma fatura paga ou cancelada.");
            }

            if (fatura.TransacoesPagamento.Any(t =>
                t.Status is StatusTransacaoPagamento.Criada
                    or StatusTransacaoPagamento.Pendente
                    or StatusTransacaoPagamento.EmProcessamento))
            {
                throw ErroValidacao(
                    "Já existe uma tentativa de pagamento em andamento para esta fatura.");
            }

            var empresa = fatura.Assinatura.Empresa;

            if (empresa is null)
            {
                throw ErroValidacao(
                    "Não foi possível localizar a empresa da assinatura.");
            }

            // 3. Registrar a tentativa local antes de chamar o gateway.
            var transacao = new TransacaoPagamento
            {
                FaturaId = fatura.Id,
                Provedor = ProvedorAsaas,
                ChaveIdempotencia = chaveIdempotencia.Trim(),
                MetodoPagamento = request.MetodoPagamento,
                Valor = fatura.Valor,
                Status = StatusTransacaoPagamento.Criada
            };

            await _repositorio.AdicionarTentativaAsync(
                transacao,
                cancellationToken);

            await _repositorio.SalvarAlteracoesAsync(
                cancellationToken);

            try
            {
                // 4. Criar o cliente no Asaas apenas quando ainda
                // não houver referência persistida para a empresa.
                if (string.IsNullOrWhiteSpace(
                    empresa.ReferenciaClienteAsaas))
                {
                    var cliente = await _asaasGateway.CriarClienteAsync(
                        new AsaasCriarClienteDto(
                            empresa.NomeFantasia,
                            empresa.Cnpj,
                            empresa.Email,
                            empresa.Telefone,
                            empresa.Id.ToString()),
                        cancellationToken);

                    empresa.ReferenciaClienteAsaas = cliente.Id;

                    transacao.ReferenciaClienteAsaas = cliente.Id;

                    // Salvar antes da próxima chamada HTTP.
                    await _repositorio.SalvarAlteracoesAsync(
                        cancellationToken);
                }
                else
                {
                    transacao.ReferenciaClienteAsaas =
                        empresa.ReferenciaClienteAsaas;
                }

                // 5. Criar a cobrança no Asaas.
                // Para cartão, o gateway recebe os dados do cartão
                // e o IP remoto. Para Pix, esses campos são nulos.
                var vencimento = fatura.Vencimento.Date < DateTime.Today
                    ? DateTime.Today
                    : fatura.Vencimento.Date;

                var cobranca = await _asaasGateway.CriarCobrancaAsync(
                    new AsaasCriarCobrancaDto(
                        empresa.ReferenciaClienteAsaas!,
                        fatura.Id,
                        transacao.Id,
                        fatura.Valor,
                        vencimento,
                        request.MetodoPagamento,
                        $"Fatura BarberManager {fatura.Id}",
                        request.MetodoPagamento == MetodoPagamento.CartaoCredito
                            ? remoteIp
                            : null,
                        request.MetodoPagamento == MetodoPagamento.CartaoCredito
                            ? request.CartaoCredito
                            : null),
                    cancellationToken);

                // 6. Persistir o resultado inicial do gateway.
                transacao.ReferenciaGateway = cobranca.Id;
                transacao.Status = MapearStatus(cobranca.Status);

                fatura.ReferenciaGateway = cobranca.Id;

                await _repositorio.SalvarAlteracoesAsync(
                    cancellationToken);

                // 7. O QR Code é obtido depois de a cobrança existir.
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
                    MetodoPagamento = request.MetodoPagamento.ToString(),
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
                ex is HttpRequestException or TaskCanceledException)
            {
                // Um timeout pode acontecer depois de o Asaas ter
                // criado a cobrança. Não marcar como recusada e não
                // gerar outra cobrança automaticamente.
                transacao.Status = StatusTransacaoPagamento.EmProcessamento;
                transacao.CodigoErro = "RESULTADO_GATEWAY_INDETERMINADO";
                transacao.MensagemErro =
                    "O resultado da solicitação ao gateway precisa ser reconciliado.";

                // Persistir mesmo se o token original tiver sido cancelado.
                await _repositorio.SalvarAlteracoesAsync(
                    CancellationToken.None);

                throw new InvalidOperationException(
                    "Não foi possível confirmar o resultado da solicitação ao Asaas. " +
                    "Consulte o status da transação antes de tentar novamente.",
                    ex);
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
                MetodoPagamento = transacao.MetodoPagamento.ToString(),
                Status = transacao.Status.ToString(),
                Valor = transacao.Valor,
                ReferenciaGateway = transacao.ReferenciaGateway,
                PixImagemBase64 = pix?.EncodedImage,
                PixCopiaECola = pix?.Payload,
                PixExpiracao = pix?.ExpirationDate
            };
        }

        private static StatusTransacaoPagamento MapearStatus(string status)
        {
            return status.Trim().ToUpperInvariant() switch
            {
                "PENDING" => StatusTransacaoPagamento.Pendente,
                "PROCESSING" => StatusTransacaoPagamento.EmProcessamento,
                "AWAITING_RISK_ANALYSIS" =>
                    StatusTransacaoPagamento.EmProcessamento,
                "CONFIRMED" => StatusTransacaoPagamento.Aprovada,
                "RECEIVED" => StatusTransacaoPagamento.Aprovada,
                "REPROVED" => StatusTransacaoPagamento.Recusada,
                "CANCELED" => StatusTransacaoPagamento.Cancelada,
                "REFUNDED" => StatusTransacaoPagamento.Estornada,
                "OVERDUE" => StatusTransacaoPagamento.Expirada,
                _ => StatusTransacaoPagamento.Criada
            };
        }

        private static void ValidarRequisicao(
            IniciarPagamentoRequest request,
            string chaveIdempotencia,
            string remoteIp)
        {
            if (request.FaturaId == Guid.Empty)
                throw ErroValidacao("A identificação da fatura é obrigatória.");

            if (!Enum.IsDefined(request.MetodoPagamento) ||
                request.MetodoPagamento is not (
                    MetodoPagamento.Pix or MetodoPagamento.CartaoCredito))
            {
                throw ErroValidacao("Método de pagamento não suportado.");
            }

            if (string.IsNullOrWhiteSpace(chaveIdempotencia) ||
                chaveIdempotencia.Trim().Length > 100)
            {
                throw ErroValidacao(
                    "Informe uma chave de idempotência válida, com até 100 caracteres.");
            }

            if (request.MetodoPagamento == MetodoPagamento.Pix &&
                request.CartaoCredito is not null)
            {
                throw ErroValidacao(
                    "Não envie dados de cartão para pagamento Pix.");
            }

            if (request.MetodoPagamento == MetodoPagamento.CartaoCredito)
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
            return new CustomValidationException(new[] { mensagem });
        }
    }
}
