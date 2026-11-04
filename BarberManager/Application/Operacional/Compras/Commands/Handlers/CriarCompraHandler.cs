using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Tenancy;
using Application.Interfaces.Compras;
using Application.Interfaces.Produtos;
using Application.Operacional.Compras.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Compras;
using Domain.Entities.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.Commands.Handlers
{
    public class CriarCompraHandler
        : ICommandHandler<CriarCompraCommand, CriarCompraDto>
    {
        private readonly IRepositorioCompra _repositorioCompra;
        private readonly IRepositorioFornecedor _repositorioFornecedor;
        private readonly IRepositorioProdutos _repositorioProdutos;
        private readonly ITenantContext _tenantContext;

        public CriarCompraHandler(
            IRepositorioCompra repositorioCompra,
            IRepositorioFornecedor repositorioFornecedor,
            IRepositorioProdutos repositorioProdutos,
            ITenantContext tenantContext)
        {
            _repositorioCompra = repositorioCompra;
            _repositorioFornecedor = repositorioFornecedor;
            _repositorioProdutos = repositorioProdutos;
            _tenantContext = tenantContext;
        }

        public async Task<CriarCompraDto> Handle(
            CriarCompraCommand command,
            CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            // 1. Validar empresa autenticada.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new UnauthorizedAccessException(
                    "Não foi possível identificar a empresa do usuário.");
            }

            var empresaId = _tenantContext.EmpresaId;

            // 2. Validar os dados gerais.
            if (command.FornecedorId == Guid.Empty)
                erros.Add("O fornecedor é obrigatório.");

            if (command.DataEmissao == default)
                erros.Add("A data de emissão é obrigatória.");

            if (command.DataCompra == default)
                erros.Add("A data da compra é obrigatória.");

            if (command.Desconto < 0)
                erros.Add("O desconto não pode ser negativo.");

            if (command.Frete < 0)
                erros.Add("O frete não pode ser negativo.");

            if (command.OutrasDespesas < 0)
                erros.Add("As outras despesas não podem ser negativas.");

            if (command.NumeroDocumento?.Length > 50)
                erros.Add("O número do documento deve ter no máximo 50 caracteres.");

            if (command.SerieDocumento?.Length > 20)
                erros.Add("A série do documento deve ter no máximo 20 caracteres.");

            if (command.Observacao?.Length > 1000)
                erros.Add("A observação deve ter no máximo 1000 caracteres.");

            if (command.Itens is null || command.Itens.Count == 0)
                erros.Add("A compra deve possuir pelo menos um item.");

            if (command.Parcelas is null || command.Parcelas.Count == 0)
                erros.Add("Informe pelo menos uma parcela para a compra.");

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 3. Consultar fornecedor dentro da empresa.
            var fornecedor = await _repositorioFornecedor.ObterPorIdAsync(
                empresaId,
                command.FornecedorId,
                cancellationToken);

            if (fornecedor is null)
                erros.Add("Fornecedor não encontrado para esta empresa.");
            else if (!fornecedor.Ativo)
                erros.Add("Não é possível registrar compra para fornecedor inativo.");

            // 4. Validar os itens.
            var produtos = new Dictionary<Guid, Domain.Entities.Produtos.Produto>();

            foreach (var item in command.Itens)
            {
                if (item.ProdutoId == Guid.Empty)
                {
                    erros.Add("Existe um item com produto inválido.");
                    continue;
                }

                if (item.Quantidade <= 0)
                    erros.Add("A quantidade dos itens deve ser maior que zero.");

                if (item.Quantidade != decimal.Round(item.Quantidade, 3))
                    erros.Add("A quantidade deve possuir no máximo três casas decimais.");

                if (item.ValorUnitario <= 0)
                    erros.Add("O valor unitário deve ser maior que zero.");

                if (item.Desconto < 0)
                    erros.Add("O desconto do item não pode ser negativo.");

                if (produtos.ContainsKey(item.ProdutoId))
                {
                    erros.Add(
                        "Não é permitido adicionar o mesmo produto mais de uma vez na compra.");
                    continue;
                }

                var produto = await _repositorioProdutos.ObterPorIdAsync(
                    empresaId,
                    item.ProdutoId,
                    cancellationToken);

                if (produto is null)
                {
                    erros.Add(
                        $"Produto {item.ProdutoId} não encontrado para esta empresa.");
                    continue;
                }

                if (!produto.Ativo)
                {
                    erros.Add(
                        $"O produto '{produto.Nome}' está inativo e não pode ser comprado.");
                    continue;
                }

                produtos.Add(produto.Id, produto);
            }

            // 5. Validar parcelas.
            decimal somaParcelas = 0;

            foreach (var parcela in command.Parcelas)
            {
                if (parcela.DataVencimento == default)
                    erros.Add("A data de vencimento de cada parcela é obrigatória.");

                if (parcela.Valor <= 0)
                    erros.Add("O valor de cada parcela deve ser maior que zero.");

                if (parcela.Valor != decimal.Round(parcela.Valor, 2))
                    erros.Add("O valor das parcelas deve possuir no máximo duas casas decimais.");

                somaParcelas += parcela.Valor;
            }

            if (erros.Count > 0)
                throw new CustomValidationException(erros);

            cancellationToken.ThrowIfCancellationRequested();

            // 6. Criar a entidade Compra.
            var compra = new Compra(
                empresaId: empresaId,
                fornecedorId: fornecedor!.Id,
                dataEmissao: command.DataEmissao,
                dataCompra: command.DataCompra,
                desconto: command.Desconto,
                frete: command.Frete,
                outrasDespesas: command.OutrasDespesas,
                numeroDocumento: command.NumeroDocumento,
                serieDocumento: command.SerieDocumento,
                observacao: command.Observacao);

            // 7. Criar os itens.
            foreach (var item in command.Itens)
            {
                var produto = produtos[item.ProdutoId];

                var compraItem = new CompraItem(
                    compraId: compra.Id,
                    produtoId: produto.Id,
                    descricaoProduto: produto.Nome,
                    quantidade: item.Quantidade,
                    valorUnitario: item.ValorUnitario,
                    desconto: item.Desconto);

                compra.AdicionarItem(compraItem);
            }

            // 8. Recalcular os totais uma única vez.
            try
            {
                compra.RecalcularTotais();
            }
            catch (InvalidOperationException ex)
            {
                throw new CustomValidationException(
                    new List<string> { ex.Message });
            }

            // 9. Conferir se o parcelamento corresponde ao total.
            somaParcelas = decimal.Round(
                somaParcelas,
                2,
                MidpointRounding.AwayFromZero);

            if (somaParcelas != compra.ValorTotal)
            {
                throw new CustomValidationException(
                    new List<string>
                    {
                        $"A soma das parcelas ({somaParcelas:C}) deve ser igual ao valor total da compra ({compra.ValorTotal:C})."
                    });
            }

            // 10. Criar uma ContaPagar para cada parcela.
            var contasPagar = new List<ContaPagar>();

            var totalParcelas = command.Parcelas.Count;

            for (var i = 0; i < totalParcelas; i++)
            {
                var parcela = command.Parcelas[i];

                var conta = new ContaPagar
                {
                    EmpresaId = empresaId,
                    FornecedorId = fornecedor.Id,
                    CompraId = compra.Id,
                    DataEmissao = compra.DataEmissao,
                    DataVencimento = parcela.DataVencimento.Date,
                    Valor = parcela.Valor,
                    ValorPago = 0,
                    Status = StatusContas.Aberta,
                    NumeroParcela = i + 1,
                    TotalParcelas = totalParcelas,
                    Descricao = string.IsNullOrWhiteSpace(
                        compra.NumeroDocumento)
                            ? $"Compra {compra.Id}"
                            : $"Compra - Documento {compra.NumeroDocumento} - Parcela {i + 1}/{totalParcelas}"
                };

                contasPagar.Add(conta);
            }

            // 11. Persistir compra, itens e contas na mesma unidade de trabalho.
            await _repositorioCompra.AdicionarAsync(
                compra,
                contasPagar,
                cancellationToken);

            // 12. Montar retorno.
            var itensDto = compra.Itens
                .Select(item => new CriarCompraItemDto(
                    Id: item.Id,
                    ProdutoId: item.ProdutoId,
                    DescricaoProduto: item.DescricaoProduto,
                    Quantidade: item.Quantidade,
                    ValorUnitario: item.ValorUnitario,
                    Desconto: item.Desconto,
                    ValorTotal: item.ValorTotal))
                .ToList();

            var contasDto = contasPagar
                .Select(conta => new CriarCompraContaPagarDto(
                    Id: conta.Id,
                    NumeroParcela: conta.NumeroParcela,
                    TotalParcelas: conta.TotalParcelas,
                    DataVencimento: conta.DataVencimento,
                    Valor: conta.Valor,
                    Status: conta.Status.ToString()))
                .ToList();

            return new CriarCompraDto(
                Id: compra.Id,
                FornecedorId: fornecedor.Id,
                NomeFornecedor: fornecedor.Nome,
                NumeroDocumento: compra.NumeroDocumento,
                DataEmissao: compra.DataEmissao,
                DataCompra: compra.DataCompra,
                Subtotal: compra.Subtotal,
                Desconto: compra.Desconto,
                Frete: compra.Frete,
                OutrasDespesas: compra.OutrasDespesas,
                ValorTotal: compra.ValorTotal,
                Status: compra.Status.ToString(),
                Itens: itensDto,
                ContasPagar: contasDto);
        }
    }
}
