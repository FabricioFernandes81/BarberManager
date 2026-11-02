using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Produtos;
using Application.Interfaces.Servicos;
using Application.Interfaces.Vendas;
using Application.Operacional.Vendas.DTOs;
using Domain.Entities.Common.Enuns;
using Domain.Entities.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Vendas.Commands.Handlers
{
    public class AdicionarItemVendaHandler : ICommandHandler<AdicionarItemVendaCommand,AdicionarItemVendaDto>
    {
        private readonly IRepositorioVenda _repositorioVenda;
        private readonly IRepositorioServico _repositorioServico;
        private readonly IRepositorioProdutos _repositorioProdutos;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public AdicionarItemVendaHandler(
            IRepositorioVenda repositorioVenda,
            IRepositorioServico repositorioServico,
            IRepositorioProdutos repositorioProdutos,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorioVenda = repositorioVenda;
            _repositorioServico = repositorioServico;
            _repositorioProdutos = repositorioProdutos;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<AdicionarItemVendaDto> Handle(
            AdicionarItemVendaCommand command,
            CancellationToken cancellationToken)
        {
            // 1. Validar o contexto da empresa.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw Validacao(
                    "Não foi possível identificar a empresa.");
            }

            // 2. Validar a autenticação.
            if (!_usuarioLogado.IsAuthenticated)
            {
                throw Validacao(
                    "Usuário não autenticado.");
            }

            // 3. Validar os identificadores.
            if (command.VendaId == Guid.Empty)
            {
                throw Validacao(
                    "O identificador da venda é obrigatório.");
            }

            if (command.ItemId == Guid.Empty)
            {
                throw Validacao(
                    "O identificador do item é obrigatório.");
            }

            // 4. Validar o tipo do item.
            if (command.TipoItem != TipoItemVenda.Servico &&
                command.TipoItem != TipoItemVenda.Produto)
            {
                throw Validacao(
                    "O tipo de item informado é inválido.");
            }

            // 5. Validar quantidade e desconto.
            if (command.Quantidade <= 0)
            {
                throw Validacao(
                    "A quantidade deve ser maior que zero.");
            }

            if (command.Desconto < 0)
            {
                throw Validacao(
                    "O desconto não pode ser negativo.");
            }

            cancellationToken.ThrowIfCancellationRequested();

            var empresaId = _tenantContext.EmpresaId;

            // 6. Obter a venda com rastreamento para atualização.
            var venda =
                await _repositorioVenda.ObterParaAtualizacaoAsync(
                    empresaId,
                    command.VendaId,
                    cancellationToken);

            if (venda is null)
            {
                throw Validacao(
                    "Venda não encontrada para a empresa informada.");
            }

            // 7. Impedir alterações em vendas encerradas.
            if (venda.Status != StatusVenda.Aberta)
            {
                throw Validacao(
                    "Só é possível adicionar itens a uma venda aberta.");
            }

            // 8. Resolver o item a partir do catálogo.
            // Preço e descrição são sempre obtidos do banco.
            string descricaoItem;
            decimal valorUnitario;

            switch (command.TipoItem)
            {
                case TipoItemVenda.Servico:
                    {
                        var servico =
                            await _repositorioServico.ObterPorIdAsync(
                                empresaId,
                                command.ItemId,
                                cancellationToken);

                        if (servico is null)
                        {
                            throw Validacao(
                                "Serviço não encontrado para a empresa informada.");
                        }

                        // Um serviço inativo não pode ser vendido novamente.
                        if (servico.Ativo != StatusGeral.Ativo)
                        {
                            throw Validacao(
                                "Não é possível adicionar um serviço inativo à venda.");
                        }

                        if (servico.Valor < 0)
                        {
                            throw Validacao(
                                "O valor do serviço não pode ser negativo.");
                        }

                        descricaoItem = servico.Nome;
                        valorUnitario = servico.Valor;

                        break;
                    }

                case TipoItemVenda.Produto:
                    {
                        var produto =
                            await _repositorioProdutos.ObterPorIdAsync(
                                empresaId,
                                command.ItemId,
                                cancellationToken);

                        if (produto is null)
                        {
                            throw Validacao(
                                "Produto não encontrado para a empresa informada.");
                        }

                        // REGRA DE NEGÓCIO:
                        // produtos desativados não podem ser utilizados
                        // em novas operações comerciais.
                        if (!produto.Ativo)
                        {
                            throw Validacao(
                                "Não é possível adicionar um produto inativo à venda.");
                        }

                        if (produto.ValorVenda < 0)
                        {
                            throw Validacao(
                                "O valor de venda do produto não pode ser negativo.");
                        }

                        descricaoItem = produto.Nome;
                        valorUnitario = produto.ValorVenda;

                        break;
                    }

                default:
                    throw Validacao(
                        "O tipo de item informado é inválido.");
            }

            // 9. Validar o desconto em relação ao subtotal do item.
            var subtotalItem = decimal.Round(
                command.Quantidade * valorUnitario,
                2,
                MidpointRounding.AwayFromZero);

            if (command.Desconto > subtotalItem)
            {
                throw Validacao(
                    "O desconto não pode ser superior ao subtotal do item.");
            }

            // 10. Criar o item utilizando os dados do catálogo.
            var itemVenda = new VendaItem(
                vendaId: venda.Id,
                tipoItem: command.TipoItem,
                itemId: command.ItemId,
                descricao: descricaoItem,
                quantidade: command.Quantidade,
                valorUnitario: valorUnitario,
                desconto: command.Desconto);

            // 11. Adicionar o item através do domínio.
            // O domínio recalcula os totais da venda.
            venda.AdicionarItem(itemVenda);

            await _repositorioVenda.AdicionarItemAsync(
                itemVenda,
                cancellationToken);

            // Não chamar SaveChangesAsync aqui.
            // A persistência é coordenada pelo TransactionMiddleware.

            // 12. Retornar os valores atualizados.
            return new AdicionarItemVendaDto(
                VendaId: venda.Id,
                ItemVendaId: itemVenda.Id,
                Descricao: itemVenda.Descricao,
                Quantidade: itemVenda.Quantidade,
                ValorUnitario: itemVenda.ValorUnitario,
                Desconto: itemVenda.Desconto,
                ValorTotalItem: itemVenda.ValorTotal,
                SubtotalVenda: venda.Subtotal,
                ValorTotalVenda: venda.ValorTotal);
        }

        private static CustomValidationException Validacao(
            string mensagem)
        {
            return new CustomValidationException(
                new[] { mensagem });
        }
    }
}
