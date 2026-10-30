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

        public string Codigo { get; set; } = string.Empty;

        public string? CodigoBarras { get; set; }

        public Guid? CategoriaProdutoId { get; set; }

        public Guid? MarcaId { get; set; }

        public decimal Custo { get; set; }

        public decimal ValorVenda { get; set; }

        public decimal EstoqueMinimo { get; set; }

        public string UnidadeMedida { get; set; } = "UN";

        public bool Ativo { get; set; } = true;

        public string? Observacao { get; set; }

        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
