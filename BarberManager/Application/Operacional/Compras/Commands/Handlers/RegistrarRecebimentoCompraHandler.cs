using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Compras;
using Application.Interfaces.Produtos;
using Application.Operacional.Compras.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Compras;
using Domain.Entities.Estoque;

namespace Application.Operacional.Compras.Commands.Handlers
{
    public sealed class RegistrarRecebimentoCompraHandler
        : ICommandHandler<
            RegistrarRecebimentoCompraCommand,
            RegistrarRecebimentoCompraDto>
    {
        private readonly IRepositorioCompra _repositorioCompra;
        private readonly IRepositorioProdutos _repositorioProdutos;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public RegistrarRecebimentoCompraHandler(
            IRepositorioCompra repositorioCompra,
            IRepositorioProdutos repositorioProdutos,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorioCompra = repositorioCompra;
            _repositorioProdutos = repositorioProdutos;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<RegistrarRecebimentoCompraDto> Handle(
            RegistrarRecebimentoCompraCommand command,
            CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            // 1. Validar empresa autenticada.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário autenticado.");
            }

            // 2. Validar usuário responsável.
            if (!_usuarioLogado.IsAuthenticated ||
                _usuarioLogado.UsuarioId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar o usuário autenticado.");
            }

            var empresaId = _tenantContext.EmpresaId;
            var usuarioId = _usuarioLogado.UsuarioId;

            // 3. Normalizar dados.
            var chaveIdempotencia =
                command.ChaveIdempotencia?.Trim() ?? string.Empty;

            var observacao = string.IsNullOrWhiteSpace(command.Observacao)
                ? null
                : command.Observacao.Trim();

            // 4. Validar dados gerais.
            if (command.CompraId == Guid.Empty)
                erros.Add("O identificador da compra é inválido.");

            if (string.IsNullOrWhiteSpace(chaveIdempotencia))
                erros.Add("A chave de idempotência é obrigatória.");
            else if (chaveIdempotencia.Length > 100)
                erros.Add(
                    "A chave de idempotência deve possuir no máximo 100 caracteres.");

            if (observacao?.Length > 500)
                erros.Add(
                    "A observação deve possuir no máximo 500 caracteres.");

            if (command.Itens is null || command.Itens.Count == 0)
                erros.Add("Informe pelo menos um item para recebimento.");

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 5. Validar estrutura dos itens.
            var itensInformados = command.Itens!;

            if (itensInformados.Any(i => i.CompraItemId == Guid.Empty))
                erros.Add("Existe um item com identificador inválido.");

            if (itensInformados.Any(i => i.QuantidadeRecebida <= 0))
                erros.Add(
                    "Todas as quantidades recebidas devem ser maiores que zero.");

            if (itensInformados.Any(i =>
                i.QuantidadeRecebida !=
                decimal.Round(i.QuantidadeRecebida, 3)))
            {
                erros.Add(
                    "As quantidades devem possuir no máximo três casas decimais.");
            }

            if (itensInformados
                .GroupBy(i => i.CompraItemId)
                .Any(g => g.Count() > 1))
            {
                erros.Add(
                    "Não informe o mesmo item da compra mais de uma vez.");
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 6. Verificar idempotência.
            var recebimentoExistente =
                await _repositorioCompra.ObterPorChaveIdempotenciaAsync(
                    empresaId,
                    chaveIdempotencia,
                    cancellationToken);

            if (recebimentoExistente is not null)
            {
                var mesmaCompra =
                    recebimentoExistente.CompraId == command.CompraId;

                var mesmosItens =
                    CompararItens(
                        recebimentoExistente,
                        itensInformados);

                if (!mesmaCompra || !mesmosItens)
                {
                    throw new CustomValidationException(
                        new[]
                        {
                            "A chave de idempotência já foi utilizada " +
                            "em outra operação ou com dados diferentes."
                        });
                }

                // A mesma operação já foi processada.
                // Retornar o resultado sem movimentar o estoque novamente.
                var compraAnterior =
                    await _repositorioCompra.ObterParaRecebimentoAsync(
                        empresaId,
                        command.CompraId,
                        cancellationToken);

                if (compraAnterior is null)
                {
                    throw new CustomValidationException(
                        new[]
                        {
                            "Compra não encontrada nesta empresa."
                        });
                }

                return CriarResposta(
                    recebimentoExistente);
            }

            // 7. Obter compra rastreada com seus itens.
            var compra =
                await _repositorioCompra.ObterParaRecebimentoAsync(
                    empresaId,
                    command.CompraId,
                    cancellationToken);

            if (compra is null)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Compra não encontrada nesta empresa."
                    });
            }

