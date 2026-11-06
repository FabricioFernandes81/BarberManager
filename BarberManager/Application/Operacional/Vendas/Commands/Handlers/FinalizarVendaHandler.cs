```csharp
using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Financeiro;
using Application.Interfaces.Produtos;
using Application.Interfaces.Vendas;
using Application.Operacional.Vendas.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Estoque;
using Domain.Entities.Financeiro;
using Domain.Entities.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Operacional.Vendas.Commands.Handlers
{
    public sealed class FinalizarVendaHandler
        : ICommandHandler<FinalizarVendaCommand, FinalizarVendaDto>
    {
        private readonly IRepositorioVenda _repositorioVenda;
        private readonly IRepositorioRecebimento _repositorioRecebimento;
        private readonly IRepositorioCaixa _repositorioCaixa;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;
        private readonly IRepositorioProdutos _repositorioProdutos;

        public FinalizarVendaHandler(
            IRepositorioVenda repositorioVenda,
            IRepositorioRecebimento repositorioRecebimento,
            IRepositorioCaixa repositorioCaixa,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado,
            IRepositorioProdutos repositorioProdutos)
        {
            _repositorioVenda = repositorioVenda;
            _repositorioRecebimento = repositorioRecebimento;
            _repositorioCaixa = repositorioCaixa;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
            _repositorioProdutos = repositorioProdutos;
        }

        public async Task<FinalizarVendaDto> Handle(
            FinalizarVendaCommand command,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // 1. Validar o contexto da empresa.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw Erro(
                    "Não foi possível identificar a empresa do usuário.");
            }

            var empresaId = _tenantContext.EmpresaId;

            // 2. Validar o usuário autenticado.
            if (!_usuarioLogado.IsAuthenticated)
            {
                throw Erro(
                    "É necessário estar autenticado para finalizar a venda.");
            }

            var usuarioId = _usuarioLogado.UsuarioId;

            if (usuarioId == Guid.Empty)
            {
                throw Erro(
                    "O identificador do usuário autenticado é inválido.");
            }

            // 3. Validar o identificador da venda.
            if (command.VendaId == Guid.Empty)
            {
                throw Erro(
                    "O identificador da venda é obrigatório.");
            }

            // 4. Normalizar e validar os pagamentos.
            // A ausência de pagamentos é permitida.
            var pagamentos = command.Pagamentos
                ?? Array.Empty<PagamentoVendaCommand>();

            if (pagamentos.Any(
                p => p.FormaPagamentoId == Guid.Empty))
            {
                throw Erro(
                    "Todas as formas de pagamento devem possuir identificadores válidos.");
            }

            if (pagamentos.Any(p => p.Valor <= 0))
            {
                throw Erro(
                    "O valor de cada pagamento deve ser maior que zero.");
            }

            if (pagamentos.Any(
                p => p.Valor != decimal.Round(p.Valor, 2)))
            {
                throw Erro(
                    "Os valores dos pagamentos devem possuir no máximo duas casas decimais.");
            }

            if (pagamentos
                .GroupBy(p => p.FormaPagamentoId)
                .Any(g => g.Count() > 1))
            {
                throw Erro(
                    "Informe cada forma de pagamento apenas uma vez.");
            }

            // 5. Carregar a venda rastreada pelo EF Core.
            var venda = await _repositorioVenda.ObterParaAtualizacaoAsync(
                empresaId,
                command.VendaId,
                cancellationToken);

            if (venda is null)
            {
                throw Erro(
                    "Venda não encontrada para a empresa informada.");
            }

            // 6. Impedir a finalização repetida.
            if (venda.Status != StatusVenda.Aberta)
            {
                throw Erro(
                    "Só é possível finalizar uma venda que esteja aberta.");
            }

            if (venda.Itens.Count == 0)
            {
                throw Erro(
                    "Não é possível finalizar uma venda sem itens.");
            }

            if (venda.ValorTotal <= 0)
            {
                throw Erro(
                    "Não é possível finalizar uma venda com valor total zero ou negativo.");
            }

            // 7. Validar a observação.
            var observacao = command.Observacao?.Trim();

            if (observacao is { Length: > 500 })
            {
                throw Erro(
                    "A observação não pode ultrapassar 500 caracteres.");
            }

            // 8. O caixa só é necessário quando houver pagamento presencial.
            Caixa? caixa = null;

            if (pagamentos.Count > 0)
            {
                await _repositorioCaixa.BloquearOperacaoCaixaAsync(
                    empresaId,
                    usuarioId,
                    cancellationToken);

                caixa = await _repositorioRecebimento.ObterCaixaAbertoAsync(
                    empresaId,
                    usuarioId,
                    cancellationToken);

                if (caixa is null)
                {
                    throw Erro(
                        "Não existe um caixa aberto para este usuário nesta empresa. " +
                        "Abra o caixa antes de registrar o recebimento presencial.");
                }
            }

            // 9. Validar os valores da venda e os pagamentos.
            var valorVenda = decimal.Round(
                venda.ValorTotal,
                2,
                MidpointRounding.AwayFromZero);

            if (valorVenda <= 0)
            {
                throw Erro(
                    "O valor total da venda deve ser maior que zero.");
            }

            var formasPagamento = new Dictionary<Guid, FormaPagamento>();

            // A quitação integral e a validação das formas de pagamento
            // são exigidas somente quando há pagamento presencial.
            if (pagamentos.Count > 0)
            {
                var totalPagamentos = decimal.Round(
                    pagamentos.Sum(p => p.Valor),
                    2,
                    MidpointRounding.AwayFromZero);

                if (totalPagamentos != valorVenda)
                {
                    throw Erro(
                        $"A soma dos pagamentos ({totalPagamentos:N2}) " +
                        $"deve ser igual ao total da venda ({valorVenda:N2}).");
                }

                foreach (var pagamento in pagamentos)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var formaPagamento =
                        await _repositorioRecebimento.ObterFormaPagamentoAtivaAsync(
                            empresaId,
                            pagamento.FormaPagamentoId,
                            cancellationToken);

                    if (formaPagamento is null)
                    {
                        throw Erro(
                            $"A forma de pagamento {pagamento.FormaPagamentoId} " +
                            "não existe, está inativa ou não pertence à empresa.");
                    }

                    formasPagamento.Add(
                        pagamento.FormaPagamentoId,
                        formaPagamento);
                }
            }

            // 10. Impedir duplicidade de conta financeira.
            var contaExistente =
                await _repositorioRecebimento.ObterPorVendaAsync(
                    empresaId,
                    venda.Id,
                    cancellationToken);

            if (contaExistente is not null)
            {
                throw Erro(
                    "Já existe uma conta a receber vinculada a esta venda. " +
                    "Verifique o financeiro antes de tentar finalizá-la novamente.");
            }

            // 11. Validar e baixar o estoque dos produtos da venda.
            await BaixarEstoqueProdutosAsync(
                venda,
                empresaId,
                usuarioId,
                cancellationToken);

            // 12. Definir os valores exclusivamente no servidor.
            var dataFinalizacao = DateTimeOffset.UtcNow;
            var dataOperacao = dataFinalizacao.UtcDateTime;

            // 13. Criar uma única conta a receber para a venda.
            var contaReceber = new ContaReceber(
                empresaId: empresaId,
                clienteId: venda.ClienteId,
                dataEmissao: dataOperacao,
                dataVencimento: dataOperacao,
                valor: valorVenda,
                descricao: $"Venda {venda.Id}");

            contaReceber.VincularVenda(venda.Id);

            // 14. Registrar recebimento somente quando houver
            // pagamento presencial.
            if (pagamentos.Count > 0)
            {
                try
                {
                    contaReceber.RegistrarRecebimento(
                        valorVenda,
                        dataOperacao);
                }
                catch (InvalidOperationException ex)
                {
                    throw Erro(ex.Message);
                }
            }

            await _repositorioRecebimento.AdicionarContaAsync(
                contaReceber,
                cancellationToken);

            // 15. Criar recebimentos e movimentos de caixa
            // somente para pagamentos presenciais.
            foreach (var pagamento in pagamentos)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var formaPagamento =
                    formasPagamento[pagamento.FormaPagamentoId];

                var recebimento = new Recebimento
                {
                    ContaReceberId = contaReceber.Id,
                    ContaReceber = contaReceber,

                    FormaPagamentoId = formaPagamento.Id,

                    DataRecebimento = dataOperacao,
                    Valor = pagamento.Valor,
                    Observacao = observacao
                };

                await _repositorioRecebimento.AdicionarAsync(
                    recebimento,
                    cancellationToken);

                var movimento = new MovimentoCaixa
                {
                    CaixaId = caixa!.Id,
                    Caixa = caixa,

                    TipoMovimento = TipoMovimentoCaixa.Entrada,

                    FormaPagamentoId = formaPagamento.Id,

                    DataMovimento = dataOperacao,
                    Valor = pagamento.Valor,

                    Historico = $"Recebimento da venda {venda.Id}",

                    Documento = recebimento.Id.ToString()
                };

                await _repositorioRecebimento.AdicionarMovimentoCaixaAsync(
                    movimento,
                    cancellationToken);
            }

            // 16. Finalizar a venda depois de preparar
            // todos os registros financeiros.
            venda.Finalizar(dataFinalizacao);

            // Não chamar SaveChangesAsync.
            // O TransactionMiddleware confirma ou reverte
            // todas as alterações da operação.
            return new FinalizarVendaDto(
                VendaId: venda.Id,
                Status: venda.Status,
                DataAbertura: venda.DataAbertura,
                DataFinalizacao: venda.DataFinalizacao!.Value,
                Subtotal: venda.Subtotal,
                Desconto: venda.Desconto,
                ValorTotal: venda.ValorTotal);
        }

        private async Task BaixarEstoqueProdutosAsync(
            Venda venda,
            Guid empresaId,
            Guid usuarioId,
            CancellationToken cancellationToken)
        {
            var itensProduto = venda.Itens
                .Where(item => item.TipoItem == TipoItemVenda.Produto)
                .GroupBy(item => item.ItemId)
                .Select(grupo => new
                {
                    ProdutoId = grupo.Key,
                    Quantidade = grupo.Sum(item => item.Quantidade)
                })
                .ToList();

            foreach (var item in itensProduto)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var produto =
                    await _repositorioProdutos.ObterParaMovimentacaoEstoqueAsync(
                        empresaId,
                        item.ProdutoId,
                        cancellationToken);

                if (produto is null)
                {
                    throw Erro(
                        $"O produto {item.ProdutoId} não foi encontrado para esta empresa.");
                }

                if (!produto.Ativo)
                {
                    throw Erro(
                        $"O produto '{produto.Nome}' está inativo e não pode " +
                        "ser utilizado na finalização da venda.");
                }

                if (!produto.ControlaEstoque)
                {
                    continue;
                }

                var estoque = produto.Estoque;

                if (estoque is null)
                {
                    throw Erro(
                        $"O produto '{produto.Nome}' controla estoque, " +
                        "mas não possui um registro de estoque cadastrado.");
                }

                decimal saldoAnterior;

                try
                {
                    // O método de domínio valida a quantidade e o saldo.
                    saldoAnterior = estoque.RegistrarSaida(item.Quantidade);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    throw Erro(
                        $"Quantidade inválida para o produto " +
                        $"'{produto.Nome}': {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    throw Erro(
                        $"Não foi possível baixar o estoque do produto " +
                        $"'{produto.Nome}': {ex.Message}");
                }

                var saldoPosterior = estoque.QuantidadeAtual;

                var movimentacao = new MovimentacaoEstoque(
                    empresaId: empresaId,
                    produtoId: produto.Id,
                    tipo: TipoMovimentacaoEstoque.Saida,
                    origem: OrigemMovimentacaoEstoque.Venda,
                    quantidade: item.Quantidade,
                    saldoAnterior: saldoAnterior,
                    saldoPosterior: saldoPosterior,
                    usuarioResponsavelId: usuarioId,
                    documentoOrigemId: venda.Id,
                    observacao: $"Baixa automática da venda {venda.Id}.");

                await _repositorioProdutos.AdicionarMovimentacaoEstoqueAsync(
                    movimentacao,
                    cancellationToken);
            }
        }

        private static CustomValidationException Erro(
            string mensagem)
        {
            return new CustomValidationException(
                new[] { mensagem });
        }
    }
}
```