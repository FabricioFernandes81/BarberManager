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

            // 2. Validar o usuário autenticado.
            if (!_usuarioLogado.IsAuthenticated)
            {
                throw Validacao(
                    "Usuário não autenticado.");
            }

            // 3. Validar os dados recebidos.
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

            // 4. Nesta etapa, somente serviços são aceitos.
            if (command.TipoItem != TipoItemVenda.Servico)
            {
                throw Validacao(
                    "A inclusão de produtos ainda não está disponível.");
            }

            var empresaId = _tenantContext.EmpresaId;

            // 5. Recuperar a venda rastreada pelo EF Core.
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

            // 6. Validar o estado da venda.
            if (venda.Status != StatusVenda.Aberta)
            {
                throw Validacao(
                    "Somente vendas abertas podem receber novos itens.");
            }

            // 7. Consultar o serviço no catálogo.
            var servico =
                await _repositorioServico.ObterPorIdAsync(
                    command.ItemId,
                    empresaId, cancellationToken);

            if (servico is null)
            {
                throw Validacao(
                    "Serviço não encontrado para a empresa informada.");
            }

            if (servico.Ativo != StatusGeral.Ativo)
            {
                throw Validacao(
                    "Não é possível adicionar um serviço inativo à venda.");
            }

            // 8. Validar o desconto em relação ao subtotal.
            var subtotalItem = decimal.Round(
                command.Quantidade * servico.Valor,
                2,
                MidpointRounding.AwayFromZero);

            if (command.Desconto > subtotalItem)
            {
                throw Validacao(
                    "O desconto não pode superar o subtotal do item.");
            }

            // 9. Criar o item com dados confiáveis do backend.
            var item = new VendaItem(
                vendaId: venda.Id,
                tipoItem: TipoItemVenda.Servico,
                itemId: servico.Id,
                descricao: servico.Nome,
                quantidade: command.Quantidade,
                valorUnitario: servico.Valor,
                desconto: command.Desconto);

            // 10. Aplicar as regras de domínio e recalcular os totais.
            venda.AdicionarItem(item);

            // A entidade Venda já está rastreada pelo EF Core.
            // A persistência será realizada pelo mecanismo
            // transacional da aplicação.

            return new AdicionarItemVendaDto(
                venda.Id,
                item.Id,
                item.Descricao,
                item.Quantidade,
                item.ValorUnitario,
                item.Desconto,
                item.ValorTotal,
                venda.Subtotal,
                venda.ValorTotal);
        }

        private static CustomValidationException Validacao(
            string mensagem)
        {
            return new CustomValidationException(
                new[] { mensagem });
        }
    }
}
