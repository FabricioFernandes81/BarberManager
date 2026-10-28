using Domain.Entities.Common;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Vendas
{
    public class VendaItem : EntityBase
    {
        public Guid VendaId { get; private set; }

        public Venda Venda { get; private set; } = null!;

        public TipoItemVenda TipoItem { get; private set; }

        public Guid ItemId { get; private set; }

        public string Descricao { get; private set; } = string.Empty;

        public decimal Quantidade { get; private set; }

        public decimal ValorUnitario { get; private set; }

        public decimal Desconto { get; private set; }

        public decimal ValorTotal { get; private set; }

        protected VendaItem()
        {
        }

        public VendaItem(
            Guid vendaId,
            TipoItemVenda tipoItem,
            Guid itemId,
            string descricao,
            decimal quantidade,
            decimal valorUnitario,
            decimal desconto)
        {
            if (vendaId == Guid.Empty)
                throw new ArgumentException(
                    "A venda é obrigatória.",
                    nameof(vendaId));

            if (itemId == Guid.Empty)
                throw new ArgumentException(
                    "O serviço ou produto é obrigatório.",
                    nameof(itemId));

            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException(
                    "A descrição do item é obrigatória.",
                    nameof(descricao));

            if (quantidade <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(quantidade),
                    "A quantidade deve ser maior que zero.");

            if (valorUnitario < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(valorUnitario),
                    "O preço não pode ser negativo.");

            if (desconto < 0)
                throw new ArgumentOutOfRangeException(
                    nameof(desconto),
                    "O desconto não pode ser negativo.");

            var subtotal = decimal.Round(
                quantidade * valorUnitario,
                2,
                MidpointRounding.AwayFromZero);

            if (desconto > subtotal)
                throw new ArgumentOutOfRangeException(
                    nameof(desconto),
                    "O desconto não pode superar o subtotal.");

            VendaId = vendaId;
            TipoItem = tipoItem;
            ItemId = itemId;
            Descricao = descricao.Trim();
            Quantidade = quantidade;
            ValorUnitario = decimal.Round(
                valorUnitario, 2, MidpointRounding.AwayFromZero);
            Desconto = decimal.Round(
                desconto, 2, MidpointRounding.AwayFromZero);

            ValorTotal = subtotal - Desconto;
        }
    }
}