            if (compra.Status == StatusCompra.Cancelada)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Não é possível receber uma compra cancelada."
                    });
            }

            if (compra.Status == StatusCompra.Recebida)
            {
                throw new CustomValidationException(
                    new[]
                    {
                        "Esta compra já foi totalmente recebida."
                    });
            }

            // 8. Validar pertencimento e quantidades pendentes.
            var itensCompra = compra.Itens.ToDictionary(i => i.Id);

            foreach (var itemInformado in itensInformados)
            {
                if (!itensCompra.TryGetValue(
                    itemInformado.CompraItemId,
                    out var itemCompra))
                {
                    erros.Add(
                        $"O item {itemInformado.CompraItemId} " +
                        "não pertence a esta compra.");

                    continue;
                }

                if (itemInformado.QuantidadeRecebida >
                    itemCompra.QuantidadePendente)
                {
                    erros.Add(
                        $"A quantidade recebida do produto " +
                        $"{itemCompra.DescricaoProduto} supera " +
                        "a quantidade pendente.");
                }
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 9. Obter produtos rastreados e validar estoque.
            var produtos = new Dictionary<Guid, Domain.Entities.Produtos.Produto>();

            foreach (var itemInformado in itensInformados)
            {
                var itemCompra =
                    itensCompra[itemInformado.CompraItemId];

                var produto =
                    await _repositorioProdutos.ObterParaMovimentacaoEstoqueAsync(
                        empresaId,
                        itemCompra.ProdutoId,
                        cancellationToken);

                if (produto is null)
                {
                    erros.Add(
                        $"O produto vinculado ao item " +
                        $"{itemCompra.DescricaoProduto} não foi encontrado.");

                    continue;
                }

                if (produto.ControlaEstoque &&
                    produto.Estoque is null)
                {
                    erros.Add(
                        $"O produto {produto.Nome} controla estoque, " +
                        "mas não possui registro de estoque.");
                }

                produtos[produto.Id] = produto;
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 10. Criar registro do recebimento.
            var recebimento = new RecebimentoCompra(
                empresaId: empresaId,
                compraId: compra.Id,
                usuarioResponsavelId: usuarioId,
                chaveIdempotencia: chaveIdempotencia,
                observacao: observacao);

            // 11. Processar os itens recebidos.
            foreach (var itemInformado in itensInformados)
            {
                var itemCompra =
                    itensCompra[itemInformado.CompraItemId];

                var produto = produtos[itemCompra.ProdutoId];

                var quantidade =
                    itemInformado.QuantidadeRecebida;

                // Atualizar acumulado recebido na compra.
                itemCompra.RegistrarRecebimento(quantidade);
             

                // Movimentar estoque somente quando aplicável.
                if (produto.ControlaEstoque)
                {
                    var estoque = produto.Estoque!;

                    var saldoAnterior =
                        estoque.RegistrarEntrada(quantidade);

                    var saldoPosterior =
                        estoque.QuantidadeAtual;

                    var movimentacao = new MovimentacaoEstoque(
                        empresaId: empresaId,
                        produtoId: produto.Id,
                        tipo: TipoMovimentacaoEstoque.Entrada,
                        origem: OrigemMovimentacaoEstoque.Compra,
                        quantidade: quantidade,
                        saldoAnterior: saldoAnterior,
                        saldoPosterior: saldoPosterior,
                        usuarioResponsavelId: usuarioId,
                        documentoOrigemId: recebimento.Id,
                        observacao:
                            $"Recebimento da compra {compra.Id}.");

                    await _repositorioProdutos
                        .AdicionarMovimentacaoEstoqueAsync(
                            movimentacao,
                            cancellationToken);
                }

                // Registrar o histórico do item recebido.
                var recebimentoItem = new RecebimentoCompraItem(
                         recebimentoCompraId: recebimento.Id,
                         compraItemId: itemCompra.Id,
                         produtoId: itemCompra.ProdutoId,
                         quantidadeRecebida: quantidade,
                         quantidadeAcumuladaAposRecebimento:
                             itemCompra.QuantidadeRecebida,
                         quantidadePendenteAposRecebimento:
                             itemCompra.QuantidadePendente);

                recebimento.AdicionarItem(recebimentoItem);
            }

            recebimento.ValidarItens();

            // 12. Atualizar status da compra.
            compra.AtualizarStatusRecebimento();

            // Registrar o snapshot do status após este recebimento.
            recebimento.RegistrarStatusAposRecebimento(
                compra.Status);

            // 13. Adicionar recebimento ao contexto transacional.
            await _repositorioCompra.AdicionarRecebimentoAsync(
                recebimento,
                cancellationToken);

            // 14. Retornar resultado.
            return CriarResposta(recebimento);
        }

        private static bool CompararItens(
            RecebimentoCompra recebimento,
            IReadOnlyCollection<RegistrarRecebimentoCompraItemDto> itens)
        {
            if (recebimento.Itens.Count != itens.Count)
                return false;

            var itensExistentes = recebimento.Itens
                .ToDictionary(
                    i => i.CompraItemId,
                    i => i.QuantidadeRecebida);

            foreach (var item in itens)
            {
                if (!itensExistentes.TryGetValue(
                    item.CompraItemId,
                    out var quantidade))
                {
                    return false;
                }

                if (quantidade != item.QuantidadeRecebida)
                    return false;
            }

            return true;
        }

        private static RegistrarRecebimentoCompraDto CriarResposta(
       RecebimentoCompra recebimento)
        {
            var itensResultado = recebimento.Itens
                .Select(item =>
                    new RegistrarRecebimentoCompraItemResultadoDto(
                        CompraItemId: item.CompraItemId,
                        ProdutoId: item.ProdutoId,
                        QuantidadeRecebida: item.QuantidadeRecebida,
                        QuantidadeAcumulada:
                            item.QuantidadeAcumuladaAposRecebimento,
                        QuantidadePendente:
                            item.QuantidadePendenteAposRecebimento))
                .ToList();

            return new RegistrarRecebimentoCompraDto(
                Id: recebimento.Id,
                CompraId: recebimento.CompraId,
                DataRecebimento: recebimento.DataRecebimento,
                StatusCompra: recebimento.StatusCompraAposRecebimento,
                Itens: itensResultado);
        }
    }
}