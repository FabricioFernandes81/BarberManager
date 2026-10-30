using Application.Common.Transactions;
using Application.Operacional.Produtos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Produtos.Commands
{
    public class CriarProdutoCommand : ITransactionalCommand
    {
        public string Nome { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        public string? CodigoInterno { get; set; }

        public string? CodigoBarras { get; set; }

        public Guid? CategoriaProdutoId { get; set; }

        public Guid? MarcaId { get; set; }

        public decimal ValorVenda { get; set; }

        public decimal Custo { get; set; }

        public bool ControlaEstoque { get; set; }

        public decimal QuantidadeEstoqueInicial { get; set; }

        public decimal EstoqueMinimo { get; set; }

        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
