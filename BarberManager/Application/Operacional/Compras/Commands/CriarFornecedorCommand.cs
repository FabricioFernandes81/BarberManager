using Application.Common.Transactions;
using Domain.Entities.Common.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operacional.Compras.Commands
{
    public class CriarFornecedorCommand : ITransactionalCommand
    {
        public string Nome { get; set; } = string.Empty;

        public string? NomeFantasia { get; set; }

        public string? Documento { get; set; }

        public string? Email { get; set; }

        public string? Telefone { get; set; }

        public string? Observacao { get; set; }

        public PoliticaFinanceiraDevolucao PoliticaFinanceiraDevolucao { get; set; }
    = PoliticaFinanceiraDevolucao.CreditoFornecedor;

        [NotMapped]
        public TransactionContext TransactionContext =>
            TransactionContext.Operacional;
    }
}
