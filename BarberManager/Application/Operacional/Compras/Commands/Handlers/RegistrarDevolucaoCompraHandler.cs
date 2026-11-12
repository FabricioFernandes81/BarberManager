
using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Compras;
using Application.Interfaces.Produtos;
using Application.Operacional.Compras.Commands;
using Application.Operacional.Compras.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Compras;
using Domain.Entities.Estoque;
using Domain.Entities.Produtos;

namespace Application.Operacional.Compras.Commands.Handlers
{
    public sealed class RegistrarDevolucaoCompraHandler
        : ICommandHandler<
            RegistrarDevolucaoCompraCommand,
            RegistrarDevolucaoCompraDto>
    {
        private readonly IRepositorioDevolucaoCompra _repositorioDevolucao;
        private readonly IRepositorioProdutos _repositorioProdutos;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public RegistrarDevolucaoCompraHandler(
            IRepositorioDevolucaoCompra repositorioDevolucao,
            IRepositorioProdutos repositorioProdutos,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorioDevolucao = repositorioDevolucao;
            _repositorioProdutos = repositorioProdutos;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<RegistrarDevolucaoCompraDto> Handle(
            RegistrarDevolucaoCompraCommand command,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // 1. Validar contexto de autenticação.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário autenticado.");
            }

            if (!_usuarioLogado.IsAuthenticated ||
                _usuarioLogado.UsuarioId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar o usuário autenticado.");
            }

            var empresaId = _tenantContext.EmpresaId;
            var usuarioId = _usuarioLogado.UsuarioId;

            var erros = new List<string>();

            // 2. Normalizar os dados.
            var chaveIdempotencia =
                command.ChaveIdempotencia?.Trim() ?? string.Empty;

            var motivo = command.Motivo?.Trim() ?? string.Empty;

            var observacao = string.IsNullOrWhiteSpace(command.Observacao)
                ? null
                : command.Observacao.Trim();

            // 3. Validar dados gerais.
            if (command.CompraId == Guid.Empty)
                erros.Add("O identificador da compra é inválido.");

            if (string.IsNullOrWhiteSpace(chaveIdempotencia))
                erros.Add("A chave de idempotência é obrigatória.");
            else if (chaveIdempotencia.Length > 100)
                erros.Add("A chave de idempotência deve possuir no máximo 100 caracteres.");

            if (string.IsNullOrWhiteSpace(motivo))
                erros.Add("O motivo da devolução é obrigatório.");
            else if (motivo.Length > 200)
                erros.Add("O motivo deve possuir no máximo 200 caracteres.");

            if (observacao?.Length > 500)
                erros.Add("A observação deve possuir no máximo 500 caracteres.");

            if (command.Itens is null || command.Itens.Count == 0)
                erros.Add("Informe pelo menos um item para devolução.");

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            var itensInformados = command.Itens!;

            // 4. Validar estrutura dos itens.
            if (itensInformados.Any(i =>
                i.RecebimentoCompraItemId == Guid.Empty))
            {
                erros.Add("Existe um item com identificador de recebimento inválido.");
            }

            if (itensInformados.Any(i =>
                i.QuantidadeDevolvida <= 0))
            {
                erros.Add("Todas as quantidades devolvidas devem ser maiores que zero.");
            }

            if (itensInformados.Any(i =>
                i.QuantidadeDevolvida !=
                decimal.Round(i.QuantidadeDevolvida, 3)))
            {
                erros.Add("As quantidades devem possuir no máximo três casas decimais.");
            }

            if (itensInformados
                .GroupBy(i => i.RecebimentoCompraItemId)
                .Any(g => g.Count() > 1))
            {
                erros.Add("Não informe o mesmo item de recebimento mais de uma vez.");
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 5. Verificar idempotência.
            var devolucaoExistente =
                await _repositorioDevolucao.ObterPorChaveIdempotenciaAsync(
                    empresaId,
                    chaveIdempotencia,
                    cancellationToken);

            if (devolucaoExistente is not null)
            {
                var mesmaCompra =
                    devolucaoExistente.CompraId == command.CompraId;

                var mesmosDados =
                    CompararItens(devolucaoExistente, itensInformados);

                if (!mesmaCompra ||
                    !mesmosDados ||
                    devolucaoExistente.Motivo != motivo ||
                    devolucaoExistente.Observacao != observacao)
                {
                    throw new CustomValidationException(
                        new[]
                        {
                            "A chave de idempotência já foi utilizada " +
                            "em outra operação ou com dados diferentes."
                        });
                }

                return CriarResposta(devolucaoExistente);
            }

            cancellationToken.ThrowIfCancellationRequested();

            // 6. Obter compra e recebimentos rastreados.
            var compra =
                await _repositorioDevolucao.ObterParaDevolucaoAsync(
                    empresaId,
                    command.CompraId,
                    cancellationToken);

            if (compra is null)
            {
                throw new CustomValidationException(
                    new[] { "Compra não encontrada nesta empresa." });
            }

            // 7. Indexar os itens dos recebimentos da compra.
            var itensRecebidos = compra.Recebimentos
                .SelectMany(r => r.Itens)
                .ToDictionary(i => i.Id);

            // Estruturas para a pré-validação.
            var itensParaDevolver =
                new List<(RecebimentoCompraItem Item, decimal Quantidade)>();

            var quantidadesPorProduto = new Dictionary<Guid, decimal>();

            // 8. Validar pertencimento e saldo disponível.
            foreach (var itemInformado in itensInformados)
            {
                if (!itensRecebidos.TryGetValue(
                    itemInformado.RecebimentoCompraItemId,
                    out var itemRecebido))
                {
                    erros.Add(
                        $"O item de recebimento {itemInformado.RecebimentoCompraItemId} " +
                        "não pertence a esta compra.");

                    continue;
                }

                if (itemInformado.QuantidadeDevolvida >
                    itemRecebido.QuantidadeDisponivelParaDevolucao)
                {
                    erros.Add(
                        $"A quantidade solicitada para o produto " +
                        $"{itemRecebido.ProdutoId} supera a quantidade disponível " +
                        "para devolução neste recebimento.");

                    continue;
                }

                itensParaDevolver.Add((
                    itemRecebido,
                    itemInformado.QuantidadeDevolvida));

                if (!quantidadesPorProduto.ContainsKey(itemRecebido.ProdutoId))
                    quantidadesPorProduto[itemRecebido.ProdutoId] = 0;

                quantidadesPorProduto[itemRecebido.ProdutoId] +=
                    itemInformado.QuantidadeDevolvida;
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 9. Carregar todos os produtos antes de alterar qualquer saldo.
            var produtos = new Dictionary<Guid, Produto>();

            foreach (var produtoId in quantidadesPorProduto.Keys)
            {
                var produto =
                    await _repositorioProdutos.ObterParaMovimentacaoEstoqueAsync(
                        empresaId,
                        produtoId,
                        cancellationToken);

                if (produto is null)
                {
                    erros.Add(
                        $"O produto {produtoId} não foi encontrado nesta empresa.");

                    continue;
                }

                if (!produto.ControlaEstoque)
                {
                    erros.Add(
                        $"O produto {produto.Nome} não possui controle de estoque. " +
                        "Não é possível validar sua disponibilidade física para devolução.");

                    continue;
                }

                if (produto.Estoque is null)
                {
                    erros.Add(
                        $"O produto {produto.Nome} não possui registro de estoque.");

                    continue;
                }

                produtos.Add(produtoId, produto);
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            // 10. Validar saldo agregado de todos os produtos.
            foreach (var (produtoId, quantidadeTotal) in quantidadesPorProduto)
            {
                var produto = produtos[produtoId];
                var estoque = produto.Estoque!;

                if (quantidadeTotal > estoque.QuantidadeAtual)
                {
                    erros.Add(
                        $"Estoque insuficiente para devolver o produto " +
                        $"{produto.Nome}. Quantidade solicitada: {quantidadeTotal}; " +
                        $"estoque disponível: {estoque.QuantidadeAtual}.");
                }
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 11. Criar a entidade principal da devolução.
            var devolucao = new DevolucaoCompra(
                empresaId: empresaId,
                compraId: compra.Id,
                usuarioResponsavelId: usuarioId,
                chaveIdempotencia: chaveIdempotencia,
                motivo: motivo,
                observacao: observacao);

            // 12. Registrar os itens da devolução.
            foreach (var (itemRecebido, quantidade) in itensParaDevolver)
            {
                itemRecebido.RegistrarDevolucao(quantidade);

                var itemDevolucao = new DevolucaoCompraItem(
                    devolucaoCompraId: devolucao.Id,
                    recebimentoCompraItemId: itemRecebido.Id,
                    produtoId: itemRecebido.ProdutoId,
                    quantidadeDevolvida: quantidade);

                devolucao.AdicionarItem(itemDevolucao);
            }

            devolucao.ValidarItens();

            // 13. Registrar uma saída por produto.
            foreach (var (produtoId, quantidadeTotal) in quantidadesPorProduto)
            {
                var produto = produtos[produtoId];
                var estoque = produto.Estoque!;

                var saldoAnterior = estoque.RegistrarSaida(
                    quantidadeTotal);

                var saldoPosterior = estoque.QuantidadeAtual;

                var movimentacao = new MovimentacaoEstoque(
                    empresaId: empresaId,
                    produtoId: produtoId,
                    tipo: TipoMovimentacaoEstoque.Saida,
                    origem: OrigemMovimentacaoEstoque.DevolucaoCompra,
                    quantidade: quantidadeTotal,
                    saldoAnterior: saldoAnterior,
                    saldoPosterior: saldoPosterior,
                    usuarioResponsavelId: usuarioId,
                    documentoOrigemId: devolucao.Id,
                    observacao: $"Devolução da compra {compra.Id}. Motivo: {motivo}");

                await _repositorioProdutos.AdicionarMovimentacaoEstoqueAsync(
                    movimentacao,
                    cancellationToken);
            }

            // 14. Adicionar a devolução ao contexto.
            await _repositorioDevolucao.AdicionarAsync(
                devolucao,
                cancellationToken);

            // O TransactionMiddleware persiste todas as alterações
            // na mesma transação operacional.

            return CriarResposta(devolucao);
        }

        private static bool CompararItens(
            DevolucaoCompra devolucao,
            IReadOnlyCollection<RegistrarDevolucaoCompraItemDto> itens)
        {
            if (devolucao.Itens.Count != itens.Count)
                return false;

            var existentes = devolucao.Itens.ToDictionary(
                i => i.RecebimentoCompraItemId,
                i => i.QuantidadeDevolvida);

            foreach (var item in itens)
            {
                if (!existentes.TryGetValue(
                    item.RecebimentoCompraItemId,
                    out var quantidade))
                {
                    return false;
                }

                if (quantidade != item.QuantidadeDevolvida)
                    return false;
            }

            return true;
        }

        private static RegistrarDevolucaoCompraDto CriarResposta(
            DevolucaoCompra devolucao)
        {
            var itens = devolucao.Itens
                .Select(i =>
                    new RegistrarDevolucaoCompraItemResultadoDto(
                        RecebimentoCompraItemId: i.RecebimentoCompraItemId,
                        ProdutoId: i.ProdutoId,
                        QuantidadeDevolvida: i.QuantidadeDevolvida))
                .ToList();

            return new RegistrarDevolucaoCompraDto(
                Id: devolucao.Id,
                CompraId: devolucao.CompraId,
                DataDevolucao: devolucao.DataDevolucao,
                Motivo: devolucao.Motivo,
                Observacao: devolucao.Observacao,
                Itens: itens);
        }
    }
}
