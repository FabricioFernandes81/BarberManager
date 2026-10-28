using Application.Common.Middlewares.Handler;
using Application.Common.Security;
using Application.Common.Tenancy;
using Application.Interfaces.Vendas;
using Application.Operacional.Vendas.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Vendas.Commands.Handlers
{
    public class RemoverItemVendaHandler
      : ICommandHandler<
          RemoverItemVendaCommand,
          RemoverItemVendaDto>
    {
        private readonly IRepositorioVenda _repositorioVenda;
        private readonly ITenantContext _tenantContext;
        private readonly IUsuarioLogado _usuarioLogado;

        public RemoverItemVendaHandler(
            IRepositorioVenda repositorioVenda,
            ITenantContext tenantContext,
            IUsuarioLogado usuarioLogado)
        {
            _repositorioVenda = repositorioVenda;
            _tenantContext = tenantContext;
            _usuarioLogado = usuarioLogado;
        }

        public async Task<RemoverItemVendaDto> Handle(
            RemoverItemVendaCommand command,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // 1. Validar o contexto da empresa.
            if (!_tenantContext.IsAvailable ||
                _tenantContext.EmpresaId == Guid.Empty)
            {
                throw new InvalidOperationException(
                    "Não foi possível identificar a empresa autenticada.");
            }

            var empresaId = _tenantContext.EmpresaId;

            // 2. Validar a autenticação.
            if (!_usuarioLogado.IsAuthenticated)
            {
                throw new UnauthorizedAccessException(
                    "É necessário estar autenticado para remover itens da venda.");
            }

            // 3. Validar os identificadores.
            if (command.VendaId == Guid.Empty)
            {
                throw new ArgumentException(
                    "O identificador da venda é obrigatório.");
            }

            if (command.ItemVendaId == Guid.Empty)
            {
                throw new ArgumentException(
                    "O identificador do item da venda é obrigatório.");
            }

            // 4. Buscar a venda da empresa autenticada.
            var venda = await _repositorioVenda
                .ObterParaAtualizacaoAsync(
                    empresaId,
                    command.VendaId,
                    cancellationToken);

            if (venda is null)
            {
                throw new InvalidOperationException(
                    "Venda não encontrada para a empresa autenticada.");
            }

            // 5. Localizar o item na coleção da venda.
            var item = venda.Itens.SingleOrDefault(
                i => i.Id == command.ItemVendaId);

            if (item is null)
            {
                throw new InvalidOperationException(
                    "O item informado não pertence à venda.");
            }

            // 6. Capturar a descrição antes da remoção.
            var descricao = item.Descricao;

            // 7. Aplicar a regra de domínio.
            // A entidade valida o status e recalcula os totais.
            venda.RemoverItem(command.ItemVendaId);

            // 8. Registrar a exclusão no contexto do EF Core.
            await _repositorioVenda.RemoverItemAsync(
                item,
                cancellationToken);

            // 9. Retornar os totais atualizados.
            return new RemoverItemVendaDto(
                VendaId: venda.Id,
                ItemVendaId: command.ItemVendaId,
                Descricao: descricao,
                SubtotalVenda: venda.Subtotal,
                DescontoVenda: venda.Desconto,
                ValorTotalVenda: venda.ValorTotal);
        }
    }
}
