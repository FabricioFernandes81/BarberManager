using Application.Common.Exceptions;
using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
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
    public sealed class AdicionarItemVendaHandler
        : ICommandHandler<
            AdicionarItemVendaCommand,
            AdicionarItemVendaDto>
    {
        private readonly IRepositorioVenda _repositorioVenda;
        private readonly IRepositorioServico _repositorioServico;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public AdicionarItemVendaHandler(
            IRepositorioVenda repositorioVenda,
            IRepositorioServico repositorioServico,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorioVenda = repositorioVenda;
            _repositorioServico = repositorioServico;
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
                    "O identificador do serviço é obrigatório.");
            }

            // 4. Nesta etapa, somente serviços são aceitos.
            if (command.TipoItem != TipoItemVenda.Servico)
            {
                throw Validacao(
                    "Nesta etapa, somente serviços podem ser adicionados à venda.");
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

            // 8. Consultar o serviço dentro da empresa atual.
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

            // 9. Impedir a venda de serviços inativos.
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

            // 10. Validar o desconto em relação ao subtotal do item.
            var subtotalItem = decimal.Round(
                command.Quantidade * servico.Valor,
                2,
                MidpointRounding.AwayFromZero);

            if (command.Desconto > subtotalItem)
            {
                throw Validacao(
                    "O desconto não pode ser superior ao subtotal do item.");
            }

            // 11. Criar o item utilizando os dados do serviço.
            // O preço e a descrição vêm do banco, não da requisição.
            var itemVenda = new VendaItem(
                vendaId: venda.Id,
                tipoItem: TipoItemVenda.Servico,
                itemId: servico.Id,
                descricao: servico.Nome,
                quantidade: command.Quantidade,
                valorUnitario: servico.Valor,
                desconto: command.Desconto);

            // 12. Adicionar o item através do domínio.
            // O método recalcula subtotal, desconto e total da venda.
            venda.AdicionarItem(itemVenda);

            // Não chamar SaveChangesAsync aqui.
            // A persistência é coordenada pelo TransactionMiddleware.

            // 13. Retornar os valores atualizados.
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
